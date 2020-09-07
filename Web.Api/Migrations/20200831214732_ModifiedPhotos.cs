using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Migrations
{
    public partial class ModifiedPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Photos",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f5c7200-144e-4a05-8337-1c7f6d5fb11d", "da59452b-3720-4cac-9750-f96f8f6696c2", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93d913ba-80fc-4071-bf02-30fb6cb400ab", "99e875e2-89f4-407e-886a-270a799bfce6", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6d2c0bb5-2278-42b3-bed3-1d1956660c03", "53df3bd1-c0ea-4f84-b9ba-4fb4290c5971", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostId",
                table: "Photos",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PostId",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f5c7200-144e-4a05-8337-1c7f6d5fb11d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d2c0bb5-2278-42b3-bed3-1d1956660c03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93d913ba-80fc-4071-bf02-30fb6cb400ab");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Photos");

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
        }
    }
}
