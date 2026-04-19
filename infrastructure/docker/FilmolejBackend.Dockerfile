FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./apps/FilmolejBackend/*.csproj ./
RUN dotnet restore

COPY ./apps/FilmolejBackend/. ./
RUN dotnet publish FilmolejBackend.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

RUN apt-get update && apt-get install -y ffmpeg

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FilmolejBackend.dll"]