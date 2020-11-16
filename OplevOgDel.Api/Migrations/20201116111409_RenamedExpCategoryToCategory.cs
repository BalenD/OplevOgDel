using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class RenamedExpCategoryToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Categories_ExpCategoryId",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "ExpCategoryId",
                table: "Experiences",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_ExpCategoryId",
                table: "Experiences",
                newName: "IX_Experiences_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Categories_CategoryId",
                table: "Experiences",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Categories_CategoryId",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Experiences",
                newName: "ExpCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_CategoryId",
                table: "Experiences",
                newName: "IX_Experiences_ExpCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Categories_ExpCategoryId",
                table: "Experiences",
                column: "ExpCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
