---
description: Design the domain model for a new feature
---

Design and refine the Domain model for the following feature in AI Readiness Assessment Portal.

Feature:
$input:feature_name

Context:
This product is a full-stack portal for assessing organizational readiness for AI adoption across data, technology, processes, security, governance, and team capabilities.

Requirements:
- Follow repository-wide and path-specific instructions.
- Build the model using explicit domain language.
- Keep the model production-like but not overengineered.
- Focus only on Domain layer concerns.
- Do not include persistence-specific code, DTOs, controllers, or EF Core concerns.
- Prefer entities and enums for the first iteration.
- Use one type per file.
- Keep the first version minimal but realistic for an MVP.
- If useful, propose simple domain behavior methods instead of anemic data-only models.

When designing the model:
- identify the main entities
- identify supporting entities
- identify enums
- define relationships
- define important invariants
- suggest which logic belongs inside entities

Return:
1. domain concepts
2. entities
3. enums
4. relationships
5. invariants
6. suggested folder structure
7. exact files to create
8. assumptions or trade-offs

Do not generate API, Application, or Infrastructure code.