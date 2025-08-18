using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorLevelOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Message_Ticket_Ticket_Id",
                table: "Ticket_Message");

            migrationBuilder.AlterColumn<int>(
                name: "Ticket_Id",
                table: "Ticket_Message",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Ticket_Message",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Ticket_Message",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Ticket",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Ticket",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Close_Date",
                table: "Ticket",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Last_Name",
                table: "Person",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Message_Ticket_Ticket_Id",
                table: "Ticket_Message",
                column: "Ticket_Id",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Message_Ticket_Ticket_Id",
                table: "Ticket_Message");

            migrationBuilder.AlterColumn<int>(
                name: "Ticket_Id",
                table: "Ticket_Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Ticket_Message",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Ticket_Message",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Ticket",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Ticket",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Close_Date",
                table: "Ticket",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Last_Name",
                table: "Person",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Message_Ticket_Ticket_Id",
                table: "Ticket_Message",
                column: "Ticket_Id",
                principalTable: "Ticket",
                principalColumn: "Id");
        }
    }
}
