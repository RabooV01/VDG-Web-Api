using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class startupConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Personal_Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Phone = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Person__3214EC07A0A03B3C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specialty = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Speciali__3214EC074808F671", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Person_Id = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Password_Hash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Role = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3214EC071AAE95BF", x => x.Id);
                    table.ForeignKey(
                        name: "User_Person_FK",
                        column: x => x.Person_Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Syndicate_Id = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Speciality_Id = table.Column<int>(type: "int", nullable: true),
                    SpecialityId1 = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor__B5BD6B27701103BA", x => x.Syndicate_Id);
                    table.ForeignKey(
                        name: "Doctor_Speciality_FK",
                        column: x => x.Speciality_Id,
                        principalTable: "Speciality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Doctor_User_FK",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctor_Speciality_SpecialityId1",
                        column: x => x.SpecialityId1,
                        principalTable: "Speciality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctor_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Id = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Post__3214EC07FAD031E1", x => x.Id);
                    table.ForeignKey(
                        name: "Post_Doctor_FK",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "Syndicate_Id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Doctor_Id = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Avg_Wait = table.Column<double>(type: "float", nullable: true),
                    Avg_Service = table.Column<double>(type: "float", nullable: true),
                    Act = table.Column<double>(type: "float", nullable: true),
                    DoctorSyndicateId = table.Column<string>(type: "varchar(16)", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rating__3214EC07B32B9B16", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Doctor_DoctorSyndicateId",
                        column: x => x.DoctorSyndicateId,
                        principalTable: "Doctor",
                        principalColumn: "Syndicate_Id");
                    table.ForeignKey(
                        name: "FK_Rating_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Rating_Doctor_FK",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "Syndicate_Id");
                    table.ForeignKey(
                        name: "Rating_User_FK",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Doctor_Id = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Status = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Close_Date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket__3214EC076F6CA0F8", x => x.Id);
                    table.ForeignKey(
                        name: "Ticket_Doctor_FK",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "Syndicate_Id");
                    table.ForeignKey(
                        name: "Ticket_User_FK",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Virtual_Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Id = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Start_Work_Hours = table.Column<TimeOnly>(type: "time", nullable: true),
                    End_Work_Hours = table.Column<TimeOnly>(type: "time", nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Avg_Service = table.Column<double>(type: "float", nullable: true),
                    Ticket_Status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Preview_Const = table.Column<double>(type: "float", nullable: true),
                    Ticket_Const = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Virtual___3214EC078277E8B9", x => x.Id);
                    table.ForeignKey(
                        name: "Clinic_Doctor_FK",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "Syndicate_Id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket_Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Id = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Owner = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket_M__3214EC079464AE2E", x => x.Id);
                    table.ForeignKey(
                        name: "Message_Ticket_FK",
                        column: x => x.Ticket_Id,
                        principalTable: "Ticket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Vritual_Id = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Test = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__3214EC07765A5547", x => x.Id);
                    table.ForeignKey(
                        name: "Reservation_User_FK",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Reservation_Virtual_FK",
                        column: x => x.Vritual_Id,
                        principalTable: "Virtual_Clinic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                unique: true,
                filter: "[Speciality_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_SpecialityId1",
                table: "Doctor",
                column: "SpecialityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId1",
                table: "Doctor",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Doctor_Id",
                table: "Post",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_Doctor_Id",
                table: "Rating",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_DoctorSyndicateId",
                table: "Rating",
                column: "DoctorSyndicateId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_User_Id",
                table: "Rating",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId1",
                table: "Rating",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_User_Id",
                table: "Reservation",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Vritual_Id",
                table: "Reservation",
                column: "Vritual_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Doctor_Id",
                table: "Ticket",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_User_Id",
                table: "Ticket",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Message_Ticket_Id",
                table: "Ticket_Message",
                column: "Ticket_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Person_Id",
                table: "User",
                column: "Person_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Virtual_Clinic_Doctor_Id",
                table: "Virtual_Clinic",
                column: "Doctor_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Ticket_Message");

            migrationBuilder.DropTable(
                name: "Virtual_Clinic");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
