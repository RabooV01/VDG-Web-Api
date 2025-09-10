using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class supportAndUrlForPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Holidays",
                table: "Virtual_Clinic");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupportModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reviewed = table.Column<bool>(type: "bit", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportModels_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "RegisteredAt" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_SupportModels_UserId",
                table: "SupportModels",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportModels");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Holidays",
                table: "Virtual_Clinic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
