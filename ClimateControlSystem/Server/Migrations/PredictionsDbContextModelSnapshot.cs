﻿// <auto-generated />
using System;
using ClimateControlSystem.Server.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClimateControlSystem.Server.Migrations
{
    [DbContext(typeof(PredictionsDbContext))]
    partial class PredictionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.AccuracyRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("PredictedHumidityAccuracy")
                        .HasColumnType("real");

                    b.Property<float>("PredictedTemperatureAccuracy")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Accuracies");
                });

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.ClimateEventTypeRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClimateEvents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EventType = 0
                        },
                        new
                        {
                            Id = 2,
                            EventType = 1
                        },
                        new
                        {
                            Id = 3,
                            EventType = 2
                        },
                        new
                        {
                            Id = 4,
                            EventType = 3
                        },
                        new
                        {
                            Id = 5,
                            EventType = 4
                        });
                });

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.ConfigRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("HumidityLimit")
                        .HasColumnType("float");

                    b.Property<double>("TemperatureLimit")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.MonitoringRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("AirDryTemperatureOutside")
                        .HasColumnType("real");

                    b.Property<float>("AirHumidityOutside")
                        .HasColumnType("real");

                    b.Property<float>("AirWetTemperatureOutside")
                        .HasColumnType("real");

                    b.Property<float>("ClusterLoad")
                        .HasColumnType("real");

                    b.Property<float>("ClusterTemperature")
                        .HasColumnType("real");

                    b.Property<float>("CpuUsage")
                        .HasColumnType("real");

                    b.Property<float>("CurrentRealHumidity")
                        .HasColumnType("real");

                    b.Property<float>("CurrentRealTemperature")
                        .HasColumnType("real");

                    b.Property<float>("MeanCoolingValue")
                        .HasColumnType("real");

                    b.Property<DateTimeOffset>("MeasurementTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<float>("WindDirection")
                        .HasColumnType("real");

                    b.Property<float>("WindEnthalpy")
                        .HasColumnType("real");

                    b.Property<float>("WindSpeed")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Monitorings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AirDryTemperatureOutside = -3f,
                            AirHumidityOutside = 91f,
                            AirWetTemperatureOutside = -3.91f,
                            ClusterLoad = 50.8f,
                            ClusterTemperature = 56f,
                            CpuUsage = 5945.632f,
                            CurrentRealHumidity = 19.71f,
                            CurrentRealTemperature = 23.48f,
                            MeanCoolingValue = 17.7f,
                            MeasurementTime = new DateTimeOffset(new DateTime(2022, 12, 3, 22, 6, 0, 110, DateTimeKind.Unspecified).AddTicks(2797), new TimeSpan(0, 5, 0, 0, 0)),
                            WindDirection = 225f,
                            WindEnthalpy = -4.06f,
                            WindSpeed = 3f
                        });
                });

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.PredictionRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccuracyId")
                        .HasColumnType("int");

                    b.Property<int>("MonitoringDataId")
                        .HasColumnType("int");

                    b.Property<float>("PredictedHumidity")
                        .HasColumnType("real");

                    b.Property<float>("PredictedTemperature")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AccuracyId");

                    b.HasIndex("MonitoringDataId");

                    b.ToTable("Predictions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MonitoringDataId = 1,
                            PredictedHumidity = 18.77f,
                            PredictedTemperature = 23.32f
                        });
                });

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.UserRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            PasswordHash = new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 },
                            PasswordSalt = new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 },
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("ClimateEventTypeRecordPredictionRecord", b =>
                {
                    b.Property<int>("ClimateEventsId")
                        .HasColumnType("int");

                    b.Property<int>("PredictionsId")
                        .HasColumnType("int");

                    b.HasKey("ClimateEventsId", "PredictionsId");

                    b.HasIndex("PredictionsId");

                    b.ToTable("ClimateEventTypeRecordPredictionRecord");
                });

            modelBuilder.Entity("ClimateControlSystem.Server.Resources.RepositoryResources.PredictionRecord", b =>
                {
                    b.HasOne("ClimateControlSystem.Server.Resources.RepositoryResources.AccuracyRecord", "Accuracy")
                        .WithMany()
                        .HasForeignKey("AccuracyId");

                    b.HasOne("ClimateControlSystem.Server.Resources.RepositoryResources.MonitoringRecord", "MonitoringData")
                        .WithMany()
                        .HasForeignKey("MonitoringDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accuracy");

                    b.Navigation("MonitoringData");
                });

            modelBuilder.Entity("ClimateEventTypeRecordPredictionRecord", b =>
                {
                    b.HasOne("ClimateControlSystem.Server.Resources.RepositoryResources.ClimateEventTypeRecord", null)
                        .WithMany()
                        .HasForeignKey("ClimateEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimateControlSystem.Server.Resources.RepositoryResources.PredictionRecord", null)
                        .WithMany()
                        .HasForeignKey("PredictionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
