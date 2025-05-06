using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarm_Farms_farmId",
                table: "Alarm");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Harvests_harvestId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Pond_pondId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_CleanSensor_Farms_farmId",
                table: "CleanSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentStatus_Pond_pondId",
                table: "EnvironmentStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Farms_farmId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodFeeding_Pond_pondId",
                table: "FoodFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodForFeeding_FoodFeeding_foodFeedingId",
                table: "FoodForFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_Farms_farmId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_Pond_pondId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_LossShrimp_Pond_pondId",
                table: "LossShrimp");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Farms_farmId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Farms_farmId",
                table: "Medicine");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineFeeding_Pond_pondId",
                table: "MedicineFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineForFeeding_MedicineFeeding_medicineFeedingId",
                table: "MedicineForFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_Pond_PondType_pondTypeId",
                table: "Pond");

            migrationBuilder.DropForeignKey(
                name: "FK_PondIds_Machines_machineId",
                table: "PondIds");

            migrationBuilder.DropForeignKey(
                name: "FK_PondType_Farms_farmId",
                table: "PondType");

            migrationBuilder.DropForeignKey(
                name: "FK_SizeShrimp_Pond_pondId",
                table: "SizeShrimp");

            migrationBuilder.DropForeignKey(
                name: "FK_timeSettingObjects_TimeSettings_timeSettingId",
                table: "timeSettingObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSettings_Farms_farmId",
                table: "TimeSettings");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "TimeSettings",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "enableFarm",
                table: "TimeSettings",
                newName: "EnableFarm");

            migrationBuilder.RenameColumn(
                name: "timeSettingId",
                table: "TimeSettings",
                newName: "TimeSettingId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSettings_farmId",
                table: "TimeSettings",
                newName: "IX_TimeSettings_FarmId");

            migrationBuilder.RenameColumn(
                name: "timeSettingId",
                table: "timeSettingObjects",
                newName: "TimeSettingId");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "timeSettingObjects",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "index",
                table: "timeSettingObjects",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "timeSettingObjectId",
                table: "timeSettingObjects",
                newName: "TimeSettingObjectId");

            migrationBuilder.RenameIndex(
                name: "IX_timeSettingObjects_timeSettingId",
                table: "timeSettingObjects",
                newName: "IX_timeSettingObjects_TimeSettingId");

            migrationBuilder.RenameColumn(
                name: "updateDate",
                table: "SizeShrimp",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "sizeValue",
                table: "SizeShrimp",
                newName: "SizeValue");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "SizeShrimp",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "sizeShrimpId",
                table: "SizeShrimp",
                newName: "SizeShrimpId");

            migrationBuilder.RenameIndex(
                name: "IX_SizeShrimp_pondId",
                table: "SizeShrimp",
                newName: "IX_SizeShrimp_PondId");

            migrationBuilder.RenameColumn(
                name: "pondTypeName",
                table: "PondType",
                newName: "PondTypeName");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "PondType",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "pondTypeId",
                table: "PondType",
                newName: "PondTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_PondType_farmId",
                table: "PondType",
                newName: "IX_PondType_FarmId");

            migrationBuilder.RenameColumn(
                name: "pondName",
                table: "PondIds",
                newName: "PondName");

            migrationBuilder.RenameColumn(
                name: "machineId",
                table: "PondIds",
                newName: "MachineId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PondIds",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "PondIds",
                newName: "PondIdForMachine");

            migrationBuilder.RenameIndex(
                name: "IX_PondIds_machineId",
                table: "PondIds",
                newName: "IX_PondIds_MachineId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Pond",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Pond",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "seedName",
                table: "Pond",
                newName: "SeedName");

            migrationBuilder.RenameColumn(
                name: "seedId",
                table: "Pond",
                newName: "SeedId");

            migrationBuilder.RenameColumn(
                name: "pondTypeId",
                table: "Pond",
                newName: "PondTypeId");

            migrationBuilder.RenameColumn(
                name: "pondName",
                table: "Pond",
                newName: "PondName");

            migrationBuilder.RenameColumn(
                name: "originPondId",
                table: "Pond",
                newName: "OriginPondId");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Pond",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "diameter",
                table: "Pond",
                newName: "Diameter");

            migrationBuilder.RenameColumn(
                name: "deep",
                table: "Pond",
                newName: "Deep");

            migrationBuilder.RenameColumn(
                name: "amountShrimp",
                table: "Pond",
                newName: "AmountShrimp");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "Pond",
                newName: "PondId");

            migrationBuilder.RenameIndex(
                name: "IX_Pond_pondTypeId",
                table: "Pond",
                newName: "IX_Pond_PondTypeId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "MedicineForFeeding",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "medicineFeedingId",
                table: "MedicineForFeeding",
                newName: "MedicineFeedingId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "MedicineForFeeding",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "medicineForFeedingId",
                table: "MedicineForFeeding",
                newName: "MedicineForFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineForFeeding_medicineFeedingId",
                table: "MedicineForFeeding",
                newName: "IX_MedicineForFeeding_MedicineFeedingId");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "MedicineFeeding",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "feedingDate",
                table: "MedicineFeeding",
                newName: "FeedingDate");

            migrationBuilder.RenameColumn(
                name: "medicineFeedingId",
                table: "MedicineFeeding",
                newName: "MedicineFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineFeeding_pondId",
                table: "MedicineFeeding",
                newName: "IX_MedicineFeeding_PondId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Medicine",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Medicine",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "medicineId",
                table: "Medicine",
                newName: "MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_farmId",
                table: "Medicine",
                newName: "IX_Medicine_FarmId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Machines",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "machineName",
                table: "Machines",
                newName: "MachineName");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Machines",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "machineId",
                table: "Machines",
                newName: "MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_farmId",
                table: "Machines",
                newName: "IX_Machines_FarmId");

            migrationBuilder.RenameColumn(
                name: "updateDate",
                table: "LossShrimp",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "LossShrimp",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "lossValue",
                table: "LossShrimp",
                newName: "LossValue");

            migrationBuilder.RenameColumn(
                name: "lossShrimpId",
                table: "LossShrimp",
                newName: "LossShrimpId");

            migrationBuilder.RenameIndex(
                name: "IX_LossShrimp_pondId",
                table: "LossShrimp",
                newName: "IX_LossShrimp_PondId");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Harvests",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "seedId",
                table: "Harvests",
                newName: "SeedId");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "Harvests",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "harvestType",
                table: "Harvests",
                newName: "HarvestType");

            migrationBuilder.RenameColumn(
                name: "harvestTime",
                table: "Harvests",
                newName: "HarvestTime");

            migrationBuilder.RenameColumn(
                name: "harvestDate",
                table: "Harvests",
                newName: "HarvestDate");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Harvests",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Harvests",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "harvestId",
                table: "Harvests",
                newName: "HarvestId");

            migrationBuilder.RenameIndex(
                name: "IX_Harvests_pondId",
                table: "Harvests",
                newName: "IX_Harvests_PondId");

            migrationBuilder.RenameIndex(
                name: "IX_Harvests_farmId",
                table: "Harvests",
                newName: "IX_Harvests_FarmId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "FoodForFeeding",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "foodFeedingId",
                table: "FoodForFeeding",
                newName: "FoodFeedingId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "FoodForFeeding",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "foodForFeedingId",
                table: "FoodForFeeding",
                newName: "FoodForFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodForFeeding_foodFeedingId",
                table: "FoodForFeeding",
                newName: "IX_FoodForFeeding_FoodFeedingId");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "FoodFeeding",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "feedingDate",
                table: "FoodFeeding",
                newName: "FeedingDate");

            migrationBuilder.RenameColumn(
                name: "foodFeedingId",
                table: "FoodFeeding",
                newName: "FoodFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodFeeding_pondId",
                table: "FoodFeeding",
                newName: "IX_FoodFeeding_PondId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Food",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Food",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "foodId",
                table: "Food",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_farmId",
                table: "Food",
                newName: "IX_Food_FarmId");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Farms",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "farmName",
                table: "Farms",
                newName: "FarmName");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Farms",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Farms",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "EnvironmentStatus",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "EnvironmentStatus",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "EnvironmentStatus",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "EnvironmentStatus",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "environmentStatusId",
                table: "EnvironmentStatus",
                newName: "EnvironmentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EnvironmentStatus_pondId",
                table: "EnvironmentStatus",
                newName: "IX_EnvironmentStatus_PondId");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "CleanSensor",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "cleanTime",
                table: "CleanSensor",
                newName: "CleanTime");

            migrationBuilder.RenameColumn(
                name: "cleanSensorId",
                table: "CleanSensor",
                newName: "CleanSensorId");

            migrationBuilder.RenameIndex(
                name: "IX_CleanSensor_farmId",
                table: "CleanSensor",
                newName: "IX_CleanSensor_FarmId");

            migrationBuilder.RenameColumn(
                name: "pondId",
                table: "Certificate",
                newName: "PondId");

            migrationBuilder.RenameColumn(
                name: "harvestId",
                table: "Certificate",
                newName: "HarvestId");

            migrationBuilder.RenameColumn(
                name: "fileData",
                table: "Certificate",
                newName: "FileData");

            migrationBuilder.RenameColumn(
                name: "certificateName",
                table: "Certificate",
                newName: "CertificateName");

            migrationBuilder.RenameColumn(
                name: "certificateId",
                table: "Certificate",
                newName: "CertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_pondId",
                table: "Certificate",
                newName: "IX_Certificate_PondId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_harvestId",
                table: "Certificate",
                newName: "IX_Certificate_HarvestId");

            migrationBuilder.RenameColumn(
                name: "farmId",
                table: "Alarm",
                newName: "FarmId");

            migrationBuilder.RenameIndex(
                name: "IX_Alarm_farmId",
                table: "Alarm",
                newName: "IX_Alarm_FarmId");

            migrationBuilder.AlterColumn<string>(
                name: "OriginPondId",
                table: "Pond",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarm_Farms_FarmId",
                table: "Alarm",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Harvests_HarvestId",
                table: "Certificate",
                column: "HarvestId",
                principalTable: "Harvests",
                principalColumn: "HarvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Pond_PondId",
                table: "Certificate",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleanSensor_Farms_FarmId",
                table: "CleanSensor",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentStatus_Pond_PondId",
                table: "EnvironmentStatus",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Farms_FarmId",
                table: "Food",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodFeeding_Pond_PondId",
                table: "FoodFeeding",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodForFeeding_FoodFeeding_FoodFeedingId",
                table: "FoodForFeeding",
                column: "FoodFeedingId",
                principalTable: "FoodFeeding",
                principalColumn: "FoodFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_Farms_FarmId",
                table: "Harvests",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_Pond_PondId",
                table: "Harvests",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_LossShrimp_Pond_PondId",
                table: "LossShrimp",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Farms_FarmId",
                table: "Machines",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Farms_FarmId",
                table: "Medicine",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineFeeding_Pond_PondId",
                table: "MedicineFeeding",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineForFeeding_MedicineFeeding_MedicineFeedingId",
                table: "MedicineForFeeding",
                column: "MedicineFeedingId",
                principalTable: "MedicineFeeding",
                principalColumn: "MedicineFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pond_PondType_PondTypeId",
                table: "Pond",
                column: "PondTypeId",
                principalTable: "PondType",
                principalColumn: "PondTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PondIds_Machines_MachineId",
                table: "PondIds",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PondType_Farms_FarmId",
                table: "PondType",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_SizeShrimp_Pond_PondId",
                table: "SizeShrimp",
                column: "PondId",
                principalTable: "Pond",
                principalColumn: "PondId");

            migrationBuilder.AddForeignKey(
                name: "FK_timeSettingObjects_TimeSettings_TimeSettingId",
                table: "timeSettingObjects",
                column: "TimeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "TimeSettingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSettings_Farms_FarmId",
                table: "TimeSettings",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarm_Farms_FarmId",
                table: "Alarm");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Harvests_HarvestId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Pond_PondId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_CleanSensor_Farms_FarmId",
                table: "CleanSensor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentStatus_Pond_PondId",
                table: "EnvironmentStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Farms_FarmId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodFeeding_Pond_PondId",
                table: "FoodFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodForFeeding_FoodFeeding_FoodFeedingId",
                table: "FoodForFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_Farms_FarmId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_Pond_PondId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_LossShrimp_Pond_PondId",
                table: "LossShrimp");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Farms_FarmId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Farms_FarmId",
                table: "Medicine");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineFeeding_Pond_PondId",
                table: "MedicineFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineForFeeding_MedicineFeeding_MedicineFeedingId",
                table: "MedicineForFeeding");

            migrationBuilder.DropForeignKey(
                name: "FK_Pond_PondType_PondTypeId",
                table: "Pond");

            migrationBuilder.DropForeignKey(
                name: "FK_PondIds_Machines_MachineId",
                table: "PondIds");

            migrationBuilder.DropForeignKey(
                name: "FK_PondType_Farms_FarmId",
                table: "PondType");

            migrationBuilder.DropForeignKey(
                name: "FK_SizeShrimp_Pond_PondId",
                table: "SizeShrimp");

            migrationBuilder.DropForeignKey(
                name: "FK_timeSettingObjects_TimeSettings_TimeSettingId",
                table: "timeSettingObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSettings_Farms_FarmId",
                table: "TimeSettings");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "TimeSettings",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "EnableFarm",
                table: "TimeSettings",
                newName: "enableFarm");

            migrationBuilder.RenameColumn(
                name: "TimeSettingId",
                table: "TimeSettings",
                newName: "timeSettingId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSettings_FarmId",
                table: "TimeSettings",
                newName: "IX_TimeSettings_farmId");

            migrationBuilder.RenameColumn(
                name: "TimeSettingId",
                table: "timeSettingObjects",
                newName: "timeSettingId");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "timeSettingObjects",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "timeSettingObjects",
                newName: "index");

            migrationBuilder.RenameColumn(
                name: "TimeSettingObjectId",
                table: "timeSettingObjects",
                newName: "timeSettingObjectId");

            migrationBuilder.RenameIndex(
                name: "IX_timeSettingObjects_TimeSettingId",
                table: "timeSettingObjects",
                newName: "IX_timeSettingObjects_timeSettingId");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "SizeShrimp",
                newName: "updateDate");

            migrationBuilder.RenameColumn(
                name: "SizeValue",
                table: "SizeShrimp",
                newName: "sizeValue");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "SizeShrimp",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "SizeShrimpId",
                table: "SizeShrimp",
                newName: "sizeShrimpId");

            migrationBuilder.RenameIndex(
                name: "IX_SizeShrimp_PondId",
                table: "SizeShrimp",
                newName: "IX_SizeShrimp_pondId");

            migrationBuilder.RenameColumn(
                name: "PondTypeName",
                table: "PondType",
                newName: "pondTypeName");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "PondType",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "PondTypeId",
                table: "PondType",
                newName: "pondTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_PondType_FarmId",
                table: "PondType",
                newName: "IX_PondType_farmId");

            migrationBuilder.RenameColumn(
                name: "PondName",
                table: "PondIds",
                newName: "pondName");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "PondIds",
                newName: "machineId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PondIds",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PondIdForMachine",
                table: "PondIds",
                newName: "pondId");

            migrationBuilder.RenameIndex(
                name: "IX_PondIds_MachineId",
                table: "PondIds",
                newName: "IX_PondIds_machineId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pond",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Pond",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "SeedName",
                table: "Pond",
                newName: "seedName");

            migrationBuilder.RenameColumn(
                name: "SeedId",
                table: "Pond",
                newName: "seedId");

            migrationBuilder.RenameColumn(
                name: "PondTypeId",
                table: "Pond",
                newName: "pondTypeId");

            migrationBuilder.RenameColumn(
                name: "PondName",
                table: "Pond",
                newName: "pondName");

            migrationBuilder.RenameColumn(
                name: "OriginPondId",
                table: "Pond",
                newName: "originPondId");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Pond",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "Diameter",
                table: "Pond",
                newName: "diameter");

            migrationBuilder.RenameColumn(
                name: "Deep",
                table: "Pond",
                newName: "deep");

            migrationBuilder.RenameColumn(
                name: "AmountShrimp",
                table: "Pond",
                newName: "amountShrimp");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "Pond",
                newName: "pondId");

            migrationBuilder.RenameIndex(
                name: "IX_Pond_PondTypeId",
                table: "Pond",
                newName: "IX_Pond_pondTypeId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MedicineForFeeding",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MedicineFeedingId",
                table: "MedicineForFeeding",
                newName: "medicineFeedingId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "MedicineForFeeding",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "MedicineForFeedingId",
                table: "MedicineForFeeding",
                newName: "medicineForFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineForFeeding_MedicineFeedingId",
                table: "MedicineForFeeding",
                newName: "IX_MedicineForFeeding_medicineFeedingId");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "MedicineFeeding",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "FeedingDate",
                table: "MedicineFeeding",
                newName: "feedingDate");

            migrationBuilder.RenameColumn(
                name: "MedicineFeedingId",
                table: "MedicineFeeding",
                newName: "medicineFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineFeeding_PondId",
                table: "MedicineFeeding",
                newName: "IX_MedicineFeeding_pondId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Medicine",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Medicine",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "Medicine",
                newName: "medicineId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_FarmId",
                table: "Medicine",
                newName: "IX_Medicine_farmId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Machines",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "MachineName",
                table: "Machines",
                newName: "machineName");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Machines",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "Machines",
                newName: "machineId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_FarmId",
                table: "Machines",
                newName: "IX_Machines_farmId");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "LossShrimp",
                newName: "updateDate");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "LossShrimp",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "LossValue",
                table: "LossShrimp",
                newName: "lossValue");

            migrationBuilder.RenameColumn(
                name: "LossShrimpId",
                table: "LossShrimp",
                newName: "lossShrimpId");

            migrationBuilder.RenameIndex(
                name: "IX_LossShrimp_PondId",
                table: "LossShrimp",
                newName: "IX_LossShrimp_pondId");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Harvests",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "SeedId",
                table: "Harvests",
                newName: "seedId");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "Harvests",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "HarvestType",
                table: "Harvests",
                newName: "harvestType");

            migrationBuilder.RenameColumn(
                name: "HarvestTime",
                table: "Harvests",
                newName: "harvestTime");

            migrationBuilder.RenameColumn(
                name: "HarvestDate",
                table: "Harvests",
                newName: "harvestDate");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Harvests",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Harvests",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "HarvestId",
                table: "Harvests",
                newName: "harvestId");

            migrationBuilder.RenameIndex(
                name: "IX_Harvests_PondId",
                table: "Harvests",
                newName: "IX_Harvests_pondId");

            migrationBuilder.RenameIndex(
                name: "IX_Harvests_FarmId",
                table: "Harvests",
                newName: "IX_Harvests_farmId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FoodForFeeding",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "FoodFeedingId",
                table: "FoodForFeeding",
                newName: "foodFeedingId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "FoodForFeeding",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "FoodForFeedingId",
                table: "FoodForFeeding",
                newName: "foodForFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodForFeeding_FoodFeedingId",
                table: "FoodForFeeding",
                newName: "IX_FoodForFeeding_foodFeedingId");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "FoodFeeding",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "FeedingDate",
                table: "FoodFeeding",
                newName: "feedingDate");

            migrationBuilder.RenameColumn(
                name: "FoodFeedingId",
                table: "FoodFeeding",
                newName: "foodFeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodFeeding_PondId",
                table: "FoodFeeding",
                newName: "IX_FoodFeeding_pondId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Food",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Food",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "Food",
                newName: "foodId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_FarmId",
                table: "Food",
                newName: "IX_Food_farmId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Farms",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "FarmName",
                table: "Farms",
                newName: "farmName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Farms",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Farms",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "EnvironmentStatus",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "EnvironmentStatus",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "EnvironmentStatus",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EnvironmentStatus",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "EnvironmentStatusId",
                table: "EnvironmentStatus",
                newName: "environmentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EnvironmentStatus_PondId",
                table: "EnvironmentStatus",
                newName: "IX_EnvironmentStatus_pondId");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "CleanSensor",
                newName: "farmId");

            migrationBuilder.RenameColumn(
                name: "CleanTime",
                table: "CleanSensor",
                newName: "cleanTime");

            migrationBuilder.RenameColumn(
                name: "CleanSensorId",
                table: "CleanSensor",
                newName: "cleanSensorId");

            migrationBuilder.RenameIndex(
                name: "IX_CleanSensor_FarmId",
                table: "CleanSensor",
                newName: "IX_CleanSensor_farmId");

            migrationBuilder.RenameColumn(
                name: "PondId",
                table: "Certificate",
                newName: "pondId");

            migrationBuilder.RenameColumn(
                name: "HarvestId",
                table: "Certificate",
                newName: "harvestId");

            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "Certificate",
                newName: "fileData");

            migrationBuilder.RenameColumn(
                name: "CertificateName",
                table: "Certificate",
                newName: "certificateName");

            migrationBuilder.RenameColumn(
                name: "CertificateId",
                table: "Certificate",
                newName: "certificateId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_PondId",
                table: "Certificate",
                newName: "IX_Certificate_pondId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_HarvestId",
                table: "Certificate",
                newName: "IX_Certificate_harvestId");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Alarm",
                newName: "farmId");

            migrationBuilder.RenameIndex(
                name: "IX_Alarm_FarmId",
                table: "Alarm",
                newName: "IX_Alarm_farmId");

            migrationBuilder.AlterColumn<string>(
                name: "originPondId",
                table: "Pond",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarm_Farms_farmId",
                table: "Alarm",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Harvests_harvestId",
                table: "Certificate",
                column: "harvestId",
                principalTable: "Harvests",
                principalColumn: "harvestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Pond_pondId",
                table: "Certificate",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleanSensor_Farms_farmId",
                table: "CleanSensor",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentStatus_Pond_pondId",
                table: "EnvironmentStatus",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Farms_farmId",
                table: "Food",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodFeeding_Pond_pondId",
                table: "FoodFeeding",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodForFeeding_FoodFeeding_foodFeedingId",
                table: "FoodForFeeding",
                column: "foodFeedingId",
                principalTable: "FoodFeeding",
                principalColumn: "foodFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_Farms_farmId",
                table: "Harvests",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_Pond_pondId",
                table: "Harvests",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_LossShrimp_Pond_pondId",
                table: "LossShrimp",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Farms_farmId",
                table: "Machines",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Farms_farmId",
                table: "Medicine",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineFeeding_Pond_pondId",
                table: "MedicineFeeding",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineForFeeding_MedicineFeeding_medicineFeedingId",
                table: "MedicineForFeeding",
                column: "medicineFeedingId",
                principalTable: "MedicineFeeding",
                principalColumn: "medicineFeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pond_PondType_pondTypeId",
                table: "Pond",
                column: "pondTypeId",
                principalTable: "PondType",
                principalColumn: "pondTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PondIds_Machines_machineId",
                table: "PondIds",
                column: "machineId",
                principalTable: "Machines",
                principalColumn: "machineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PondType_Farms_farmId",
                table: "PondType",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_SizeShrimp_Pond_pondId",
                table: "SizeShrimp",
                column: "pondId",
                principalTable: "Pond",
                principalColumn: "pondId");

            migrationBuilder.AddForeignKey(
                name: "FK_timeSettingObjects_TimeSettings_timeSettingId",
                table: "timeSettingObjects",
                column: "timeSettingId",
                principalTable: "TimeSettings",
                principalColumn: "timeSettingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSettings_Farms_farmId",
                table: "TimeSettings",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");
        }
    }
}
