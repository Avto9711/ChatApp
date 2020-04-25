using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Model.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "ChatRoomCode", "ChatRoomName", "CreatedBy", "CreatedDate", "Deleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "f1fd1f5b-a1df-4257-82c9-c305f46546a2", "Room Chat 1", null, null, false, null, null });

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "ChatRoomCode", "ChatRoomName", "CreatedBy", "CreatedDate", "Deleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 2, "0b729dc1-2b84-49b7-ad90-4f15cbaf09e1", "Room Chat 2", null, null, false, null, null });

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "ChatRoomCode", "ChatRoomName", "CreatedBy", "CreatedDate", "Deleted", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 3, "c13705b9-0518-479f-8caf-66bb576a7cdc", "Room Chat 3", null, null, false, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
