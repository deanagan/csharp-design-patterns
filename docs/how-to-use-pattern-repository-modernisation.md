# How to Use the Pattern Repository Modernisation Skill

## What the skill is for

The Pattern Repository Modernisation methodology is a reusable way to assess and improve a repository containing design-pattern examples, architecture examples, educational code, or similar small demonstrations.

It is designed to act as a structured technical guide and reviewer.

It does not require the AI to make the changes itself.

## Three ways to use it

### 1. Guided migration

Use this when you have an existing repository that you want to modernise.

Example:

> Use the pattern repository modernisation skill to assess this repository. I want to make the changes myself, so give me the recommended sequence and only guide me through one phase at a time.

The skill should inspect the repository and produce:

- current-state assessment;
- risks and constraints;
- recommended phases;
- first task;
- validation criteria.

After completing the task:

> I've completed the phase. Review the repository and tell me what should happen next.

### 2. Codebase assessment

Use this when you want an assessment without changing anything.

Example:

> Assess this repository using the pattern repository modernisation methodology. Do not make changes. Identify structural, dependency, testing, documentation, and maintainability issues and propose a sequence for addressing them.

### 3. New repository design

Use this before creating a new example repository.

Example:

> Use the pattern repository modernisation methodology to propose the structure and conventions for a new design-pattern repository.

The skill can help establish:

- one example project per pattern;
- colocated tests;
- naming conventions;
- testing conventions;
- documentation conventions;
- domain conventions;
- CI expectations.

## Recommended interactive workflow

The most useful workflow is iterative.

### Step 1 — Assess

Ask the skill to inspect the repository.

### Step 2 — Accept or modify the plan

Review the proposed sequence. Tell it about constraints it could not infer.

### Step 3 — Implement the first phase yourself

Make the changes locally.

### Step 4 — Validate

Run the project's build and test commands.

### Step 5 — Ask for a review

Tell the skill what you completed and ask it to reassess the repository.

### Step 6 — Continue

Repeat until the migration is complete.

### Step 7 — Generalise

At the end, ask:

> Review what we learned during this migration and identify anything that should be added to the reusable modernisation methodology.

## Useful prompts

### Initial assessment

> Assess this repository using the Pattern Repository Modernisation methodology. Don't change anything.

### Create a plan

> Create a phased modernisation plan. Keep mechanical migrations separate from architectural and example changes.

### One phase at a time

> Give me only the next phase. I want to implement it myself.

### Review completed work

> I've completed this phase. Inspect the repository and verify whether it is actually complete.

### Investigate a dependency

> Evaluate this dependency for licence, maintenance, API, and ecosystem concerns and identify suitable alternatives.

### Review project structure

> Assess whether the project and test-project structure is consistent with the methodology.

### Review an example

> Review this pattern example as a teaching example. Is the pattern obvious, is the example unnecessarily complicated, and do the tests demonstrate the important behaviour?

### Distinguish patterns

> Compare this new pattern with the existing patterns in the repository and identify whether there is meaningful overlap or confusion.

### Final review

> Perform the final modernisation review. Identify unfinished work, inconsistent conventions, missing tests, documentation gaps, and CI/build issues.

### Extract reusable practices

> Review the entire migration and update the reusable methodology with lessons that generalise beyond this repository.

## What to provide the skill

The repository itself is the most important input.

Also provide constraints when you know them, such as:

- target runtime;
- licensing requirements;
- supported operating systems;
- preferred test framework;
- preferred libraries;
- whether examples should use a particular business domain;
- whether the developer or AI should perform implementation work.

Do not assume the skill knows why a dependency or architecture decision matters unless you tell it.

## When not to use it

This methodology is not necessary for a small isolated change.

For example, it is probably unnecessary for:

- fixing a typo;
- changing one assertion;
- fixing one compiler error;
- adding one small test;
- answering a question about a single pattern.

Use it when the work involves multiple related changes that benefit from sequencing.

## Important behaviour

The default interaction should be:

    Inspect
       ↓
    Explain
       ↓
    Propose
       ↓
    Wait for developer
       ↓
    Review
       ↓
    Next phase

The skill should not assume that "use the skill" means "rewrite my repository automatically."

The developer remains responsible for architectural decisions and implementation unless they explicitly request otherwise.
