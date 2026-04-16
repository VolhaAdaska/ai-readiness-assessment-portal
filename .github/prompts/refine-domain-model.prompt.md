---
description: Refine the current Domain model after review
agent: edit
---

Refine the current Domain model in the AiReadinessAssessment.Domain project based on the latest review.

Current focus:
We are building the initial assessment MVP for AI Readiness Assessment Portal.

Apply only the minimum necessary improvements for the MVP.

Required changes:
- add an invariant so an assessment cannot be completed unless all 6 required categories exist and are complete
- verify that Recommendation correctly uses Priority
- keep ReadinessLevel placement reasonable for the current project structure
- do not introduce post-MVP concepts such as templates, scoring weights, score snapshots, or extra entities
- do not introduce EF Core, DTOs, repositories, controllers, or persistence concerns
- do not overengineer with unnecessary value objects

Requirements:
- follow repository and path-specific instructions
- keep one type per file
- preserve the current domain language and structure where possible
- before applying changes, list the files that will be updated and explain why

Return:
1. files to update
2. summary of changes
3. then generate the updated code