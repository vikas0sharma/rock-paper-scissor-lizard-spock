#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Prod

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get update && apt-get install -y nodejs

COPY ["RockPaperScissorLizardSpock.csproj", "."]
RUN dotnet restore "./RockPaperScissorLizardSpock.csproj"
COPY . .

COPY ./ClientApp/. /app/publish/ClientApp/.
WORKDIR "/app/publish/ClientApp/."
RUN npm install && npm run build

WORKDIR "/src/."
RUN dotnet build "RockPaperScissorLizardSpock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RockPaperScissorLizardSpock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RockPaperScissorLizardSpock.dll"]