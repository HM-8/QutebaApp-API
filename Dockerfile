#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["QutebaApp-API/QutebaApp-API.csproj", "QutebaApp-API/"]
COPY ["QutebaApp-Core/QutebaApp-Core.csproj", "QutebaApp-Core/"]
COPY ["QutebaApp-Data/QutebaApp-Data.csproj", "QutebaApp-Data/"]
RUN dotnet restore "QutebaApp-API/QutebaApp-API.csproj"
COPY . .
WORKDIR "/src/QutebaApp-API"
RUN dotnet build "QutebaApp-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QutebaApp-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QutebaApp-API.dll"]