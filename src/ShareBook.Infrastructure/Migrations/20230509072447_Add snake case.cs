using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addsnakecase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pK_books",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "sharedByOwner",
                table: "books",
                newName: "shared_by_owner");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "books",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_Status",
                table: "books",
                newName: "current_loan_request_status");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_RequestingUser",
                table: "books",
                newName: "current_loan_request_requesting_user");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_Id",
                table: "books",
                newName: "current_loan_request_id");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_CreatedAt",
                table: "books",
                newName: "current_loan_request_created_at");

            migrationBuilder.AddPrimaryKey(
                name: "pk_books",
                table: "books",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_books",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "shared_by_owner",
                table: "books",
                newName: "sharedByOwner");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "books",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "current_loan_request_status",
                table: "books",
                newName: "currentLoanRequest_Status");

            migrationBuilder.RenameColumn(
                name: "current_loan_request_requesting_user",
                table: "books",
                newName: "currentLoanRequest_RequestingUser");

            migrationBuilder.RenameColumn(
                name: "current_loan_request_id",
                table: "books",
                newName: "currentLoanRequest_Id");

            migrationBuilder.RenameColumn(
                name: "current_loan_request_created_at",
                table: "books",
                newName: "currentLoanRequest_CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "pK_books",
                table: "books",
                column: "id");
        }
    }
}
