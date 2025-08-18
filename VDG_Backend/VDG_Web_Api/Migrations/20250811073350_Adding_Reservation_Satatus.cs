using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Reservation_Satatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Person_Id",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Speciality",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Person_Id",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_Person_Id",
                table: "User",
                column: "Person_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Person_Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Speciality",
                newName: "name");

            migrationBuilder.AlterColumn<int>(
                name: "Person_Id",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_User_Person_Id",
                table: "User",
                column: "Person_Id",
                unique: true,
                filter: "[Person_Id] IS NOT NULL");
        }
    }
}
