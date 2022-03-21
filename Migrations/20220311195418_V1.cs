using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinarskaStanica.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pregledi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mesec = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregledi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Veterinari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrojTelefona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zivotinje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKartona = table.Column<int>(type: "int", nullable: false),
                    ImeZivotinje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImeVlasnika = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zivotinje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ZivotinjeVeterinari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreglediID = table.Column<int>(type: "int", nullable: true),
                    ZivotinjaID = table.Column<int>(type: "int", nullable: true),
                    VeterinarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZivotinjeVeterinari", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZivotinjeVeterinari_Pregledi_PreglediID",
                        column: x => x.PreglediID,
                        principalTable: "Pregledi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZivotinjeVeterinari_Veterinari_VeterinarID",
                        column: x => x.VeterinarID,
                        principalTable: "Veterinari",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZivotinjeVeterinari_Zivotinje_ZivotinjaID",
                        column: x => x.ZivotinjaID,
                        principalTable: "Zivotinje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZivotinjeVeterinari_PreglediID",
                table: "ZivotinjeVeterinari",
                column: "PreglediID");

            migrationBuilder.CreateIndex(
                name: "IX_ZivotinjeVeterinari_VeterinarID",
                table: "ZivotinjeVeterinari",
                column: "VeterinarID");

            migrationBuilder.CreateIndex(
                name: "IX_ZivotinjeVeterinari_ZivotinjaID",
                table: "ZivotinjeVeterinari",
                column: "ZivotinjaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZivotinjeVeterinari");

            migrationBuilder.DropTable(
                name: "Pregledi");

            migrationBuilder.DropTable(
                name: "Veterinari");

            migrationBuilder.DropTable(
                name: "Zivotinje");
        }
    }
}
