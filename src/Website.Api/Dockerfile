#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Website.Api/Website.Api.csproj", "src/Website.Api/"]
COPY ["src/Website.Bal/Website.Bal.csproj", "src/Website.Bal/"]
COPY ["src/Website.Dal/Website.Dal.csproj", "src/Website.Dal/"]
COPY ["src/Website.Shared/Website.Shared.csproj", "src/Website.Shared/"]
RUN dotnet restore "src/Website.Api/Website.Api.csproj"
COPY . .
WORKDIR "/src/src/Website.Api"
RUN dotnet build "Website.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Website.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Website.Api.dll"]