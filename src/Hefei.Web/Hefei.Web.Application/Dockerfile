FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
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
