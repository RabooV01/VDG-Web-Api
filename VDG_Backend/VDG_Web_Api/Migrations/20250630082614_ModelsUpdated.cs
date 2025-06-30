using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Doctor_Speciality_FK",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "Doctor_User_FK",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "Post_Doctor_FK",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Speciality",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Avg_Wait",
                table: "Rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Avg_Service",
                table: "Rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Act",
                table: "Rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Post",
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

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Ticket_Status",
                table: "Doctor",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Ticket_Cost",
                table: "Doctor",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor",
                column: "User_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Doctor_Speciality_FK",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Doctor_User_FK",
                table: "Doctor",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Post_Doctor_FK",
                table: "Post",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Doctor_Speciality_FK",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "Doctor_User_FK",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "Post_Doctor_FK",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Speciality",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "Avg_Wait",
                table: "Rating",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Avg_Service",
                table: "Rating",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Act",
                table: "Rating",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Post",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Ticket_Status",
                table: "Doctor",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "Ticket_Cost",
                table: "Doctor",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "Doctor_Speciality_FK",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Speciality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Doctor_User_FK",
                table: "Doctor",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Post_Doctor_FK",
                table: "Post",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");
        }
    }
}
