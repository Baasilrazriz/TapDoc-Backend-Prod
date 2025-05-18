using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TapDoc_Mobile_App_Backend.Migrations
{
    /// <inheritdoc />
    public partial class attachmentID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentTypeID",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentTypeID",
                table: "Attachments");
        }
    }
}
