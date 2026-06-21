FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LuftbornMusic.slnx", "./"]
COPY ["LuftbornMusic.Core/LuftbornMusic.Core.csproj", "LuftbornMusic.Core/"]
COPY ["LuftbornMusic.Infrastructure/LuftbornMusic.Infrastructure.csproj", "LuftbornMusic.Infrastructure/"]
COPY ["LuftbornMusic.API/LuftbornMusic.API.csproj", "LuftbornMusic.API/"]
RUN dotnet restore "LuftbornMusic.API/LuftbornMusic.API.csproj"

COPY . .
WORKDIR "/src/LuftbornMusic.API"
RUN dotnet publish "LuftbornMusic.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080 
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LuftbornMusic.API.dll"]