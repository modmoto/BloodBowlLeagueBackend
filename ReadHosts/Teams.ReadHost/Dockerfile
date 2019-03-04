FROM microsoft/aspnetcore:latest AS build
WORKDIR /src
COPY . .
COPY NuGet.config ./
RUN dir
RUN dotnet restore --configfile NuGet.Config -nowarn:msb3202,nu1503 --verbosity diag
RUN dotnet publish --output /output --configuration Release

FROM microsoft/aspnetcore:latest
WORKDIR /app
COPY --from=build /output /app
ENTRYPOINT ["dotnet", "Teams.ReadHost.dll"]