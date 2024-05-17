FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY Models/*.csproj Models/
COPY Business/*.csproj Business/
COPY Data/*.csproj Data/
COPY API/*.csproj API/
RUN dotnet restore FisioSolutionAPI.sln

COPY . .
RUN dotnet publish FisioSolutionAPI.sln -c Release -o API/out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out .

EXPOSE 80

ENTRYPOINT ["sh", "-c", "dotnet ef database update -p ./Data/FisioSolution.Data.csproj -s ./API/FisioSolution.API.csproj && dotnet FisioSolution.API.dll"]