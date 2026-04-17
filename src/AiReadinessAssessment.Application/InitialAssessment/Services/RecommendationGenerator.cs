using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Services;

/// <summary>
/// Rule-based recommendation generator that produces actionable improvement steps
/// derived from each category's maturity level and calculated score.
/// </summary>
public class RecommendationGenerator : IRecommendationGenerator
{
    private static readonly Dictionary<CategoryType, (string LowTitle, string LowDesc, string MedTitle, string MedDesc)> CategoryTemplates = new()
    {
        [CategoryType.Data] = (
            "Establish a Data Governance Foundation",
            "Your data maturity is critically low. Prioritize defining data ownership, cataloguing key datasets, and establishing baseline quality standards to unblock AI initiatives.",
            "Improve Data Quality and Accessibility",
            "Data pipelines and quality controls need strengthening. Invest in automated validation, centralised data storage, and documented data contracts to support ML workloads."),
        [CategoryType.Technology] = (
            "Build Core AI/ML Infrastructure",
            "Essential AI/ML tooling is missing or immature. Begin by adopting a managed cloud platform, version-controlling models, and establishing a reproducible experimentation environment.",
            "Standardise the AI Technology Stack",
            "Tooling exists but lacks standardisation. Consolidate platforms, introduce CI/CD for model deployments, and enforce environment parity between development and production."),
        [CategoryType.Process] = (
            "Define AI-Ready Processes",
            "Process maturity is insufficient for reliable AI delivery. Document end-to-end workflows, identify automation candidates, and establish feedback loops between business and technical teams.",
            "Automate and Optimise Key Workflows",
            "Processes are partially documented but manual steps create bottlenecks. Increase automation coverage and introduce monitoring to detect process drift in production."),
        [CategoryType.Security] = (
            "Address Critical Security and Compliance Gaps",
            "Security controls for AI workloads are inadequate. Immediately implement access controls, data encryption at rest and in transit, and conduct a compliance gap assessment.",
            "Harden Security Posture for AI Workloads",
            "Baseline controls exist but advanced threats are not fully mitigated. Adopt least-privilege access for model serving infrastructure and establish an AI-specific incident response plan."),
        [CategoryType.Governance] = (
            "Establish AI Governance and Accountability Structures",
            "There is no clear ownership or oversight for AI decisions. Define an AI ethics policy, assign accountable roles, and create a lightweight review board for high-risk use cases.",
            "Mature AI Governance Frameworks",
            "Governance structures exist but lack teeth. Formalise model risk assessments, implement audit trails, and integrate governance checkpoints into the model lifecycle."),
        [CategoryType.TeamCapabilities] = (
            "Invest in Foundational AI Skills Development",
            "The team lacks the skills required to deliver and maintain AI solutions. Launch structured training programmes, hire or partner for critical competencies, and foster an experimentation culture.",
            "Accelerate Team AI Proficiency",
            "Core skills exist but capability is uneven. Introduce internal communities of practice, stretch assignments on live AI projects, and targeted upskilling for domain experts.")
    };

    /// <inheritdoc />
    public Task<List<Recommendation>> GenerateRecommendationsAsync(
        BaselineAssessment assessment,
        CancellationToken cancellationToken = default)
    {
        var recommendations = new List<Recommendation>();

        foreach (var categoryAssessment in assessment.CategoryAssessments)
        {
            var score = categoryAssessment.CalculateCategoryScore();
            var recommendation = BuildRecommendation(categoryAssessment.Category, score);

            if (recommendation is not null)
                recommendations.Add(recommendation);
        }

        return Task.FromResult(recommendations);
    }

    private static Recommendation? BuildRecommendation(CategoryType category, double score)
    {
        if (!CategoryTemplates.TryGetValue(category, out var templates))
            return null;

        return score switch
        {
            < 40 => Recommendation.Create(
                category,
                score < 20 ? Priority.Critical : Priority.High,
                templates.LowTitle,
                templates.LowDesc),
            < 70 => Recommendation.Create(
                category,
                Priority.Medium,
                templates.MedTitle,
                templates.MedDesc),
            _ => null
        };
    }
}
