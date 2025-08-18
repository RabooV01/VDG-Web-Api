using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class ClinicHolidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Holidays",
                table: "Virtual_Clinic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationCoords",
                table: "Virtual_Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Virtual_Clinic",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Holidays",
                table: "Virtual_Clinic");

            migrationBuilder.DropColumn(
                name: "LocationCoords",
                table: "Virtual_Clinic");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Virtual_Clinic");
        }
    }
}
