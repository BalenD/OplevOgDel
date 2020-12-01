using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OplevOgDel.Api.Migrations
{
    public partial class fixedpitures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: new Guid("b393e49a-4097-4629-b15f-75fd9edd99c1"));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: new Guid("052c9135-c787-4fc5-8e1d-64c20aeda9bc"),
                column: "Path",
                value: "TestImage2.jpg");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: new Guid("93e4f688-d9a0-4f8a-bc69-1d7f5a46101d"),
                column: "Path",
                value: "TestImage1.jpg");

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "CreatedOn", "ExperienceId", "ModifiedOn", "Path", "ProfileId" },
                values: new object[] { new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"), null, "TestImage2.jpg", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                column: "Password",
                value: "$2a$11$ySOrR/7rgZZQ/6VMkZMtpu4CDLEO4TSRSGowob58alhf18btmC46K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"),
                column: "Password",
                value: "$2a$11$zvMIuLHiNGlt2ui2MAiscOP9fnws7BnkGqTQ./bsap5ooI4VAn4t2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79539d93-2a2d-4600-abce-3f727e5cae7d"),
                column: "Password",
                value: "$2a$11$Sg1J1CNLikKUK3xQwlogVeAhFlCGcOHngy/nwisohw.oTzYvTgKaq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                column: "Password",
                value: "$2a$11$8Pav8Jik667DR.ffCMM1fuL5CG1HHBjKXSkgvJjKZztEoL45S5prO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: new Guid("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: new Guid("052c9135-c787-4fc5-8e1d-64c20aeda9bc"),
                column: "Path",
                value: "TestImage2");

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: new Guid("93e4f688-d9a0-4f8a-bc69-1d7f5a46101d"),
                column: "Path",
                value: "TestImage1");

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "CreatedOn", "ExperienceId", "ModifiedOn", "Path", "ProfileId" },
                values: new object[] { new Guid("b393e49a-4097-4629-b15f-75fd9edd99c1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bd345b81-462b-4ba9-999f-48ff44fad5e8"), null, "TestImage2", new Guid("9600bf95-bf37-4e6d-aeed-53d84a96a205") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                column: "Password",
                value: "$2a$11$EALfK9HGLIo8SUykUFLkserjS7ReiLYc2ZiU64YDxpguvtlYPL.C2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("53e881e9-1b7a-461f-a286-48476deb343d"),
                column: "Password",
                value: "$2a$11$QEnGaZeoTu0i2luTSedImejhWu0TMECaGNgFwMajKOwthuBxca4oe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79539d93-2a2d-4600-abce-3f727e5cae7d"),
                column: "Password",
                value: "$2a$11$9cWNeo6vb0hhPpZkVAKBk.WyR6dMqVwNJnBf36P0mgwMvMfvbZvGe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                column: "Password",
                value: "$2a$11$UiDH3OkVIhWCKWR2x8fEcOKn.nfz2WHcmwEUqoIFj7LsB3jWtzmUy");
        }
    }
}
