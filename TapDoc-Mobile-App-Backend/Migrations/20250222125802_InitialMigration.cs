using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TapDoc_Mobile_App_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailabilityDetails",
                columns: table => new
                {
                    AvailabilityDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityDetails", x => x.AvailabilityDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "DoctorCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    EmergencyContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.EmergencyContactID);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyServices",
                columns: table => new
                {
                    EmergencyServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyServices", x => x.EmergencyServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    LocalizationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.LocalizationID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PaymentStripeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordType = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleTypeID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderTypeID = table.Column<int>(type: "int", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    HeightUnitID = table.Column<int>(type: "int", nullable: false),
                    WeightUnitID = table.Column<int>(type: "int", nullable: false),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabRecords",
                columns: table => new
                {
                    LabRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabRecordUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRecords", x => x.LabRecordID);
                    table.ForeignKey(
                        name: "FK_LabRecords_PatientDetails_PatientID",
                        column: x => x.PatientID,
                        principalTable: "PatientDetails",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabRecords_Records_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Records",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabRecordAnalysis",
                columns: table => new
                {
                    LabRecordAnalysisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabRecordId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRecordAnalysis", x => x.LabRecordAnalysisId);
                    table.ForeignKey(
                        name: "FK_LabRecordAnalysis_LabRecords_LabRecordId",
                        column: x => x.LabRecordId,
                        principalTable: "LabRecords",
                        principalColumn: "LabRecordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    AppointmentStatus = table.Column<int>(type: "int", nullable: false),
                    AppointmentType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK_Appointments_PatientDetails_PatientID",
                        column: x => x.PatientID,
                        principalTable: "PatientDetails",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsDetails",
                columns: table => new
                {
                    AppointmentDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentFee = table.Column<double>(type: "float", nullable: false),
                    NoOfSlots = table.Column<int>(type: "int", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    AppointmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsDetails", x => x.AppointmentDetailsID);
                    table.ForeignKey(
                        name: "FK_AppointmentsDetails_Appointments_AppointmentID",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDetails",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Cnic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalExperience = table.Column<double>(type: "float", nullable: false),
                    DoctorFee = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentDetailsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDetails", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK_DoctorDetails_AppointmentsDetails_AppointmentDetailsID",
                        column: x => x.AppointmentDetailsID,
                        principalTable: "AppointmentsDetails",
                        principalColumn: "AppointmentDetailsID");
                    table.ForeignKey(
                        name: "FK_DoctorDetails_DoctorCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "DoctorCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDetails_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorRatings",
                columns: table => new
                {
                    DoctorRatingsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorRatings", x => x.DoctorRatingsID);
                    table.ForeignKey(
                        name: "FK_DoctorRatings_DoctorDetails_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "DoctorDetails",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorRatings_PatientDetails_PatientID",
                        column: x => x.PatientID,
                        principalTable: "PatientDetails",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionRecords",
                columns: table => new
                {
                    PrescriptionRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    AppointmentID = table.Column<int>(type: "int", nullable: false),
                    PrescriptionRecordUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionRecords", x => x.PrescriptionRecordID);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecords_Appointments_AppointmentID",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecords_DoctorDetails_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "DoctorDetails",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecords_PatientDetails_PatientID",
                        column: x => x.PatientID,
                        principalTable: "PatientDetails",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionRecords_Records_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Records",
                        principalColumn: "RecordID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionDetails",
                columns: table => new
                {
                    PrescriptionDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionRecordID = table.Column<int>(type: "int", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isReminderEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDetails", x => x.PrescriptionDetailID);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetails_PrescriptionRecords_PrescriptionRecordID",
                        column: x => x.PrescriptionRecordID,
                        principalTable: "PrescriptionRecords",
                        principalColumn: "PrescriptionRecordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionDetailTimings",
                columns: table => new
                {
                    PrescriptionDetailTimingsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionDetailID = table.Column<int>(type: "int", nullable: false),
                    FrequencyID = table.Column<int>(type: "int", nullable: false),
                    ReminderOffsetMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDetailTimings", x => x.PrescriptionDetailTimingsID);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetailTimings_PrescriptionDetails_PrescriptionDetailID",
                        column: x => x.PrescriptionDetailID,
                        principalTable: "PrescriptionDetails",
                        principalColumn: "PrescriptionDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorID",
                table: "Appointments",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsDetails_AppointmentID",
                table: "AppointmentsDetails",
                column: "AppointmentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDetails_AppointmentDetailsID",
                table: "DoctorDetails",
                column: "AppointmentDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDetails_CategoryID",
                table: "DoctorDetails",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDetails_UserID",
                table: "DoctorDetails",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_DoctorID",
                table: "DoctorRatings",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_PatientID",
                table: "DoctorRatings",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_LabRecordAnalysis_LabRecordId",
                table: "LabRecordAnalysis",
                column: "LabRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LabRecords_PatientID",
                table: "LabRecords",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_LabRecords_RecordID",
                table: "LabRecords",
                column: "RecordID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_UserID",
                table: "PatientDetails",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_PrescriptionRecordID",
                table: "PrescriptionDetails",
                column: "PrescriptionRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetailTimings_PrescriptionDetailID",
                table: "PrescriptionDetailTimings",
                column: "PrescriptionDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecords_AppointmentID",
                table: "PrescriptionRecords",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecords_DoctorID",
                table: "PrescriptionRecords",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecords_PatientID",
                table: "PrescriptionRecords",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionRecords_RecordID",
                table: "PrescriptionRecords",
                column: "RecordID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorDetails_DoctorID",
                table: "Appointments",
                column: "DoctorID",
                principalTable: "DoctorDetails",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorDetails_DoctorID",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AvailabilityDetails");

            migrationBuilder.DropTable(
                name: "DoctorRatings");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "EmergencyServices");

            migrationBuilder.DropTable(
                name: "LabRecordAnalysis");

            migrationBuilder.DropTable(
                name: "Localizations");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PrescriptionDetailTimings");

            migrationBuilder.DropTable(
                name: "LabRecords");

            migrationBuilder.DropTable(
                name: "PrescriptionDetails");

            migrationBuilder.DropTable(
                name: "PrescriptionRecords");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "DoctorDetails");

            migrationBuilder.DropTable(
                name: "AppointmentsDetails");

            migrationBuilder.DropTable(
                name: "DoctorCategories");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "PatientDetails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
