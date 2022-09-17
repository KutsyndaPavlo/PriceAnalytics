#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sevices/Search/PriceAnalytics.Search.csproj", "Sevices/Search/"]
COPY ["Infrastructure/EventBusRabbitMQ/PriceAnalytics.Infrustructure.EventBusRabbitMQ.csproj", "Infrastructure/EventBusRabbitMQ/"]
COPY ["Infrastructure/EventBus/PriceAnalytics.Infrustructure.EventBus.csproj", "Infrastructure/EventBus/"]
RUN dotnet restore "Sevices/Search/PriceAnalytics.Search.csproj"
COPY . .
WORKDIR "/src/Sevices/Search"
RUN dotnet build "PriceAnalytics.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PriceAnalytics.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceAnalytics.Search.dll"]