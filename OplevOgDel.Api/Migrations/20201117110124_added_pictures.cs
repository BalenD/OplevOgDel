using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class added_pictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Experiences");

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Picture_Profiles_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "CreatedOn", "CreatorId", "DeletedOn", "ExperienceId", "IsDeleted", "ModifiedOn", "Path" },
                values: new object[] { new Guid("93e4f688-d9a0-4f8a-bc69-1d7f5a46101d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), null, new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), false, null, "TestImage1" });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "CreatedOn", "CreatorId", "DeletedOn", "ExperienceId", "IsDeleted", "ModifiedOn", "Path" },
                values: new object[] { new Guid("b393e49a-4097-4629-b15f-75fd9edd99c1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), null, new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), false, null, "TestImage2" });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "CreatedOn", "CreatorId", "DeletedOn", "ExperienceId", "IsDeleted", "ModifiedOn", "Path" },
                values: new object[] { new Guid("052c9135-c787-4fc5-8e1d-64c20aeda9bc"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("62357886-d888-44f2-a929-c015a4b31dad"), null, new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), false, null, "TestImage2" });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_CreatorId",
                table: "Picture",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ExperienceId",
                table: "Picture",
                column: "ExperienceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
