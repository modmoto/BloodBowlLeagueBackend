FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
COPY NuGet.config ./
RUN dir
RUN dotnet restore --configfile NuGet.config -nowarn:msb3202,nu1503 --verbosity diag
RUN dotnet publish --output /output --configuration Release

FROM microsoft/dotnet:2.2-sdk
WORKDIR /app
COPY --from=build /output /app
ENTRYPOINT ["dotnet", "Teams.ReadHost.dll"]