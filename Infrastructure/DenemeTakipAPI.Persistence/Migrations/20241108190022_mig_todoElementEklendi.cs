using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenemeTakipAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_todoElementEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ToDoElementTitle = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDoDate = table.Column<DateOnly>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoElements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "128f0e53-f259-411a-b4be-e050e48c199e",
                column: "ConcurrencyStamp",
                value: "d21486eb-a7ca-4827-b78e-498ec1dac644");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a55c5f9f-4f8c-4848-882f-0bcb3ec62171",
                column: "ConcurrencyStamp",
                value: "468d7d30-af1a-4c79-b9dd-d6645bcbfb70");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5bc8bb5-0f4f-452a-911c-9844f7e2aac7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3850d800-d902-4870-bc6b-a2d19d5c5569", "AQAAAAIAAYagAAAAEI4KgdXx6No6S7CgPkBWD6AiD8L1LfglLV7T+mqkfK7kvDaxbHxg7EY3mCrVHYR+zw==", "4a5ac45f-a067-4bd4-a871-a586437ae576" });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoElements_UserId",
                table: "ToDoElements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoElements");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "128f0e53-f259-411a-b4be-e050e48c199e",
                column: "ConcurrencyStamp",
                value: "90916599-5e1c-4029-95b6-2795de639a1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a55c5f9f-4f8c-4848-882f-0bcb3ec62171",
                column: "ConcurrencyStamp",
                value: "7531cfe8-d4bf-46f2-a2f7-9ab0237dec0b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5bc8bb5-0f4f-452a-911c-9844f7e2aac7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7053488-c18f-415f-89f7-35df3524784b", "AQAAAAIAAYagAAAAEIl8Wza3qjQKShoK5z1mJA3HHB+LtoGiYifABBupUgEByQCT9uxCoDfXHY63D/8N8g==", "013c44e9-7da5-4853-be15-64b1918ab684" });
        }
    }
}
