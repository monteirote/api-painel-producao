FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["api-painel-producao/api-painel-producao.csproj", "api-painel-producao/"]
RUN dotnet restore "api-painel-producao/api-painel-producao.csproj"
COPY . . 
WORKDIR "/src/api-painel-producao"
RUN dotnet build "api-painel-producao.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "api-painel-producao.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "api-painel-producao.dll" ]

