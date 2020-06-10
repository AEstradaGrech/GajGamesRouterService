FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS base
WORKDIR /app

COPY GajGamesServiceRouter.sln .
COPY GajGamesServiceRouter/GajGamesServiceRouter.csproj ./GajGamesServiceRouter/
COPY GajGamesServiceRouter.Infrastructure/GajGamesServiceRouter.Infrastructure.csproj ./GajGamesServiceRouter.Infrastructure/

COPY GajGamesServiceRouter/GajCert.pfx .

RUN dotnet restore

COPY . ./

RUN dotnet publish GajGamesServiceRouter -c Release -o /app
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=base /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "GajGamesServiceRouter.dll"]