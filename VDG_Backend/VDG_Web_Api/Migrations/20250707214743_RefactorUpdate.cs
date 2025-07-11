using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Clinic_WorkTime_FK",
                table: "Clinic_WorkTime");

            migrationBuilder.DropForeignKey(
                name: "Doctor_Speciality_FK",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "Post_Doctor_FK",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "Rating_Doctor_FK",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "Rating_User_FK",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "Reservation_User_FK",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "Reservation_Virtual_FK",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "Ticket_Doctor_FK",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "Ticket_User_FK",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "Message_Ticket_FK",
                table: "Ticket_Message");

            migrationBuilder.DropForeignKey(
                name: "Clinic_Doctor_FK",
                table: "Virtual_Clinic");

            migrationBuilder.AlterColumn<double>(
                name: "Preview_Cost",
                table: "Virtual_Clinic",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "User",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldUnicode: false,
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Virtual_Id",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Doctor",
                type: "varchar(1024)",
                unicode: false,
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_WorkTime_Virtual_Clinic_Clinic_Id",
                table: "Clinic_WorkTime",
                column: "Clinic_Id",
                principalTable: "Virtual_Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Speciality_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Doctor_Doctor_Id",
                table: "Post",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Doctor_Doctor_Id",
                table: "Rating",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_User_Id",
                table: "Rating",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_User_Id",
                table: "Reservation",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Virtual_Clinic_Virtual_Id",
                table: "Reservation",
                column: "Virtual_Id",
                principalTable: "Virtual_Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Doctor_Doctor_Id",
                table: "Ticket",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_User_Id",
                table: "Ticket",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Message_Ticket_Ticket_Id",
                table: "Ticket_Message",
                column: "Ticket_Id",
                principalTable: "Ticket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Virtual_Clinic_Doctor_Doctor_Id",
                table: "Virtual_Clinic",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_WorkTime_Virtual_Clinic_Clinic_Id",
                table: "Clinic_WorkTime");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Speciality_Speciality_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Doctor_Doctor_Id",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Doctor_Doctor_Id",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_User_Id",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_User_Id",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Virtual_Clinic_Virtual_Id",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Doctor_Doctor_Id",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_User_Id",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Message_Ticket_Ticket_Id",
                table: "Ticket_Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Virtual_Clinic_Doctor_Doctor_Id",
                table: "Virtual_Clinic");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Doctor");

            migrationBuilder.AlterColumn<double>(
                name: "Preview_Cost",
                table: "Virtual_Clinic",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Virtual_Clinic",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Virtual_Clinic",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "User",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldUnicode: false,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<int>(
                name: "Virtual_Id",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "Clinic_WorkTime_FK",
                table: "Clinic_WorkTime",
                column: "Clinic_Id",
                principalTable: "Virtual_Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Doctor_Speciality_FK",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Post_Doctor_FK",
                table: "Post",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Rating_Doctor_FK",
                table: "Rating",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Rating_User_FK",
                table: "Rating",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Reservation_User_FK",
                table: "Reservation",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Reservation_Virtual_FK",
                table: "Reservation",
                column: "Virtual_Id",
                principalTable: "Virtual_Clinic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Ticket_Doctor_FK",
                table: "Ticket",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Ticket_User_FK",
                table: "Ticket",
                column: "User_Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Message_Ticket_FK",
                table: "Ticket_Message",
                column: "Ticket_Id",
                principalTable: "Ticket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Clinic_Doctor_FK",
                table: "Virtual_Clinic",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id");
        }
    }
}
