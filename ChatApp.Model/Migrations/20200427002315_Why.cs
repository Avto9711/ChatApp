using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Model.Migrations
{
    public partial class Why : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatRoomCode",
                value: "905b4cbc-d1ed-4a85-bbbb-7e0d55e3ac49");

            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChatRoomCode",
                value: "df687b07-cf0d-4de2-9604-fa1ca07d0ac2");

            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChatRoomCode",
                value: "c14a3594-85b7-4b48-8ef3-4f5e2d7dab8c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatRoomCode",
                value: "238aab0d-db30-45d4-8fb3-71ba1ed334eb");

            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChatRoomCode",
                value: "2ae76df0-b06b-4979-bfcc-aa989297ca7e");

            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChatRoomCode",
                value: "b2c77bc5-054d-4fe4-a6e6-e58fbeb68ab3");
        }
    }
}
