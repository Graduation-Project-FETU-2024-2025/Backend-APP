using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class profile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_Id",
                table: "Accounts");

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
                name: "FK_Doctor_AspNetUsers_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinic_Clinic_ClinicId",
                table: "DoctorClinic");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinic_Doctor_DoctorId",
                table: "DoctorClinic");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_UserId",
                table: "Orders");

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
                name: "FK_WorkingPeriodInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodInClinic");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPeriodInClinic",
                table: "WorkingPeriodInClinic");

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
                name: "WorkingPeriodInClinic",
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

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Doctor",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Gnder",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Doctor",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Accounts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Gnder",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Accounts",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts",
                columns: new[] { "Id", "PharmacyId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDates_Clinics_ClinicId",
                table: "AppointmentDates",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "AspNetUsers",
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
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
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
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clinics_ClinicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctor_DoctorId",
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
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

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

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts");

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

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Gnder",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Gnder",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "WorkingPeriodsInClinics",
                newName: "WorkingPeriodInClinic");

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
                name: "PK_WorkingPeriodInClinic",
                table: "WorkingPeriodInClinic",
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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts",
                columns: new[] { "Id", "PharmacyId" },
                unique: true,
                filter: "[PharmacyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_Id",
                table: "Accounts",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Doctor_AspNetUsers_Id",
                table: "Doctor",
                column: "Id",
                principalTable: "AspNetUsers",
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
                name: "FK_Orders_User_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "User",
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
                name: "FK_WorkingPeriodInClinic_AppointmentDates_AppointmentDateId",
                table: "WorkingPeriodInClinic",
                column: "AppointmentDateId",
                principalTable: "AppointmentDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
