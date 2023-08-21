FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FolderCleaner/FolderCleaner.csproj", "FolderCleaner/"]
RUN dotnet restore "FolderCleaner/FolderCleaner.csproj"
COPY . .
WORKDIR "/src/FolderCleaner"
RUN dotnet build "FolderCleaner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FolderCleaner.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FolderCleaner.dll"]
