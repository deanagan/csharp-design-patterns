# Pattern Repository Modernisation Skill

## Purpose

Use this methodology to assess, modernise, reorganise, and maintain code repositories whose primary purpose is demonstrating patterns, architecture, techniques, or examples.

The methodology is deliberately implementation-light: the default behaviour is to inspect the repository, explain the current state, propose a staged sequence, and let the developer make the changes.

## Core principles

1. Inspect before changing.
2. Establish a passing baseline.
3. Separate mechanical migrations from design changes.
4. Make changes in small, reversible phases.
5. Validate after each meaningful phase.
6. Preserve the teaching or demonstration intent of examples.
7. Evaluate dependencies rather than blindly replacing them.
8. Prefer consistent project organisation.
9. Keep examples small enough to understand.
10. Record decisions that may be useful elsewhere.
11. Generalise lessons learned at the end.

## Operating modes

### Assess

Inspect the repository and report:

- language/runtime;
- project structure;
- test framework;
- important dependencies;
- build/test commands;
- CI;
- documentation;
- apparent conventions;
- obvious migration risks.

Do not modify anything.

### Plan

Produce a phased sequence ordered by dependency and risk.

Prefer:

    baseline
    -> platform
    -> dependencies
    -> structure
    -> design/example changes
    -> new examples
    -> standardisation
    -> documentation
    -> final review
    -> generalisation

### Guide

When the developer is implementing the changes themselves, give one clear next task at a time. Explain the purpose, files likely to be affected, expected result, and validation command.

Do not take over implementation unless explicitly asked.

### Review

After a phase is reported complete, inspect the repository again. Verify the intended result before recommending the next phase.

## Dependency and licensing evaluation

When replacing a dependency:

1. Identify why it is being replaced.
2. Check its current licence and project status.
3. Identify realistic alternatives.
4. Compare API style, ecosystem support, maintenance, and migration cost.
5. Avoid assuming two libraries are behaviourally identical.
6. Recommend a replacement only in the context of the repository's requirements.

For example, a repository may choose:

    xUnit + NSubstitute + Shouldly

but the methodology should not hard-code that stack as universally correct.

## Project organisation

For repositories containing many independent examples, prefer a structure that makes each example self-contained:

    PatternName/
        PatternName.csproj
        implementation
        Tests/
            PatternName.Tests.csproj
            tests

A test project should reference the corresponding implementation project rather than unrelated examples.

Do not reorganise the entire repository and rewrite implementations in the same phase.

## Example/domain design

For teaching repositories:

- choose domains that make the pattern intuitive;
- avoid unnecessary infrastructure;
- keep examples intentionally smaller than production systems;
- use a consistent domain where that improves coherence;
- do not force a domain onto an example when it makes the pattern harder to understand.

When converting examples to a new domain, preserve the pattern's intent first.

## Adding new patterns

For every new pattern:

1. Give it its own project.
2. Give it its own test project.
3. Provide a small, concrete example.
4. Demonstrate the problem the pattern addresses.
5. Demonstrate the pattern itself.
6. Test the important behaviour.
7. Explain how it differs from closely related patterns.

## Distinguishing related patterns

When adding concepts such as Pipeline and Workflow, explicitly document the distinction from existing patterns.

For example:

- Pipeline: sequential processing through a series of steps.
- Chain of Responsibility: handlers pass responsibility onward.
- Workflow: progression through a business process or lifecycle.

The repository should teach these distinctions rather than merely implement similar-looking classes.

## Validation

At minimum, validate:

    dotnet build
    dotnet test

When applicable also validate:

    dotnet format --verify-no-changes

and the repository's CI workflow.

A completed phase is not complete merely because the files were changed; the repository should still build and test successfully.

## Decision records

For consequential choices, capture:

- decision;
- context;
- alternatives considered;
- chosen approach;
- reason;
- consequences.

Useful decisions include:

- runtime version;
- test libraries;
- project structure;
- domain conventions;
- package management;
- CI policy.

## Final generalisation phase

After the repository is modernised:

1. Review what changed.
2. Identify repeatable practices.
3. Separate repository-specific decisions from general principles.
4. Update the reusable methodology.
5. Try the methodology on another repository.
6. Record failures or ambiguities.
7. Refine the methodology.

The goal is a feedback loop rather than a static checklist.

## Default response format

When guiding a developer, prefer:

### Current state

What was found.

### Recommended phase

The next coherent unit of work.

### Why

Why it belongs here.

### Your task

Concrete changes the developer should make.

### Validation

Commands or checks to run.

### Done when

Observable completion criteria.

Do not provide a giant implementation dump unless the developer explicitly asks for implementation.
