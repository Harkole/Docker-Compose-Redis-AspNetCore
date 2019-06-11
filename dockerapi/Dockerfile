# Configure the run time environment
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app

# Configure the build process
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY . .

# Trigger the restore/build of the project
RUN dotnet build "dockerapi.csproj" -c Release -o /app

# With the build completed, publish the application
FROM build AS publish
RUN dotnet publish "dockerapi.csproj" -c Release -o /app

# Now execute the worker using dotnet runtimes
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# Configure Kestral Envrionment
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# Execute program
ENTRYPOINT ["dotnet", "dockerapi.dll"]