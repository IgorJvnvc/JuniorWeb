using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuniorWeb.DetaAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMigrationz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosissDoctorId_DiagnosissPatientId",
                table: "DiagnosisPatient");

            migrationBuilder.RenameColumn(
                name: "DiagnosissPatientId",
                table: "DiagnosisPatient",
                newName: "DiagnosisPatientId");

            migrationBuilder.RenameColumn(
                name: "DiagnosissDoctorId",
                table: "DiagnosisPatient",
                newName: "DiagnosisDoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DiagnosisPatient_DiagnosissDoctorId_DiagnosissPatientId",
                table: "DiagnosisPatient",
                newName: "IX_DiagnosisPatient_DiagnosisDoctorId_DiagnosisPatientId");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosisDoctorId_DiagnosisPatientId",
                table: "DiagnosisPatient",
                columns: new[] { "DiagnosisDoctorId", "DiagnosisPatientId" },
                principalTable: "Diagnoses",
                principalColumns: new[] { "DoctorId", "PatientId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Rooms_RoomId",
                table: "Patients",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosisDoctorId_DiagnosisPatientId",
                table: "DiagnosisPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Rooms_RoomId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_RoomId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "DiagnosisPatientId",
                table: "DiagnosisPatient",
                newName: "DiagnosissPatientId");

            migrationBuilder.RenameColumn(
                name: "DiagnosisDoctorId",
                table: "DiagnosisPatient",
                newName: "DiagnosissDoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DiagnosisPatient_DiagnosisDoctorId_DiagnosisPatientId",
                table: "DiagnosisPatient",
                newName: "IX_DiagnosisPatient_DiagnosissDoctorId_DiagnosissPatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosissDoctorId_DiagnosissPatientId",
                table: "DiagnosisPatient",
                columns: new[] { "DiagnosissDoctorId", "DiagnosissPatientId" },
                principalTable: "Diagnoses",
                principalColumns: new[] { "DoctorId", "PatientId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
