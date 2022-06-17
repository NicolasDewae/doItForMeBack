using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doItForMeBack.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state",
                table: "Users",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Users",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "adress",
                table: "Users",
                newName: "Adress");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Users",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Users",
                newName: "adress");
        }
    }
}
