using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doItForMeBack.Migrations
{
    public partial class add_Ban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Users_UserId",
                table: "Rate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rate",
                table: "Rate");

            migrationBuilder.RenameTable(
                name: "Rate",
                newName: "rates");

            migrationBuilder.RenameColumn(
                name: "star",
                table: "rates",
                newName: "Star");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_UserId",
                table: "rates",
                newName: "IX_rates_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "Ban",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_rates",
                table: "rates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rates_Users_UserId",
                table: "rates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rates_Users_UserId",
                table: "rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rates",
                table: "rates");

            migrationBuilder.DropColumn(
                name: "Ban",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "rates",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "Star",
                table: "Rate",
                newName: "star");

            migrationBuilder.RenameIndex(
                name: "IX_rates_UserId",
                table: "Rate",
                newName: "IX_Rate_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rate",
                table: "Rate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Users_UserId",
                table: "Rate",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
