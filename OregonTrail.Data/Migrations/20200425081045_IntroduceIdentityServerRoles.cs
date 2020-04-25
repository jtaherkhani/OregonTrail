using Microsoft.EntityFrameworkCore.Migrations;

namespace OregonTrail.Data.Migrations
{
    public partial class IntroduceIdentityServerRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14a7267e-2237-4698-a4fe-89b559ea6fd5", "5bddbd62-e3cc-4601-9993-37e0bae54a7a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f50fc07a-bd48-4223-9261-42f4f6952ad8", "c2f6a6ac-d28e-4d49-946e-00f0f0967ddb", "GameMaster", "GAMEMASTER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa9296f3-cb1b-4bbb-96eb-01bd4a7386cd", "cc55113a-e3fa-4949-8acc-a08395822cae", "Player", "PLAYER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14a7267e-2237-4698-a4fe-89b559ea6fd5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f50fc07a-bd48-4223-9261-42f4f6952ad8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa9296f3-cb1b-4bbb-96eb-01bd4a7386cd");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Image", "Name", "Points" },
                values: new object[] { 1, "DummyImage.jpg", "dummy", 10 });
        }
    }
}
