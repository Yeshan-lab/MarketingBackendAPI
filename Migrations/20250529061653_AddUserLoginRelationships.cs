using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLoginRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserLogins");

            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "MarketingOfficers",
                newName: "JoinedDate");

            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "Admins",
                newName: "JoinedDate");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Admins_AdminId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_MarketingOfficers_OfficerId",
                table: "UserLogins");

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

            migrationBuilder.RenameColumn(
                name: "JoinedDate",
                table: "MarketingOfficers",
                newName: "JoinDate");

            migrationBuilder.RenameColumn(
                name: "JoinedDate",
                table: "Admins",
                newName: "JoinDate");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
