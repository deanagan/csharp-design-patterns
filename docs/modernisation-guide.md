# C# Design Patterns Modernisation Guide

This guide is the concrete roadmap for modernising this repository. It is intentionally separated into small, reversible phases so that each stage can be built, tested, reviewed, and committed independently.

## Goals

- Upgrade the repository to a current supported .NET release.
- Remove Moq and FluentAssertions because of their commercial licensing implications for companies.
- Use NSubstitute and a permissively licensed assertion approach such as Shouldly.
- Keep each pattern example in its own project and colocate its test project with the example.
- Add Null Object, Pipeline, and Workflow examples.
- Rework examples around finance/lending concepts where that makes the pattern clearer.
- Leave the implementation work with the developer; use this guide as the sequence and review checklist.

## Target project shape

Each pattern should eventually look like:

    Patterns/
        Strategy/
            Strategy.csproj
            *.cs
            Tests/
                Strategy.Tests.csproj
                *.cs

The test project references only the corresponding pattern project. The test project belongs beside the example conceptually, but remains a separate project.

## Phase 1 — Establish a clean baseline

1. Create a working branch.
2. Run dotnet build.
3. Run dotnet test.
4. Record the baseline .NET SDK, target frameworks, package versions, and test count.
5. Do not refactor production code yet.
6. Commit the known-good starting point.

## Phase 2 — Modernise the .NET platform

1. Determine the appropriate current supported .NET version.
2. Update the target framework and SDK configuration.
3. Update compatible NuGet dependencies.
4. Resolve compilation errors and warnings caused by the upgrade.
5. Run the complete test suite.
6. Commit the platform-only change.

Keep this phase mechanical. Do not combine it with pattern redesign.

## Phase 3 — Replace Moq

Replace Moq usages with NSubstitute.

For each test:

1. Identify the mock setup.
2. Translate it to NSubstitute syntax.
3. Preserve the original test intent.
4. Avoid changing production behaviour.
5. Run the affected tests.
6. Run the complete suite.

Commit the Moq removal separately.

## Phase 4 — Replace FluentAssertions

Evaluate the assertion replacement against the repository's licensing, readability, maintenance, and teaching requirements.

A good candidate is Shouldly because it keeps assertions readable without requiring the FluentAssertions API.

Translate assertions without changing test intent. Keep this as a separate commit from the mocking migration.

The desired testing stack is:

- xUnit
- NSubstitute
- Shouldly (or another deliberately selected permissive assertion library)

## Phase 5 — Reorganise projects

Move each pattern's tests next to its implementation.

Preferred shape:

    Patterns/
        Adapter/
            Adapter.csproj
            ...
            Tests/
                Adapter.Tests.csproj
                ...

For each pattern:

1. Move the test project.
2. Update its project reference.
3. Update namespaces only where necessary.
4. Update the solution/project references.
5. Run build and tests.
6. Repeat pattern by pattern.

Do not redesign examples during this phase.

## Phase 6 — Establish finance domain conventions

Use a small, reusable finance vocabulary rather than building a large domain model.

Useful concepts include:

- Customer
- Account
- Loan
- Payment
- Transaction
- CreditApplication
- CreditDecision
- InterestRate
- Currency
- Portfolio

Choose the domain concept that makes each pattern obvious. Avoid forcing a finance example when it obscures the pattern.

Example mappings:

| Pattern | Example |
|---|---|
| Strategy | Loan pricing strategy |
| Factory Method | Create loan types |
| Abstract Factory | Create related account products |
| Decorator | Add loan fees/features |
| Chain of Responsibility | Credit application checks |
| State | Loan lifecycle |
| Observer | Account notifications |
| Command | Execute a payment |
| Adapter | External payment provider |
| Facade | Loan application service |
| Null Object | Optional customer notification |
| Pipeline | Loan application processing |
| Workflow | Loan application lifecycle |

## Phase 7 — Add Null Object

Create a standalone Null Object example.

