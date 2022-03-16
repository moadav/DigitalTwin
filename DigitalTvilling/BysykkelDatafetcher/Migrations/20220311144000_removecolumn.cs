using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BysykkelDatafetcher.Migrations
{
    public partial class removecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Stations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Stations",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "GETDATE()");
        }
    }
}
