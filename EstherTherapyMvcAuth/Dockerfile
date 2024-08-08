#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EstherTherapyMvcAuth/EstherTherapyMvcAuth.csproj", "EstherTherapyMvcAuth/"]
COPY ["EstherTherapyMvcAuth.ServiceInterface/EstherTherapyMvcAuth.ServiceInterface.csproj", "EstherTherapyMvcAuth.ServiceInterface/"]
COPY ["EstherTherapyMvcAuth.ServiceModel/EstherTherapyMvcAuth.ServiceModel.csproj", "EstherTherapyMvcAuth.ServiceModel/"]
RUN dotnet restore "./EstherTherapyMvcAuth/./EstherTherapyMvcAuth.csproj"
COPY . .
WORKDIR "/src/EstherTherapyMvcAuth"
RUN dotnet build "./EstherTherapyMvcAuth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EstherTherapyMvcAuth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstherTherapyMvcAuth.dll"]