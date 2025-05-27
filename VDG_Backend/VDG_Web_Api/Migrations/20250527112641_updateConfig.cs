using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class updateConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Speciality_SpecialityId1",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_User_UserId1",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Doctor_DoctorSyndicateId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserId1",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_User_Person_Id",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Rating_DoctorSyndicateId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_UserId1",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_SpecialityId1",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_UserId1",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DoctorSyndicateId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "SpecialityId1",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Doctor");

            migrationBuilder.CreateIndex(
                name: "IX_User_Person_Id",
                table: "User",
                column: "Person_Id",
                unique: true,
                filter: "[Person_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Person_Id",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "DoctorSyndicateId",
                table: "Rating",
                type: "varchar(16)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId1",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Person_Id",
                table: "User",
                column: "Person_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_DoctorSyndicateId",
                table: "Rating",
                column: "DoctorSyndicateId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId1",
                table: "Rating",
                column: "UserId1");

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
                name: "IX_Doctor_UserId1",
                table: "Doctor",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Speciality_SpecialityId1",
                table: "Doctor",
                column: "SpecialityId1",
                principalTable: "Speciality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_User_UserId1",
                table: "Doctor",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Doctor_DoctorSyndicateId",
                table: "Rating",
                column: "DoctorSyndicateId",
                principalTable: "Doctor",
                principalColumn: "Syndicate_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserId1",
                table: "Rating",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
