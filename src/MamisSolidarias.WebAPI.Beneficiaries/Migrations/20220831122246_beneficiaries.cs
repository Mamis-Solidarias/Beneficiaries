using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    public partial class beneficiaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Families_Id",
                table: "Families",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShoeSize = table.Column<int>(type: "integer", nullable: true),
                    ShirtSize = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PantsSize = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    School = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransportationMethod = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Health",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HasCovidVaccine = table.Column<bool>(type: "boolean", nullable: true),
                    HasMandatoryVaccines = table.Column<bool>(type: "boolean", nullable: true),
                    Observations = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Health", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Dni = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Comments = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Likes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FamilyId = table.Column<string>(type: "text", nullable: false),
                    ClothesId = table.Column<int>(type: "integer", nullable: true),
                    EducationId = table.Column<int>(type: "integer", nullable: true),
                    HealthId = table.Column<int>(type: "integer", nullable: true),
                    JobId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "Clothes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Education",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Health_HealthId",
                        column: x => x.HealthId,
                        principalTable: "Health",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_ClothesId",
                table: "Beneficiaries",
                column: "ClothesId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_Dni",
                table: "Beneficiaries",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_EducationId",
                table: "Beneficiaries",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_FamilyId",
                table: "Beneficiaries",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_HealthId",
                table: "Beneficiaries",
                column: "HealthId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_JobId",
                table: "Beneficiaries",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "Clothes");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Health");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Families_Id",
                table: "Families");
        }
    }
}
