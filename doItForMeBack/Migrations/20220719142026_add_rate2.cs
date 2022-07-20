using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doItForMeBack.Migrations
{
    public partial class add_rate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rate_RateId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rate",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rate_UserId",
                table: "Rate",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Users_UserId",
                table: "Rate",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Users_UserId",
                table: "Rate");

            migrationBuilder.DropIndex(
                name: "IX_Rate_UserId",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rate");

            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RateId",
                table: "Users",
                column: "RateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rate_RateId",
                table: "Users",
                column: "RateId",
                principalTable: "Rate",
                principalColumn: "Id");
        }
    }
}
