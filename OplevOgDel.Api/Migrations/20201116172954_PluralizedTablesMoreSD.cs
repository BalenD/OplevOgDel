using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class PluralizedTablesMoreSD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceReport_Experiences_ExperienceId",
                table: "ExperienceReport");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceReport_Profiles_ProfileId",
                table: "ExperienceReport");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExperiences_Profiles_ProfileId",
                table: "ListOfExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExpsExperience_Experiences_ExperienceId",
                table: "ListOfExpsExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExpsExperience_ListOfExperiences_ListOfExpsId",
                table: "ListOfExpsExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListOfExpsExperience",
                table: "ListOfExpsExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListOfExperiences",
                table: "ListOfExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperienceReport",
                table: "ExperienceReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator");

            migrationBuilder.RenameTable(
                name: "ListOfExpsExperience",
                newName: "ListOfExpsExperiences");

            migrationBuilder.RenameTable(
                name: "ListOfExperiences",
                newName: "ListOfExps");

            migrationBuilder.RenameTable(
                name: "ExperienceReport",
                newName: "ExperienceReports");

            migrationBuilder.RenameTable(
                name: "Administrator",
                newName: "Administrators");

            migrationBuilder.RenameIndex(
                name: "IX_ListOfExpsExperience_ListOfExpsId",
                table: "ListOfExpsExperiences",
                newName: "IX_ListOfExpsExperiences_ListOfExpsId");

            migrationBuilder.RenameIndex(
                name: "IX_ListOfExpsExperience_ExperienceId",
                table: "ListOfExpsExperiences",
                newName: "IX_ListOfExpsExperiences_ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_ListOfExperiences_ProfileId",
                table: "ListOfExps",
                newName: "IX_ListOfExps_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ExperienceReport_ProfileId",
                table: "ExperienceReports",
                newName: "IX_ExperienceReports_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ExperienceReport_ExperienceId",
                table: "ExperienceReports",
                newName: "IX_ExperienceReports_ExperienceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListOfExpsExperiences",
                table: "ListOfExpsExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListOfExps",
                table: "ListOfExps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperienceReports",
                table: "ExperienceReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ExperienceReports",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "ExperienceId", "IsDeleted", "ModifiedOn", "ProfileId" },
                values: new object[] { new Guid("f334abcc-8849-42f9-94cf-836e5aad5a18"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Brænd også det her!", new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), false, null, new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43") });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "ExperienceId", "IsDeleted", "ModifiedOn", "ProfileId" },
                values: new object[] { new Guid("4ede258a-d72d-4d97-83c9-58e801da3952"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Lorte sted!", new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), false, null, new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43") });

            migrationBuilder.InsertData(
                table: "ReviewReports",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "IsDeleted", "ModifiedOn", "ProfileId", "ReviewId" },
                values: new object[] { new Guid("6f9a5b6b-a53b-4d97-8a74-06344b828aca"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Dårlig kritik", false, null, new Guid("62357886-d888-44f2-a929-c015a4b31dad"), new Guid("4ede258a-d72d-4d97-83c9-58e801da3952") });

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceReports_Experiences_ExperienceId",
                table: "ExperienceReports",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceReports_Profiles_ProfileId",
                table: "ExperienceReports",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExps_Profiles_ProfileId",
                table: "ListOfExps",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExpsExperiences_Experiences_ExperienceId",
                table: "ListOfExpsExperiences",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExpsExperiences_ListOfExps_ListOfExpsId",
                table: "ListOfExpsExperiences",
                column: "ListOfExpsId",
                principalTable: "ListOfExps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceReports_Experiences_ExperienceId",
                table: "ExperienceReports");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceReports_Profiles_ProfileId",
                table: "ExperienceReports");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExps_Profiles_ProfileId",
                table: "ListOfExps");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExpsExperiences_Experiences_ExperienceId",
                table: "ListOfExpsExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExpsExperiences_ListOfExps_ListOfExpsId",
                table: "ListOfExpsExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListOfExpsExperiences",
                table: "ListOfExpsExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListOfExps",
                table: "ListOfExps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperienceReports",
                table: "ExperienceReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators");

            migrationBuilder.DeleteData(
                table: "ExperienceReports",
                keyColumn: "Id",
                keyValue: new Guid("f334abcc-8849-42f9-94cf-836e5aad5a18"));

            migrationBuilder.DeleteData(
                table: "ReviewReports",
                keyColumn: "Id",
                keyValue: new Guid("6f9a5b6b-a53b-4d97-8a74-06344b828aca"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("4ede258a-d72d-4d97-83c9-58e801da3952"));

            migrationBuilder.RenameTable(
                name: "ListOfExpsExperiences",
                newName: "ListOfExpsExperience");

            migrationBuilder.RenameTable(
                name: "ListOfExps",
                newName: "ListOfExperiences");

            migrationBuilder.RenameTable(
                name: "ExperienceReports",
                newName: "ExperienceReport");

            migrationBuilder.RenameTable(
                name: "Administrators",
                newName: "Administrator");

            migrationBuilder.RenameIndex(
                name: "IX_ListOfExpsExperiences_ListOfExpsId",
                table: "ListOfExpsExperience",
                newName: "IX_ListOfExpsExperience_ListOfExpsId");

            migrationBuilder.RenameIndex(
                name: "IX_ListOfExpsExperiences_ExperienceId",
                table: "ListOfExpsExperience",
                newName: "IX_ListOfExpsExperience_ExperienceId");

            migrationBuilder.RenameIndex(
                name: "IX_ListOfExps_ProfileId",
                table: "ListOfExperiences",
                newName: "IX_ListOfExperiences_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ExperienceReports_ProfileId",
                table: "ExperienceReport",
                newName: "IX_ExperienceReport_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ExperienceReports_ExperienceId",
                table: "ExperienceReport",
                newName: "IX_ExperienceReport_ExperienceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListOfExpsExperience",
                table: "ListOfExpsExperience",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListOfExperiences",
                table: "ListOfExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperienceReport",
                table: "ExperienceReport",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceReport_Experiences_ExperienceId",
                table: "ExperienceReport",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceReport_Profiles_ProfileId",
                table: "ExperienceReport",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExperiences_Profiles_ProfileId",
                table: "ListOfExperiences",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExpsExperience_Experiences_ExperienceId",
                table: "ListOfExpsExperience",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExpsExperience_ListOfExperiences_ListOfExpsId",
                table: "ListOfExpsExperience",
                column: "ListOfExpsId",
                principalTable: "ListOfExperiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
