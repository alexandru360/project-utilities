﻿FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GigglyMouse/GigglyMouse.csproj", "GigglyMouse/"]
RUN dotnet restore "GigglyMouse/GigglyMouse.csproj"
COPY . .
WORKDIR "/src/GigglyMouse"
RUN dotnet build "GigglyMouse.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GigglyMouse.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GigglyMouse.dll"]
