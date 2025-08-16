using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VDG_Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddingSpecialityToPromotionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "PromotionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_SpecialityId",
                table: "PromotionRequests",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionRequests_Speciality_SpecialityId",
                table: "PromotionRequests",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionRequests_Speciality_SpecialityId",
                table: "PromotionRequests");

            migrationBuilder.DropIndex(
                name: "IX_PromotionRequests_SpecialityId",
                table: "PromotionRequests");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "PromotionRequests");
        }
    }
}
