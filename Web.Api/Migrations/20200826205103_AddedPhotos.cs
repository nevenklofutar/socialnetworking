using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class AddedPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ec509101-8d30-4291-8235-48bf13c3fe83", "fc15405a-9aa8-4048-bd77-72461493baa3", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "92fe95cb-cdfb-467b-9384-675785fbdce6", "f331ab35-2900-42f5-8c91-f4f5fdd94213", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8b0c528-2dc7-463a-9f31-287a22a7760e", "e9cbc66a-3d00-4101-a24a-73c9075d87a5", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92fe95cb-cdfb-467b-9384-675785fbdce6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8b0c528-2dc7-463a-9f31-287a22a7760e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec509101-8d30-4291-8235-48bf13c3fe83");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a78d2140-b795-4252-8453-b82ea5cee846", "83198b28-d535-4250-9115-7d7c6a5cfb2f", "Manager", "MANAGER" },
                    { "39639db1-103d-429e-9a30-ae0d0377fedd", "8e10d53b-392b-4893-ab27-a9e257563665", "Administrator", "ADMINISTRATOR" },
                    { "5908c082-4654-4748-98c4-371a83e9dd3c", "113764c6-bab6-45b6-956f-fd45f6adb9fa", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "CreatedById", "CreatedOn", "Title" },
                values: new object[,]
                {
                    { 1, "Post body 1", null, new DateTime(2020, 7, 21, 19, 45, 41, 226, DateTimeKind.Local).AddTicks(5964), "Post Title 1" },
                    { 2, "Post body 2", null, new DateTime(2020, 7, 21, 19, 45, 41, 226, DateTimeKind.Local).AddTicks(6438), "Post Title 2" },
                    { 3, "Post body 3", null, new DateTime(2020, 7, 21, 19, 45, 41, 226, DateTimeKind.Local).AddTicks(6455), "Post Title 3" }
                });
        }
    }
}
