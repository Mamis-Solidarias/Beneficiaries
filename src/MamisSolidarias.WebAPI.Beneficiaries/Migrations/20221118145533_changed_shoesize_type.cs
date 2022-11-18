using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    public partial class changed_shoesize_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Clothes\" ALTER COLUMN \"ShoeSize\" TYPE integer USING \"ShoeSize\"::integer;"
                );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Clothes\" ALTER COLUMN \"ShoeSize\" TYPE varchar(50) USING \"ShoeSize\"::varchar(50);"
            );
        }
    }
}
