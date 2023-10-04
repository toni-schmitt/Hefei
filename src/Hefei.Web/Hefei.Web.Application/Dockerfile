FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
ARG NODE_MAJOR=16
RUN apt-get update \
    &&  apt-get install -y \
      ca-certificates curl gnupg \
    && mkdir -p /etc/apt/keyrings \
    && curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key  \
      | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg \
    && echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_$NODE_MAJOR.x nodistro main"  \
      | tee /etc/apt/sources.list.d/nodesource.list \
    && apt-get update \
    && apt-get install -y \
      nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["src/Hefei.Web/Hefei.Web.Application/Hefei.Web.Application.csproj", "src/Hefei.Web/Hefei.Web.Application/"]
RUN dotnet restore "src/Hefei.Web/Hefei.Web.Application/Hefei.Web.Application.csproj"
COPY . .
WORKDIR "/src/src/Hefei.Web/Hefei.Web.Application"
RUN dotnet build "Hefei.Web.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hefei.Web.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hefei.Web.Application.dll"]
