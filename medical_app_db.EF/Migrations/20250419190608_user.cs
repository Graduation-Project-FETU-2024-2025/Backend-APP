using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medical_app_db.EF.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts");

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
                name: "FK_Appointments_User_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "User",
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
                name: "FK_Orders_User_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_Id",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_AspNetUsers_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_UserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts");

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

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id_PharmacyId",
                table: "Accounts",
                columns: new[] { "Id", "PharmacyId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
