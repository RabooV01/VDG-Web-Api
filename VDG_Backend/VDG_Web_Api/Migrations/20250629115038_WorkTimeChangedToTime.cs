using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class WorkTimeChangedToTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Start_WorkHours",
                table: "Clinic_WorkTime",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "End_WorkHours",
                table: "Clinic_WorkTime",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_WorkHours",
                table: "Clinic_WorkTime",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "End_WorkHours",
                table: "Clinic_WorkTime",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }
    }
}
