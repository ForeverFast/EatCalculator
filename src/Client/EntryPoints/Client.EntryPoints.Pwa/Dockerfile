
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

WORKDIR /src
COPY [".config/", ".config" ]
COPY ["src/Client/EntryPoints/Client.EntryPoints.Pwa/", "Client/EntryPoints/Client.EntryPoints.Pwa/" ]
COPY ["src/Client/Client.Core/", "Client/Client.Core/" ]
COPY ["src/Common/", "Common/"]
COPY ["src/UI/", "UI/"]
#COPY ["src/UI/webcompilerconfiguration.json", "UI/webcompilerconfiguration.json"]

RUN apt-get update
RUN apt-get install -y python3
RUN dotnet workload install wasm-tools
RUN dotnet restore "Client/EntryPoints/Client.EntryPoints.Pwa/Client.EntryPoints.Pwa.csproj"
COPY . .
WORKDIR "Client/EntryPoints/Client.EntryPoints.Pwa"
RUN ls -al
RUN dotnet publish "Client.EntryPoints.Pwa.csproj" -c Release -o /app/src/publish /p:UseAppHost=false

FROM nginx:1.23.0-alpine
WORKDIR /app
EXPOSE 8080
COPY ["src/Client/EntryPoints/Client.EntryPoints.Pwa/nginx.conf", "/etc/nginx/nginx.conf"]

COPY --from=build /app/src/publish/wwwroot /usr/share/nginx/html