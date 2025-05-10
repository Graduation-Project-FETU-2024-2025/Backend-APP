using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddingSpecializationNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Specializations",
                newName: "EnName");

            migrationBuilder.AddColumn<string>(
                name: "ArName",
                table: "Specializations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArName",
                table: "Specializations");

            migrationBuilder.RenameColumn(
                name: "EnName",
                table: "Specializations",
                newName: "Name");
        }
    }
}
