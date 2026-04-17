---
agent: 'agent'
description: Fix recommendation generator dependency injection
---

Fix the current backend startup error related to IRecommendationGenerator.

Current error:
The service provider cannot resolve:
AiReadinessAssessment.Application.InitialAssessment.Services.IRecommendationGenerator
while creating:
CompleteInitialAssessmentCommandHandler

Requirements:
- inspect the Application and Infrastructure layers
- determine whether a concrete RecommendationGenerator implementation exists
- if it exists, register it correctly in dependency injection
- if it does not exist, create the minimum production-like implementation needed for the MVP
- keep the architecture clean
- do not move business logic into controllers
- do not redesign the MVP flow
- before applying changes, list:
  1. the files to update
  2. whether the problem is missing registration or missing implementation
  3. how the fix will preserve architecture boundaries

After that:
- apply the fix
- summarize what was changed
- confirm whether the startup error should now be resolved