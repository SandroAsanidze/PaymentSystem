using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystem.Migrations
{
    /// <inheritdoc />
    public partial class removePaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
