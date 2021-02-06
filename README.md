![.NET Core](https://github.com/deanagan/csharp-design-patterns/workflows/.NET%20Core/badge.svg)   [![Build Status](https://travis-ci.com/deanagan/csharp-design-patterns.svg?branch=master)](https://travis-ci.com/github/deanagan/csharp-design-patterns)   [![Coverage Status](https://coveralls.io/repos/github/deanagan/csharp-design-patterns/badge.svg?branch=master)](https://coveralls.io/github/deanagan/csharp-design-patterns?branch=master)

# csharp-design-patterns
A bunch of demo codes for:
1. GoF Design patterns written in C#
2. Unit testing with Xunit
3. Mocking with Moq
4. Using FluentAssertions.
5. Ninject is used for testing the demo on DI based singletons.

# What Design Patterns can I find in this repository?
- Patterns\AbstractFactory\AbstractFactory
- Patterns\Adapter\Adapter
- Patterns\Bridge\Bridge
- Patterns\Builder\Builder
- Patterns\ChainOfResponsibility\ChainOfResponsibility
- Patterns\Command\Command
- Patterns\Composite\Composite
- Patterns\Decorator\Decorator
- Patterns\Facade\Facade
- Patterns\FactoryMethod\FactoryMethod
- Patterns\Flyweight\Flyweight
- Patterns\Mediator\Mediator
- Patterns\Memento\Memento
- Patterns\Observer\Observer
- Patterns\Prototype\Prototype
- Patterns\Proxy\Proxy
- Patterns\SingletonDependencyInjection\SingletonDI
- Patterns\State\State
- Patterns\Strategy\Strategy
- Patterns\TemplateMethod\TemplateMethod
- Patterns\Visitor\Visitor
- Patterns\Iterator\Iterator
- Patterns\MediatR_ASPNET\MediatR_ASPNET (in progress)

# How To Run the Demos
1. Demos are divided into folders with the pattern.
2. To build all, enter `dotnet build`
3. To execute all the tests, enter `dotnet test` or `dotnet test -l:"console;verbosity=detailed"` to be more verbose.


# TODOs
1. Interpreter Pattern
2. Unit of Work
3. Repository Pattern
4. MediatR with ASP.net (in progress)


# Adding a Pattern
1. Under Patterns folder, create a new directory with the new pattern name. Example: `mkdir MediatRASPNet`
2. Change directory to new folder.
2. Create a new project. Example: `dotnet new webapi -n MediatRASPNet -o .` or just `dotnet new webapi` if you like having the same project name as the current folder.

# Adding a package
1. Change directory to the pattern you want to add a package for or to the test folder (if you want to add a test package).
2. Add the package: Example: `dotnet add package MediatR`