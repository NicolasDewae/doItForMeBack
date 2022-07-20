using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doItForMeBack.Migrations
{
    public partial class add_rate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUserRate = table.Column<int>(type: "int", nullable: false),
                    IdUserRated = table.Column<int>(type: "int", nullable: false),
                    star = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rate_RateId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropIndex(
                name: "IX_Users_RateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Rate",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
