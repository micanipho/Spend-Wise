FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/SpendWise.Web.Host/SpendWise.Web.Host.csproj", "src/SpendWise.Web.Host/"]
COPY ["src/SpendWise.Web.Core/SpendWise.Web.Core.csproj", "src/SpendWise.Web.Core/"]
COPY ["src/SpendWise.Application/SpendWise.Application.csproj", "src/SpendWise.Application/"]
COPY ["src/SpendWise.Core/SpendWise.Core.csproj", "src/SpendWise.Core/"]
COPY ["src/SpendWise.EntityFrameworkCore/SpendWise.EntityFrameworkCore.csproj", "src/SpendWise.EntityFrameworkCore/"]
WORKDIR "/src/src/SpendWise.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/SpendWise.Web.Host", "src/SpendWise.Web.Host"]
COPY ["src/SpendWise.Web.Core", "src/SpendWise.Web.Core"]
COPY ["src/SpendWise.Application", "src/SpendWise.Application"]
COPY ["src/SpendWise.Core", "src/SpendWise.Core"]
COPY ["src/SpendWise.EntityFrameworkCore", "src/SpendWise.EntityFrameworkCore"]
WORKDIR "/src/src/SpendWise.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "SpendWise.Web.Host.dll"]
