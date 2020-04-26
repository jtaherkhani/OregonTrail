using Microsoft.EntityFrameworkCore.Migrations;

namespace OregonTrail.Data.Migrations
{
    public partial class SeedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15391dfd-c5ca-416d-8737-ff074d651a57", "942ba0c6-f86c-4742-814e-bd6581838ede", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e21c35aa-383d-4a48-9192-b5f707c46a28", "c696ccf1-6898-4b82-9105-b498539505d5", "GameMaster", "GAMEMASTER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50a35fb3-fdff-42ce-9c77-fc7b1564195f", "36abb9fc-d21c-4ba2-bdcd-fd26c7d69f0e", "Player", "PLAYER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15391dfd-c5ca-416d-8737-ff074d651a57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50a35fb3-fdff-42ce-9c77-fc7b1564195f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e21c35aa-383d-4a48-9192-b5f707c46a28");

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
    }
}
