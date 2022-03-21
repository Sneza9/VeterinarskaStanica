using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinarskaStanica.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrojTelefonaVlasnika",
                table: "Zivotinje",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojTelefonaVlasnika",
                table: "Zivotinje");
        }
    }
}
