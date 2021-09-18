using Microsoft.EntityFrameworkCore.Migrations;

namespace WillyNet.SGP.Infraestructure.Persistence.Migrations
{
    public partial class RelacionFlujoIniModEst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Iniciativa_AspNetUsers_UserCreaId",
                table: "Iniciativa");

            migrationBuilder.DropForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa");

            migrationBuilder.DropIndex(
                name: "IX_Iniciativa_EstadId",
                table: "Iniciativa");

            migrationBuilder.DropColumn(
                name: "EstadId",
                table: "Iniciativa");

            migrationBuilder.RenameColumn(
                name: "FlujoPriori",
                table: "Flujo",
                newName: "FlujoActivo");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreaId",
                table: "Iniciativa",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IniNomb",
                table: "Iniciativa",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IniDescrip",
                table: "Iniciativa",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IniPriori",
                table: "Iniciativa",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Iniciativa_AspNetUsers_UserCreaId",
                table: "Iniciativa",
                column: "UserCreaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flujo_Estado_EstadId",
                table: "Flujo");

            migrationBuilder.DropForeignKey(
                name: "FK_Iniciativa_AspNetUsers_UserCreaId",
                table: "Iniciativa");

            migrationBuilder.DropIndex(
                name: "IX_Flujo_EstadId",
                table: "Flujo");

            migrationBuilder.DropColumn(
                name: "IniPriori",
                table: "Iniciativa");

            migrationBuilder.DropColumn(
                name: "EstadId",
                table: "Flujo");

            migrationBuilder.RenameColumn(
                name: "FlujoActivo",
                table: "Flujo",
                newName: "FlujoPriori");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreaId",
                table: "Iniciativa",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "IniNomb",
                table: "Iniciativa",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "IniDescrip",
                table: "Iniciativa",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "EstadId",
                table: "Iniciativa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Iniciativa_EstadId",
                table: "Iniciativa",
                column: "EstadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Iniciativa_AspNetUsers_UserCreaId",
                table: "Iniciativa",
                column: "UserCreaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Iniciativa_Estado_EstadId",
                table: "Iniciativa",
                column: "EstadId",
                principalTable: "Estado",
                principalColumn: "EstadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
