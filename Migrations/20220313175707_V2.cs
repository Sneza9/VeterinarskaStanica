using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinarskaStanica.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZivotinjeVeterinari_Pregledi_PreglediID",
                table: "ZivotinjeVeterinari");

            migrationBuilder.RenameColumn(
                name: "PreglediID",
                table: "ZivotinjeVeterinari",
                newName: "PregledID");

            migrationBuilder.RenameIndex(
                name: "IX_ZivotinjeVeterinari_PreglediID",
                table: "ZivotinjeVeterinari",
                newName: "IX_ZivotinjeVeterinari_PregledID");

            migrationBuilder.AlterColumn<string>(
                name: "Lek",
                table: "ZivotinjeVeterinari",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VrstaZivotinje",
                table: "Zivotinje",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ZivotinjeVeterinari_Pregledi_PregledID",
                table: "ZivotinjeVeterinari",
                column: "PregledID",
                principalTable: "Pregledi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZivotinjeVeterinari_Pregledi_PregledID",
                table: "ZivotinjeVeterinari");

            migrationBuilder.DropColumn(
                name: "VrstaZivotinje",
                table: "Zivotinje");

            migrationBuilder.RenameColumn(
                name: "PregledID",
                table: "ZivotinjeVeterinari",
                newName: "PreglediID");

            migrationBuilder.RenameIndex(
                name: "IX_ZivotinjeVeterinari_PregledID",
                table: "ZivotinjeVeterinari",
                newName: "IX_ZivotinjeVeterinari_PreglediID");

            migrationBuilder.AlterColumn<string>(
                name: "Lek",
                table: "ZivotinjeVeterinari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_ZivotinjeVeterinari_Pregledi_PreglediID",
                table: "ZivotinjeVeterinari",
                column: "PreglediID",
                principalTable: "Pregledi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
