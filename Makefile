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

add-migration:
	dotnet ef migrations add --project src/ShareBook.Infrastructure --startup-project src/ShareBook.API "$(migration_name)"

remove-migration:
	dotnet ef migrations remove --project src/ShareBook.Infrastructure --startup-project src/ShareBook.API

persist-migration:
	dotnet ef database update --project src/ShareBook.Infrastructure --startup-project src/ShareBook.API