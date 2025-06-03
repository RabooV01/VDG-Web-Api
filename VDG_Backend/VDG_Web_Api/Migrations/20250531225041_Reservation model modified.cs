using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class Reservationmodelmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Reservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Reservation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "Test",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Reservation",
                type: "time",
                nullable: true);
        }
    }
}
