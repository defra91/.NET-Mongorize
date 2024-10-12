FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./mongorize.csproj .
RUN dotnet restore mongorize.csproj

COPY . .

RUN dotnet build mongorize.csproj -c Release -o /bin/Release/net8.0
