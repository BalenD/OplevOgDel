using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class AddedSeedDataWChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Profiles_CreatorId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_ListOfExperiences_ListOfExperiencesId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExperiences_Profiles_OwnerId",
                table: "ListOfExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Experiences_ExperienceId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Profiles_OwnerId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Profiles_OwnerId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Reviews_ReviewId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Experiences_ExperienceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Profiles_OwnerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OwnerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reports_OwnerId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_OwnerId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_ListOfExperiences_OwnerId",
                table: "ListOfExperiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_CreatorId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_ListOfExperiencesId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ListOfExperiences");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ListOfExperiencesId",
                table: "Experiences");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExperienceId",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Reviews",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ReviewId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Reports",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ExperienceId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Ratings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Profiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Profiles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "ListOfExperiences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExpCategoryId",
                table: "Experiences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Experiences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListOfExpsExperience",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ListOfExpsId = table.Column<Guid>(nullable: false),
                    ExperienceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfExpsExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListOfExpsExperience_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListOfExpsExperience_ListOfExperiences_ListOfExpsId",
                        column: x => x.ListOfExpsId,
                        principalTable: "ListOfExperiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Administrator",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Email", "IsDeleted", "ModifiedOn", "Password", "Username" },
                values: new object[] { new Guid("85416f64-0f0e-4b8d-8687-4f3b9cc6b40f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "admin@admin.dk", false, null, "password", "admin" });

            migrationBuilder.InsertData(
                table: "ExpCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("199b5113-ae3b-47ce-adee-dcbac4935f88"), "Mad" },
                    { new Guid("45671c48-b7a2-4663-b826-044aeefd59ff"), "Natur" },
                    { new Guid("21015075-67a0-4f6e-8db7-b9eefd4361a8"), "Musik" },
                    { new Guid("af09b6cc-e3f0-4eae-bb89-ef36affd27d7"), "Action" },
                    { new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), "Kultur" },
                    { new Guid("037d26c3-66c4-4017-9c54-ca8e72ae56fa"), "Historie" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Email", "IsDeleted", "ModifiedOn", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "user1@user.dk", false, null, "password", "user1" },
                    { new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "user2@user.dk", false, null, "password", "user2" },
                    { new Guid("53e881e9-1b7a-461f-a286-48476deb343d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "user3@user.dk", false, null, "password", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "Birthday", "City", "CreatedOn", "DeletedOn", "FirstName", "Gender", "IsDeleted", "LastName", "ModifiedOn", "UserId" },
                values: new object[] { new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), 28, new DateTime(1992, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Jens", 0, false, "Olesen", null, new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed") });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "Birthday", "City", "CreatedOn", "DeletedOn", "FirstName", "Gender", "IsDeleted", "LastName", "ModifiedOn", "UserId" },
                values: new object[] { new Guid("62357886-d888-44f2-a929-c015a4b31dad"), 22, new DateTime(1998, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Humlebæk", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Annita", 1, false, "Bech Jensen", null, new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e") });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "Birthday", "City", "CreatedOn", "DeletedOn", "FirstName", "Gender", "IsDeleted", "LastName", "ModifiedOn", "UserId" },
                values: new object[] { new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"), 25, new DateTime(1995, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ballerup", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Frederik", 0, false, "Skov Laursen", null, new Guid("53e881e9-1b7a-461f-a286-48476deb343d") });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Address", "City", "CreatedOn", "DeletedOn", "Description", "ExpCategoryId", "IsDeleted", "ModifiedOn", "Name", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), "Nyhavn 2", "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Apollo Bar er en simpel restaurant beliggende i baggården ved Charlottenborg Kunsthal. Apollo Bar tilbyder morgenmad, frokost og middag.", new Guid("199b5113-ae3b-47ce-adee-dcbac4935f88"), false, null, "Apollo Bar", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), "Bagerstræde 7", "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Akustikken er en guitarbutik, der både sælger og reparerer musikudstyr.", new Guid("21015075-67a0-4f6e-8db7-b9eefd4361a8"), false, null, "Akustikken", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), "Vindmøllevej 6", "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Amager Bakke er en kunstig skibakke, der ligger på toppen af det nye forbrændingsanlæg, Amager Ressource Center (ARC).", new Guid("af09b6cc-e3f0-4eae-bb89-ef36affd27d7"), false, null, "Amager Bakke", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("c3965bec-3a76-40a9-b435-546d4cd2ad2f"), "Roskildevej 32", "Frederiksberg", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Med sine godt 160 år på bagen er Zoologisk Have København en af Europas ældste zoologiske haver. Mere end 3000 dyr, fordelt på over 200 arter, har deres daglige gang i København Zoo.", new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), false, null, "Zoologisk Have", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"), "Kronborg 2 C", "Helsingør", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Kronborg er et af Danmarks mest betydningsfulde slotte og fuld af Danmarkshistorie.", new Guid("037d26c3-66c4-4017-9c54-ca8e72ae56fa"), false, null, "Kronborg Slot", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("f574dea5-088b-4ecf-a0ba-439381cdfabf"), "København", "Gammel kongevej 10", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "I Planetarium kan du opleve stjernehimlen, galakser og planeter helt tæt på - både i deres udstilling, på rumrejser i Kuppelsalen og til foredrag om astronomi og rumfart.", new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), false, null, "Planetarium", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("ce9ba768-c6d0-4c1d-842e-2027eb3542d1"), "Gl. Strandvej 13", "Humlebæk", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Louisiana Museum of Modern Art er et museum beliggende i en stor gammel park ved Nordsjællands kyst i Humlebæk.", new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), false, null, "Louisiana", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("173bf385-9aba-408a-a7ea-8bfe892e91b3"), "Dyrehaven", "Klampenborg", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Dyrehaven er en stor naturpark, der ligger nord for København. Dyrehaven rummer skovområder, små søer og åbne sletter, hvor mere end 2000 vilde hjorte har deres daglige gang.", new Guid("45671c48-b7a2-4663-b826-044aeefd59ff"), false, null, "Dyrehaven", new Guid("62357886-d888-44f2-a929-c015a4b31dad") }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ExperienceId", "IsDeleted", "ModifiedOn", "ProfileId", "RatingCount" },
                values: new object[,]
                {
                    { new Guid("fe2f716e-65b3-484a-8a52-42a275f03e7d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), false, null, new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), 4 },
                    { new Guid("959ec261-0361-405a-86ef-84a33092ae4a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), false, null, new Guid("62357886-d888-44f2-a929-c015a4b31dad"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Description", "ExperienceId", "IsDeleted", "ModifiedOn", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("e7c89ea8-7d68-4d37-b592-fefd69277684"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Er en god oplevelse!", new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), false, null, new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("fb68f42a-09a0-45db-9216-7570f68219a4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Elsker maden", new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), false, null, new Guid("62357886-d888-44f2-a929-c015a4b31dad") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProfileId",
                table: "Reviews",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProfileId",
                table: "Reports",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProfileId",
                table: "Ratings",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExperiences_ProfileId",
                table: "ListOfExperiences",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ExpCategoryId",
                table: "Experiences",
                column: "ExpCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ProfileId",
                table: "Experiences",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExpsExperience_ExperienceId",
                table: "ListOfExpsExperience",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExpsExperience_ListOfExpsId",
                table: "ListOfExpsExperience",
                column: "ListOfExpsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_ExpCategory_ExpCategoryId",
                table: "Experiences",
                column: "ExpCategoryId",
                principalTable: "ExpCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Profiles_ProfileId",
                table: "Experiences",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExperiences_Profiles_ProfileId",
                table: "ListOfExperiences",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Experiences_ExperienceId",
                table: "Ratings",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Profiles_ProfileId",
                table: "Ratings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Profiles_ProfileId",
                table: "Reports",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Reviews_ReviewId",
                table: "Reports",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Experiences_ExperienceId",
                table: "Reviews",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Profiles_ProfileId",
                table: "Reviews",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_ExpCategory_ExpCategoryId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Profiles_ProfileId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_ListOfExperiences_Profiles_ProfileId",
                table: "ListOfExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Experiences_ExperienceId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Profiles_ProfileId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Profiles_ProfileId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Reviews_ReviewId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Experiences_ExperienceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Profiles_ProfileId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "ExpCategory");

            migrationBuilder.DropTable(
                name: "ListOfExpsExperience");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProfileId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ProfileId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ProfileId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_ListOfExperiences_ProfileId",
                table: "ListOfExperiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_ExpCategoryId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_ProfileId",
                table: "Experiences");

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("173bf385-9aba-408a-a7ea-8bfe892e91b3"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("c3965bec-3a76-40a9-b435-546d4cd2ad2f"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("ce9ba768-c6d0-4c1d-842e-2027eb3542d1"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("f574dea5-088b-4ecf-a0ba-439381cdfabf"));

            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"));

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: new Guid("959ec261-0361-405a-86ef-84a33092ae4a"));

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: new Guid("fe2f716e-65b3-484a-8a52-42a275f03e7d"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("e7c89ea8-7d68-4d37-b592-fefd69277684"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("fb68f42a-09a0-45db-9216-7570f68219a4"));

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"));

            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("62357886-d888-44f2-a929-c015a4b31dad"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"));

            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"));

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "ListOfExperiences");

            migrationBuilder.DropColumn(
                name: "ExpCategoryId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Experiences");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ExperienceId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReviewId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ExperienceId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Profiles",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Profiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ListOfExperiences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Experiences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Experiences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ListOfExperiencesId",
                table: "Experiences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OwnerId",
                table: "Reviews",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_OwnerId",
                table: "Reports",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_OwnerId",
                table: "Ratings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExperiences_OwnerId",
                table: "ListOfExperiences",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CreatorId",
                table: "Experiences",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ListOfExperiencesId",
                table: "Experiences",
                column: "ListOfExperiencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Profiles_CreatorId",
                table: "Experiences",
                column: "CreatorId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_ListOfExperiences_ListOfExperiencesId",
                table: "Experiences",
                column: "ListOfExperiencesId",
                principalTable: "ListOfExperiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfExperiences_Profiles_OwnerId",
                table: "ListOfExperiences",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Experiences_ExperienceId",
                table: "Ratings",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Profiles_OwnerId",
                table: "Ratings",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Profiles_OwnerId",
                table: "Reports",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Reviews_ReviewId",
                table: "Reports",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Experiences_ExperienceId",
                table: "Reviews",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Profiles_OwnerId",
                table: "Reviews",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
