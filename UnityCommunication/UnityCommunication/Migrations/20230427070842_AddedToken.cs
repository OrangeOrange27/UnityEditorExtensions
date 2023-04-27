using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnityCommunication.Migrations
{
    /// <inheritdoc />
    public partial class AddedToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "messageModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "messageModels");
        }
    }
}
