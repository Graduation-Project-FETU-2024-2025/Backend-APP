using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class updatePrescriptionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Prescriptions",
                newName: "NextAppointment");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "NextAppointment",
                table: "Prescriptions",
                newName: "Notes");
        }
    }
}
