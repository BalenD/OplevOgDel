using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class profileOnUserNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Profiles_ProfileId",
                table: "AdminUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "AdminUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                column: "Password",
                value: "$2a$11$lR8KHHTUwwgGA3sC0kEmdulUV8EqCfdlZUMDDaX3JB0nvnXxy0jJa");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"),
                column: "Password",
                value: "$2a$11$6xYb0RPj7HWp7Wc1frTIA.5XQd7nUwqouQwMgrZz43e/ko95ibm.S");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                column: "Password",
                value: "$2a$11$SWG9ShlyIH1bhGCmdoaqqeUpCQnze08pnMt.9hQkJ5PrXS9k/0qvu");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Profiles_ProfileId",
                table: "AdminUsers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Profiles_ProfileId",
                table: "AdminUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "AdminUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                column: "Password",
                value: "$2a$11$bYRoGBBDHzuxzdUMkvQ7peQvRUxr8UBHwa23PYNDhgLKcXYdubJh6");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"),
                column: "Password",
                value: "$2a$11$rDo3264c.pNHvExuQHPnFO.Sk8K2pVcm32sba2BsAXqfM8/FAbcS2");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                column: "Password",
                value: "$2a$11$l3s3bdeCmqeVWTBV5qwlNeSjIabLtfuO/PviuhU6Qht9DWYjy4PIq");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Profiles_ProfileId",
                table: "AdminUsers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
