using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessMVC.Data.Migrations
{
    public partial class imageeeee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "GymItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "GymItems");
        }
    }
}
