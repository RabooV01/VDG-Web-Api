using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class PromotionRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SyndicateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespondBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionRequests_User_RespondBy",
                        column: x => x.RespondBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionRequests_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Birthdate", "First_Name", "Gender", "Last_Name", "Personal_Id", "Phone" },
                values: new object[] { 1, null, "Admin", null, null, null, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password_Hash", "Person_Id", "Role" },
                values: new object[] { 1, "admin@vdg.com", "AdminIsAdmin", 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_RespondBy",
                table: "PromotionRequests",
                column: "RespondBy");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_UserId",
                table: "PromotionRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionRequests");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
