#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
RUN apt-get update && apt-get install -y libfontconfig1
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Numus/Server/Numus.Server.csproj", "Numus/Server/"]
COPY ["Numus/Client/Numus.Client.csproj", "Numus/Client/"]
COPY ["Numus/Shared/Numus.Shared.csproj", "Numus/Shared/"]
RUN dotnet restore "Numus/Server/Numus.Server.csproj"
COPY . .
WORKDIR "/src/Numus/Server"
RUN dotnet build "Numus.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Numus.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Numus.Server.dll"]