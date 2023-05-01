
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ["src/Client/EntryPoints/Client.EntryPoints.Pwa/Client.EntryPoints.Pwa.csproj", "src/Client/EntryPoints/Client.EntryPoints.Pwa/" ]
COPY ["src/Client/Client.Core/Client.Core.csproj", "src/Client/Client.Core/" ]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/UI/UI.csproj", "src/UI/"]

RUN dotnet restore "src/Client/EntryPoints/Client.EntryPoints.Pwa/Client.EntryPoints.Pwa.csproj"
COPY . .
WORKDIR "/src/src/Client/EntryPoints/Client.EntryPoints.Pwa"
RUN dotnet publish "Client.EntryPoints.Pwa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:1.23.0-alpine
WORKDIR /app
EXPOSE 8080
COPY ["src/Client/EntryPoints/Client.EntryPoints.Pwa/nginx.conf", "/etc/nginx/nginx.conf"]
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html