FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /source

COPY *.sln .

COPY Src/OrderApi.Api/*.csproj ./Src/OrderApi.Api/
COPY Src/OrderApi.Data/*.csproj ./Src/OrderApi.Data/
COPY Src/OrderApi.Domain/*.csproj ./Src/OrderApi.Domain/
COPY Src/OrderApi.EventBus/*.csproj ./Src/OrderApi.EventBus/
COPY Src/OrderApi.Services/*.csproj ./Src/OrderApi.Services/

COPY Tests/OrderApi.Api.Tests/*.csproj ./Tests/OrderApi.Api.Tests/
COPY Tests/OrderApi.Data.Tests/*.csproj ./Tests/OrderApi.Data.Tests/
COPY Tests/OrderApi.Services.Tests/*.csproj ./Tests/OrderApi.Services.Tests/

RUN dotnet restore

COPY . .
RUN dotnet build

RUN dotnet test

FROM build AS publish
WORKDIR /source/Src/OrderApi.Api
RUN dotnet publish -c Release -o /app/publish --no-restore 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./ 
ENTRYPOINT ["dotnet", "OrderApi.Api.dll"]