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

# FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
# WORKDIR /src
# COPY *.sln ./

# COPY CustomerApi.Api/*.csproj ./CustomerApi.Api/
# COPY CustomerApi.Data/*.csproj ./CustomerApi.Data/
# COPY CustomerApi.Domain/*.csproj ./CustomerApi.Domain/
# COPY CustomerApi.EventBus/*.csproj ./CustomerApi.EventBus/
# COPY CustomerApi.Services/*.csproj ./CustomerApi.Services/

# # COPY ["CustomerApi.Api.csproj", "CustomerApi/CustomerApi.Api/"]

# # COPY ["CustomerApi/CustomerApi.Data/CustomerApi.Data.csproj", "CustomerApi/CustomerApi.Data/"]
# # COPY ["../CustomerApi/CustomerApi.Domain/CustomerApi.Domain.csproj", "CustomerApi/CustomerApi.Domain/"]
# # COPY ["../CustomerApi/CustomerApi.Service/CustomerApi.Services.csproj", "CustomerApi/CustomerApi.Services/"]
# # COPY ["../CustomerApi/CustomerApi.EventBus/CustomerApi.EventBus.csproj", "CustomerApi/CustomerApi.EventBus/"]
# # COPY ["*.props", "./"]

# RUN dotnet restore
# # RUN dotnet restore "CustomerApi/CustomerApi.Api/CustomerApi.Api.csproj"

# COPY CustomerApi.Api/. ./CustomerApi.Api/
# COPY CustomerApi.Data/. ./CustomerApi.Data/
# COPY CustomerApi.Domain/. ./CustomerApi.Domain/
# COPY CustomerApi.EventBus/. ./CustomerApi.EventBus/
# COPY CustomerApi.Services/. ./CustomerApi.Services/

# WORKDIR /src/CustomerApi.Api
# RUN dotnet publish -c Release -o /app/publish --no-restore 

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "CustomerApi.Api.dll"]




# # RUN dotnet build "CustomerApi/CustomerApi.Api/CustomerApi.Api.csproj" -c Release -o /app/build --no-restore

# # RUN dotnet build "CustomerApi.Api.csproj" -c Release -o /app/build  --no-restore

# # FROM build AS publish
# # RUN dotnet publish "CustomerApi/CustomerApi.Api/CustomerApi.Api.csproj" -c Release -o /app/publish --no-restore 
# # RUN dotnet publish "CustomerApi.Api.csproj" -c Release -o /app/publish

# # FROM base AS final
# # WORKDIR /app
# # COPY --from=publish /app/publish .
# # ENTRYPOINT ["dotnet", "CustomerApi.Api.dll"]
