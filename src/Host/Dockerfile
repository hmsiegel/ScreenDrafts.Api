#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/Host/Host.csproj", "src/Host/"]
COPY ["src/Presentation/Presentation.csproj", "src/Presentation/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]
COPY ["src/Migrators/Migrators.MSSQL/Migrators.MSSQL.csproj", "src/Migrators/Migrators.MSSQL/"]
COPY ["src/Migrators/Migrators.MySQL/Migrators.MySQL.csproj", "src/Migrators/Migrators.MySQL/"]
COPY ["src/Migrators/Migrators.Oracle/Migrators.Oracle.csproj", "src/Migrators/Migrators.Oracle/"]
COPY ["src/Migrators/Migrators.PostgresSQL/Migrators.PostgresSQL.csproj", "src/Migrators/Migrators.PostgresSQL/"]
COPY ["src/Migrators/Migrators.SqLite/Migrators.SqLite.csproj", "src/Migrators/Migrators.SqLite/"]
RUN dotnet restore "src/Host/Host.csproj"
COPY . .
WORKDIR "/src/src/Host"
RUN dotnet build "Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScreenDrafts.Api.Host.dll"]