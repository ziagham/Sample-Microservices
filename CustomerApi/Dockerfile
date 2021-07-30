FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY *.sln .

COPY CustomerApi.Api/*.csproj ./CustomerApi.Api/
COPY CustomerApi.Data/*.csproj ./CustomerApi.Data/
COPY CustomerApi.Domain/*.csproj ./CustomerApi.Domain/
COPY CustomerApi.EventBus/*.csproj ./CustomerApi.EventBus/
COPY CustomerApi.Services/*.csproj ./CustomerApi.Services/

# Restore to cache the layers
RUN dotnet restore

# copy full solution over
COPY . .
RUN dotnet build

# publish the API
FROM build AS publish
WORKDIR /src/CustomerApi.Api
RUN dotnet publish -c Release -o /app/publish --no-restore 

# run the api
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./ 
ENTRYPOINT ["dotnet", "CustomerApi.Api.dll"]
