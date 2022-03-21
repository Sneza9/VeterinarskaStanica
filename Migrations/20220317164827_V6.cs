using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinarskaStanica.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VrstaZivotinje",
                table: "Zivotinje");

            migrationBuilder.AddColumn<int>(
                name: "VrstaZivotinjeID",
                table: "Zivotinje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VrsteZivotinja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrsta = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteZivotinja", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zivotinje_VrstaZivotinjeID",
                table: "Zivotinje",
                column: "VrstaZivotinjeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zivotinje_VrsteZivotinja_VrstaZivotinjeID",
                table: "Zivotinje",
                column: "VrstaZivotinjeID",
                principalTable: "VrsteZivotinja",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zivotinje_VrsteZivotinja_VrstaZivotinjeID",
                table: "Zivotinje");

            migrationBuilder.DropTable(
                name: "VrsteZivotinja");

            migrationBuilder.DropIndex(
                name: "IX_Zivotinje_VrstaZivotinjeID",
                table: "Zivotinje");

            migrationBuilder.DropColumn(
                name: "VrstaZivotinjeID",
                table: "Zivotinje");

            migrationBuilder.AddColumn<string>(
                name: "VrstaZivotinje",
                table: "Zivotinje",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
