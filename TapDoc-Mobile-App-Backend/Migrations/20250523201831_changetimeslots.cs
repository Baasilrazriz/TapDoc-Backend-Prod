using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TapDoc_Mobile_App_Backend.Migrations
{
    /// <inheritdoc />
    public partial class changetimeslots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentRescheduleRequests",
                columns: table => new
                {
                    AppointmentRescheduleRequestsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentID = table.Column<int>(type: "int", nullable: false),
                    NewStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeRequestStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRescheduleRequests", x => x.AppointmentRescheduleRequestsID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentRescheduleRequests");
        }
    }
}
