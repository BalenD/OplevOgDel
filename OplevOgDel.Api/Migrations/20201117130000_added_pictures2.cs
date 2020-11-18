using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class added_pictures2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Experiences_ExperienceId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Profiles_CreatorId",
                table: "Picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picture",
                table: "Picture");

            migrationBuilder.RenameTable(
                name: "Picture",
                newName: "pictures");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_ExperienceId",
                table: "pictures",
                newName: "IX_pictures_ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_CreatorId",
                table: "pictures",
                newName: "IX_pictures_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "pictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pictures",
                table: "pictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_Experiences_ExperienceId",
                table: "pictures",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_Profiles_CreatorId",
                table: "pictures",
                column: "CreatorId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pictures_Experiences_ExperienceId",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_Profiles_CreatorId",
                table: "pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pictures",
                table: "pictures");

            migrationBuilder.RenameTable(
                name: "pictures",
                newName: "Picture");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_ExperienceId",
                table: "Picture",
                newName: "IX_Picture_ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_CreatorId",
                table: "Picture",
                newName: "IX_Picture_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Picture",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picture",
                table: "Picture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Experiences_ExperienceId",
                table: "Picture",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Profiles_CreatorId",
                table: "Picture",
                column: "CreatorId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
