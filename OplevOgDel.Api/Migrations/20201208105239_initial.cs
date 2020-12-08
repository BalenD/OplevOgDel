using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiences_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ListOfExps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfExps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListOfExps_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceReports_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceReports_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pictures_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingCount = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListOfExpsExperiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListOfExpsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfExpsExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListOfExpsExperiences_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListOfExpsExperiences_ListOfExps_ListOfExpsId",
                        column: x => x.ListOfExpsId,
                        principalTable: "ListOfExps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewReports_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewReports_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
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
                columns: new[] { "Id", "CreatedOn", "Email", "ModifiedOn", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("79539d93-2a2d-4600-abce-3f727e5cae7d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "admin1@admin.dk", null, "$2a$11$VC3/ge.OWDAK5C3eRAxHaO/TpmGvJ6lsFRtcVbHt7BvIdSdfi3.RK", "Admin", "admin1" },
                    { new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "user1@user.dk", null, "$2a$11$9/ow7FZ2Iy7MT80l1G1XiehGQTnpeT9xcPmiJNTbkbd.3YwsmZgJu", "User", "user1" },
                    { new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "user2@user.dk", null, "$2a$11$f0o2kcXGXMuTiWGdDktuzeR9Cnc2u7fQXmkz740nwyxyYvGHRF9bG", "User", "user2" },
                    { new Guid("53e881e9-1b7a-461f-a286-48476deb343d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "user3@user.dk", null, "$2a$11$deCL6gpyZHdFU3k0sqaPMuVpRZP97VVr2U/CWr6zLekYeEb1QmKca", "User", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "Birthday", "City", "CreatedOn", "FirstName", "Gender", "LastName", "ModifiedOn", "UserId" },
                values: new object[] { new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), 28, new DateTime(1992, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Jens", "Male", "Olesen", null, new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed") });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "Birthday", "City", "CreatedOn", "FirstName", "Gender", "LastName", "ModifiedOn", "UserId" },
                values: new object[] { new Guid("62357886-d888-44f2-a929-c015a4b31dad"), 22, new DateTime(1998, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Humlebæk", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Annita", "Female", "Bech Jensen", null, new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e") });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "Birthday", "City", "CreatedOn", "FirstName", "Gender", "LastName", "ModifiedOn", "UserId" },
                values: new object[] { new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"), 25, new DateTime(1995, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ballerup", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Frederik", "Male", "Skov Laursen", null, new Guid("53e881e9-1b7a-461f-a286-48476deb343d") });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CreatedOn", "Description", "ModifiedOn", "Name", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), "Nyhavn 2", new Guid("199b5113-ae3b-47ce-adee-dcbac4935f88"), "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Apollo Bar er en simpel restaurant beliggende i baggården ved Charlottenborg Kunsthal. Apollo Bar tilbyder morgenmad, frokost og middag.", null, "Apollo Bar", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), "Bagerstræde 7", new Guid("21015075-67a0-4f6e-8db7-b9eefd4361a8"), "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Akustikken er en guitarbutik, der både sælger og reparerer musikudstyr.", null, "Akustikken", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), "Vindmøllevej 6", new Guid("af09b6cc-e3f0-4eae-bb89-ef36affd27d7"), "København", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Amager Bakke er en kunstig skibakke, der ligger på toppen af det nye forbrændingsanlæg, Amager Ressource Center (ARC).", null, "Amager Bakke", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("c3965bec-3a76-40a9-b435-546d4cd2ad2f"), "Roskildevej 32", new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), "Frederiksberg", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Med sine godt 160 år på bagen er Zoologisk Have København en af Europas ældste zoologiske haver. Mere end 3000 dyr, fordelt på over 200 arter, har deres daglige gang i København Zoo.", null, "Zoologisk Have", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"), "Kronborg 2 C", new Guid("037d26c3-66c4-4017-9c54-ca8e72ae56fa"), "Helsingør", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Kronborg er et af Danmarks mest betydningsfulde slotte og fuld af Danmarkshistorie.", null, "Kronborg Slot", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("f574dea5-088b-4ecf-a0ba-439381cdfabf"), "København", new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), "Gammel kongevej 10", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "I Planetarium kan du opleve stjernehimlen, galakser og planeter helt tæt på - både i deres udstilling, på rumrejser i Kuppelsalen og til foredrag om astronomi og rumfart.", null, "Planetarium", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("ce9ba768-c6d0-4c1d-842e-2027eb3542d1"), "Gl. Strandvej 13", new Guid("b6f3f639-1450-4334-a123-2b1fb8c68808"), "Humlebæk", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Louisiana Museum of Modern Art er et museum beliggende i en stor gammel park ved Nordsjællands kyst i Humlebæk.", null, "Louisiana", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("173bf385-9aba-408a-a7ea-8bfe892e91b3"), "Dyrehaven", new Guid("45671c48-b7a2-4663-b826-044aeefd59ff"), "Klampenborg", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dyrehaven er en stor naturpark, der ligger nord for København. Dyrehaven rummer skovområder, små søer og åbne sletter, hvor mere end 2000 vilde hjorte har deres daglige gang.", null, "Dyrehaven", new Guid("62357886-d888-44f2-a929-c015a4b31dad") }
                });

            migrationBuilder.InsertData(
                table: "ListOfExps",
                columns: new[] { "Id", "CreatedOn", "Description", "ModifiedOn", "Name", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "De oplevelser jeg har oprettet", null, "Egne oplevelser", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("0e88c548-be6e-4437-b30a-ecfe39f05a8a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mine favorit oplevelser", null, "Favorit oplevelser", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") }
                });

            migrationBuilder.InsertData(
                table: "ExperienceReports",
                columns: new[] { "Id", "CreatedOn", "Description", "ExperienceId", "ModifiedOn", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("593f0428-4898-4b3f-add1-96ca682acf4c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Brænd det!", new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43") },
                    { new Guid("f334abcc-8849-42f9-94cf-836e5aad5a18"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Brænd også det her!", new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), null, new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43") }
                });

            migrationBuilder.InsertData(
                table: "ListOfExpsExperiences",
                columns: new[] { "Id", "ExperienceId", "ListOfExpsId" },
                values: new object[,]
                {
                    { new Guid("aec784bc-e861-46a1-bf64-b76058a036de"), new Guid("f574dea5-088b-4ecf-a0ba-439381cdfabf"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("2a86fff6-5eab-4c94-8587-8a59cd3798eb"), new Guid("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("a8fa3415-5230-4569-9d03-ced0df749185"), new Guid("c3965bec-3a76-40a9-b435-546d4cd2ad2f"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("08e7e2c3-0fb5-48b2-90df-1e1e192e99bf"), new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("9e1f46f6-898b-4a85-a545-4d3bec94ca53"), new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") },
                    { new Guid("53737a29-c6d2-48ab-8a68-f534c55ed56d"), new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), new Guid("dadfd17a-7e46-4d8f-87af-abd32bd6c12d") }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "CreatedOn", "ExperienceId", "ModifiedOn", "Path", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("b9260b28-f0b1-4bdd-80c1-b55dd7283a57"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f574dea5-088b-4ecf-a0ba-439381cdfabf"), null, "Planetarium.jpg", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("b47f5c92-9a1e-4328-a00a-d7be8fbecc5b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"), null, "Kronborg.jpg", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("bf6a2c72-ea8d-4ec7-9787-7a43e642595f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c3965bec-3a76-40a9-b435-546d4cd2ad2f"), null, "Zoo.jpg", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("17414f03-fb52-4c77-b46e-b40bd3837254"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("173bf385-9aba-408a-a7ea-8bfe892e91b3"), null, "Dyrehaven.jpg", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), null, "Amager.jpg", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("052c9135-c787-4fc5-8e1d-64c20aeda9bc"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("82a5a437-35b3-44b8-b10a-01d13577b7f1"), null, "Akustikken.jpg", new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("93e4f688-d9a0-4f8a-bc69-1d7f5a46101d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, "Apollo.jpg", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("44b64611-67c7-4230-b146-d8ac2c234639"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("ce9ba768-c6d0-4c1d-842e-2027eb3542d1"), null, "Lousiana.jpg", new Guid("62357886-d888-44f2-a929-c015a4b31dad") }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "CreatedOn", "ExperienceId", "ModifiedOn", "ProfileId", "RatingCount" },
                values: new object[,]
                {
                    { new Guid("959ec261-0361-405a-86ef-84a33092ae4a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, new Guid("62357886-d888-44f2-a929-c015a4b31dad"), 5 },
                    { new Guid("fe2f716e-65b3-484a-8a52-42a275f03e7d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), 4 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedOn", "Description", "ExperienceId", "ModifiedOn", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("a67a1c3e-c487-4231-b5af-6da7bd11032f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Hader dette sted, burde brændes ned!", new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43") },
                    { new Guid("fb68f42a-09a0-45db-9216-7570f68219a4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Elsker maden", new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, new Guid("62357886-d888-44f2-a929-c015a4b31dad") },
                    { new Guid("e7c89ea8-7d68-4d37-b592-fefd69277684"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Er en god oplevelse!", new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") },
                    { new Guid("4ede258a-d72d-4d97-83c9-58e801da3952"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Lorte sted!", new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), null, new Guid("229f7d4f-ffcc-437d-b3ab-82a0096f9c43") }
                });

            migrationBuilder.InsertData(
                table: "ReviewReports",
                columns: new[] { "Id", "CreatedOn", "Description", "ModifiedOn", "ProfileId", "ReviewId" },
                values: new object[] { new Guid("d2a85423-db7b-4fe9-a9b1-b7cf1eedb82b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Truende adfær", null, new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205"), new Guid("a67a1c3e-c487-4231-b5af-6da7bd11032f") });

            migrationBuilder.InsertData(
                table: "ReviewReports",
                columns: new[] { "Id", "CreatedOn", "Description", "ModifiedOn", "ProfileId", "ReviewId" },
                values: new object[] { new Guid("a34578de-f10f-4ae8-9648-7319cd1d8533"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Upassende", null, new Guid("62357886-d888-44f2-a929-c015a4b31dad"), new Guid("a67a1c3e-c487-4231-b5af-6da7bd11032f") });

            migrationBuilder.InsertData(
                table: "ReviewReports",
                columns: new[] { "Id", "CreatedOn", "Description", "ModifiedOn", "ProfileId", "ReviewId" },
                values: new object[] { new Guid("6f9a5b6b-a53b-4d97-8a74-06344b828aca"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dårlig kritik", null, new Guid("62357886-d888-44f2-a929-c015a4b31dad"), new Guid("4ede258a-d72d-4d97-83c9-58e801da3952") });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceReports_ExperienceId",
                table: "ExperienceReports",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceReports_ProfileId",
                table: "ExperienceReports",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CategoryId",
                table: "Experiences",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_City_Name",
                table: "Experiences",
                columns: new[] { "City", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ProfileId",
                table: "Experiences",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExps_ProfileId",
                table: "ListOfExps",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExpsExperiences_ExperienceId",
                table: "ListOfExpsExperiences",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfExpsExperiences_ListOfExpsId",
                table: "ListOfExpsExperiences",
                column: "ListOfExpsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ExperienceId",
                table: "Pictures",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProfileId",
                table: "Pictures",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ExperienceId",
                table: "Ratings",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProfileId",
                table: "Ratings",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReports_ProfileId",
                table: "ReviewReports",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReports_ReviewId",
                table: "ReviewReports",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ExperienceId",
                table: "Reviews",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProfileId",
                table: "Reviews",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceReports");

            migrationBuilder.DropTable(
                name: "ListOfExpsExperiences");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ReviewReports");

            migrationBuilder.DropTable(
                name: "ListOfExps");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
