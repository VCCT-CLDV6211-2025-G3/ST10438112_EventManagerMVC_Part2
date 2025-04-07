using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerMVC.Migrations
{
    /// <inheritdoc />
    public partial class SecondInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Venues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 1,
                column: "Status",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 2,
                column: "Status",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 3,
                column: "Status",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 3,
                column: "IsActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");
        }
    }
}
