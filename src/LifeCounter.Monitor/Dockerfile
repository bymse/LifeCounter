FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LifeCounter.Monitor/LifeCounter.Monitor.csproj", "LifeCounter.Monitor/"]
RUN dotnet restore "LifeCounter.Monitor/LifeCounter.Monitor.csproj"
COPY . .
WORKDIR "/src/LifeCounter.Monitor"
RUN dotnet build "LifeCounter.Monitor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LifeCounter.Monitor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LifeCounter.Monitor.dll"]
