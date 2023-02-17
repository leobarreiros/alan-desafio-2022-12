FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . ./

WORKDIR /app/src/ApiDeMoedas

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/src/ApiDeMoedas

COPY --from=build-env /app/src/ApiDeMoedas/out .
ENTRYPOINT ["dotnet", "ApiDeMoedas.dll"]