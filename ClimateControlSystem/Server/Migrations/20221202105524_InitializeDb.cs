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
                name: "ClimateEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemperatureLimit = table.Column<double>(type: "float", nullable: false),
                    HumidityLimit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
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
                    PreviousTemperature = table.Column<float>(type: "real", nullable: false),
                    PreviousHumidity = table.Column<float>(type: "real", nullable: false),
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
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredictedTemperature = table.Column<float>(type: "real", nullable: false),
                    PredictedHumidity = table.Column<float>(type: "real", nullable: false),
                    MonitoringDataId = table.Column<int>(type: "int", nullable: false),
                    AccuracyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predictions_Accuracies_AccuracyId",
                        column: x => x.AccuracyId,
                        principalTable: "Accuracies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Predictions_Monitorings_MonitoringDataId",
                        column: x => x.MonitoringDataId,
                        principalTable: "Monitorings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClimateEventTypeRecordPredictionRecord",
                columns: table => new
                {
                    ClimateEventsId = table.Column<int>(type: "int", nullable: false),
                    PredictionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateEventTypeRecordPredictionRecord", x => new { x.ClimateEventsId, x.PredictionsId });
                    table.ForeignKey(
                        name: "FK_ClimateEventTypeRecordPredictionRecord_ClimateEvents_ClimateEventsId",
                        column: x => x.ClimateEventsId,
                        principalTable: "ClimateEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClimateEventTypeRecordPredictionRecord_Predictions_PredictionsId",
                        column: x => x.PredictionsId,
                        principalTable: "Predictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClimateEvents",
                columns: new[] { "Id", "EventType" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "Monitorings",
                columns: new[] { "Id", "AirDryTemperatureOutside", "AirHumidityOutside", "AirWetTemperatureOutside", "ClusterLoad", "ClusterTemperature", "CpuUsage", "MeanCoolingValue", "MeasurementTime", "PreviousHumidity", "PreviousTemperature", "WindDirection", "WindEnthalpy", "WindSpeed" },
                values: new object[] { 1, -3f, 91f, -3.91f, 50.8f, 56f, 5945.632f, 17.7f, new DateTimeOffset(new DateTime(2022, 12, 2, 15, 55, 24, 465, DateTimeKind.Unspecified).AddTicks(9391), new TimeSpan(0, 5, 0, 0, 0)), 19.71f, 23.48f, 225f, -4.06f, 3f });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 1, "admin", new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 }, new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 }, "Admin" });

            migrationBuilder.InsertData(
                table: "Predictions",
                columns: new[] { "Id", "AccuracyId", "MonitoringDataId", "PredictedHumidity", "PredictedTemperature" },
                values: new object[] { 1, null, 1, 18.77f, 23.32f });

            migrationBuilder.CreateIndex(
                name: "IX_ClimateEventTypeRecordPredictionRecord_PredictionsId",
                table: "ClimateEventTypeRecordPredictionRecord",
                column: "PredictionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_AccuracyId",
                table: "Predictions",
                column: "AccuracyId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_MonitoringDataId",
                table: "Predictions",
                column: "MonitoringDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimateEventTypeRecordPredictionRecord");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ClimateEvents");

            migrationBuilder.DropTable(
                name: "Predictions");

            migrationBuilder.DropTable(
                name: "Accuracies");

            migrationBuilder.DropTable(
                name: "Monitorings");
        }
    }
}
