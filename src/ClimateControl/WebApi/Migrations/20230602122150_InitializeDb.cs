using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class InitializeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpperTemperatureWarningLimit = table.Column<float>(type: "real", nullable: false),
                    LowerTemperatureWarningLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemperatureInside = table.Column<float>(type: "real", nullable: false),
                    TemperatureOutside = table.Column<float>(type: "real", nullable: false),
                    CoolingPower = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Warnings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warnings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    ForecastId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarningId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predictions_Forecasts_ForecastId",
                        column: x => x.ForecastId,
                        principalTable: "Forecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Predictions_Warnings_WarningId",
                        column: x => x.WarningId,
                        principalTable: "Warnings",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Configs",
                columns: new[] { "Id", "LowerTemperatureWarningLimit", "UpperTemperatureWarningLimit" },
                values: new object[] { new Guid("04b7f479-5c2c-42b3-b96d-e17f035ced4b"), 18f, 27f });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CoolingPower", "TemperatureInside", "TemperatureOutside" },
                values: new object[,]
                {
                    { new Guid("00630a3c-d3b3-4c4a-81e4-3aa1706c7569"), 18f, 17.36f, -6.9f },
                    { new Guid("04379702-0f62-4436-9801-d5b382fb8335"), 20f, 18.09f, -13.79f },
                    { new Guid("048baf6d-0db2-4bab-8430-76f71db86071"), 20f, 18.11f, -10.83f },
                    { new Guid("0617753d-72d1-4d48-b607-21fafae31a79"), 18f, 18.86f, -2f },
                    { new Guid("089b0f1c-d725-4f8e-8a31-6ae2177b838e"), 22f, 18.06f, -14.22f },
                    { new Guid("0ba35bd0-cfef-495c-b0e6-9cddf3c4d2a8"), 21f, 17.63f, -14.32f },
                    { new Guid("0e7447f4-c507-43a1-abd2-f43d0a43ed8b"), 19f, 18.26f, -9.33f },
                    { new Guid("0ea7d23a-9017-45ee-8081-cdd7aed72574"), 22f, 16.73f, -14.83f },
                    { new Guid("10f5ed86-629d-44f4-ae5c-bedbcc8e1b03"), 17f, 17.74f, -2f },
                    { new Guid("112867ad-9c07-41a0-bc86-97ee6068639e"), 18f, 16.61f, -12.89f },
                    { new Guid("1208bc84-3ead-41d1-b099-6cf224323b17"), 19f, 16.73f, -14.67f },
                    { new Guid("180ff924-6ca3-414b-8abf-78d4f23cdc64"), 19f, 18.56f, -10.94f },
                    { new Guid("1b16aa60-9a7f-4d5b-83c0-913085f5a8d2"), 18f, 17.23f, -12.67f },
                    { new Guid("1e656672-2018-475e-85ca-b32c638721db"), 19f, 18.59f, -9.5f },
                    { new Guid("1e986fc7-9070-4f3f-a338-5c8f95f737ea"), 19f, 15.41f, -13.78f },
                    { new Guid("1f0d32b9-f634-4944-a056-4538058ac85d"), 18f, 17.67f, -7.65f },
                    { new Guid("1ffd1351-c96b-4a82-b7b0-e179f1093a7c"), 15f, 18.59f, -2f },
                    { new Guid("27e6d32e-5f17-47b9-bd1b-9c2f215ed6a2"), 19f, 16.42f, -14.33f },
                    { new Guid("28d68c2e-6a56-4d31-8df6-4457a150294f"), 18f, 17.95f, -5.85f },
                    { new Guid("28da61e9-946e-42b7-a06c-4fad0d2ec650"), 19f, 17.26f, -13.22f },
                    { new Guid("29f13e76-9dda-444c-97dc-37fde2d1728b"), 18f, 18.32f, -7.35f },
                    { new Guid("2c06d6aa-e531-4270-ae2e-0ea7265011d6"), 20f, 18.52f, -13.89f },
                    { new Guid("2d225c5c-7f40-49bb-afce-0aba7b012052"), 21f, 17.04f, -14.89f },
                    { new Guid("344e0a02-8a8f-4c01-8fe6-4f55ea8a02ff"), 19f, 18.32f, -10.67f },
                    { new Guid("34567822-0817-4034-81e0-4f10d2e39a95"), 20f, 16.72f, -13.56f },
                    { new Guid("35d2d64a-fdb9-4246-8b50-11ce99de1de9"), 17f, 19.02f, -2f },
                    { new Guid("378a7838-ea11-4701-9155-0ddeb400f3d6"), 19f, 18.85f, -5.84f },
                    { new Guid("3adaa15a-0bd6-483a-8d20-3aae4b840fa2"), 18f, 16.9f, -8.25f },
                    { new Guid("3b814484-024c-4e44-bbea-34b215a2f1c7"), 22f, 17.32f, -14.44f },
                    { new Guid("3cb06e8f-9fa2-47ab-ab9f-fe9dcbe1cf9c"), 20f, 17.97f, -10.56f },
                    { new Guid("429f15b2-db05-4523-9716-c9e374746c40"), 18f, 18.78f, -9f },
                    { new Guid("453f9f17-3181-4bd9-9f5a-42a68eca278c"), 20f, 17.35f, -11.89f },
                    { new Guid("457a37ab-5aa4-499e-a78d-ee385f53f8fe"), 19f, 18f, -14.06f },
                    { new Guid("47aabfed-3667-43ed-a160-ebc547f007f9"), 19f, 17.35f, -6.3f },
                    { new Guid("47b7371e-aa00-48bf-9bb9-471dc006b167"), 17f, 19.37f, -2f },
                    { new Guid("4825d855-1c0c-432a-940f-3111b764ab44"), 19f, 17.84f, -10.61f },
                    { new Guid("48f10e2b-37fe-4fa1-8f41-d2af6ab6083e"), 19f, 16.67f, -13.11f },
                    { new Guid("49ece36f-69ea-4b38-8fe5-0d7d63229f6f"), 20f, 18.08f, -10.72f },
                    { new Guid("4b0da327-924e-4508-b4a6-a78034635813"), 20f, 18.1f, -13.88f },
                    { new Guid("4d58f93e-5ab9-4e74-84ab-fbfb9ccfd683"), 20f, 17.8f, -9.17f },
                    { new Guid("4d9019a5-e6ed-4556-96cd-2879fcebf403"), 19f, 17.06f, -8.51f }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CoolingPower", "TemperatureInside", "TemperatureOutside" },
                values: new object[,]
                {
                    { new Guid("4f1f1833-1ba9-462b-a035-16082db7ee8a"), 16f, 18.85f, -2f },
                    { new Guid("511f3ce3-d6e4-4513-9d15-83689864b59c"), 20f, 18.34f, -11.11f },
                    { new Guid("528a4659-4144-45fe-8aac-8005541ccea9"), 19f, 16.89f, -14.41f },
                    { new Guid("52c75ec5-e149-4651-b379-8a7497ca9ddc"), 19f, 17.73f, -7.5f },
                    { new Guid("54bdaa07-c3c9-411c-bc1b-bc1a9e99f858"), 15f, 19.48f, -2f },
                    { new Guid("554f7278-1f2c-43fd-a707-81fd627ea7c4"), 20f, 15.94f, -12.78f },
                    { new Guid("56d1d48b-bd03-4b46-a390-f4ec9cfdb857"), 20f, 18.46f, -11f },
                    { new Guid("58d8e515-ff4f-4405-8a2a-d51d69136a0b"), 19f, 18.09f, -10.17f },
                    { new Guid("592669ba-7818-4155-9157-f13ee29cb9fc"), 19f, 18.33f, -13.33f },
                    { new Guid("59669e59-5e12-489b-bf4e-357f6d041276"), 18f, 16.35f, -14.89f },
                    { new Guid("5bded403-44f5-4d94-a78d-8d677052456d"), 20f, 18.57f, -14.06f },
                    { new Guid("5bf919c9-7a30-46d6-a212-29cc68d7db63"), 20f, 17.91f, -13.7f },
                    { new Guid("5e9a6774-f600-4bf5-8536-a0008a44f817"), 20f, 18.78f, -9.67f },
                    { new Guid("5f7b562e-6490-4d97-ba9f-5a539161290d"), 22f, 17.77f, -14.33f },
                    { new Guid("603d31d9-32c4-466a-b85d-5fbf44f1cdad"), 18f, 19.31f, -2.11f },
                    { new Guid("60ca4fdc-70de-4bcf-b2d2-c448ce12a0bd"), 20f, 17.47f, -13.97f },
                    { new Guid("6244cd52-7943-4483-9606-ebae4f65c89d"), 19f, 17.33f, -7.2f },
                    { new Guid("62ec44fa-55bb-45a9-9d7d-cbef46ddefb4"), 17f, 16.67f, -13.89f },
                    { new Guid("63f57361-ecb2-4751-9f7e-175c15ae3ad6"), 19f, 18.78f, -5.99f },
                    { new Guid("64823003-2325-458e-a844-fdfc87e99b01"), 19f, 16.93f, -8.5f },
                    { new Guid("6508042e-368e-4ca2-b388-0490bb329718"), 17f, 17.45f, -8.73f },
                    { new Guid("675fd8ae-84f7-4f62-b106-2745acb02203"), 18f, 17.61f, -7.8f },
                    { new Guid("6c03475d-cb91-4a8a-929d-0443e12c95e6"), 19f, 18.04f, -13.52f },
                    { new Guid("6fb5589d-4e02-40de-9543-719a8f7e0d1a"), 18f, 19f, -2f },
                    { new Guid("700dadf6-b75d-406f-a8d8-9e4116028b15"), 22f, 18.02f, -14.5f },
                    { new Guid("72694e83-6783-4c19-a9f3-c0e411fed83f"), 18f, 18.11f, -5.7f },
                    { new Guid("726a9ff5-9f40-4af8-ab7a-4380dcc34307"), 20f, 18.19f, -10.28f },
                    { new Guid("7319bff5-dd2d-47b0-9b93-908330cd42c3"), 20f, 18.19f, -10.5f },
                    { new Guid("770a2ee4-4d38-46e4-be0d-29bdbe13cbb9"), 19f, 18.66f, -10.39f },
                    { new Guid("79d4cac6-da56-4191-be00-48b6ed833236"), 18f, 17.68f, -6.45f },
                    { new Guid("7aca389f-2a0c-4677-b0c3-93592cc1b8f2"), 17f, 18.72f, -2f },
                    { new Guid("7c7fcdee-ffbc-48b0-a0d7-77606e373cf5"), 18f, 17.25f, -8.1f },
                    { new Guid("7d9ff21b-3140-4ff6-8470-107fca7e8a3a"), 19f, 17.72f, -12f },
                    { new Guid("8076cc10-ca8e-4d15-8290-6f27509410f9"), 21f, 17.75f, -14.28f },
                    { new Guid("80a420d3-49c2-41f6-ac24-998a4a5de248"), 23f, 16.59f, -14.94f },
                    { new Guid("81373a79-0d3e-4b44-973d-edd02edfc6ac"), 19f, 18.76f, -10.78f },
                    { new Guid("8252d607-c46e-42af-8aff-cbb34cf12a57"), 19f, 16.69f, -14.11f },
                    { new Guid("8452fca6-3b42-4269-a062-0a1beff059c4"), 22f, 17.03f, -14.56f },
                    { new Guid("85089c74-64c5-4de3-b0cd-ddc6c7e8030e"), 16f, 18.66f, -2f },
                    { new Guid("86c6f63d-7727-4a32-a6a9-a9276c4abede"), 18f, 17.66f, -12.44f },
                    { new Guid("86f1586d-8768-47f2-913f-b231495fc9c1"), 18f, 18.18f, -8.17f },
                    { new Guid("8a722490-7e5b-4cd7-bb8b-493cc8db968f"), 19f, 16.72f, -13.67f }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CoolingPower", "TemperatureInside", "TemperatureOutside" },
                values: new object[,]
                {
                    { new Guid("8bb24261-14f2-4c73-91b7-771d7f830239"), 16f, 19.39f, -2f },
                    { new Guid("8d9f0d21-9775-46fa-a7d0-b98fb6e39132"), 19f, 17.34f, -13.44f },
                    { new Guid("8fdf2cba-9297-4d12-980a-3d2fb1ea0535"), 19f, 16.75f, -13.33f },
                    { new Guid("90b44f5d-42f3-44d7-b872-58f702027922"), 21f, 18.21f, -14f },
                    { new Guid("9127008a-8ecc-41ed-bf7c-349c027374f1"), 19f, 18.08f, -11.33f },
                    { new Guid("9270e22d-357a-4aa8-9265-f4ea87edbad8"), 18f, 17.61f, -8.4f },
                    { new Guid("93a1d218-d73d-428a-aff2-9dc32abfeb00"), 19f, 18.06f, -8.33f },
                    { new Guid("94f4f7bb-09a8-4812-89e8-7a37efc2881e"), 22f, 17.21f, -14.67f },
                    { new Guid("9b3667b3-d4b1-4938-bc6c-63fa9791d55a"), 19f, 18.46f, -13.78f },
                    { new Guid("9fbaf98f-0c98-4d2d-aced-1ed496d29dcd"), 18f, 17.96f, -7.05f },
                    { new Guid("a195d90b-c882-4064-8aad-84c4acc1d052"), 18f, 18.05f, -6.15f },
                    { new Guid("a2908e75-5545-4eec-a18d-6a4247b3316f"), 19f, 16.91f, -14.56f },
                    { new Guid("a2917c10-8afc-4ecb-a590-4e0b2689980c"), 22f, 17.76f, -14.39f },
                    { new Guid("a39658a8-ef1f-4469-a677-e7dede4d38fa"), 19f, 17.43f, -12.22f },
                    { new Guid("a86a7232-5820-4828-8b10-fa3d0ef7f3b1"), 17f, 19.06f, -2f },
                    { new Guid("ab11af31-5655-4352-aea3-fa95ef6d8535"), 21f, 18.37f, -14.17f },
                    { new Guid("ae664f6d-fddf-41ff-951e-41f02efa8f11"), 16f, 18.98f, -2f },
                    { new Guid("aff9d74b-70cd-4f41-9422-e07afc84773a"), 18f, 17.42f, -8.62f },
                    { new Guid("affd3c7e-6e9f-4bab-aa79-91ac7b66cf83"), 20f, 17.88f, -10.89f },
                    { new Guid("b18951dc-6144-48d7-8124-8e157f7ecc9a"), 19f, 16.72f, -13f },
                    { new Guid("b4644622-db4f-4561-a2fa-52d888a1e831"), 16f, 19.23f, -2f },
                    { new Guid("b470a6dd-1b9a-4713-b54c-4c60f4798e8a"), 21f, 17.56f, -14.23f },
                    { new Guid("bae97728-0989-43ff-8d9e-cc5c36aacccf"), 19f, 18.17f, -10.33f },
                    { new Guid("bf2519f8-00a0-4817-b7a5-c3cd1dabf490"), 20f, 17.68f, -11.44f },
                    { new Guid("bfc58875-8717-4d77-a6d2-ed7fb3141c2f"), 20f, 18.46f, -10.45f },
                    { new Guid("c2377785-9f70-47f1-8ccf-f5c3cc6526de"), 17f, 18.07f, -2f },
                    { new Guid("c60ef54d-2472-41fb-844d-56fa4dd7df70"), 20f, 17.96f, -11.22f },
                    { new Guid("c70bbfdf-ee7e-43c9-96ef-dfd2048dcc39"), 20f, 18.49f, -10f },
                    { new Guid("cc67cd3a-507c-4869-b680-918aee36dfb3"), 20f, 17.93f, -10.11f },
                    { new Guid("cca9f48b-afe0-4acf-b0cb-68beb8b1b6ee"), 21f, 18.02f, -14.11f },
                    { new Guid("d2d0f43a-5363-4a97-9c34-2cccf92b5b91"), 20f, 16.24f, -12.56f },
                    { new Guid("d5036ea9-af77-473d-886b-ef59135375b2"), 19f, 17.65f, -11.56f },
                    { new Guid("d54eca7f-77a4-40fb-9461-148fa1d4da61"), 19f, 16.75f, -14f },
                    { new Guid("d83e61e5-2154-438f-965b-970305132962"), 18f, 17.74f, -6.6f },
                    { new Guid("d8cd6bc9-3de6-427a-91a0-c2aca2404b5a"), 22f, 17f, -14.72f },
                    { new Guid("dac5adbe-11a1-4915-b63a-ebcb9c1c9d6d"), 22f, 17.01f, -14.78f },
                    { new Guid("daef3e13-71f6-4b19-b8ee-5463b0f02adf"), 20f, 18.47f, -10.06f },
                    { new Guid("dc6c1036-645f-4cef-973d-3e05d5f85bb0"), 19f, 18.68f, -10.22f },
                    { new Guid("dee509c7-e6e2-405d-bba4-3bb7ca6db74c"), 19f, 18.14f, -13.15f },
                    { new Guid("df92bf22-f200-46f1-92fe-31dfbf46afba"), 20f, 15.51f, -14.78f },
                    { new Guid("e15a5771-be6e-4161-a364-b0aab9f44b87"), 14f, 19.25f, -2f },
                    { new Guid("e1903dbc-babc-4e1a-8a37-7f11bbff34a7"), 18f, 17.8f, -6f }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CoolingPower", "TemperatureInside", "TemperatureOutside" },
                values: new object[,]
                {
                    { new Guid("e394d158-c7db-411a-bdef-8525cb6c8124"), 19f, 17.8f, -8.83f },
                    { new Guid("e59e5fda-6afb-452f-995f-1eaa085b72cf"), 19f, 17.04f, -14.44f },
                    { new Guid("e5ee7fd0-8d29-4762-abc8-2625bf031f68"), 20f, 18.01f, -11.78f },
                    { new Guid("e784ac80-2c16-4459-9a7a-3f7ae2fe021c"), 17f, 18.93f, -2f },
                    { new Guid("e78b29b2-c0bd-4829-8ca2-e8ad64c0718a"), 21f, 15.87f, -14.61f },
                    { new Guid("eac36c55-c9d9-4d3e-8ce4-4f2d9b0ec707"), 20f, 18.55f, -9.83f },
                    { new Guid("eadd4334-7766-4fdb-87b2-00691d30df48"), 19f, 16.3f, -14.22f },
                    { new Guid("ede74088-17d8-43b6-8244-8bdf084a8b53"), 21f, 16.83f, -14.5f },
                    { new Guid("ee2fd752-0644-4aa3-8f44-243efcc94bba"), 19f, 17.12f, -12.9f },
                    { new Guid("ee4a6ba0-02d9-491c-bee0-ebce6f8ccbb1"), 19f, 19.22f, -2.22f },
                    { new Guid("ee8b674c-88f2-41fe-a32b-3b9c8f91dba4"), 19f, 16.34f, -15f },
                    { new Guid("f2672068-d9df-484c-a55a-c267d4c837dd"), 18f, 17.55f, -6.75f },
                    { new Guid("f74e3f17-3660-4069-86f9-3717068f8c61"), 20f, 16.44f, -12.33f },
                    { new Guid("f79019bb-0469-44a8-87c5-432ad873c4e0"), 18f, 18.68f, -2f },
                    { new Guid("faf49eff-58ea-4bd5-8ad4-54451045a81a"), 19f, 18.36f, -11.67f },
                    { new Guid("faf7f64b-a177-43ab-9e6a-e212a414b336"), 18f, 17.17f, -7.95f },
                    { new Guid("fbda8370-272f-4b1a-803b-13b3d3ac2ebb"), 18f, 17.9f, -8.67f },
                    { new Guid("fd5a44d1-b5de-45f2-b7cd-5ec2fbb86b79"), 19f, 17.65f, -12.11f },
                    { new Guid("ff84b95f-8ca9-4051-a2ae-223e0d2fb689"), 19f, 17.62f, -14.15f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new Guid("061ab610-a57c-463c-a3aa-c1d3dc5d92be"), "admin", new byte[] { 234, 67, 146, 201, 134, 164, 86, 202, 125, 217, 174, 99, 230, 69, 196, 32, 223, 130, 86, 2, 110, 245, 35, 7, 159, 20, 84, 62, 49, 84, 81, 28, 175, 203, 198, 202, 128, 63, 128, 15, 96, 135, 210, 4, 252, 15, 252, 17, 150, 160, 104, 243, 99, 40, 181, 210, 193, 226, 14, 26, 229, 165, 150, 197 }, new byte[] { 29, 90, 245, 35, 83, 27, 162, 74, 226, 234, 171, 134, 93, 187, 246, 80, 193, 193, 90, 50, 37, 118, 116, 254, 107, 30, 200, 72, 10, 31, 43, 139, 58, 135, 118, 189, 5, 99, 211, 203, 0, 84, 81, 146, 28, 164, 132, 63, 61, 143, 124, 25, 66, 231, 99, 189, 203, 55, 91, 105, 23, 169, 254, 10, 20, 179, 147, 58, 198, 70, 204, 60, 221, 77, 160, 128, 50, 190, 189, 205, 83, 48, 107, 183, 51, 48, 173, 248, 28, 230, 153, 194, 13, 108, 51, 123, 87, 228, 62, 31, 167, 11, 30, 180, 130, 172, 254, 241, 22, 7, 150, 212, 195, 48, 144, 92, 52, 199, 221, 202, 91, 200, 83, 109, 66, 70, 223, 200 }, "Admin" });

            migrationBuilder.InsertData(
                table: "Warnings",
                columns: new[] { "Id", "Message", "Type" },
                values: new object[,]
                {
                    { new Guid("807d7871-788d-4554-a34a-6f07edb2aea4"), "Ожидается критическое повышение температуры! Необходимо принять меры: увеличеть мощность охлаждения!", "CriticalUpper" },
                    { new Guid("c1e5c843-c79b-4526-91f6-4a9abf80802b"), "Ожидается критическое снижение температуры! Необходимо принять меры: уменьшить мощность охлаждения!", "CriticalLower" },
                    { new Guid("c37e1146-cbdd-49c1-9de0-b9be9c0b7d39"), "Ожидается повышение температуры выше оптимальной! Для более эффективной работы необходимо уменьшить мощность охлаждения!", "Upper" },
                    { new Guid("c913d12e-2bc6-423b-997c-a20f624620f4"), "Ожидается понижение температуры ниже оптимальной! Для более эффективной работы необходимо увеличить мощность охлаждения!", "Lower" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "FeatureId", "Time" },
                values: new object[,]
                {
                    { new Guid("0059ee17-0cf4-4aa3-9843-da41214119ba"), new Guid("048baf6d-0db2-4bab-8430-76f71db86071"), new DateTimeOffset(new DateTime(2023, 6, 1, 14, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("025f9b7f-c429-4205-bd0a-4cb0527db875"), new Guid("e784ac80-2c16-4459-9a7a-3f7ae2fe021c"), new DateTimeOffset(new DateTime(2023, 5, 29, 15, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("053d45d4-8255-4d70-b081-f050c98c6b18"), new Guid("faf49eff-58ea-4bd5-8ad4-54451045a81a"), new DateTimeOffset(new DateTime(2023, 6, 1, 5, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("05d794d6-1b74-4392-b7dd-9b739a058626"), new Guid("60ca4fdc-70de-4bcf-b2d2-c448ce12a0bd"), new DateTimeOffset(new DateTime(2023, 5, 29, 3, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("0ff188b5-e69d-4f30-9d98-93ce1a8453c7"), new Guid("1208bc84-3ead-41d1-b099-6cf224323b17"), new DateTimeOffset(new DateTime(2023, 5, 31, 2, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("11f60cb5-1474-435c-8bce-56a2a5467449"), new Guid("453f9f17-3181-4bd9-9f5a-42a68eca278c"), new DateTimeOffset(new DateTime(2023, 6, 1, 3, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("12e26231-a50f-416c-8fb0-e1d9d2afccaf"), new Guid("9fbaf98f-0c98-4d2d-aced-1ed496d29dcd"), new DateTimeOffset(new DateTime(2023, 5, 28, 3, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("14316f7b-0610-4643-9b42-e073ebc487a0"), new Guid("ee4a6ba0-02d9-491c-bee0-ebce6f8ccbb1"), new DateTimeOffset(new DateTime(2023, 5, 30, 10, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("155074db-a17b-4dfd-9cf6-b0c62ec06c3f"), new Guid("700dadf6-b75d-406f-a8d8-9e4116028b15"), new DateTimeOffset(new DateTime(2023, 5, 29, 16, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("19f84958-1ede-4eac-95aa-f87e2b9d0eea"), new Guid("ae664f6d-fddf-41ff-951e-41f02efa8f11"), new DateTimeOffset(new DateTime(2023, 5, 30, 3, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("1b2ab312-c9a6-4429-99ea-f58289f8a0e5"), new Guid("a2908e75-5545-4eec-a18d-6a4247b3316f"), new DateTimeOffset(new DateTime(2023, 5, 31, 3, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("1e879306-b019-4668-b797-4082fc545aa3"), new Guid("f2672068-d9df-484c-a55a-c267d4c837dd"), new DateTimeOffset(new DateTime(2023, 5, 28, 5, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("2055052e-f008-4083-9aef-d6a1ac47d739"), new Guid("2d225c5c-7f40-49bb-afce-0aba7b012052"), new DateTimeOffset(new DateTime(2023, 5, 30, 15, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("20d0572b-874b-47f5-bd59-94d9c2730f0d"), new Guid("0ea7d23a-9017-45ee-8081-cdd7aed72574"), new DateTimeOffset(new DateTime(2023, 5, 30, 12, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("21b4e1ab-3bed-4fd2-9c68-3c611da4e2a8"), new Guid("79d4cac6-da56-4191-be00-48b6ed833236"), new DateTimeOffset(new DateTime(2023, 5, 28, 7, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("22b9c92a-d831-4178-8cb4-87e5dcf25d15"), new Guid("e1903dbc-babc-4e1a-8a37-7f11bbff34a7"), new DateTimeOffset(new DateTime(2023, 5, 28, 10, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("22dda388-73dc-4ec0-a75b-939962453484"), new Guid("c60ef54d-2472-41fb-844d-56fa4dd7df70"), new DateTimeOffset(new DateTime(2023, 6, 1, 9, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("23b2bc49-6956-473c-b568-bbe4d9e53ec4"), new Guid("ede74088-17d8-43b6-8244-8bdf084a8b53"), new DateTimeOffset(new DateTime(2023, 5, 30, 20, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("23e78c70-6627-4115-8b44-ce6bb6a947b0"), new Guid("4d58f93e-5ab9-4e74-84ab-fbfb9ccfd683"), new DateTimeOffset(new DateTime(2023, 6, 2, 10, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("291d00c8-dd02-4818-84ad-a98847724b0d"), new Guid("ee8b674c-88f2-41fe-a32b-3b9c8f91dba4"), new DateTimeOffset(new DateTime(2023, 5, 30, 23, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("2be38371-a05a-4531-bed1-acd17ce3262a"), new Guid("49ece36f-69ea-4b38-8fe5-0d7d63229f6f"), new DateTimeOffset(new DateTime(2023, 6, 1, 16, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("2cca74e7-a99a-4549-8c12-7abe17fb900b"), new Guid("86c6f63d-7727-4a32-a6a9-a9276c4abede"), new DateTimeOffset(new DateTime(2023, 5, 31, 22, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("2e68f786-609f-4f95-9211-baebf6eeddf1"), new Guid("86f1586d-8768-47f2-913f-b231495fc9c1"), new DateTimeOffset(new DateTime(2023, 6, 2, 16, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("3727d923-51b4-4ac8-8bb7-d3fcc0ece02a"), new Guid("58d8e515-ff4f-4405-8a2a-d51d69136a0b"), new DateTimeOffset(new DateTime(2023, 6, 2, 2, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("3880180a-d861-4be1-ab85-16f3382a4daf"), new Guid("a39658a8-ef1f-4469-a677-e7dede4d38fa"), new DateTimeOffset(new DateTime(2023, 6, 1, 0, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("38d2f310-f705-45cd-89b5-be58dd8c648b"), new Guid("ff84b95f-8ca9-4051-a2ae-223e0d2fb689"), new DateTimeOffset(new DateTime(2023, 5, 29, 18, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("38fb31c1-10b0-4e4b-a211-81e8e2b85a95"), new Guid("1ffd1351-c96b-4a82-b7b0-e179f1093a7c"), new DateTimeOffset(new DateTime(2023, 5, 30, 16, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("3c67a513-5ca9-4007-8d4f-57b9c0d8678d"), new Guid("fd5a44d1-b5de-45f2-b7cd-5ec2fbb86b79"), new DateTimeOffset(new DateTime(2023, 6, 1, 1, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("403eb0bc-3ec2-4ee5-b0bd-66db73f1bf37"), new Guid("344e0a02-8a8f-4c01-8fe6-4f55ea8a02ff"), new DateTimeOffset(new DateTime(2023, 6, 1, 17, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("4188b3a4-a322-4f3d-966d-42a7c13069df"), new Guid("3adaa15a-0bd6-483a-8d20-3aae4b840fa2"), new DateTimeOffset(new DateTime(2023, 5, 27, 19, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("4233ade5-0ba0-48a2-85bf-b1e1357073de"), new Guid("d8cd6bc9-3de6-427a-91a0-c2aca2404b5a"), new DateTimeOffset(new DateTime(2023, 5, 30, 4, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("4a5354db-915e-4980-af22-bd19f0282399"), new Guid("bae97728-0989-43ff-8d9e-cc5c36aacccf"), new DateTimeOffset(new DateTime(2023, 6, 1, 23, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("4c9ea45f-0584-4396-8ed1-57887e57ee71"), new Guid("1b16aa60-9a7f-4d5b-83c0-913085f5a8d2"), new DateTimeOffset(new DateTime(2023, 5, 31, 20, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("4f625719-05e4-4160-a569-1257accc88a8"), new Guid("56d1d48b-bd03-4b46-a390-f4ec9cfdb857"), new DateTimeOffset(new DateTime(2023, 6, 1, 11, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("50d1abed-aef4-460f-86fa-3985841fdca1"), new Guid("a195d90b-c882-4064-8aad-84c4acc1d052"), new DateTimeOffset(new DateTime(2023, 5, 28, 9, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("52563a96-6a6f-4fd4-8c20-329b2b90f817"), new Guid("6c03475d-cb91-4a8a-929d-0443e12c95e6"), new DateTimeOffset(new DateTime(2023, 5, 29, 4, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("530c3feb-f31c-4d7b-b358-671215132265"), new Guid("d54eca7f-77a4-40fb-9461-148fa1d4da61"), new DateTimeOffset(new DateTime(2023, 5, 31, 8, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("539d1027-bbc7-46d3-bfd0-27778dca9c5e"), new Guid("e394d158-c7db-411a-bdef-8525cb6c8124"), new DateTimeOffset(new DateTime(2023, 6, 2, 12, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("56d92b3e-8633-4271-9cf5-608c24aecd33"), new Guid("47b7371e-aa00-48bf-9bb9-471dc006b167"), new DateTimeOffset(new DateTime(2023, 5, 29, 0, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("5720965e-5893-45b9-9b7b-2827969c61fa"), new Guid("b470a6dd-1b9a-4713-b54c-4c60f4798e8a"), new DateTimeOffset(new DateTime(2023, 5, 29, 23, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("59041eac-4603-42d0-aa22-1219afb6951b"), new Guid("e15a5771-be6e-4161-a364-b0aab9f44b87"), new DateTimeOffset(new DateTime(2023, 5, 29, 20, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("5ad27957-55db-4ccf-9117-5333f4864ca7"), new Guid("f74e3f17-3660-4069-86f9-3717068f8c61"), new DateTimeOffset(new DateTime(2023, 5, 31, 23, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "FeatureId", "Time" },
                values: new object[,]
                {
                    { new Guid("5c6b53fa-099e-4709-b72e-0e86bbad3f01"), new Guid("90b44f5d-42f3-44d7-b872-58f702027922"), new DateTimeOffset(new DateTime(2023, 5, 28, 13, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("5dc06468-9726-4f30-bc98-68d618b83107"), new Guid("35d2d64a-fdb9-4246-8b50-11ce99de1de9"), new DateTimeOffset(new DateTime(2023, 5, 28, 21, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("5eb8c4a7-7bf8-4273-b5be-0f3e4ca4a7c3"), new Guid("5bf919c9-7a30-46d6-a212-29cc68d7db63"), new DateTimeOffset(new DateTime(2023, 5, 27, 17, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("6013a444-5187-43e0-a448-69400e1ad57b"), new Guid("457a37ab-5aa4-499e-a78d-ee385f53f8fe"), new DateTimeOffset(new DateTime(2023, 5, 29, 13, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("628e19a6-c185-4c34-8548-b83e47605e8d"), new Guid("62ec44fa-55bb-45a9-9d7d-cbef46ddefb4"), new DateTimeOffset(new DateTime(2023, 5, 31, 9, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("638b9981-b75e-4516-ac36-60da87878290"), new Guid("1e656672-2018-475e-85ca-b32c638721db"), new DateTimeOffset(new DateTime(2023, 6, 2, 8, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("64f199b1-fc60-46b1-8227-9bb21f0b1bba"), new Guid("089b0f1c-d725-4f8e-8a31-6ae2177b838e"), new DateTimeOffset(new DateTime(2023, 5, 28, 23, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("665636a8-20d8-47a4-bad2-3a8de67ff429"), new Guid("8fdf2cba-9297-4d12-980a-3d2fb1ea0535"), new DateTimeOffset(new DateTime(2023, 5, 31, 14, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("679a4317-a46e-4feb-966f-e7d5a0f36bb3"), new Guid("85089c74-64c5-4de3-b0cd-ddc6c7e8030e"), new DateTimeOffset(new DateTime(2023, 5, 30, 13, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("68bb9626-56e8-4c11-8b6e-0a64e46b24b5"), new Guid("8a722490-7e5b-4cd7-bb8b-493cc8db968f"), new DateTimeOffset(new DateTime(2023, 5, 31, 11, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("6958f15c-39a4-4dfd-8a0d-bf7333f4e85c"), new Guid("1e986fc7-9070-4f3f-a338-5c8f95f737ea"), new DateTimeOffset(new DateTime(2023, 5, 31, 10, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("6cdd1db7-3a69-4914-9346-9749d86b963d"), new Guid("daef3e13-71f6-4b19-b8ee-5463b0f02adf"), new DateTimeOffset(new DateTime(2023, 6, 2, 4, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("6ffd8cd7-e453-4bf8-a41b-b243b1023d33"), new Guid("554f7278-1f2c-43fd-a707-81fd627ea7c4"), new DateTimeOffset(new DateTime(2023, 5, 31, 19, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("70a19f73-e2dd-4f14-88bf-1cc76452bbc6"), new Guid("cca9f48b-afe0-4acf-b0cb-68beb8b1b6ee"), new DateTimeOffset(new DateTime(2023, 5, 28, 18, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("70b0a381-b520-445d-b04a-9c45d1a9c998"), new Guid("c70bbfdf-ee7e-43c9-96ef-dfd2048dcc39"), new DateTimeOffset(new DateTime(2023, 6, 2, 5, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("7204dac6-1474-4fd8-adfd-eff69171ac09"), new Guid("34567822-0817-4034-81e0-4f10d2e39a95"), new DateTimeOffset(new DateTime(2023, 5, 31, 12, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("72f43c51-434b-478f-97b4-145a7c457c7f"), new Guid("603d31d9-32c4-466a-b85d-5fbf44f1cdad"), new DateTimeOffset(new DateTime(2023, 5, 29, 11, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("73027887-4615-400d-b4e7-c07a15bcdebf"), new Guid("4f1f1833-1ba9-462b-a035-16082db7ee8a"), new DateTimeOffset(new DateTime(2023, 5, 29, 2, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("758b5347-34f4-4f7b-915f-d66ec0003366"), new Guid("9270e22d-357a-4aa8-9265-f4ea87edbad8"), new DateTimeOffset(new DateTime(2023, 5, 27, 18, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("7675d6cf-2a66-4d0f-ac45-4d1ba8427292"), new Guid("28da61e9-946e-42b7-a06c-4fad0d2ec650"), new DateTimeOffset(new DateTime(2023, 5, 31, 15, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("77b7b8da-0834-42e5-a974-a5ec733ac0b4"), new Guid("d2d0f43a-5363-4a97-9c34-2cccf92b5b91"), new DateTimeOffset(new DateTime(2023, 5, 31, 21, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("79a1774d-ca51-433f-9491-c279f0bce4d1"), new Guid("a2917c10-8afc-4ecb-a590-4e0b2689980c"), new DateTimeOffset(new DateTime(2023, 5, 29, 10, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("7b1768d1-5db9-4209-94ca-5bced83cfe9b"), new Guid("80a420d3-49c2-41f6-ac24-998a4a5de248"), new DateTimeOffset(new DateTime(2023, 5, 30, 17, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("8014e0e1-b517-435c-80e1-0cc3dd331919"), new Guid("52c75ec5-e149-4651-b379-8a7497ca9ddc"), new DateTimeOffset(new DateTime(2023, 5, 28, 0, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("817ae155-4b04-48e6-8284-5d33c8189676"), new Guid("592669ba-7818-4155-9157-f13ee29cb9fc"), new DateTimeOffset(new DateTime(2023, 5, 30, 0, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("8187f429-a23e-4a12-b5e6-3779af5d3aa3"), new Guid("94f4f7bb-09a8-4812-89e8-7a37efc2881e"), new DateTimeOffset(new DateTime(2023, 5, 30, 2, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("8597ee6f-4bb3-46ac-95e9-a6b668076d26"), new Guid("112867ad-9c07-41a0-bc86-97ee6068639e"), new DateTimeOffset(new DateTime(2023, 5, 31, 18, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("88480f2f-9586-4331-8ec2-d379f8779dc8"), new Guid("6244cd52-7943-4483-9606-ebae4f65c89d"), new DateTimeOffset(new DateTime(2023, 5, 28, 2, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("8edf2429-cc62-455e-a35c-6d5836515abf"), new Guid("3b814484-024c-4e44-bbea-34b215a2f1c7"), new DateTimeOffset(new DateTime(2023, 5, 29, 14, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("8f036f48-dd60-43f7-9c02-c2aaca6b85bf"), new Guid("dc6c1036-645f-4cef-973d-3e05d5f85bb0"), new DateTimeOffset(new DateTime(2023, 6, 2, 1, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("90dda153-38d0-47ac-b16a-d53bbabd9cda"), new Guid("0ba35bd0-cfef-495c-b0e6-9cddf3c4d2a8"), new DateTimeOffset(new DateTime(2023, 5, 30, 6, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("929e8ca7-f1ca-4e2e-99e3-464d271f114f"), new Guid("d5036ea9-af77-473d-886b-ef59135375b2"), new DateTimeOffset(new DateTime(2023, 6, 1, 6, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("94db4d52-d8d6-4108-bc34-f90814171781"), new Guid("e78b29b2-c0bd-4829-8ca2-e8ad64c0718a"), new DateTimeOffset(new DateTime(2023, 5, 29, 21, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("955a7ffc-e47d-46b8-97f3-d55cec6362ea"), new Guid("a86a7232-5820-4828-8b10-fa3d0ef7f3b1"), new DateTimeOffset(new DateTime(2023, 5, 30, 5, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("955a8e18-ab2c-4c85-8124-4ee17fb8103e"), new Guid("b4644622-db4f-4561-a2fa-52d888a1e831"), new DateTimeOffset(new DateTime(2023, 5, 29, 17, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("9b510be3-06d3-49a4-9312-2866c9ae4f78"), new Guid("10f5ed86-629d-44f4-ae5c-bedbcc8e1b03"), new DateTimeOffset(new DateTime(2023, 5, 28, 16, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("9d352f82-4bac-46b7-86b4-5fe6b6442c13"), new Guid("bfc58875-8717-4d77-a6d2-ed7fb3141c2f"), new DateTimeOffset(new DateTime(2023, 6, 1, 21, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("9e31592d-aa77-48f3-8de4-f094377a9036"), new Guid("df92bf22-f200-46f1-92fe-31dfbf46afba"), new DateTimeOffset(new DateTime(2023, 5, 31, 1, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("9e889e63-86f9-49da-8bc3-dfbd25dc35c0"), new Guid("6508042e-368e-4ca2-b388-0490bb329718"), new DateTimeOffset(new DateTime(2023, 5, 30, 22, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("a13cb001-5ddb-42d1-b481-399fd8550d5c"), new Guid("ee2fd752-0644-4aa3-8f44-243efcc94bba"), new DateTimeOffset(new DateTime(2023, 5, 30, 19, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("a58109c9-42d2-4006-aca7-262b7896b4d0"), new Guid("e5ee7fd0-8d29-4762-abc8-2625bf031f68"), new DateTimeOffset(new DateTime(2023, 6, 1, 4, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("a5b19938-16ad-4cc0-932a-8626d8f98761"), new Guid("aff9d74b-70cd-4f41-9422-e07afc84773a"), new DateTimeOffset(new DateTime(2023, 5, 30, 1, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "FeatureId", "Time" },
                values: new object[,]
                {
                    { new Guid("a6569dd7-80b3-4362-9d14-2b0c5a9bdb0b"), new Guid("d83e61e5-2154-438f-965b-970305132962"), new DateTimeOffset(new DateTime(2023, 5, 28, 6, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("a7bf41e2-8aa0-4d87-9090-93545272c435"), new Guid("378a7838-ea11-4701-9155-0ddeb400f3d6"), new DateTimeOffset(new DateTime(2023, 5, 29, 8, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("a986987d-209c-4fef-a2c1-1bbfe565adee"), new Guid("7c7fcdee-ffbc-48b0-a0d7-77606e373cf5"), new DateTimeOffset(new DateTime(2023, 5, 27, 20, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("aa4c38d8-842b-4e96-9ec0-f3037ec76d90"), new Guid("8d9f0d21-9775-46fa-a7d0-b98fb6e39132"), new DateTimeOffset(new DateTime(2023, 5, 31, 13, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("ab6234d1-6a65-4fe7-90a9-fdfe559b608b"), new Guid("675fd8ae-84f7-4f62-b106-2745acb02203"), new DateTimeOffset(new DateTime(2023, 5, 27, 22, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("aba3a111-4454-4eae-a29b-46515d53f3e9"), new Guid("f79019bb-0469-44a8-87c5-432ad873c4e0"), new DateTimeOffset(new DateTime(2023, 5, 28, 19, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("af2d95ee-154d-403e-83a7-637ddca3a9d8"), new Guid("48f10e2b-37fe-4fa1-8f41-d2af6ab6083e"), new DateTimeOffset(new DateTime(2023, 5, 31, 16, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("b72089db-28ff-4884-821b-9e48026b6988"), new Guid("00630a3c-d3b3-4c4a-81e4-3aa1706c7569"), new DateTimeOffset(new DateTime(2023, 5, 28, 4, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("b72898b5-900d-4cfd-a4c2-da9e0f3c84ed"), new Guid("72694e83-6783-4c19-a9f3-c0e411fed83f"), new DateTimeOffset(new DateTime(2023, 5, 28, 12, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("b7806d93-1f80-4fa1-9d42-c32489fd8b8c"), new Guid("5e9a6774-f600-4bf5-8536-a0008a44f817"), new DateTimeOffset(new DateTime(2023, 6, 2, 7, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("b872072b-24a6-4f13-ba85-52575632e58d"), new Guid("47aabfed-3667-43ed-a160-ebc547f007f9"), new DateTimeOffset(new DateTime(2023, 5, 28, 8, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("bb00525a-b81c-4e77-a8b7-bbd722e2b3a4"), new Guid("29f13e76-9dda-444c-97dc-37fde2d1728b"), new DateTimeOffset(new DateTime(2023, 5, 28, 1, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("bd05689b-38ee-4ff6-9d05-d2f39bff15c1"), new Guid("63f57361-ecb2-4751-9f7e-175c15ae3ad6"), new DateTimeOffset(new DateTime(2023, 5, 30, 7, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("bf763774-de4d-4af3-84ea-fb5945b9b5a8"), new Guid("8452fca6-3b42-4269-a062-0a1beff059c4"), new DateTimeOffset(new DateTime(2023, 5, 29, 19, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("bf84dd42-90ee-4586-955a-3bbb10a41e37"), new Guid("429f15b2-db05-4523-9716-c9e374746c40"), new DateTimeOffset(new DateTime(2023, 6, 2, 11, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c155000f-26ec-4e64-9ce6-5dc4a8cba537"), new Guid("dac5adbe-11a1-4915-b63a-ebcb9c1c9d6d"), new DateTimeOffset(new DateTime(2023, 5, 30, 9, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c236604c-ef13-48dd-84f9-fa8591b3665d"), new Guid("2c06d6aa-e531-4270-ae2e-0ea7265011d6"), new DateTimeOffset(new DateTime(2023, 5, 29, 9, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c309ab85-ddbe-44e5-8365-f8c65b5e2363"), new Guid("0e7447f4-c507-43a1-abd2-f43d0a43ed8b"), new DateTimeOffset(new DateTime(2023, 6, 2, 9, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c3617398-5265-45f7-bb81-650d99b6c22b"), new Guid("59669e59-5e12-489b-bf4e-357f6d041276"), new DateTimeOffset(new DateTime(2023, 5, 31, 0, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c48e7ab1-b56a-47a3-be49-43643dca313e"), new Guid("4d9019a5-e6ed-4556-96cd-2879fcebf403"), new DateTimeOffset(new DateTime(2023, 5, 29, 5, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c4efe9b1-ffb1-45be-ba57-8e06b93f8f85"), new Guid("6fb5589d-4e02-40de-9543-719a8f7e0d1a"), new DateTimeOffset(new DateTime(2023, 5, 29, 7, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c6e2da0b-71fc-4318-8e8e-946f0ca2c897"), new Guid("faf7f64b-a177-43ab-9e6a-e212a414b336"), new DateTimeOffset(new DateTime(2023, 5, 27, 21, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c79e0ab9-ce15-419e-b1be-c3022db2477e"), new Guid("c2377785-9f70-47f1-8ccf-f5c3cc6526de"), new DateTimeOffset(new DateTime(2023, 5, 29, 22, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c853bd0b-da52-4552-a4df-3b54d1df9d88"), new Guid("e59e5fda-6afb-452f-995f-1eaa085b72cf"), new DateTimeOffset(new DateTime(2023, 5, 31, 4, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("c91e2e4e-54be-48df-b3d2-dbef8b1b5a95"), new Guid("fbda8370-272f-4b1a-803b-13b3d3ac2ebb"), new DateTimeOffset(new DateTime(2023, 6, 2, 13, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("cadb92f1-c645-45b2-8b0b-89aed653fe90"), new Guid("7aca389f-2a0c-4677-b0c3-93592cc1b8f2"), new DateTimeOffset(new DateTime(2023, 5, 29, 12, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("cc1de8a6-6f71-4dbb-a9e5-303268f8e278"), new Guid("511f3ce3-d6e4-4513-9d15-83689864b59c"), new DateTimeOffset(new DateTime(2023, 6, 1, 10, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("cd962c96-8ad0-422b-9bd0-430a26062ec2"), new Guid("64823003-2325-458e-a844-fdfc87e99b01"), new DateTimeOffset(new DateTime(2023, 6, 2, 14, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("cde2b468-3bec-4967-bf24-f5e4a288b06e"), new Guid("eadd4334-7766-4fdb-87b2-00691d30df48"), new DateTimeOffset(new DateTime(2023, 5, 31, 6, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("ce72b7c4-bcc7-40d0-ba1e-3fecfc33e3a3"), new Guid("9127008a-8ecc-41ed-bf7c-349c027374f1"), new DateTimeOffset(new DateTime(2023, 6, 1, 8, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("cfc54703-6288-4cec-a1c9-b867f228f0fb"), new Guid("ab11af31-5655-4352-aea3-fa95ef6d8535"), new DateTimeOffset(new DateTime(2023, 5, 28, 20, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("d03a334d-68d8-4f78-b8ec-f031cb0d418a"), new Guid("7d9ff21b-3140-4ff6-8470-107fca7e8a3a"), new DateTimeOffset(new DateTime(2023, 6, 1, 2, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("d0e3440c-94a2-46b3-aba4-3560e5ea0a21"), new Guid("4b0da327-924e-4508-b4a6-a78034635813"), new DateTimeOffset(new DateTime(2023, 5, 28, 22, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("d21a1f1b-e640-4266-be43-4a610ff8feb2"), new Guid("5bded403-44f5-4d94-a78d-8d677052456d"), new DateTimeOffset(new DateTime(2023, 5, 28, 15, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("d3b91d72-3d9b-43d5-9981-0f89c614ce41"), new Guid("bf2519f8-00a0-4817-b7a5-c3cd1dabf490"), new DateTimeOffset(new DateTime(2023, 6, 1, 7, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("d56e2e6a-a548-4f80-9a38-103e1f488cc7"), new Guid("4825d855-1c0c-432a-940f-3111b764ab44"), new DateTimeOffset(new DateTime(2023, 6, 1, 18, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("d81a24cb-3849-46ec-8a78-8c1f5810ff64"), new Guid("1f0d32b9-f634-4944-a056-4538058ac85d"), new DateTimeOffset(new DateTime(2023, 5, 27, 23, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("da95b72e-665d-4478-a674-4564e4474477"), new Guid("b18951dc-6144-48d7-8124-8e157f7ecc9a"), new DateTimeOffset(new DateTime(2023, 5, 31, 17, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("dc2949ba-06bc-42fe-8025-a0821cd0501c"), new Guid("dee509c7-e6e2-405d-bba4-3bb7ca6db74c"), new DateTimeOffset(new DateTime(2023, 5, 30, 21, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("dd7b46e9-c598-4a8d-bea4-08344e9538d2"), new Guid("7319bff5-dd2d-47b0-9b93-908330cd42c3"), new DateTimeOffset(new DateTime(2023, 6, 1, 20, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("e317abb6-0ffd-4ca2-b627-debc5231348f"), new Guid("81373a79-0d3e-4b44-973d-edd02edfc6ac"), new DateTimeOffset(new DateTime(2023, 6, 1, 15, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("e37c2b08-319c-4848-8b39-8c5678f34b6f"), new Guid("770a2ee4-4d38-46e4-be0d-29bdbe13cbb9"), new DateTimeOffset(new DateTime(2023, 6, 1, 22, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "FeatureId", "Time" },
                values: new object[,]
                {
                    { new Guid("e38b5170-cbcf-4c91-bb57-6be22b436efd"), new Guid("54bdaa07-c3c9-411c-bc1b-bc1a9e99f858"), new DateTimeOffset(new DateTime(2023, 5, 30, 11, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("e440a777-bcec-4d6e-8f3b-ee0dc4b8de39"), new Guid("93a1d218-d73d-428a-aff2-9dc32abfeb00"), new DateTimeOffset(new DateTime(2023, 6, 2, 15, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("e76dd8d4-6a2f-45a0-b5ee-f017e54c7378"), new Guid("8076cc10-ca8e-4d15-8290-6f27509410f9"), new DateTimeOffset(new DateTime(2023, 5, 29, 1, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("e8b9caf6-d4bb-4f1e-a91b-9ecaa30df646"), new Guid("8252d607-c46e-42af-8aff-cbb34cf12a57"), new DateTimeOffset(new DateTime(2023, 5, 31, 7, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("e9d1273d-8b1a-4386-ac83-2f71ba6196ee"), new Guid("eac36c55-c9d9-4d3e-8ce4-4f2d9b0ec707"), new DateTimeOffset(new DateTime(2023, 6, 2, 6, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("eae9fdb0-7672-49db-8e08-06cbd8fcf86f"), new Guid("affd3c7e-6e9f-4bab-aa79-91ac7b66cf83"), new DateTimeOffset(new DateTime(2023, 6, 1, 13, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("eb492ee7-333d-4a12-9f79-1979b5146b13"), new Guid("28d68c2e-6a56-4d31-8df6-4457a150294f"), new DateTimeOffset(new DateTime(2023, 5, 28, 11, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("eca7d3e8-1ae1-42f5-b0c3-d7a5c02d7a06"), new Guid("3cb06e8f-9fa2-47ab-ab9f-fe9dcbe1cf9c"), new DateTimeOffset(new DateTime(2023, 6, 1, 19, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("edaeb3bd-7931-4095-b512-8af43a2c20d5"), new Guid("0617753d-72d1-4d48-b607-21fafae31a79"), new DateTimeOffset(new DateTime(2023, 5, 28, 14, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("edf05a50-c22b-4df3-8509-2bcbb87f02f7"), new Guid("04379702-0f62-4436-9801-d5b382fb8335"), new DateTimeOffset(new DateTime(2023, 5, 28, 17, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("f1105830-42e6-4c0f-8d0a-86d63d5a5dad"), new Guid("27e6d32e-5f17-47b9-bd1b-9c2f215ed6a2"), new DateTimeOffset(new DateTime(2023, 5, 31, 5, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("f2e21362-4d93-426d-b5cd-b29122142a2d"), new Guid("180ff924-6ca3-414b-8abf-78d4f23cdc64"), new DateTimeOffset(new DateTime(2023, 6, 1, 12, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("f5ce0170-edaa-4665-a227-226b2e8415ff"), new Guid("528a4659-4144-45fe-8aac-8005541ccea9"), new DateTimeOffset(new DateTime(2023, 5, 30, 14, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("f7e1e3fb-f1ab-45a1-82bd-866c368e2714"), new Guid("8bb24261-14f2-4c73-91b7-771d7f830239"), new DateTimeOffset(new DateTime(2023, 5, 30, 18, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("fb74d9f6-e71c-494d-93cc-8f7e18531edd"), new Guid("cc67cd3a-507c-4869-b680-918aee36dfb3"), new DateTimeOffset(new DateTime(2023, 6, 2, 3, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("fca3513a-9b08-4660-a452-2dff74327561"), new Guid("726a9ff5-9f40-4af8-ab7a-4380dcc34307"), new DateTimeOffset(new DateTime(2023, 6, 2, 0, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("fde810c0-38c5-47f9-984f-b755e6f8f8ad"), new Guid("9b3667b3-d4b1-4938-bc6c-63fa9791d55a"), new DateTimeOffset(new DateTime(2023, 5, 30, 8, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) },
                    { new Guid("ff379a39-8b6d-468f-b962-14eca2a2a8a8"), new Guid("5f7b562e-6490-4d97-ba9f-5a539161290d"), new DateTimeOffset(new DateTime(2023, 5, 29, 6, 21, 50, 198, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 5, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_FeatureId",
                table: "Forecasts",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_ForecastId",
                table: "Predictions",
                column: "ForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_WarningId",
                table: "Predictions",
                column: "WarningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Predictions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.DropTable(
                name: "Warnings");

            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
