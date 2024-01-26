using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Domain.Constants;
using ShipperStation.Infrastructure;
using ShipperStation.Infrastructure.Hubs;
using ShipperStation.Shared.Helpers;
using ShipperStation.WebApi.Extensions;
using ShipperStation.WebApi.Middleware;
using ShipperStation.WebApi.Transformers;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShipperStation.WebApi;
public static class DependencyInjection
{
    public static void AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddControllerServices();
        services.AddSwaggerServices();
        services.AddAuthenticationServices(configuration);
        services.AddUrlHelperServices();
        services.AddAuthorizationServices();
        services.AddSignalRServices();
        services.AddHangfireServices(configuration);
        services.AddFirebaseServices();

    }

    private static void AddUrlHelperServices(this IServiceCollection services)
    {

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
         .AddScoped((IServiceProvider it) =>
             it.GetRequiredService<IUrlHelperFactory>()
               .GetUrlHelper(it.GetRequiredService<IActionContextAccessor>().ActionContext!));

    }

    private static void AddControllerServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    private static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            xmlFilename = $"{typeof(Application.AssemblyReference).Assembly.GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            c.OperationFilter<SecurityRequirementsOperationFilter>(JwtBearerDefaults.AuthenticationScheme);
            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.EnableAnnotations();
        });
    }

    private static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                     configuration.GetSection("Authentication:Schemes:Bearer:SerectKey").Value!)),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                NameClaimType = ClaimTypes.NameIdentifier
            };
            options.RequireHttpsMetadata = false;
            options.HandleEvents();
        });
    }

    private static void AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.Admin, policy => policy.RequireRole(Roles.Admin));
            options.AddPolicy(Policies.Staff, policy => policy.RequireRole(Roles.Staff));
            options.AddPolicy(Policies.StationManager, policy => policy.RequireRole(Roles.StationManager));
            options.AddPolicy(Policies.User, policy => policy.RequireRole(Roles.User));
            options.AddPolicy(Policies.StationManager_Or_Staff, policy => policy.RequireRole(Roles.StationManager).RequireRole(Roles.Staff));
            options.AddPolicy(Policies.Admin_Or_StationManager, policy => policy.RequireRole(Roles.Admin, Roles.StationManager));
        });
    }

    private static void AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR(options => options.EnableDetailedErrors = true);
    }

    private static void AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(c => c.UseMemoryStorage());
        services.AddHangfireServer();
    }

    private static void AddFirebaseServices(this IServiceCollection services)
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("sstation-ac2ef-firebase-adminsdk-vpk23-43e3a5a330.json")
        });
    }

    public static async Task UseWebApplication(this WebApplication app)
    {

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.EnableDeepLinking();
            c.EnablePersistAuthorization();
            c.EnableTryItOutByDefault();
            c.DisplayRequestDuration();
        });

        app.UseExceptionApplication();

        await app.UseInitialiseDatabaseAsync();

        app.UseMiddleware<PerformanceMiddleware>();

        app.UseCors(x => x
           .AllowCredentials()
           .SetIsOriginAllowed(origin => true)
           .AllowAnyMethod()
           .AllowAnyHeader());

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.MapHangfireDashboard(); // /hangfire

        app.MapHub<NotificationHub>("/notification-hub");
    }

    private static void UseExceptionApplication(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var _factory = context.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature?.Error;

                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = exception switch
                {
                    BadRequestException e => StatusCodes.Status400BadRequest,
                    ValidationBadRequestException e => StatusCodes.Status400BadRequest,
                    ConflictException e => StatusCodes.Status409Conflict,
                    ForbiddenAccessException e => StatusCodes.Status403Forbidden,
                    NotFoundException e => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException e => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError,
                };

                var problemDetails = _factory.CreateProblemDetails(
                             httpContext: context,
                             statusCode: context.Response.StatusCode,
                             detail: exception?.Message);

                var options = JsonSerializerUtils.GetGlobalJsonSerializerOptions();

                var result = JsonSerializer.Serialize(problemDetails, options);

                if (exception is ValidationBadRequestException badRequestException)
                {
                    if (badRequestException.ModelState != null)
                    {
                        problemDetails = _factory.CreateValidationProblemDetails(
                              httpContext: context,
                              modelStateDictionary: badRequestException.ModelState,
                              statusCode: context.Response.StatusCode,
                              detail: exception?.Message);
                        result = JsonSerializer.Serialize((ValidationProblemDetails)problemDetails, options);
                    }
                }

                await context.Response.WriteAsync(result);

            });
        });
    }

}