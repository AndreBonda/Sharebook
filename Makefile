clean:
	dotnet clean src

restore:
	dotnet restore src

build:
	dotnet build src

run:
	docker-compose up -d
	dotnet run --project src/ShareBook.API

test-all:
	dotnet test src/

test-unit:
	dotnet test --filter "FullyQualifiedName~UnitTests" src