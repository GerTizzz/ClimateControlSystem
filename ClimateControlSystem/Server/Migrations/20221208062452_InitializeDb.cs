using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimateControlSystem.Server.Migrations
{
    public partial class InitializeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accuracies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredictedTemperatureAccuracy = table.Column<float>(type: "real", nullable: false),
                    PredictedHumidityAccuracy = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accuracies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpperTemperatureWarningLimit = table.Column<float>(type: "real", nullable: false),
                    LowerTemperatureWarningLimit = table.Column<float>(type: "real", nullable: false),
                    UpperHumidityWarningLimit = table.Column<float>(type: "real", nullable: false),
                    LowerHumidityWarningLimit = table.Column<float>(type: "real", nullable: false),
                    UpperTemperatureCriticalLimit = table.Column<float>(type: "real", nullable: false),
                    LowerTemperatureCriticalLimit = table.Column<float>(type: "real", nullable: false),
                    UpperHumidityCriticalLimit = table.Column<float>(type: "real", nullable: false),
                    LowerHumidityCriticalLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitorings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClusterLoad = table.Column<float>(type: "real", nullable: false),
                    CpuUsage = table.Column<float>(type: "real", nullable: false),
                    ClusterTemperature = table.Column<float>(type: "real", nullable: false),
                    CurrentRealTemperature = table.Column<float>(type: "real", nullable: false),
                    CurrentRealHumidity = table.Column<float>(type: "real", nullable: false),
                    AirHumidityOutside = table.Column<float>(type: "real", nullable: false),
                    AirDryTemperatureOutside = table.Column<float>(type: "real", nullable: false),
                    AirWetTemperatureOutside = table.Column<float>(type: "real", nullable: false),
                    WindSpeed = table.Column<float>(type: "real", nullable: false),
                    WindDirection = table.Column<float>(type: "real", nullable: false),
                    WindEnthalpy = table.Column<float>(type: "real", nullable: false),
                    MeanCoolingValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitorings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredictedTemperature = table.Column<float>(type: "real", nullable: false),
                    PredictedHumidity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Climates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredictionId = table.Column<int>(type: "int", nullable: false),
                    ConfigId = table.Column<int>(type: "int", nullable: false),
                    MonitoringDataId = table.Column<int>(type: "int", nullable: false),
                    AccuracyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Climates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Climates_Accuracies_AccuracyId",
                        column: x => x.AccuracyId,
                        principalTable: "Accuracies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Climates_Configs_ConfigId",
                        column: x => x.ConfigId,
                        principalTable: "Configs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Climates_Monitorings_MonitoringDataId",
                        column: x => x.MonitoringDataId,
                        principalTable: "Monitorings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Climates_Predictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "Predictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClimateRecordEventTypeRecord",
                columns: table => new
                {
                    ClimatesId = table.Column<int>(type: "int", nullable: false),
                    EventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateRecordEventTypeRecord", x => new { x.ClimatesId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_ClimateRecordEventTypeRecord_Climates_ClimatesId",
                        column: x => x.ClimatesId,
                        principalTable: "Climates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClimateRecordEventTypeRecord_EventsTypes_EventsId",
                        column: x => x.EventsId,
                        principalTable: "EventsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 1, "admin", new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 }, new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 }, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ClimateRecordEventTypeRecord_EventsId",
                table: "ClimateRecordEventTypeRecord",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_Climates_AccuracyId",
                table: "Climates",
                column: "AccuracyId");

            migrationBuilder.CreateIndex(
                name: "IX_Climates_ConfigId",
                table: "Climates",
                column: "ConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Climates_MonitoringDataId",
                table: "Climates",
                column: "MonitoringDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Climates_PredictionId",
                table: "Climates",
                column: "PredictionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimateRecordEventTypeRecord");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Climates");

            migrationBuilder.DropTable(
                name: "EventsTypes");

            migrationBuilder.DropTable(
                name: "Accuracies");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Monitorings");

            migrationBuilder.DropTable(
                name: "Predictions");
        }
    }
}
