using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinarskaStanica.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipStrukeVeterinaraID",
                table: "Veterinari",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoviStruke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviStruke", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veterinari_TipStrukeVeterinaraID",
                table: "Veterinari",
                column: "TipStrukeVeterinaraID");

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinari_TipoviStruke_TipStrukeVeterinaraID",
                table: "Veterinari",
                column: "TipStrukeVeterinaraID",
                principalTable: "TipoviStruke",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinari_TipoviStruke_TipStrukeVeterinaraID",
                table: "Veterinari");

            migrationBuilder.DropTable(
                name: "TipoviStruke");

            migrationBuilder.DropIndex(
                name: "IX_Veterinari_TipStrukeVeterinaraID",
                table: "Veterinari");

            migrationBuilder.DropColumn(
                name: "TipStrukeVeterinaraID",
                table: "Veterinari");
        }
    }
}
