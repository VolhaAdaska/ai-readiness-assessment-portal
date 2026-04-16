---
description: Design the Infrastructure layer for the MVP
---

Design the Infrastructure layer for the initial assessment MVP in AI Readiness Assessment Portal.

Context:
We already have the Domain model and the Application layer.
The product should support:
- creating an assessment
- starting an assessment
- updating a category assessment
- completing an assessment
- retrieving assessment details
- retrieving category assessment details

Requirements:
- follow repository-wide and path-specific instructions
- keep the design simple, clean, and production-like
- use EF Core for persistence
- use SQLite for the first MVP
- propose only the minimum necessary persistence model
- do not implement API controllers yet
- do not implement frontend yet
- keep infrastructure concerns out of Domain

Return:
1. infrastructure responsibilities
2. DbContext design
3. entity configurations needed
4. repository or data access abstractions needed for the MVP
5. exact files to create in the Infrastructure project
6. risks or trade-offs