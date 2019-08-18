# Build image
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /app
COPY ./BloodBowlLeagueBackend.sln ./

# Copy all the csproj files and restore to cache the layer for faster builds
# The dotnet_build.sh script does this anyway, so superfluous, but docker can
# cache the intermediate images so _much_ faster
COPY ./ReadHosts/ReadHosts.Common/ReadHosts.Common.csproj ./ReadHosts/ReadHosts.Common/ReadHosts.Common.csproj
COPY ./ReadHosts/Ui.Translations/Ui.Translations.csproj ./ReadHosts/Ui.Translations/Ui.Translations.csproj
COPY ./ReadHosts/Seasons.ReadHost/Seasons.ReadHost.csproj ./ReadHosts/Seasons.ReadHost/Seasons.ReadHost.csproj
COPY ./ServiceConfig/ServiceConfig.csproj ./ServiceConfig/ServiceConfig.csproj

RUN dotnet restore ./ReadHosts/ReadHosts.Common/ReadHosts.Common.csproj
RUN dotnet restore ./ReadHosts/Ui.Translations/Ui.Translations.csproj
RUN dotnet restore ./ReadHosts/Seasons.ReadHost/Seasons.ReadHost.csproj
RUN dotnet restore ./ServiceConfig/ServiceConfig.csproj

COPY ./ReadHosts ./ReadHosts
COPY ./ServiceConfig ./ServiceConfig
RUN dotnet build ./ReadHosts/Seasons.ReadHost/Seasons.ReadHost.csproj -c Release

RUN dotnet publish "./ReadHosts/Seasons.ReadHost/Seasons.ReadHost.csproj" -c Release -o "../../out"

#App image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Seasons.ReadHost.dll"]