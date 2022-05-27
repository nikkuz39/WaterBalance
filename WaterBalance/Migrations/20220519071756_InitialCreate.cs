using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterBalance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConsumerName = table.Column<string>(type: "TEXT", nullable: true),
                    Unit = table.Column<double>(type: "REAL", nullable: false),
                    UnitName = table.Column<string>(type: "TEXT", nullable: true),
                    ConsumptionRatePerDay = table.Column<int>(type: "INTEGER", nullable: false),
                    HotWaterBool = table.Column<bool>(type: "INTEGER", nullable: true),
                    HotWaterConsumption = table.Column<int>(type: "INTEGER", nullable: true),
                    WastewaterCollection = table.Column<int>(type: "INTEGER", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    NormativeDocument = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyUseColdWater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConsumptionPerDayColdWater = table.Column<double>(type: "REAL", nullable: true),
                    ConsumptionPerHourColdWater = table.Column<double>(type: "REAL", nullable: true),
                    ConsumptionPerSecondColdWater = table.Column<double>(type: "REAL", nullable: true),
                    ConsumerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUseColdWater", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyUseColdWater_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyUseHotWater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConsumptionPerDayHotWater = table.Column<double>(type: "REAL", nullable: true),
                    ConsumptionPerHourHotWater = table.Column<double>(type: "REAL", nullable: true),
                    ConsumptionPerSecondHotWater = table.Column<double>(type: "REAL", nullable: true),
                    ConsumerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUseHotWater", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyUseHotWater_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyWastewaterCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WastewaterCollectionPerDay = table.Column<double>(type: "REAL", nullable: true),
                    WastewaterCollectionPerHour = table.Column<double>(type: "REAL", nullable: true),
                    WastewaterCollectionPerSecond = table.Column<double>(type: "REAL", nullable: true),
                    ConsumerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWastewaterCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyWastewaterCollection_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyUseColdWater_ConsumerId",
                table: "DailyUseColdWater",
                column: "ConsumerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyUseHotWater_ConsumerId",
                table: "DailyUseHotWater",
                column: "ConsumerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyWastewaterCollection_ConsumerId",
                table: "DailyWastewaterCollection",
                column: "ConsumerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyUseColdWater");

            migrationBuilder.DropTable(
                name: "DailyUseHotWater");

            migrationBuilder.DropTable(
                name: "DailyWastewaterCollection");

            migrationBuilder.DropTable(
                name: "Consumers");
        }
    }
}
