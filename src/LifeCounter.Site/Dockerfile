FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LifeCounter.Site/LifeCounter.Site.csproj", "LifeCounter.Site/"]
RUN dotnet restore "LifeCounter.Site/LifeCounter.Site.csproj"
COPY . .
WORKDIR "/src/LifeCounter.Site"
RUN dotnet build "LifeCounter.Site.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LifeCounter.Site.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LifeCounter.Site.dll"]
