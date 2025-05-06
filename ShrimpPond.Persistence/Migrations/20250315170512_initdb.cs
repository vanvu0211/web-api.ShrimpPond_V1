using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    farmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    farmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.farmId);
                });

            migrationBuilder.CreateTable(
                name: "CleanSensor",
                columns: table => new
                {
                    cleanSensorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cleanTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleanSensor", x => x.cleanSensorId);
                    table.ForeignKey(
                        name: "FK_CleanSensor_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    foodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.foodId);
                    table.ForeignKey(
                        name: "FK_Food_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    medicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.medicineId);
                    table.ForeignKey(
                        name: "FK_Medicine_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "PondType",
                columns: table => new
                {
                    pondTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pondTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PondType", x => x.pondTypeId);
                    table.ForeignKey(
                        name: "FK_PondType_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "TimeSettings",
                columns: table => new
                {
                    timeSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSettings", x => x.timeSettingId);
                    table.ForeignKey(
                        name: "FK_TimeSettings_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateTable(
                name: "Pond",
                columns: table => new
                {
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deep = table.Column<float>(type: "real", nullable: false),
                    diameter = table.Column<float>(type: "real", nullable: false),
                    pondTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    originPondId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seedId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amountShrimp = table.Column<float>(type: "real", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pond", x => x.pondId);
                    table.ForeignKey(
                        name: "FK_Pond_PondType_pondTypeId",
                        column: x => x.pondTypeId,
                        principalTable: "PondType",
                        principalColumn: "pondTypeId");
                });

            migrationBuilder.CreateTable(
                name: "timeSettingObjects",
                columns: table => new
                {
                    timeSettingObjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    index = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeSettingObjects", x => x.timeSettingObjectId);
                    table.ForeignKey(
                        name: "FK_timeSettingObjects_TimeSettings_timeSettingId",
                        column: x => x.timeSettingId,
                        principalTable: "TimeSettings",
                        principalColumn: "timeSettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentStatus",
                columns: table => new
                {
                    environmentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentStatus", x => x.environmentStatusId);
                    table.ForeignKey(
                        name: "FK_EnvironmentStatus_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "FoodFeeding",
                columns: table => new
                {
                    foodFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodFeeding", x => x.foodFeedingId);
                    table.ForeignKey(
                        name: "FK_FoodFeeding_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "Harvests",
                columns: table => new
                {
                    harvestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    harvestTime = table.Column<int>(type: "int", nullable: false),
                    harvestType = table.Column<int>(type: "int", nullable: false),
                    seedId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<float>(type: "real", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    harvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harvests", x => x.harvestId);
                    table.ForeignKey(
                        name: "FK_Harvests_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Harvests_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "LossShrimp",
                columns: table => new
                {
                    lossShrimpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lossValue = table.Column<float>(type: "real", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossShrimp", x => x.lossShrimpId);
                    table.ForeignKey(
                        name: "FK_LossShrimp_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "MedicineFeeding",
                columns: table => new
                {
                    medicineFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineFeeding", x => x.medicineFeedingId);
                    table.ForeignKey(
                        name: "FK_MedicineFeeding_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "SizeShrimp",
                columns: table => new
                {
                    sizeShrimpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sizeValue = table.Column<float>(type: "real", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeShrimp", x => x.sizeShrimpId);
                    table.ForeignKey(
                        name: "FK_SizeShrimp_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "FoodForFeeding",
                columns: table => new
                {
                    foodForFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    foodFeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodForFeeding", x => x.foodForFeedingId);
                    table.ForeignKey(
                        name: "FK_FoodForFeeding_FoodFeeding_foodFeedingId",
                        column: x => x.foodFeedingId,
                        principalTable: "FoodFeeding",
                        principalColumn: "foodFeedingId");
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    certificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    certificateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    pondId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    harvestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.certificateId);
                    table.ForeignKey(
                        name: "FK_Certificate_Harvests_harvestId",
                        column: x => x.harvestId,
                        principalTable: "Harvests",
                        principalColumn: "harvestId");
                    table.ForeignKey(
                        name: "FK_Certificate_Pond_pondId",
                        column: x => x.pondId,
                        principalTable: "Pond",
                        principalColumn: "pondId");
                });

            migrationBuilder.CreateTable(
                name: "MedicineForFeeding",
                columns: table => new
                {
                    medicineForFeedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    medicineFeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineForFeeding", x => x.medicineForFeedingId);
                    table.ForeignKey(
                        name: "FK_MedicineForFeeding_MedicineFeeding_medicineFeedingId",
                        column: x => x.medicineFeedingId,
                        principalTable: "MedicineFeeding",
                        principalColumn: "medicineFeedingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_harvestId",
                table: "Certificate",
                column: "harvestId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_pondId",
                table: "Certificate",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_CleanSensor_farmId",
                table: "CleanSensor",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentStatus_pondId",
                table: "EnvironmentStatus",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_farmId",
                table: "Food",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodFeeding_pondId",
                table: "FoodFeeding",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodForFeeding_foodFeedingId",
                table: "FoodForFeeding",
                column: "foodFeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvests_farmId",
                table: "Harvests",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvests_pondId",
                table: "Harvests",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_LossShrimp_pondId",
                table: "LossShrimp",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_farmId",
                table: "Medicine",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineFeeding_pondId",
                table: "MedicineFeeding",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineForFeeding_medicineFeedingId",
                table: "MedicineForFeeding",
                column: "medicineFeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Pond_pondTypeId",
                table: "Pond",
                column: "pondTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PondType_farmId",
                table: "PondType",
                column: "farmId");

            migrationBuilder.CreateIndex(
                name: "IX_SizeShrimp_pondId",
                table: "SizeShrimp",
                column: "pondId");

            migrationBuilder.CreateIndex(
                name: "IX_timeSettingObjects_timeSettingId",
                table: "timeSettingObjects",
                column: "timeSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSettings_farmId",
                table: "TimeSettings",
                column: "farmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "CleanSensor");

            migrationBuilder.DropTable(
                name: "EnvironmentStatus");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "FoodForFeeding");

            migrationBuilder.DropTable(
                name: "LossShrimp");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "MedicineForFeeding");

            migrationBuilder.DropTable(
                name: "SizeShrimp");

            migrationBuilder.DropTable(
                name: "timeSettingObjects");

            migrationBuilder.DropTable(
                name: "Harvests");

            migrationBuilder.DropTable(
                name: "FoodFeeding");

            migrationBuilder.DropTable(
                name: "MedicineFeeding");

            migrationBuilder.DropTable(
                name: "TimeSettings");

            migrationBuilder.DropTable(
                name: "Pond");

            migrationBuilder.DropTable(
                name: "PondType");

            migrationBuilder.DropTable(
                name: "Farms");
        }
    }
}
