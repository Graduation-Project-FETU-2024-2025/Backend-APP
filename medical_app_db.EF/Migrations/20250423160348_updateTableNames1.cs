using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class updateTableNames1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Clinic_ClinicId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_DoctorId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_User_UserId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDates_Clinic_ClinicId",
                table: "AppointmentDates");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicPhone_Clinic_C_ID",
                table: "ClinicPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinic_Clinic_ClinicId",
                table: "DoctorClinic");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinic_Doctor_DoctorId",
                table: "DoctorClinic");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Appointment_AppointmentId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_DoctorId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionProduct_Prescription_PrescriptionId",
                table: "PrescriptionProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionProduct_SystemProducts_SystemProductCode",
                table: "PrescriptionProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodsInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodsInClinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodsInClinic",
                table: "WorkingPeriodsInClinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionProduct",
                table: "PrescriptionProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorClinic",
                table: "DoctorClinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicPhone",
                table: "ClinicPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clinic",
                table: "Clinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodsInClinic",
                newName: "WorkingPeriodsInClinics");

            migrationBuilder.RenameTable(
                name: "PrescriptionProduct",
                newName: "PrescriptionProducts");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "DoctorClinic",
                newName: "DoctorClinics");

            migrationBuilder.RenameTable(
                name: "ClinicPhone",
                newName: "ClinicPhones");

            migrationBuilder.RenameTable(
                name: "Clinic",
                newName: "Clinics");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionProduct_SystemProductCode",
                table: "PrescriptionProducts",
                newName: "IX_PrescriptionProducts_SystemProductCode");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_DoctorId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_AppointmentId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinic_DoctorId",
                table: "DoctorClinics",
                newName: "IX_DoctorClinics_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinic_ClinicId",
                table: "DoctorClinics",
                newName: "IX_DoctorClinics_ClinicId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_UserId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ClinicId",
                table: "Appointments",
                newName: "IX_Appointments_ClinicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodsInClinics",
                table: "WorkingPeriodsInClinics",
                columns: new[] { "AppointmentDateId", "StartTime", "EndTime" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionProducts",
                table: "PrescriptionProducts",
                columns: new[] { "PrescriptionId", "SystemProductCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics",
                columns: new[] { "ClinicId", "DoctorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicPhones",
                table: "ClinicPhones",
                columns: new[] { "C_ID", "PhoneNumber" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDates_Clinics_ClinicId",
                table: "AppointmentDates",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clinics_ClinicId",
                table: "Appointments",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctor_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_User_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicPhones_Clinics_C_ID",
                table: "ClinicPhones",
                column: "C_ID",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinics_Clinics_ClinicId",
                table: "DoctorClinics",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinics_Doctor_DoctorId",
                table: "DoctorClinics",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionProducts_Prescriptions_PrescriptionId",
                table: "PrescriptionProducts",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionProducts_SystemProducts_SystemProductCode",
                table: "PrescriptionProducts",
                column: "SystemProductCode",
                principalTable: "SystemProducts",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctor_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodsInClinics_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodsInClinics",
                column: "AppointmentDateId",
                principalTable: "AppointmentDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDates_Clinics_ClinicId",
                table: "AppointmentDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clinics_ClinicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctor_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicPhones_Clinics_C_ID",
                table: "ClinicPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinics_Clinics_ClinicId",
                table: "DoctorClinics");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinics_Doctor_DoctorId",
                table: "DoctorClinics");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionProducts_Prescriptions_PrescriptionId",
                table: "PrescriptionProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionProducts_SystemProducts_SystemProductCode",
                table: "PrescriptionProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctor_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriodsInClinics_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodsInClinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodsInClinics",
                table: "WorkingPeriodsInClinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionProducts",
                table: "PrescriptionProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicPhones",
                table: "ClinicPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodsInClinics",
                newName: "WorkingPeriodsInClinic");

            migrationBuilder.RenameTable(
                name: "Prescriptions",
                newName: "Prescription");

            migrationBuilder.RenameTable(
                name: "PrescriptionProducts",
                newName: "PrescriptionProduct");

            migrationBuilder.RenameTable(
                name: "DoctorClinics",
                newName: "DoctorClinic");

            migrationBuilder.RenameTable(
                name: "Clinics",
                newName: "Clinic");

            migrationBuilder.RenameTable(
                name: "ClinicPhones",
                newName: "ClinicPhone");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescription",
                newName: "IX_Prescription_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescription",
                newName: "IX_Prescription_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionProducts_SystemProductCode",
                table: "PrescriptionProduct",
                newName: "IX_PrescriptionProduct_SystemProductCode");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinics_DoctorId",
                table: "DoctorClinic",
                newName: "IX_DoctorClinic_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinics_ClinicId",
                table: "DoctorClinic",
                newName: "IX_DoctorClinic_ClinicId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointment",
                newName: "IX_Appointment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointment",
                newName: "IX_Appointment_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ClinicId",
                table: "Appointment",
                newName: "IX_Appointment_ClinicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPeriodsInClinic",
                table: "WorkingPeriodsInClinic",
                columns: new[] { "AppointmentDateId", "StartTime", "EndTime" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionProduct",
                table: "PrescriptionProduct",
                columns: new[] { "PrescriptionId", "SystemProductCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorClinic",
                table: "DoctorClinic",
                columns: new[] { "ClinicId", "DoctorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clinic",
                table: "Clinic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicPhone",
                table: "ClinicPhone",
                columns: new[] { "C_ID", "PhoneNumber" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Clinic_ClinicId",
                table: "Appointment",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_DoctorId",
                table: "Appointment",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_User_UserId",
                table: "Appointment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDates_Clinic_ClinicId",
                table: "AppointmentDates",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicPhone_Clinic_C_ID",
                table: "ClinicPhone",
                column: "C_ID",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinic_Clinic_ClinicId",
                table: "DoctorClinic",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinic_Doctor_DoctorId",
                table: "DoctorClinic",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Appointment_AppointmentId",
                table: "Prescription",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_DoctorId",
                table: "Prescription",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionProduct_Prescription_PrescriptionId",
                table: "PrescriptionProduct",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionProduct_SystemProducts_SystemProductCode",
                table: "PrescriptionProduct",
                column: "SystemProductCode",
                principalTable: "SystemProducts",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriodsInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodsInClinic",
                column: "AppointmentDateId",
                principalTable: "AppointmentDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
