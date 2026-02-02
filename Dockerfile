# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Kopiuj csproj z podfolderu i przywróć zależności
COPY surveymanager/SurveyManager.csproj ./ 
RUN dotnet restore

# Kopiuj resztę projektu z podfolderu i zbuduj
COPY surveymanager/. ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "SurveyManager.dll"]
