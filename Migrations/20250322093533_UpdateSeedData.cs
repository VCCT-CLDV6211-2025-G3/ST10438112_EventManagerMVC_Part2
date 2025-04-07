//Initial table values/data was created with

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManagerMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Capacity", "ImageURL", "IsActive", "Location", "VenueName" },
                values: new object[,]
                {
                    { 4, 250, "<placeholderURL>", true, "Italy", "Selva Gardina" },
                    { 5, 9000, "<placeholderURL>", true, "Rome, Italy", "Vatican" },
                    { 6, 50, "<placeholderURL>", true, "Cape Town, South Africa", "Table Mountain" },
                    { 7, 1000, "<placeholderURL>", true, "London, UK", "Buckingham Palace" },
                    { 8, 40, "<placeholderURL>", true, "Newlands, Cape Town", "Varsity College" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "Description", "EventDate", "EventName", "VenueID" },
                values: new object[,]
                {
                    { 4, "A fun activity for hotel guests", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Snow Skiing Competition", 4 },
                    { 5, "An annual speech to commemorate the birth of Christ", new DateTime(2026, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pope's Christmas Address", 5 },
                    { 6, "An exciting afternoon experience for booked customers", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunday Paragliding", 6 },
                    { 7, "A public event where the King meets the public around the Palace", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "His Majesty's Cancer Fundraiser", 7 },
                    { 8, "High stakes competition where groups of students compete to deploy an SaaS infrastructure under 60 minutes", new DateTime(2027, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cloud Development Team Death Match 2027", 8 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingID", "BookingDate", "EventID", "Status" },
                values: new object[,]
                {
                    { 4, new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Confirmed" },
                    { 5, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Confirmed" },
                    { 6, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Confirmed" },
                    { 7, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Confirmed" },
                    { 8, new DateTime(2027, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Confirmed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Capacity", "ImageURL", "IsActive", "Location", "VenueName" },
                values: new object[,]
                {
                    { 1, 500, "https://example.com/venue1.jpg", true, "123 Main Street, City Center", "Grand Conference Hall" },
                    { 2, 300, "https://example.com/venue2.jpg", true, "456 Sunset Blvd, Downtown", "Skyline Banquet" },
                    { 3, 700, "https://example.com/venue3.jpg", true, "789 Beach Road, Seaside", "Ocean View Pavilion" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "Description", "EventDate", "EventName", "VenueID" },
                values: new object[,]
                {
                    { 1, "A conference showcasing the latest in technology.", new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tech Conference 2025", 1 },
                    { 2, "A beautiful evening wedding reception.", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wedding Reception", 2 },
                    { 3, "A live music festival featuring multiple artists.", new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Music Festival", 3 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingID", "BookingDate", "EventID", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Confirmed" },
                    { 2, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Confirmed" },
                    { 3, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Confirmed" }
                });
        }
    }
}
