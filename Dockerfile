FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY "FotoQuest.WebApi/FotoQuest.WebApi.csproj" FotoQuest.WebApi/
RUN dotnet restore "FotoQuest.WebApi/FotoQuest.WebApi.csproj"
COPY . .
WORKDIR /src/FotoQuest.WebApi
RUN dotnet build "FotoQuest.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FotoQuest.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FotoQuest.WebApi.dll"]