# Solução para criar enquetes e gerenciá-las

Passos para executar a aplicação no Visual Studio com localdb
--------------------------------------------------------------
1) Setar o projeto "src/PollChallenge.Service.Api" como o projeto a ser executado

2) Abrir "Package Manager Console" e executar o comando "update-database"

3) Executar a Api


Passos para executar a aplicação via Docker
--------------------------------------------
1) Abrir o diretório raiz da solução (PollChallenge) no prompt de comando e executar:
	* docker-compose build
	* docker-compose up
		* Caso dê erro e pedir para recriar a imagem, informar y e enter

2) Abrir diretorio src\PollChallenge.Service.Api no prompt de comando e executar:
	* set ASPNETCORE_ENVIRONMENT=Migration
	* dotnet ef database update Migration-Initial

3) Agora é só utilizar a API na porta 8000