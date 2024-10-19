using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystem.Migrations
{
    /// <inheritdoc />
    public partial class addfieldURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Merchant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Merchant");
        }
    }
}
