using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4cbd3287-2056-4ee9-8ca6-b16438bb5280", "cbea5129-1d35-4fcc-a7f5-d0b5ec1f601c", "Manager", "MANAGER" },
                    { "7c74cfda-e81d-448f-9bbc-b16e4375ece2", "176145a8-f0af-4a3b-88f1-e72d60895460", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 4, 19, 7, 16, 912, DateTimeKind.Local).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 4, 19, 7, 16, 912, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 4, 19, 7, 16, 912, DateTimeKind.Local).AddTicks(4549));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cbd3287-2056-4ee9-8ca6-b16438bb5280");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c74cfda-e81d-448f-9bbc-b16e4375ece2");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 4, 19, 0, 41, 48, DateTimeKind.Local).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 4, 19, 0, 41, 48, DateTimeKind.Local).AddTicks(5646));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 4, 19, 0, 41, 48, DateTimeKind.Local).AddTicks(5664));
        }
    }
}
