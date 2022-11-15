using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimateControlSystem.Server.Migrations
{
    public partial class InitialiseDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitoringData",
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
                    MeanCoolingValue = table.Column<float>(type: "real", nullable: false),
                    PredictedTemperature = table.Column<float>(type: "real", nullable: false),
                    PredictedHumidity = table.Column<float>(type: "real", nullable: false),
                    PredictedTemperatureAccuracy = table.Column<float>(type: "real", nullable: true),
                    PredictedHumidityAccuracy = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MonitoringData",
                columns: new[] { "Id", "AirDryTemperatureOutside", "AirHumidityOutside", "AirWetTemperatureOutside", "ClusterLoad", "ClusterTemperature", "CpuUsage", "MeanCoolingValue", "MeasurementTime", "PredictedHumidity", "PredictedHumidityAccuracy", "PredictedTemperature", "PredictedTemperatureAccuracy", "PreviousHumidity", "PreviousTemperature", "WindDirection", "WindEnthalpy", "WindSpeed" },
                values: new object[] { 1, -3f, 91f, -3.91f, 50.8f, 56f, 5945.632f, 17.7f, new DateTimeOffset(new DateTime(2022, 11, 15, 9, 53, 14, 549, DateTimeKind.Unspecified).AddTicks(7676), new TimeSpan(0, 5, 0, 0, 0)), 18.77f, null, 23.32f, null, 19.71f, 23.48f, 225f, -4.06f, 3f });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringData");
        }
    }
}
