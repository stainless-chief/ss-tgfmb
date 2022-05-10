#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FrontlineMaidBot/FrontlineMaidBot.csproj", "FrontlineMaidBot/"]
RUN dotnet restore "FrontlineMaidBot/FrontlineMaidBot.csproj"
COPY . .
WORKDIR "/src/FrontlineMaidBot"
RUN dotnet build "FrontlineMaidBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrontlineMaidBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrontlineMaidBot.dll"]

LABEL org.label-schema.url="https://github.com/ChiefNoir/FrontlineMaidBot"