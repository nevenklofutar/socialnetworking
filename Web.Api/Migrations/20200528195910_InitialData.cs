using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "CreatedOn", "Title" },
                values: new object[] { 1, "Post body 1", new DateTime(2020, 5, 28, 21, 59, 10, 482, DateTimeKind.Local).AddTicks(6919), "Post Title 1" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "CreatedOn", "Title" },
                values: new object[] { 2, "Post body 2", new DateTime(2020, 5, 28, 21, 59, 10, 482, DateTimeKind.Local).AddTicks(7416), "Post Title 2" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "CreatedOn", "Title" },
                values: new object[] { 3, "Post body 3", new DateTime(2020, 5, 28, 21, 59, 10, 482, DateTimeKind.Local).AddTicks(7435), "Post Title 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
