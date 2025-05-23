using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TapDoc_Mobile_App_Backend.Migrations
{
    /// <inheritdoc />
    public partial class refundColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefundDeadline",
                table: "AppointmentsDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundDeadline",
                table: "AppointmentsDetails");
        }
    }
}
