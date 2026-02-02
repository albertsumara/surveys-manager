# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Kopiuj csproj i przywróæ zale¿noœci
COPY *.csproj ./
RUN dotnet restore

# Kopiuj resztê projektu i zbuduj
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000
ENTRYPOINT ["dotnet", "SurveyManager.dll"]
