using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BysykkelDatafetcher.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    station_id = table.Column<string>(nullable: true),
                    is_installed = table.Column<int>(nullable: false),
                    is_renting = table.Column<int>(nullable: false),
                    is_returning = table.Column<int>(nullable: false),
                    last_reported = table.Column<int>(nullable: false),
                    num_bikes_available = table.Column<int>(nullable: false),
                    num_docks_available = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    lat = table.Column<double>(nullable: false),
                    lon = table.Column<double>(nullable: false),
                    capacity = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, computedColumnSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
