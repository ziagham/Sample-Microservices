FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /source

COPY *.sln .

COPY Src/CustomerApi.Api/*.csproj ./Src/CustomerApi.Api/
COPY Src/CustomerApi.Data/*.csproj ./Src/CustomerApi.Data/
COPY Src/CustomerApi.Domain/*.csproj ./Src/CustomerApi.Domain/
COPY Src/CustomerApi.EventBus/*.csproj ./Src/CustomerApi.EventBus/
COPY Src/CustomerApi.Services/*.csproj ./Src/CustomerApi.Services/

RUN dotnet restore

COPY . .
RUN dotnet build

FROM build AS publish
WORKDIR /source/Src/CustomerApi.Api
RUN dotnet publish -c Release -o /app/publish --no-restore 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./ 
ENTRYPOINT ["dotnet", "CustomerApi.Api.dll"]
