using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    /// <inheritdoc />
    public partial class FixedFamiliesTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE or REPLACE FUNCTION fix_families()
                   RETURNS trigger AS  
                   $$

                DECLARE
                    maxFamilyNumber INTEGER;
                BEGIN
                    if NEW.""FamilyNumber"" is null or NEW.""FamilyNumber"" = 0 then
                        select Max(""FamilyNumber"") into maxFamilyNumber from ""Families"" where ""CommunityId"" = new.""CommunityId"";
 
                        case
                            when maxFamilyNumber is null then
                                NEW.""FamilyNumber"" := 1;
                            else
                                NEW.""FamilyNumber"" := 1 + maxFamilyNumber;
                        end case;
                    end if;   
                    NEW.""Id"" := concat(new.""CommunityId"",'-',cast(new.""FamilyNumber"" as varchar));
                    RETURN NEW;
                END 
                $$
            LANGUAGE 'plpgsql';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION fix_families()
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
            ");
        }
    }
}
