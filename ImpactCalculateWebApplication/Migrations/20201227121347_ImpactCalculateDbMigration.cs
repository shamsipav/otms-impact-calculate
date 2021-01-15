using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactCalculateWebApplication.Migrations
{
    public partial class ImpactCalculateDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_A_AV_VW_DeviceKey",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_A_AV_VW_GasKey",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "GasKey",
                table: "Results",
                newName: "GasID");

            migrationBuilder.RenameColumn(
                name: "DeviceKey",
                table: "Results",
                newName: "DeviceID");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Results",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Results_GasKey",
                table: "Results",
                newName: "IX_Results_GasID");

            migrationBuilder.RenameIndex(
                name: "IX_Results_DeviceKey",
                table: "Results",
                newName: "IX_Results_DeviceID");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Inputs",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "A_AV_VW",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_A_AV_VW_DeviceID",
                table: "Results",
                column: "DeviceID",
                principalTable: "A_AV_VW",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_A_AV_VW_GasID",
                table: "Results",
                column: "GasID",
                principalTable: "A_AV_VW",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_A_AV_VW_DeviceID",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_A_AV_VW_GasID",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "GasID",
                table: "Results",
                newName: "GasKey");

            migrationBuilder.RenameColumn(
                name: "DeviceID",
                table: "Results",
                newName: "DeviceKey");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Results",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_Results_GasID",
                table: "Results",
                newName: "IX_Results_GasKey");

            migrationBuilder.RenameIndex(
                name: "IX_Results_DeviceID",
                table: "Results",
                newName: "IX_Results_DeviceKey");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Inputs",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "A_AV_VW",
                newName: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_A_AV_VW_DeviceKey",
                table: "Results",
                column: "DeviceKey",
                principalTable: "A_AV_VW",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_A_AV_VW_GasKey",
                table: "Results",
                column: "GasKey",
                principalTable: "A_AV_VW",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
