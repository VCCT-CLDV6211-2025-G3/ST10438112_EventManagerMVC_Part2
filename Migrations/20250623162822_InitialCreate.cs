using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManagerMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    EventTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.EventTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "/images/placeholder.png"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "/images/placeholder.png"),
                    VenueID = table.Column<int>(type: "int", nullable: false),
                    EventTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeID",
                        column: x => x.EventTypeID,
                        principalTable: "EventTypes",
                        principalColumn: "EventTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "EventTypeID", "TypeName" },
                values: new object[,]
                {
                    { 1, "Competition" },
                    { 2, "Conference" },
                    { 3, "Tourist Activity" },
                    { 4, "Fundraiser" },
                    { 5, "Corporate" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Capacity", "ImageURL", "IsAvailable", "Location", "VenueName" },
                values: new object[,]
                {
                    { 4, 250, "<placeholderURL>", false, "Italy", "Selva Gardina" },
                    { 5, 9000, "<placeholderURL>", false, "Rome, Italy", "Vatican" },
                    { 6, 50, "<placeholderURL>", false, "Cape Town, South Africa", "Table Mountain" },
                    { 7, 1000, "<placeholderURL>", false, "London, UK", "Buckingham Palace" },
                    { 8, 40, "<placeholderURL>", false, "Newlands, Cape Town", "Varsity College" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "Description", "EventDate", "EventName", "EventTime", "EventTypeID", "VenueID" },
                values: new object[,]
                {
                    { 4, "A fun activity for hotel guests", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Snow Skiing Competition", new TimeSpan(0, 0, 0, 0, 0), 3, 4 },
                    { 5, "An annual speech to commemorate the birth of Christ", new DateTime(2026, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pope's Christmas Address", new TimeSpan(0, 0, 0, 0, 0), 2, 5 },
                    { 6, "An exciting afternoon experience for booked customers", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunday Paragliding", new TimeSpan(0, 0, 0, 0, 0), 3, 6 },
                    { 7, "A public event where the King meets the public around the Palace", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "His Majesty's Cancer Fundraiser", new TimeSpan(0, 0, 0, 0, 0), 4, 7 },
                    { 8, "High stakes competition where groups of students compete to deploy an SaaS infrastructure under 60 minutes", new DateTime(2027, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cloud Development Team Death Match 2027", new TimeSpan(0, 0, 0, 0, 0), 1, 8 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventID",
                table: "Bookings",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeID",
                table: "Events",
                column: "EventTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                table: "Events",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_VenueName",
                table: "Venues",
                column: "VenueName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
