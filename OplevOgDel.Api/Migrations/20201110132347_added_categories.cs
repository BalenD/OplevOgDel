using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class added_categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_ExpCategory_ExpCategoryId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpCategory",
                table: "ExpCategory");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "ExpCategory",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Categories_ExpCategoryId",
                table: "Experiences",
                column: "ExpCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AdminUsers_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Categories_ExpCategoryId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AdminUsers_UserId",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "ExpCategory");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpCategory",
                table: "ExpCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_ExpCategory_ExpCategoryId",
                table: "Experiences",
                column: "ExpCategoryId",
                principalTable: "ExpCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
