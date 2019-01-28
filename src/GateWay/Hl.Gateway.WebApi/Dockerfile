FROM microsoft/dotnet:2.2.0-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore Surging.Hl.Gateway.sln && \
    dotnet build --no-restore -c Release Surging.Hl.Gateway.sln

FROM build AS publish
WORKDIR src/GateWay/Hl.Gateway.WebApi
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
ARG host_name
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Hl.Gateway.WebApi.dll"]