#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Dinning Hall/Dinning Hall.csproj", "Dinning Hall/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "Dinning Hall/Dinning Hall.csproj"
COPY . .
WORKDIR "/src/Dinning Hall"
RUN dotnet build "Dinning Hall.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dinning Hall.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dinning Hall.dll"]