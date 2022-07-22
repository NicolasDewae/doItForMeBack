using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doItForMeBack.Migrations
{
    public partial class database2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Users_ClaimantId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_ClaimantId",
                table: "Missions");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Missions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Picture",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Missions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_ClaimantId",
                table: "Missions",
                column: "ClaimantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Users_ClaimantId",
                table: "Missions",
                column: "ClaimantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
