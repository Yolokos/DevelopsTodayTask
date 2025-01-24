using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevelopsTodayTask.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TpepPickupDatetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TpepDropoffDatetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PassengerCount = table.Column<int>(type: "INTEGER", nullable: true),
                    TripDistance = table.Column<double>(type: "REAL", nullable: false),
                    StoreAndFwdFlag = table.Column<string>(type: "TEXT", nullable: false),
                    PULocationID = table.Column<int>(type: "INTEGER", nullable: false),
                    DOLocationID = table.Column<int>(type: "INTEGER", nullable: false),
                    FareAmount = table.Column<double>(type: "REAL", nullable: false),
                    TipAmount = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_dolocation_id",
                table: "Trips",
                column: "DOLocationID");

            migrationBuilder.CreateIndex(
                name: "idx_dropoff_datetime",
                table: "Trips",
                column: "TpepDropoffDatetime");

            migrationBuilder.CreateIndex(
                name: "idx_pickup_datetime",
                table: "Trips",
                column: "TpepPickupDatetime");

            migrationBuilder.CreateIndex(
                name: "idx_pulocation_id",
                table: "Trips",
                column: "PULocationID");

            migrationBuilder.CreateIndex(
                name: "idx_tip_amount",
                table: "Trips",
                column: "TipAmount");

            migrationBuilder.CreateIndex(
                name: "idx_trip_distance",
                table: "Trips",
                column: "TripDistance");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TpepPickupDatetime_TpepDropoffDatetime_PassengerCount",
                table: "Trips",
                columns: new[] { "TpepPickupDatetime", "TpepDropoffDatetime", "PassengerCount" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
