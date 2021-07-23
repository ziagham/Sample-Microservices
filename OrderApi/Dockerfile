FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY *.sln .

COPY OrderApi.Api/*.csproj ./OrderApi.Api/
COPY OrderApi.Data/*.csproj ./OrderApi.Data/
COPY OrderApi.Domain/*.csproj ./OrderApi.Domain/
COPY OrderApi.EventBus/*.csproj ./OrderApi.EventBus/
COPY OrderApi.Services/*.csproj ./OrderApi.Services/

# Restore to cache the layers
RUN dotnet restore

# copy full solution over
COPY . .
RUN dotnet build

# publish the API
FROM build AS publish
WORKDIR /src/OrderApi.Api
RUN dotnet publish -c Release -o /app/publish --no-restore 

# run the api
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./ 
ENTRYPOINT ["dotnet", "OrderApi.Api.dll"]