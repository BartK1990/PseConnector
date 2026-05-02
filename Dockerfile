# Stage 1: Build the Blazor WebAssembly app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PseConnector.sln .
COPY PseConnector.Web/PseConnector.Web.csproj PseConnector.Web/
COPY PseConnector.Data/PseConnector.Data.fsproj PseConnector.Data/
COPY PseConnector.Console/PseConnector.Console.csproj PseConnector.Console/

RUN dotnet restore PseConnector.Web/PseConnector.Web.csproj

COPY . .

RUN dotnet publish PseConnector.Web/PseConnector.Web.csproj -c Release -o /app/publish

# Stage 2: Serve with nginx
FROM nginx:alpine AS final
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
