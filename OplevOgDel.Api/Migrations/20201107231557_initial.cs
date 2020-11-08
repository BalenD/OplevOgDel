using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Age = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 250, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListOfExperiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListOfExperiences_Profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 250, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    ListOfExperiencesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Profiles_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experiences_ListOfExperiences_ListOfExperiencesId",
                        column: x => x.ListOfExperiencesId,
                        principalTable: "ListOfExperiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    RatingCount = table.Column<int>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    ExperienceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    ExperienceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    ReviewId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CreatorId",
                table: "Experiences",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ListOfExperiencesId",
                table: "Experiences",
                column: "ListOfExperiencesId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExperiences_OwnerId",
                table: "ListOfExperiences",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ExperienceId",
                table: "Ratings",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_OwnerId",
                table: "Ratings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_OwnerId",
                table: "Reports",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReviewId",
                table: "Reports",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ExperienceId",
                table: "Reviews",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OwnerId",
                table: "Reviews",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "ListOfExperiences");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
