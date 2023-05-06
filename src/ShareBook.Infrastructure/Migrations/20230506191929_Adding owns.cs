using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addingowns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanRequests",
                table: "LoanRequests");

            migrationBuilder.DropIndex(
                name: "IX_LoanRequests_BookId",
                table: "LoanRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanRequests",
                table: "LoanRequests",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanRequests",
                table: "LoanRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanRequests",
                table: "LoanRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequests_BookId",
                table: "LoanRequests",
                column: "BookId",
                unique: true);
        }
    }
}
