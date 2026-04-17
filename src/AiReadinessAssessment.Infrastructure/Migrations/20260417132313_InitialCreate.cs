using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiReadinessAssessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitialAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialAssessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InitialAssessments_CategoryAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BaselineAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    MaturityLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Observations = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialAssessments_CategoryAssessments", x => new { x.BaselineAssessmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_InitialAssessments_CategoryAssessments_InitialAssessments_BaselineAssessmentId",
                        column: x => x.BaselineAssessmentId,
                        principalTable: "InitialAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitialAssessments_Recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BaselineAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialAssessments_Recommendations", x => new { x.BaselineAssessmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_InitialAssessments_Recommendations_InitialAssessments_BaselineAssessmentId",
                        column: x => x.BaselineAssessmentId,
                        principalTable: "InitialAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InitialAssessments_CategoryAssessments_Responses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryAssessmentBaselineAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialAssessments_CategoryAssessments_Responses", x => new { x.CategoryAssessmentBaselineAssessmentId, x.CategoryAssessmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_InitialAssessments_CategoryAssessments_Responses_InitialAssessments_CategoryAssessments_CategoryAssessmentBaselineAssessmentId_CategoryAssessmentId",
                        columns: x => new { x.CategoryAssessmentBaselineAssessmentId, x.CategoryAssessmentId },
                        principalTable: "InitialAssessments_CategoryAssessments",
                        principalColumns: new[] { "BaselineAssessmentId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitialAssessments_CategoryAssessments_Responses");

            migrationBuilder.DropTable(
                name: "InitialAssessments_Recommendations");

            migrationBuilder.DropTable(
                name: "InitialAssessments_CategoryAssessments");

            migrationBuilder.DropTable(
                name: "InitialAssessments");
        }
    }
}
