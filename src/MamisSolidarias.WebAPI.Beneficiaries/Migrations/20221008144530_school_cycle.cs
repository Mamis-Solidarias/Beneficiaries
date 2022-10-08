using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    public partial class school_cycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Education");

            migrationBuilder.AddColumn<int>(
                name: "Cycle",
                table: "Education",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cycle",
                table: "Education");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Education",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