Possible finance scenario:

    ICustomerNotification
        ├── EmailCustomerNotification
        └── NullCustomerNotification

The tests should demonstrate why the Null Object exists and how it removes conditional null handling from client code.

## Phase 8 — Add Pipeline

Create a standalone Pipeline example.

Possible flow:

    LoanApplication
        -> Validate
        -> Enrich
        -> CalculateCreditRisk
        -> CalculatePricing
        -> Persist

The example should make clear:

- what is passed between steps;
- how steps are composed;
- whether a step executes;
- what happens when a step fails;
- whether later steps execute after failure.

Keep Pipeline distinct from Chain of Responsibility. A pipeline is primarily sequential processing; a chain is about passing responsibility through handlers.

## Phase 9 — Add Workflow

Create a standalone Workflow example.

Possible lifecycle:

    Submitted
       -> Validated
       -> CreditChecked
       -> Approved / Declined
       -> OfferCreated
       -> Accepted
       -> Funded

The example should demonstrate progression through a business process rather than simply passing a value through transformations.

## Phase 10 — Convert existing examples to finance

Do this one pattern at a time.

For each pattern:

1. Identify what the existing example is teaching.
2. Choose a finance scenario that naturally demonstrates it.
3. Rewrite the example.
4. Rewrite the tests.
5. Keep the pattern structure visible.
6. Remove unrelated domain complexity.
7. Run the tests.
8. Commit.

Do not convert all examples in one large change.

## Phase 11 — Standardise the repository

Consider introducing:

- Directory.Build.props
- Directory.Packages.props
- .editorconfig
- global.json

Use these to make framework versions, package versions, compiler settings, and style consistent.

## Phase 12 — CI and validation

Ensure CI can perform at least:

    dotnet restore
    dotnet build --no-restore
    dotnet test --no-build

Optionally enforce formatting with:

    dotnet format --verify-no-changes

## Phase 13 — Documentation

Update the README after the structure stabilises.

Document:

- what the repository demonstrates;
- the pattern catalogue;
- where each implementation lives;
- where tests live;
- how to run the examples/tests;
- the testing stack;
- contribution conventions;
- how to add a pattern.

## Phase 14 — Final review

Review the repository as a learner rather than only as a maintainer.

For every pattern ask:

1. Is the intent obvious?
2. Is the example small enough to understand?
3. Does the domain support the pattern rather than distract from it?
4. Do the tests demonstrate the important behaviour?
5. Is the pattern distinguishable from related patterns?
6. Is the project structure consistent?
7. Are dependencies appropriately licensed and maintained?

## Phase 15 — Extract reusable engineering practices

The final phase turns the work into reusable knowledge.

1. Review decisions made during the migration.
2. Identify decisions that generalise beyond this repository.
3. Record project-organisation conventions.
4. Record testing conventions.
5. Record dependency/licensing evaluation criteria.
6. Record documentation conventions.
7. Record the rules for separating mechanical migrations from design changes.
8. Update the reusable pattern-repository-modernisation skill.
9. Test the skill against another repository.
10. Refine the skill based on what worked and what did not.

The resulting loop is:

    Repository
        -> Apply methodology
        -> Learn
        -> Generalise
        -> Skill
        -> Apply to next repository
        -> Improve skill

## Suggested commit sequence

1. Establish baseline
2. Upgrade .NET
3. Update NuGet dependencies
4. Replace Moq with NSubstitute
5. Replace FluentAssertions with the selected assertion library
6. Reorganise one pattern and its tests
7. Reorganise remaining patterns
8. Clean solution/project organisation
9. Establish finance-domain conventions
10. Convert existing patterns to finance examples
11. Add Null Object
12. Add Pipeline
13. Add Workflow
14. Standardise repository configuration
15. Update CI
16. Rewrite README
17. Extract and generalise reusable practices

The exact number of commits can change; the important principle is to keep each category of change independently understandable and testable.
