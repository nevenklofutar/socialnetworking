using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class AddedCreatedByToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cbd3287-2056-4ee9-8ca6-b16438bb5280");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c74cfda-e81d-448f-9bbc-b16e4375ece2");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Posts",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "845bd6b9-1f4d-4c08-b3f1-a71068c8def8", "335ebfcf-7ed4-4992-9853-d522ab2143ed", "Manager", "MANAGER" },
                    { "d4bc6234-54f5-4aa6-8f05-ce8698cf21ba", "119f499c-15ae-4728-986a-f5646c8589df", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 10, 30, 29, 186, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 10, 30, 29, 186, DateTimeKind.Local).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 5, 10, 30, 29, 186, DateTimeKind.Local).AddTicks(5537));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_CreatedById",
                table: "Posts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // update data that is missing createdby user
            // this is actually wrong becasue we dont seed user, so if dont have any users, this wont work
            // this actually wont work on a fresh base because all migrations will execute without created users,
            // this only works as WIP
            //migrationBuilder.Sql(@"
            //    UPDATE Posts SET CreatedById = (SELECT TOP 1 Id FROM AspNetUsers) WHERE CreatedById is null
            //");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_CreatedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "845bd6b9-1f4d-4c08-b3f1-a71068c8def8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4bc6234-54f5-4aa6-8f05-ce8698cf21ba");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Posts");

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
    }
}
