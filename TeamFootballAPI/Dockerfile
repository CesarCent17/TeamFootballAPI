FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TeamFootballAPI.csproj", "TeamFootballAPI/"]

WORKDIR /src/TeamFootballAPI
RUN dotnet restore

COPY . .
RUN dotnet build "TeamFootballAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamFootballAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamFootballAPI.dll"]


# docker build -t team-football-api:1.0 .
# docker run -d -p 8090:80 --name team-football-api team-football-api:1.0


