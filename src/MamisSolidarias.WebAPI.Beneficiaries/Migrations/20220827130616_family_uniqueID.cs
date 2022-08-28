using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    public partial class family_uniqueID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Families_Id",
                table: "Families");

            migrationBuilder.CreateIndex(
                name: "IX_Families_Id",
                table: "Families",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Families_Id",
                table: "Families");

            migrationBuilder.CreateIndex(
                name: "IX_Families_Id",
                table: "Families",
                column: "Id");
        }
    }
}
