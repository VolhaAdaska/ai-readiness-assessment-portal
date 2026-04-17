using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Infrastructure.Services;

/// <summary>
/// Rule-based implementation of IRecommendationGenerator.
/// Derives recommendations from category maturity levels using a fixed rule set.
/// Low maturity produces higher-priority recommendations; mature categories are skipped.
/// </summary>
public class RuleBasedRecommendationGenerator : IRecommendationGenerator
{
    /// <inheritdoc />
    public Task<List<Recommendation>> GenerateRecommendationsAsync(
        BaselineAssessment assessment,
        CancellationToken cancellationToken = default)
    {
        var recommendations = new List<Recommendation>();

        foreach (var category in assessment.CategoryAssessments)
        {
            // Only generate recommendations for assessed categories with improvement potential
            if (category.MaturityLevel == 0)
                continue;

            var rules = GetRulesForCategory(category.Category);

            foreach (var rule in rules)
            {
                if (category.MaturityLevel <= rule.MaxMaturityLevelToApply)
                {
                    recommendations.Add(
                        Recommendation.Create(
                            category.Category,
                            rule.Priority,
                            rule.Title,
                            rule.Description));
                }
            }
        }

        return Task.FromResult(recommendations);
    }

    private static IReadOnlyList<RecommendationRule> GetRulesForCategory(CategoryType category) =>
        category switch
        {
            CategoryType.Data =>
            [
                new(MaxMaturityLevelToApply: 2, Priority.Critical,
                    "Establish a Data Governance Foundation",
                    "Define data ownership, classification policies, and access controls before scaling AI initiatives. Without clear governance, data quality and compliance risks will block adoption."),
                new(MaxMaturityLevelToApply: 3, Priority.High,
                    "Improve Data Quality and Availability",
                    "Implement data validation pipelines, automated quality checks, and centralised data catalogues so AI models have reliable, discoverable training data."),
                new(MaxMaturityLevelToApply: 4, Priority.Medium,
                    "Expand Real-Time Data Pipelines",
                    "Move from batch-oriented ingestion to event-driven or streaming pipelines to enable low-latency AI inference and monitoring."),
            ],

            CategoryType.Technology =>
            [
                new(MaxMaturityLevelToApply: 2, Priority.Critical,
                    "Build a Minimum AI Platform Baseline",
                    "Provision managed ML infrastructure (containerised workloads, model registry, experiment tracking) to reduce manual overhead and time-to-deployment."),
                new(MaxMaturityLevelToApply: 3, Priority.High,
                    "Introduce CI/CD for Models (MLOps)",
                    "Automate model training, validation, and deployment pipelines. Manual deployments are fragile and slow down iteration cycles."),
                new(MaxMaturityLevelToApply: 4, Priority.Medium,
                    "Implement Model Monitoring and Drift Detection",
                    "Add production telemetry, data drift alerts, and model performance dashboards to catch degradation before it affects business outcomes."),
            ],

            CategoryType.Process =>
            [
                new(MaxMaturityLevelToApply: 2, Priority.Critical,
                    "Define AI Use-Case Prioritisation Process",
                    "Establish a repeatable intake and scoring process to identify, rank, and fund AI use cases objectively rather than ad-hoc."),
                new(MaxMaturityLevelToApply: 3, Priority.High,
                    "Standardise the AI Development Lifecycle",
                    "Document and enforce stages from ideation through deployment: discovery, feasibility, experimentation, production, and retirement."),
                new(MaxMaturityLevelToApply: 4, Priority.Medium,
                    "Automate Repetitive Pre-Processing Tasks",
                    "Identify manual data preparation and feature engineering steps that can be automated to accelerate model development."),
            ],

            CategoryType.Security =>
            [
                new(MaxMaturityLevelToApply: 2, Priority.Critical,
                    "Perform an AI-Specific Threat Model",
                    "Assess risks specific to AI systems: model inversion, adversarial inputs, training data poisoning, and supply-chain attacks on model artefacts."),
                new(MaxMaturityLevelToApply: 3, Priority.High,
                    "Enforce Least-Privilege Access for AI Pipelines",
                    "Ensure ML pipelines, notebooks, and serving endpoints operate with minimal IAM permissions and secrets are managed via a vault, not environment variables."),
                new(MaxMaturityLevelToApply: 4, Priority.Medium,
                    "Add Model and Data Lineage Auditing",
                    "Capture provenance for training datasets and model versions so you can audit decisions and satisfy regulatory or internal compliance reviews."),
            ],

            CategoryType.Governance =>
            [
                new(MaxMaturityLevelToApply: 2, Priority.Critical,
                    "Adopt an AI Ethics and Responsible AI Policy",
                    "Document principles for fairness, transparency, accountability, and human oversight before deploying models that affect decisions about people."),
                new(MaxMaturityLevelToApply: 3, Priority.High,
                    "Establish an AI Review Board",
                    "Create a cross-functional body with authority to approve, pause, or retire AI systems based on risk, performance, and ethical criteria."),
                new(MaxMaturityLevelToApply: 4, Priority.Medium,
                    "Implement Automated Compliance Checks in Pipelines",
                    "Embed bias detection, explainability reports, and policy checks into the CI/CD pipeline so governance is enforced at build time, not only at review time."),
            ],

            CategoryType.TeamCapabilities =>
            [
                new(MaxMaturityLevelToApply: 2, Priority.Critical,
                    "Close Critical AI Skills Gaps",
                    "Identify roles with no AI proficiency and run targeted upskilling or hiring programmes. Lack of foundational skills is the most common blocker to AI adoption."),
                new(MaxMaturityLevelToApply: 3, Priority.High,
                    "Create Internal AI Champions and Communities of Practice",
                    "Designate AI champions per business unit and establish a community of practice to share learnings, tools, and reusable assets across teams."),
                new(MaxMaturityLevelToApply: 4, Priority.Medium,
                    "Develop an AI Literacy Programme for Non-Technical Staff",
                    "Ensure decision-makers, product owners, and business analysts understand AI capabilities and limitations so they can participate meaningfully in solution design."),
            ],

            _ => []
        };

    private sealed record RecommendationRule(
        int MaxMaturityLevelToApply,
        Priority Priority,
        string Title,
        string Description);
}
