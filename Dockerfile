# build stage

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-dev-certs
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p password
RUN dotnet dev-certs https --trust

COPY . .
RUN dotnet restore storiesbook.csproj --disable-parallel

RUN dotnet publish storiesbook.csproj -c release -o /app --no-restore
# serve stage

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
COPY --from=build /https/aspnetapp.pfx /https/aspnetapp.pfx
WORKDIR /app
COPY --from=build /app .

EXPOSE 443

ENTRYPOINT ["dotnet", "storiesbook.dll"]
