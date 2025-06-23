using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddComplaintToApplointemt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.AddColumn<string>(
                name: "Complaint",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Complaint",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId");
        }
    }
}
