# C# Design Patterns Modernisation Checklist

Use this checklist to track completion of the repository modernisation. Check items off as they are completed and use it alongside modernisation-guide.md.

## Baseline

- [ ] Create a dedicated working branch
- [ ] Confirm the repository builds before changes
- [ ] Confirm all existing tests pass
- [ ] Record current .NET SDK/target framework
- [ ] Record current package versions
- [ ] Record current test count
- [ ] Record the baseline commit

## .NET modernisation

- [ ] Choose the appropriate current supported .NET version
- [ ] Update target frameworks
- [ ] Update SDK/global.json if applicable
- [ ] Update NuGet dependencies
- [ ] Resolve upgrade-related compiler warnings/errors
- [ ] Run dotnet build
- [ ] Run dotnet test
- [ ] Commit the platform migration

## Test dependency migration

### Moq → NSubstitute

- [ ] Identify every Moq usage
- [ ] Replace mock creation with NSubstitute
- [ ] Replace setups/configuration
- [ ] Replace verification calls
- [ ] Preserve original test intent
- [ ] Remove the Moq package
- [ ] Run affected tests
- [ ] Run the complete test suite

### FluentAssertions → selected replacement

- [ ] Evaluate assertion-library alternatives
- [ ] Confirm licence is appropriate for the repository
- [ ] Select the replacement
- [ ] Replace assertions without changing test intent
- [ ] Remove FluentAssertions
- [ ] Run the complete test suite
- [ ] Document the decision

## Project reorganisation

- [ ] Decide and document the standard project layout
- [ ] Move each pattern's test project beside its implementation
- [ ] Update project references
- [ ] Update namespaces where necessary
- [ ] Update solution/project references
- [ ] Confirm every pattern has one implementation project
- [ ] Confirm every pattern has one corresponding test project
- [ ] Confirm tests reference only the appropriate implementation project
- [ ] Run build
- [ ] Run tests

## Finance domain

- [ ] Define a small shared finance vocabulary
- [ ] Avoid introducing unnecessary domain infrastructure
- [ ] Map existing patterns to natural finance examples
- [ ] Identify examples where finance would obscure the pattern
- [ ] Convert patterns one at a time
- [ ] Update tests with the new examples
- [ ] Review examples for teaching clarity

## New patterns

### Null Object

- [ ] Create the Null Object project
- [ ] Create its test project
- [ ] Choose a finance example
- [ ] Demonstrate the problem solved by Null Object
- [ ] Implement the null implementation
- [ ] Test the important behaviour
- [ ] Document the intent

### Pipeline

- [ ] Create the Pipeline project
- [ ] Create its test project
- [ ] Choose a finance example
- [ ] Define the pipeline context/input
- [ ] Implement pipeline steps
- [ ] Demonstrate sequential processing
- [ ] Demonstrate failure/short-circuit behaviour
- [ ] Test step ordering and failure behaviour
- [ ] Explain how Pipeline differs from Chain of Responsibility

### Workflow

- [ ] Create the Workflow project
- [ ] Create its test project
- [ ] Choose a finance example
- [ ] Define workflow states/stages
- [ ] Define valid transitions
- [ ] Demonstrate the business-process lifecycle
- [ ] Test valid transitions
- [ ] Test invalid transitions
- [ ] Explain how Workflow differs from Pipeline and State

## Repository standardisation

- [ ] Evaluate Directory.Build.props
- [ ] Evaluate Directory.Packages.props
- [ ] Add/update .editorconfig
- [ ] Add/update global.json
- [ ] Standardise package versions
- [ ] Standardise compiler/style settings
- [ ] Check naming conventions
- [ ] Check project naming conventions

## CI

- [ ] Confirm CI restores successfully
- [ ] Confirm CI builds successfully
- [ ] Confirm CI runs all tests
- [ ] Confirm CI uses the intended .NET version
- [ ] Consider enforcing formatting
- [ ] Consider adding coverage validation
- [ ] Remove obsolete dependency references from CI

## Documentation

- [ ] Update README pattern list
- [ ] Update README testing instructions
- [ ] Update README project structure
- [ ] Document the new testing libraries
- [ ] Document how to add a pattern
- [ ] Document the finance-domain convention
- [ ] Document Null Object
- [ ] Document Pipeline
- [ ] Document Workflow
- [ ] Review all documentation for stale references to Moq/FluentAssertions

## Final quality review

- [ ] Every pattern builds
- [ ] Every test project passes
- [ ] No obsolete test dependencies remain
- [ ] Project structure is consistent
- [ ] Examples are small enough to understand
- [ ] Examples clearly demonstrate their pattern
- [ ] Related patterns are explicitly distinguished
- [ ] Finance examples improve rather than obscure understanding
- [ ] README is accurate
- [ ] CI is green
- [ ] Formatting/style checks pass

## Generalisation

- [ ] Review the migration decisions
- [ ] Identify reusable engineering practices
- [ ] Separate repository-specific decisions from general principles
- [ ] Update the Pattern Repository Modernisation skill
- [ ] Update the skill usage guide if necessary
- [ ] Try the skill against another repository
- [ ] Record shortcomings discovered during the trial
- [ ] Refine the skill
- [ ] Finalise the methodology

## Suggested final status

- [ ] Modernisation complete
- [ ] Checklist complete
- [ ] Documentation complete
- [ ] Reusable skill validated
- [ ] Repository ready for ongoing maintenance
