using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class addedRoleToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AdminUsers_UserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "AdminUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                columns: new[] { "Password", "ProfileId", "Role" },
                values: new object[] { "$2a$11$bYRoGBBDHzuxzdUMkvQ7peQvRUxr8UBHwa23PYNDhgLKcXYdubJh6", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), "Admin" });

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"),
                columns: new[] { "Password", "ProfileId", "Role" },
                values: new object[] { "$2a$11$rDo3264c.pNHvExuQHPnFO.Sk8K2pVcm32sba2BsAXqfM8/FAbcS2", new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"), "User" });

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                columns: new[] { "Password", "ProfileId", "Role" },
                values: new object[] { "$2a$11$l3s3bdeCmqeVWTBV5qwlNeSjIabLtfuO/PviuhU6Qht9DWYjy4PIq", new Guid("62357886-d888-44f2-a929-c015a4b31dad"), "User" });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"),
                column: "Gender",
                value: "Male");

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("62357886-d888-44f2-a929-c015a4b31dad"),
                column: "Gender",
                value: "Female");

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"),
                column: "Gender",
                value: "Male");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_ProfileId",
                table: "AdminUsers",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Profiles_ProfileId",
                table: "AdminUsers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Profiles_ProfileId",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_ProfileId",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AdminUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Profiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Profiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                column: "Password",
                value: "password");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"),
                column: "Password",
                value: "password");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                column: "Password",
                value: "password");

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"),
                columns: new[] { "Gender", "UserId" },
                values: new object[] { 0, new Guid("53e881e9-1b7a-461f-a286-48476deb343d") });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("62357886-d888-44f2-a929-c015a4b31dad"),
                columns: new[] { "Gender", "UserId" },
                values: new object[] { 1, new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e") });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"),
                columns: new[] { "Gender", "UserId" },
                values: new object[] { 0, new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed") });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AdminUsers_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
