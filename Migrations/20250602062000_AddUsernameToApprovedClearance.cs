using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToApprovedClearance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Admins_AdminId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_MarketingOfficers_OfficerId",
                table: "UserLogins");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "MarketingOfficers");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_AdminId",
                table: "UserLogins");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_OfficerId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "OfficerId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "TargetCapacityKW",
                table: "MarketingTargets");

            migrationBuilder.DropColumn(
                name: "TargetMonth",
                table: "MarketingTargets");

            migrationBuilder.RenameColumn(
                name: "TargetValue",
                table: "MarketingTargets",
                newName: "ValueTarget");

            migrationBuilder.RenameColumn(
                name: "MarketingOfficerName",
                table: "MarketingTargets",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "QuotationClearances",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "KWTarget",
                table: "MarketingTargets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "MarketingTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "MarketingTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "IssuedQotations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BusinessNegotiations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "CapacityKW",
                table: "ApprovedClearances",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ApprovedClearances",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "QuotationClearances");

            migrationBuilder.DropColumn(
                name: "KWTarget",
                table: "MarketingTargets");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "MarketingTargets");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "MarketingTargets");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "IssuedQotations");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "BusinessNegotiations");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ApprovedClearances");

            migrationBuilder.RenameColumn(
                name: "ValueTarget",
                table: "MarketingTargets",
                newName: "TargetValue");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "MarketingTargets",
                newName: "MarketingOfficerName");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "UserLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficerId",
                table: "UserLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TargetCapacityKW",
                table: "MarketingTargets",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetMonth",
                table: "MarketingTargets",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<double>(
                name: "CapacityKW",
                table: "ApprovedClearances",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JoinedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WhatsAppNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MarketingOfficers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JoinedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WhatsAppNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingOfficers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_AdminId",
                table: "UserLogins",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_OfficerId",
                table: "UserLogins",
                column: "OfficerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Admins_AdminId",
                table: "UserLogins",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_MarketingOfficers_OfficerId",
                table: "UserLogins",
                column: "OfficerId",
                principalTable: "MarketingOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
