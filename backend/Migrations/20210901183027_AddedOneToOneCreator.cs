using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddedOneToOneCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Creators_UserId",
                table: "Creators",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Creators_Users_UserId",
                table: "Creators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creators_Users_UserId",
                table: "Creators");

            migrationBuilder.DropIndex(
                name: "IX_Creators_UserId",
                table: "Creators");
        }
    }
}
