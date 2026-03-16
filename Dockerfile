FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
COPY ["aspnet-core/src/SpendWise.Web.Host/SpendWise.Web.Host.csproj", "src/SpendWise.Web.Host/"]
COPY ["aspnet-core/src/SpendWise.Web.Core/SpendWise.Web.Core.csproj", "src/SpendWise.Web.Core/"]
COPY ["aspnet-core/src/SpendWise.Application/SpendWise.Application.csproj", "src/SpendWise.Application/"]
COPY ["aspnet-core/src/SpendWise.Core/SpendWise.Core.csproj", "src/SpendWise.Core/"]
COPY ["aspnet-core/src/SpendWise.EntityFrameworkCore/SpendWise.EntityFrameworkCore.csproj", "src/SpendWise.EntityFrameworkCore/"]
WORKDIR "/src/src/SpendWise.Web.Host"
RUN dotnet restore

WORKDIR /src
COPY ["aspnet-core/src/", "src/"]
WORKDIR "/src/src/SpendWise.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "SpendWise.Web.Host.dll"]
