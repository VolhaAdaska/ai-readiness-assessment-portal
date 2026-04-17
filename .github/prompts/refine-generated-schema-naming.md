---
agent: 'agent'
description: refine generated schema naming
---

The database migration works, but the generated schema naming is not clean enough.

Please refine the EF Core persistence configuration so that:
- table names are explicit and professional
- foreign key column names are explicit and consistent
- mixed naming like InitialAssessment / BaselineAssessment is removed
- the schema remains functionally the same
- no Domain or Application redesign is introduced

Target naming style:
- Assessments
- AssessmentCategories
- AssessmentResponses
- Recommendations
- AssessmentId
- CategoryAssessmentId

Requirements:
- keep the current architecture intact
- update only EF Core configuration and related persistence mapping if needed
- do not change the MVP workflow
- before applying changes, list:
  1. files to update
  2. naming issues found
  3. what will be renamed