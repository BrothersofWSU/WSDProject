using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelOne19679091.Data.Migrations
{
    public partial class modelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");*/
/*
            migrationBuilder.DropColumn(
                name: "bookindId",
                table: "Booking");*/

            migrationBuilder.AddColumn<int>(
                name: "bookingId",
                table: "Booking",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            /*migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "bookingId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");*/
/*
            migrationBuilder.DropColumn(
                name: "bookingId",
                table: "Booking");
*/
            migrationBuilder.AddColumn<int>(
                name: "bookindId",
                table: "Booking",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

           /* migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "bookindId");*/
        }
    }
}
