using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BysykkelDatafetcher.Migrations
{
    public partial class addWeatherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherPoints",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    lat = table.Column<double>(nullable: false),
                    lon = table.Column<double>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    air_pressure_at_sea_level = table.Column<double>(nullable: false),
                    air_temperature = table.Column<double>(nullable: false),
                    cloud_area_fraction = table.Column<double>(nullable: false),
                    relative_humidity = table.Column<double>(nullable: false),
                    wind_from_direction = table.Column<double>(nullable: false),
                    wind_speed = table.Column<double>(nullable: false),
                    precipitation_amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherPoints", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherPoints");
        }
    }
}
