using Microsoft.EntityFrameworkCore.Migrations;

namespace Theater.Infrastructure.Data.Migrations
{
    public partial class RoleRerfIdRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles");

            migrationBuilder.AlterColumn<int>(
                name: "PerformanceId",
                table: "TheaterRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles");

            migrationBuilder.AlterColumn<int>(
                name: "PerformanceId",
                table: "TheaterRoles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
