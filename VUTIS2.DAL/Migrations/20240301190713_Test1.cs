using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VUTIS2.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Activities_ActivityId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ActivityId",
                table: "Evaluations");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ActivityId",
                table: "Evaluations",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Activities_ActivityId",
                table: "Evaluations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Activities_ActivityId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ActivityId",
                table: "Evaluations");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ActivityId",
                table: "Evaluations",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Activities_ActivityId",
                table: "Evaluations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
