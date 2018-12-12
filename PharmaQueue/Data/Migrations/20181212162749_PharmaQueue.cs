using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmaQueue.Data.Migrations
{
    public partial class PharmaQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QueueStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserRole = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Strength = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Refills = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PrescriptionStatusId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescription_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "QueueStatus" },
                values: new object[,]
                {
                    { 1, "Entered" },
                    { 2, "Reviewed" },
                    { 3, "Filled" },
                    { 4, "Ready" }
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "UserTypeId", "UserRole" },
                values: new object[,]
                {
                    { 1, "Employee" },
                    { 2, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserTypeId" },
                values: new object[] { "827cafad-2f71-44db-91a9-a23c1be0f038", 0, "a506997c-8738-4805-8da2-3d3691fbb265", "admin@admin.com", true, "admin", "admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEKIG87XSUBs5b9S9ePcG/t+7nxqCyhxj6WGwmqJuP1UahXOvDjX8GvrOL4dYPUmyrQ==", null, false, "bc9a04b2-fd77-4cc1-b58c-18e31031fff0", false, "admin@admin.com", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserTypeId" },
                values: new object[] { "b4cb0919-77b8-48ad-8603-166c33fde263", 0, "353aa89a-d979-457c-8669-2ef4c0df2866", "CUSTOMER@CUSTOEMR.com", true, "customer", "customer", false, null, "CUSTOMER@CUSTOMER.COM", "CUSTOMER@CUSTOMER.COM", "AQAAAAEAACcQAAAAEKtG1AexAtrL4yQnxmm+LADKEt7WZvEfJctx5yU+5ZQMjVnnvXcSOWZWPYGMvWdrmg==", null, false, "3b796eaf-9b4b-4573-8e03-3295a0849cdf", false, "customer@customer.com", 2 });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "DateCreated", "IsSold", "Name", "PrescriptionStatusId", "Price", "Quantity", "Refills", "StatusId", "Strength", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Metformin", 1, 5.0, 120, 4, null, "500mg", "b4cb0919-77b8-48ad-8603-166c33fde263" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Glipizide", 1, 30.0, 30, 12, null, "2.5mg", "b4cb0919-77b8-48ad-8603-166c33fde263" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Benzonatate", 2, 11.99, 20, 0, null, "200mg", "b4cb0919-77b8-48ad-8603-166c33fde263" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Hydralazine", 4, 15.0, 30, 2, null, "25mg", "b4cb0919-77b8-48ad-8603-166c33fde263" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Adderall", 1, 200.0, 60, 0, null, "20mg", "b4cb0919-77b8-48ad-8603-166c33fde263" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_StatusId",
                table: "Prescription",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_UserId",
                table: "Prescription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserType_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserType_UserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "827cafad-2f71-44db-91a9-a23c1be0f038", "a506997c-8738-4805-8da2-3d3691fbb265" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b4cb0919-77b8-48ad-8603-166c33fde263", "353aa89a-d979-457c-8669-2ef4c0df2866" });

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "AspNetUsers");
        }
    }
}
