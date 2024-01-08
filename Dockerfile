FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "./"]

COPY ["src/ShipperStation.WebApi/ShipperStation.WebApi.csproj", "src/ShipperStation.WebApi/"]
COPY ["src/ShipperStation.Infrastructure/ShipperStation.Infrastructure.csproj", "src/ShipperStation.Infrastructure/"]
COPY ["src/ShipperStation.Application/ShipperStation.Application.csproj", "src/ShipperStation.Application/"]
COPY ["src/ShipperStation.Domain/ShipperStation.Domain.csproj", "src/ShipperStation.Domain/"]
COPY ["src/ShipperStation.Shared/ShipperStation.Shared.csproj", "src/ShipperStation.Shared/"]

RUN dotnet restore "./src/ShipperStation.WebApi/./ShipperStation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ShipperStation.WebApi"
RUN dotnet build "./ShipperStation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShipperStation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG Authentication__Schemes__Bearer__TokenExpire
ENV Authentication__Schemes__Bearer__TokenExpire=${Authentication__Schemes__Bearer__TokenExpire}

ARG Authentication__Schemes__Bearer__RefreshTokenExpire
ENV Authentication__Schemes__Bearer__RefreshTokenExpire=${Authentication__Schemes__Bearer__RefreshTokenExpire}

ARG DOCKER_USERNAME
ENV DOCKER_USERNAME=${DOCKER_USERNAME}

ARG DOCKER_PASSWORD
ENV DOCKER_PASSWORD=${DOCKER_PASSWORD}

ENTRYPOINT ["dotnet", "ShipperStation.WebApi.dll"]