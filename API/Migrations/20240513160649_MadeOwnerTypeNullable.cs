using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class MadeOwnerTypeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OwnerType_OwnerTypeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OwnerType_OwnerTypeId",
                table: "AspNetUsers",
                column: "OwnerTypeId",
                principalTable: "OwnerType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OwnerType_OwnerTypeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OwnerType_OwnerTypeId",
                table: "AspNetUsers",
                column: "OwnerTypeId",
                principalTable: "OwnerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
