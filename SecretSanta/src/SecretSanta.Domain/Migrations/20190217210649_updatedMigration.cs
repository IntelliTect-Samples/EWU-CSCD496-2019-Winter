using Microsoft.EntityFrameworkCore.Migrations;

namespace SecretSanta.Domain.Migrations
{
    public partial class updatedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Pairings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pairings_GroupId",
                table: "Pairings",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairings_Groups_GroupId",
                table: "Pairings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairings_Groups_GroupId",
                table: "Pairings");

            migrationBuilder.DropIndex(
                name: "IX_Pairings_GroupId",
                table: "Pairings");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Pairings");
        }
    }
}
