using Microsoft.EntityFrameworkCore.Migrations;

namespace WillyNet.SGP.Infraestructure.Persistence.Migrations
{
    public partial class ValidationIniciativa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa");

            migrationBuilder.AlterColumn<string>(
                name: "IniCodi",
                table: "Iniciativa",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa",
                column: "EstadId",
                principalTable: "Estado",
                principalColumn: "EstadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa");

            migrationBuilder.AlterColumn<string>(
                name: "IniCodi",
                table: "Iniciativa",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa",
                column: "EstadId",
                principalTable: "Estado",
                principalColumn: "EstadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
