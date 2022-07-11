using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Remove_Realationship_Betwwen_Invoice_Config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Configs_ConfigId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ConfigId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CompanyInformationId",
                table: "Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyInformationId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ConfigId",
                table: "Invoices",
                column: "ConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Configs_ConfigId",
                table: "Invoices",
                column: "ConfigId",
                principalTable: "Configs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
