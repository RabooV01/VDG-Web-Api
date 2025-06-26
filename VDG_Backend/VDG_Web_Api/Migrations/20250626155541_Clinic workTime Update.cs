using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class ClinicworkTimeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Post_Doctor_FK",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "Rating_Doctor_FK",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "Ticket_Doctor_FK",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "Clinic_Doctor_FK",
                table: "Virtual_Clinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Doctor__B5BD6B27701103BA",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "End_Work_Hours",
                table: "Virtual_Clinic");

            migrationBuilder.DropColumn(
                name: "Preview_Const",
                table: "Virtual_Clinic");

            migrationBuilder.DropColumn(
                name: "Start_Work_Hours",
                table: "Virtual_Clinic");

            migrationBuilder.DropColumn(
                name: "Ticket_Status",
                table: "Virtual_Clinic");

            migrationBuilder.RenameColumn(
                name: "Ticket_Const",
                table: "Virtual_Clinic",
                newName: "Preview_Cost");

            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Speciality",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Virtual_Clinic",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Virtual_Clinic",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Ticket_Message",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Ticket",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Rating",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Post",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "Ticket_Cost",
                table: "Doctor",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ticket_Status",
                table: "Doctor",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Doctor__B5BD6B27701103BA",
                table: "Doctor",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clinic_WorkTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clinic_Id = table.Column<int>(type: "int", nullable: false),
                    Start_WorkHours = table.Column<TimeOnly>(type: "time", nullable: true),
                    End_WorkHours = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WorkTime", x => x.Id);
                    table.ForeignKey(
                        name: "Clinic_WorkTime_FK",
                        column: x => x.Clinic_Id,
                        principalTable: "Virtual_Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_WorkTime_Clinic_Id",
                table: "Clinic_WorkTime",
                column: "Clinic_Id");

            migrationBuilder.AddForeignKey(
                name: "Post_Doctor_FK",
                table: "Post",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Rating_Doctor_FK",
                table: "Rating",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Ticket_Doctor_FK",
                table: "Ticket",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Clinic_Doctor_FK",
                table: "Virtual_Clinic",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Post_Doctor_FK",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "Rating_Doctor_FK",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "Ticket_Doctor_FK",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "Clinic_Doctor_FK",
                table: "Virtual_Clinic");

            migrationBuilder.DropTable(
                name: "Clinic_WorkTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Doctor__B5BD6B27701103BA",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Ticket_Cost",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Ticket_Status",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "Preview_Cost",
                table: "Virtual_Clinic",
                newName: "Ticket_Const");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Speciality",
                newName: "Specialty");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Virtual_Clinic",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Id",
                table: "Virtual_Clinic",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "End_Work_Hours",
                table: "Virtual_Clinic",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Preview_Const",
                table: "Virtual_Clinic",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Start_Work_Hours",
                table: "Virtual_Clinic",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ticket_Status",
                table: "Virtual_Clinic",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Ticket_Message",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Id",
                table: "Ticket",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Id",
                table: "Rating",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Id",
                table: "Post",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Doctor__B5BD6B27701103BA",
                table: "Doctor",
                column: "Syndicate_Id");

            migrationBuilder.AddForeignKey(
                name: "Post_Doctor_FK",
                table: "Post",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Syndicate_Id");

            migrationBuilder.AddForeignKey(
                name: "Rating_Doctor_FK",
                table: "Rating",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Syndicate_Id");

            migrationBuilder.AddForeignKey(
                name: "Ticket_Doctor_FK",
                table: "Ticket",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Syndicate_Id");

            migrationBuilder.AddForeignKey(
                name: "Clinic_Doctor_FK",
                table: "Virtual_Clinic",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Syndicate_Id");
        }
    }
}
