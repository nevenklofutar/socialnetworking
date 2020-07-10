using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class AddedLikerIdInLikeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a5562c4-eb88-4063-a7ea-21e8d20c6407");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97f3b61f-66ee-4931-835b-194eaf43aec6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "de78951e-e1e4-402f-aa1d-7e2826f7c7a2", "be32f971-ccb3-47b0-890d-b77769599437", "Manager", "MANAGER" },
                    { "7fb94c23-58dd-4b61-a5b4-f8adc4b701db", "d78577bf-7a55-4c67-bcc9-0c8e7a85ae77", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 19, 13, 9, 93, DateTimeKind.Local).AddTicks(9754));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 19, 13, 9, 94, DateTimeKind.Local).AddTicks(229));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 19, 13, 9, 94, DateTimeKind.Local).AddTicks(250));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fb94c23-58dd-4b61-a5b4-f8adc4b701db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de78951e-e1e4-402f-aa1d-7e2826f7c7a2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "97f3b61f-66ee-4931-835b-194eaf43aec6", "a86ef030-644a-4566-a13a-ca3d482b8367", "Manager", "MANAGER" },
                    { "8a5562c4-eb88-4063-a7ea-21e8d20c6407", "17a8d25e-2f3d-4c48-8ed5-ff42859e262a", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 18, 43, 7, 264, DateTimeKind.Local).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 18, 43, 7, 264, DateTimeKind.Local).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 18, 43, 7, 264, DateTimeKind.Local).AddTicks(7761));
        }
    }
}
