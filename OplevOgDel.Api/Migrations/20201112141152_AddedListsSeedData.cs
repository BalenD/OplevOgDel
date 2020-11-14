using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class AddedListsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ListOfExperiences",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "IsDeleted", "ModifiedOn", "Name", "ProfileId" },
                values: new object[] { new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "De oplevelser jeg har oprettet", false, null, "Egne oplevelser", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") });

            migrationBuilder.InsertData(
                table: "ListOfExperiences",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "IsDeleted", "ModifiedOn", "Name", "ProfileId" },
                values: new object[] { new Guid("0e88c548-be6e-4437-b30a-ecfe39f05a8a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Mine favorit oplevelser", false, null, "Favorit oplevelser", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") });

            migrationBuilder.InsertData(
                table: "ListOfExpsExperience",
                columns: new[] { "Id", "ExperienceId", "ListOfExpsId" },
                values: new object[,]
                {
                    { new Guid("53737a29-c6d2-48ab-8a68-f534c55ed56d"), new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("9e1f46f6-898b-4a85-a545-4d3bec94ca53"), new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("08e7e2c3-0fb5-48b2-90df-1e1e192e99bf"), new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("a8fa3415-5230-4569-9d03-ced0df749185"), new Guid("c3965bec-3a76-40a9-b435-546d4cd2ad2f"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("2a86fff6-5eab-4c94-8587-8a59cd3798eb"), new Guid("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("aec784bc-e861-46a1-bf64-b76058a036de"), new Guid("f574dea5-088b-4ecf-a0ba-439381cdfabf"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ListOfExperiences",
                keyColumn: "Id",
                keyValue: new Guid("0e88c548-be6e-4437-b30a-ecfe39f05a8a"));

            migrationBuilder.DeleteData(
                table: "ListOfExpsExperience",
                keyColumn: "Id",
                keyValue: new Guid("08e7e2c3-0fb5-48b2-90df-1e1e192e99bf"));

            migrationBuilder.DeleteData(
                table: "ListOfExpsExperience",
                keyColumn: "Id",
                keyValue: new Guid("2a86fff6-5eab-4c94-8587-8a59cd3798eb"));

            migrationBuilder.DeleteData(
                table: "ListOfExpsExperience",
                keyColumn: "Id",
                keyValue: new Guid("53737a29-c6d2-48ab-8a68-f534c55ed56d"));

            migrationBuilder.DeleteData(
                table: "ListOfExpsExperience",
                keyColumn: "Id",
                keyValue: new Guid("9e1f46f6-898b-4a85-a545-4d3bec94ca53"));

            migrationBuilder.DeleteData(
                table: "ListOfExpsExperience",
                keyColumn: "Id",
                keyValue: new Guid("a8fa3415-5230-4569-9d03-ced0df749185"));

            migrationBuilder.DeleteData(
                table: "ListOfExpsExperience",
                keyColumn: "Id",
                keyValue: new Guid("aec784bc-e861-46a1-bf64-b76058a036de"));

            migrationBuilder.DeleteData(
                table: "ListOfExperiences",
                keyColumn: "Id",
                keyValue: new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d"));
        }
    }
}
