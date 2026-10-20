# C# Design Patterns

[![.NET](https://github.com/deanagan/csharp-design-patterns/actions/workflows/ci.yaml/badge.svg)](https://github.com/deanagan/csharp-design-patterns/actions/workflows/ci.yaml)
[![Coverage Status](https://coveralls.io/repos/github/deanagan/csharp-design-patterns/badge.svg?branch=master)](https://coveralls.io/github/deanagan/csharp-design-patterns?branch=master)

A collection of GoF design pattern examples written in C#. Each pattern has a
source project and, where applicable, a corresponding test project under
`Patterns/`.

## Requirements

- .NET 10 SDK

## Design patterns

- Abstract Factory
- Adapter
- Bridge
- Builder
- Chain of Responsibility
- Command
- Composite
- Decorator
- Facade
- Factory Method
- Flyweight
- Iterator
- Mediator
- Memento
- Observer
- Prototype
- Proxy
- Singleton with Dependency Injection
- State
- Strategy
- Template Method
- Visitor

## Build and test

Run these commands from the repository root:

```sh
dotnet restore DesignPatterns.slnx
dotnet build DesignPatterns.slnx --no-restore
dotnet test DesignPatterns.slnx --no-build
```

The test projects use xUnit. The repository is migrating to NSubstitute for
mocking and Shouldly for assertions; use these for new and updated tests.

## Code coverage

Collect coverage for all test projects with Coverlet:

```sh
dotnet test DesignPatterns.slnx \
  --collect:"XPlat Code Coverage" \
  --results-directory ./coverage-results
```

Install ReportGenerator once, then create an HTML report and coverage summary:

```sh
dotnet tool install dotnet-reportgenerator-globaltool --tool-path ./.tools
./.tools/reportgenerator \
  "-reports:coverage-results/**/coverage.cobertura.xml" \
  "-targetdir:coverage-report" \
  "-reporttypes:Html;Cobertura;lcov;MarkdownSummaryGithub"
```

## Adding a pattern

1. Create a source project under `Patterns/<PatternName>/src/` and a test
   project under `Patterns/<PatternName>/test/`.
2. Add both projects to `DesignPatterns.slnx`:

   ```sh
   dotnet sln DesignPatterns.slnx add \
     Patterns/<PatternName>/src/<PatternName>.csproj \
     Patterns/<PatternName>/test/<PatternName>Test.csproj
   ```

3. Add a project reference from the test project to the source project, then
   build and run the tests using the commands above.

## Planned patterns

- Interpreter
- Unit of Work
- Pipeline
- Workflow
- Rules Engine
