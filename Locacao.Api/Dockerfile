FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Locacao.Api/Locacao.Api.csproj", "Locacao.Api/"]
RUN dotnet restore "Locacao.Api/Locacao.Api.csproj"
COPY . .
WORKDIR "/src/Locacao.Api"
RUN dotnet build "Locacao.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Locacao.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Locacao.Api.dll"]
