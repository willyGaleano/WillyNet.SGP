using Microsoft.EntityFrameworkCore.Migrations;

namespace WillyNet.SGP.Infraestructure.Persistence.Migrations
{
    public partial class Estado1NIniciativa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flujo_Estado_EstadId",
                table: "Flujo");

            migrationBuilder.DropIndex(
                name: "IX_Flujo_EstadId",
                table: "Flujo");

            migrationBuilder.DropColumn(
                name: "EstadId",
                table: "Flujo");

            migrationBuilder.AddColumn<int>(
                name: "EstadId",
                table: "Iniciativa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Iniciativa_EstadId",
                table: "Iniciativa",
                column: "EstadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa",
                column: "EstadId",
                principalTable: "Estado",
                principalColumn: "EstadId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa");

            migrationBuilder.DropIndex(
                name: "IX_Iniciativa_EstadId",
                table: "Iniciativa");

            migrationBuilder.DropColumn(
                name: "EstadId",
                table: "Iniciativa");

            migrationBuilder.AddColumn<int>(
                name: "EstadId",
                table: "Flujo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flujo_EstadId",
                table: "Flujo",
                column: "EstadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flujo_Estado_EstadId",
                table: "Flujo",
                column: "EstadId",
                principalTable: "Estado",
                principalColumn: "EstadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
