using Microsoft.EntityFrameworkCore.Migrations;

namespace Theater.Infrastructure.Data.Migrations
{
    public partial class AddDescriptionToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TheaterRoles",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TheaterRoles");
        }
    }
}
