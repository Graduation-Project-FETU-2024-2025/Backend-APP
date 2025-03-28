using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class addARandEn_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Branches",
                newName: "EN_Address");

            migrationBuilder.AddColumn<string>(
                name: "AR_Address",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AR_Address",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "EN_Address",
                table: "Branches",
                newName: "Address");
        }
    }
}
