using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FisioSolution.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Insurance = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Physios",
                columns: table => new
                {
                    PhysioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availeable = table.Column<bool>(type: "bit", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physios", x => x.PhysioId);
                    table.ForeignKey(
                        name: "FK_Physios_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhysioId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TreatmentCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoreSessionsNeeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_Physios_PhysioId",
                        column: x => x.PhysioId,
                        principalTable: "Physios",
                        principalColumn: "PhysioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "BirthDate", "Dni", "Height", "Insurance", "Name", "Password", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "730151515", 180.0m, true, "John Doe", "1234", 80.5m },
                    { 2, new DateTime(1993, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "730203040", 172.0m, true, "Pedro Martínez", "pass123", 70.5m }
                });

            migrationBuilder.InsertData(
                table: "Physios",
                columns: new[] { "PhysioId", "Availeable", "ClosingTime", "Name", "OpeningTime", "Password", "PatientId", "Price", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, true, new TimeSpan(0, 18, 0, 0, 0), "Daniel", new TimeSpan(0, 8, 0, 0, 0), "1234", null, 50.00m, 1783 },
                    { 2, false, new TimeSpan(0, 17, 0, 0, 0), "Maria", new TimeSpan(0, 9, 0, 0, 0), "futbol3", null, 60.00m, 1700 },
                    { 3, true, new TimeSpan(0, 17, 0, 0, 0), "Jaime", new TimeSpan(0, 9, 0, 0, 0), "cocacola27", null, 60.00m, 1600 }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "TreatmentId", "MoreSessionsNeeded", "PatientId", "PhysioId", "TreatmentCause", "TreatmentDate" },
                values: new object[] { 1, true, 1, 1, "Dolor de espalda", new DateTime(2024, 5, 10, 20, 17, 31, 716, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.CreateIndex(
                name: "IX_Physios_PatientId",
                table: "Physios",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientId",
                table: "Treatments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PhysioId",
                table: "Treatments",
                column: "PhysioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Physios");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
