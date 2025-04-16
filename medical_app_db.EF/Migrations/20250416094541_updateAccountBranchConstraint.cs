using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateAccountBranchConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBranches_Branches_BranchId",
                table: "AccountBranches");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBranches_Branches_BranchId",
                table: "AccountBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBranches_Branches_BranchId",
                table: "AccountBranches");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBranches_Branches_BranchId",
                table: "AccountBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }
    }
}
