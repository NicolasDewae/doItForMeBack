using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doItForMeBack.Migrations
{
    public partial class report3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_claimantId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_claimantId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "claimantId",
                table: "Reports",
                newName: "ClaimantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimantId",
                table: "Reports",
                newName: "claimantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_claimantId",
                table: "Reports",
                column: "claimantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_claimantId",
                table: "Reports",
                column: "claimantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
