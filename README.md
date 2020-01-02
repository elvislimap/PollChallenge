# Solução para criar enquetes e gerenciá-las

Passos para executar a aplicação no Visual Studio com localdb
--------------------------------------------------------------
1) Setar o projeto "src/PollChallenge.Service.Api" como o projeto a ser executado

2) Abrir "Package Manager Console" e executar o comando "update-database"

3) Executar a Api


Passos para executar a aplicação via Docker
--------------------------------------------
1) Instalações requeridas (Links para baixar em Windows)
	* Dot Net Core SDK >= 2.2.100
		* https://download.visualstudio.microsoft.com/download/pr/7ae62589-2bc1-412d-a653-5336cff54194/b573c4b135280fb369e671a8f477163a/dotnet-sdk-2.2.100-win-x64.exe

	* CLI do Entity Framework
		* dotnet tool install --global dotnet-ef --version 3.0.0

	* Docker e Docker-compose
		* https://hub.docker.com/?overlay=onboarding

2) Abrir o diretório raiz da solução (PollChallenge) no prompt de comando e executar:
	* docker-compose build
	* docker-compose up
		* Caso dê erro e pedir para recriar a imagem, informar y e enter

3) Abrir diretorio src\PollChallenge.Service.Api no prompt de comando e executar:
	* set ASPNETCORE_ENVIRONMENT=Migration
	* dotnet ef database update Migration-Initial

4) Agora é só utilizar a API na porta 8000