FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Manta.WebApi/Manta.WebApi.csproj", "Manta.WebApi/"]
COPY ["Manta.Application/Manta.Application.csproj", "Manta.Application/"]
COPY ["Manta.Domain/Manta.Domain.csproj", "Manta.Domain/"]
COPY ["Manta.Infrastructure/Manta.Infrastructure.csproj", "Manta.Infrastructure/"]
COPY ["Manta.Contracts/Manta.Contracts.csproj", "Manta.Contracts/"]
RUN dotnet restore "Manta.WebApi/Manta.WebApi.csproj"

COPY . .
WORKDIR "/src/Manta.WebApi"
RUN dotnet publish "Manta.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Manta.WebApi.dll"]