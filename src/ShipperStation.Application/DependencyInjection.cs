using AspNetCore.Pattern.CQRS.MediatorPattern.MediatR.Behaviors;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ShipperStation.Application.Helpers;
using System.Reflection;

namespace ShipperStation.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidators();
        services.AddMediator();
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        });
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationRulesToSwagger();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
        services.AddFluentValidationAutoValidation();
    }

}
