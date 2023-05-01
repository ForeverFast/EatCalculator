#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Server/EntryPoints/Server.EntryPoints.WebApi/Server.EntryPoints.WebApi.csproj", "src/Server/EntryPoints/Server.EntryPoints.WebApi/"]
COPY ["src/Server/Server.Core/Server.Core.csproj", "src/Server/Server.Core/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
RUN dotnet restore "src/Server/EntryPoints/Server.EntryPoints.WebApi/Server.EntryPoints.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Server/EntryPoints/Server.EntryPoints.WebApi"
RUN dotnet build "Server.EntryPoints.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server.EntryPoints.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.EntryPoints.WebApi.dll"]