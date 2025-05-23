using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TapDoc_Mobile_App_Backend.Migrations
{
    /// <inheritdoc />
    public partial class qualifactiontableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorQualifacations",
                columns: table => new
                {
                    DoctorQualificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    QualificationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorQualifacations", x => x.DoctorQualificationID);
                    table.ForeignKey(
                        name: "FK_DoctorQualifacations_DoctorDetails_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "DoctorDetails",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorQualifacations_DoctorID",
                table: "DoctorQualifacations",
                column: "DoctorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorQualifacations");
        }
    }
}
