.PHONY: build test clean coverage report

build:
	dotnet build

test:
	dotnet test

coverage:
	dotnet test --collect:"XPlat Code Coverage" --results-directory coverage-results

report: coverage
	export PATH="$$PATH:/Users/agand/.dotnet/tools" && \
	reportgenerator -reports:"coverage-results/**/*.cobertura.xml" -targetdir:"coveragereport" -reporttypes:"Html;MarkdownSummaryGithub"

clean:
	dotnet clean
	rm -rf coveragereport
	rm -rf coverage-results
	rm -rf Patterns/*/test/coverage.cobertura.xml
	rm -rf Patterns/*/test/TestResults

all: build test