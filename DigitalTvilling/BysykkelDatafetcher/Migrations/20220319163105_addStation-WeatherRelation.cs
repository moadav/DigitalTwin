using Microsoft.EntityFrameworkCore.Migrations;

namespace BysykkelDatafetcher.Migrations
{
    public partial class addStationWeatherRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "name",
                table: "WeatherPoints");

            migrationBuilder.AddColumn<string>(
                name: "stationId",
                table: "WeatherPoints",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherPoints_stationId",
                table: "WeatherPoints",
                column: "stationId",
                unique: true,
                filter: "[stationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherPoints_Stations_stationId",
                table: "WeatherPoints",
                column: "stationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPoints_Stations_stationId",
                table: "WeatherPoints");

            migrationBuilder.DropIndex(
                name: "IX_WeatherPoints_stationId",
                table: "WeatherPoints");

            migrationBuilder.DropColumn(
                name: "stationId",
                table: "WeatherPoints");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "WeatherPoints",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
