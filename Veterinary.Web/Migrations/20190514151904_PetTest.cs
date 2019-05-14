using Microsoft.EntityFrameworkCore.Migrations;

namespace Veterinary.Web.Migrations
{
    public partial class PetTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetTypes_PetTypeId",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "PetTypeId",
                table: "Pets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Pets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetTypes_PetTypeId",
                table: "Pets",
                column: "PetTypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetTypes_PetTypeId",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "PetTypeId",
                table: "Pets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Pets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetTypes_PetTypeId",
                table: "Pets",
                column: "PetTypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
