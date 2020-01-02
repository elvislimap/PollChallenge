FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/PollChallenge.Service.Api/PollChallenge.Service.Api.csproj src/PollChallenge.Service.Api/
COPY src/PollChallenge.Infra.Data/PollChallenge.Infra.Data.csproj src/PollChallenge.Infra.Data/
COPY src/PollChallenge.Domain/PollChallenge.Domain.csproj src/PollChallenge.Domain/
COPY src/PollChallenge.Infra.CrossCutting.Json/PollChallenge.Infra.CrossCutting.Json.csproj src/PollChallenge.Infra.CrossCutting.Json/
COPY src/PollChallenge.Infra.CrossCutting.Ioc/PollChallenge.Infra.CrossCutting.Ioc.csproj src/PollChallenge.Infra.CrossCutting.Ioc/
COPY src/PollChallenge.Application/PollChallenge.Application.csproj src/PollChallenge.Application/
COPY src/PollChallenge.Infra.CrossCutting.Validation/PollChallenge.Infra.CrossCutting.Validation.csproj src/PollChallenge.Infra.CrossCutting.Validation/
RUN dotnet restore src/PollChallenge.Service.Api/PollChallenge.Service.Api.csproj
COPY . .
WORKDIR /src/src/PollChallenge.Service.Api
RUN dotnet build PollChallenge.Service.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PollChallenge.Service.Api.dll"]