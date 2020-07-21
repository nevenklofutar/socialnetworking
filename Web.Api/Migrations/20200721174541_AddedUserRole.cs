using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class AddedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d55a6cb-c4bb-44e0-b323-24646579a9eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f39ba47c-5a64-404d-8d68-522c91b73442");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a78d2140-b795-4252-8453-b82ea5cee846", "83198b28-d535-4250-9115-7d7c6a5cfb2f", "Manager", "MANAGER" },
                    { "39639db1-103d-429e-9a30-ae0d0377fedd", "8e10d53b-392b-4893-ab27-a9e257563665", "Administrator", "ADMINISTRATOR" },
                    { "5908c082-4654-4748-98c4-371a83e9dd3c", "113764c6-bab6-45b6-956f-fd45f6adb9fa", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 21, 19, 45, 41, 226, DateTimeKind.Local).AddTicks(5964));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 21, 19, 45, 41, 226, DateTimeKind.Local).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 21, 19, 45, 41, 226, DateTimeKind.Local).AddTicks(6455));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39639db1-103d-429e-9a30-ae0d0377fedd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5908c082-4654-4748-98c4-371a83e9dd3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a78d2140-b795-4252-8453-b82ea5cee846");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f39ba47c-5a64-404d-8d68-522c91b73442", "e4b8fec9-405d-4f5e-b690-f6bfe23ecba0", "Manager", "MANAGER" },
                    { "1d55a6cb-c4bb-44e0-b323-24646579a9eb", "8e008117-30fa-43d7-9901-7e5400127159", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 19, 31, 4, 739, DateTimeKind.Local).AddTicks(5871));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 19, 31, 4, 739, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 19, 31, 4, 739, DateTimeKind.Local).AddTicks(6451));
        }
    }
}
