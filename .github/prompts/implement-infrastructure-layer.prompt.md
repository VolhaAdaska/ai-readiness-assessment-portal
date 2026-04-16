---
description: Implement the Infrastructure layer for the MVP
agent: 'agent'
---

Implement the Infrastructure layer for the initial assessment MVP in AI Readiness Assessment Portal.

Context:
We already have the Domain model and the Application layer.
Use EF Core with SQLite for the first MVP.

The Infrastructure layer should support:
- persisting InitialAssessment aggregate
- loading InitialAssessment with related CategoryAssessments, AssessmentResponses, and Recommendations
- updating the aggregate
- dependency injection wiring for persistence services

Requirements:
- follow repository-wide and path-specific instructions
- keep the implementation simple, clean, and production-like
- create only the minimum necessary files for the MVP
- include DbContext, EF Core configurations, repository implementation, and persistence registration
- split EF Core configurations by entity, do not use one configuration file for all entities
- keep persistence concerns out of Domain
- repository and service contracts must remain in Application; Infrastructure should only implement them
- do not generate migration files manually
- do not implement API controllers yet
- do not implement frontend yet
- do not add unnecessary abstractions
- before applying changes, list the files to create or update

Preferred structure:
- Persistence/AiReadinessAssessmentDbContext.cs
- Persistence/Configurations/InitialAssessmentConfiguration.cs
- Persistence/Configurations/CategoryAssessmentConfiguration.cs
- Persistence/Configurations/AssessmentResponseConfiguration.cs
- Persistence/Configurations/RecommendationConfiguration.cs
- Persistence/Repositories/InitialAssessmentRepository.cs
- DependencyInjection.cs

Additional guidance:
- if RecommendationGenerator depends only on domain/application rules and not on external systems, do not place it in Infrastructure
- use SQLite-compatible EF Core configuration
- configure aggregate loading properly with Includes where needed
- keep namespaces correct and aligned with the existing solution structure

Return:
1. files to create or update
2. short purpose of each file
3. then generate the code