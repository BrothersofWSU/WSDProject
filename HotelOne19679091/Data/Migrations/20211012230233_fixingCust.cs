using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelOne19679091.Data.Migrations
{
    public partial class fixingCust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.DropColumn(
                name: "cutomerId",
                table: "Customer");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cutomerId",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
