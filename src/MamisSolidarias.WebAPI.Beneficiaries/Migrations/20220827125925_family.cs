using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    public partial class family : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    InternalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<string>(type: "text", nullable: false),
                    FamilyNumber = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CommunityId = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.InternalId);
                    table.ForeignKey(
                        name: "FK_Families_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsPreferred = table.Column<bool>(type: "boolean", nullable: false),
                    FamilyInternalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Families_FamilyInternalId",
                        column: x => x.FamilyInternalId,
                        principalTable: "Families",
                        principalColumn: "InternalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_FamilyInternalId",
                table: "Contact",
                column: "FamilyInternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_CommunityId",
                table: "Families",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_FamilyNumber",
                table: "Families",
                column: "FamilyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Families_Id",
                table: "Families",
                column: "Id");
            
            migrationBuilder.Sql(@"
            CREATE FUNCTION fix_families()
                   RETURNS trigger AS  
                   $$
                   BEGIN
                  if NEW.""FamilyNumber"" is null or NEW.""FamilyNumber"" = 0 then
            NEW.""FamilyNumber"" := 1 + (select count(*) from ""Families""
            where ""CommunityId"" = new.""CommunityId"");
            end if;
            NEW.""Id"" := concat(new.""CommunityId"",'-',cast(new.""FamilyNumber"" as varchar));
            RETURN NEW;
            END 
                $$
            LANGUAGE 'plpgsql';
            create trigger family_before_insert_trigger
                before insert
                on ""Families""
            for each row
            execute procedure fix_families();
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"drop trigger if exists family_before_insert_trigger on ""Families""");
            migrationBuilder.Sql(@"drop function if exists fix_families");
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Families");
            
        }
    }
}
