$texto = @"
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ApiFlota.csproj", "./"]
RUN dotnet restore "ApiFlota.csproj"
COPY . .
RUN dotnet publish "ApiFlota.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ApiFlota.dll"]
"@

$texto | Out-File -FilePath "api/Dockerfile" -Encoding utf8