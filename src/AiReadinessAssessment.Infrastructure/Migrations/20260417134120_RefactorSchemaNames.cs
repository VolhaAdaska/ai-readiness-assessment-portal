using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiReadinessAssessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSchemaNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitialAssessments_CategoryAssessments_Responses");

            migrationBuilder.DropTable(
                name: "InitialAssessments_Recommendations");

            migrationBuilder.DropTable(
                name: "InitialAssessments_CategoryAssessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitialAssessments",
                table: "InitialAssessments");

            migrationBuilder.RenameTable(
                name: "InitialAssessments",
                newName: "Assessments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assessments",
                table: "Assessments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssessmentCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    MaturityLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Observations = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentCategories", x => new { x.AssessmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_AssessmentCategories_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => new { x.AssessmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Recommendations_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResponses", x => new { x.AssessmentId, x.CategoryAssessmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_AssessmentResponses_AssessmentCategories_AssessmentId_CategoryAssessmentId",
                        columns: x => new { x.AssessmentId, x.CategoryAssessmentId },
                        principalTable: "AssessmentCategories",
                        principalColumns: new[] { "AssessmentId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentResponses");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "AssessmentCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assessments",
                table: "Assessments");

            migrationBuilder.RenameTable(
                name: "Assessments",
                newName: "InitialAssessments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitialAssessments",
                table: "InitialAssessments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InitialAssessments_CategoryAssessments",
                columns: table => new
                {
                    BaselineAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    BaselineAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
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
                    CategoryAssessmentBaselineAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryAssessmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
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
    }
}
