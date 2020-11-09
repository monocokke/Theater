using Microsoft.EntityFrameworkCore.Migrations;

namespace Theater.Infrastructure.Data.Migrations
{
    public partial class Add_PK_ActorRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles");

            migrationBuilder.AlterColumn<int>(
                name: "PerformanceId",
                table: "TheaterRoles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "TheaterRoles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "TheaterRoles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ActorRoles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ActorRoles_ActorId_RoleId",
                table: "ActorRoles",
                columns: new[] { "ActorId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ActorRoles_ActorId_RoleId",
                table: "ActorRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActorRoles");

            migrationBuilder.AlterColumn<int>(
                name: "PerformanceId",
                table: "TheaterRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "TheaterRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "TheaterRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles",
                columns: new[] { "ActorId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TheaterRoles_Performances_PerformanceId",
                table: "TheaterRoles",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
