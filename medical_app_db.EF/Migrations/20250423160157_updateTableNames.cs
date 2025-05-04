using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodInClinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodInClinic",
                table: "WorkingPeriodInClinic");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodInClinic",
                newName: "WorkingPeriodsInClinic");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodsInClinic",
                table: "WorkingPeriodsInClinic",
                columns: new[] { "AppointmentDateId", "StartTime", "EndTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodsInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodsInClinic",
                column: "AppointmentDateId",
                principalTable: "AppointmentDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodsInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodsInClinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodsInClinic",
                table: "WorkingPeriodsInClinic");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodsInClinic",
                newName: "WorkingPeriodInClinic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodInClinic",
                table: "WorkingPeriodInClinic",
                columns: new[] { "AppointmentDateId", "StartTime", "EndTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodInClinic",
                column: "AppointmentDateId",
                principalTable: "AppointmentDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
