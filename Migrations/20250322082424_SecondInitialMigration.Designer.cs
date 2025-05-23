﻿// <auto-generated />
using System;
using EventManagerMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventManagerMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250322082424_SecondInitialMigration")]
    partial class SecondInitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventManagerMVC.Models.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingID"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BookingID");

                    b.HasIndex("EventID");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingID = 1,
                            BookingDate = new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventID = 1,
                            Status = "Confirmed"
                        },
                        new
                        {
                            BookingID = 2,
                            BookingDate = new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventID = 2,
                            Status = "Confirmed"
                        },
                        new
                        {
                            BookingID = 3,
                            BookingDate = new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventID = 3,
                            Status = "Confirmed"
                        });
                });

            modelBuilder.Entity("EventManagerMVC.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"));

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("VenueID")
                        .HasColumnType("int");

                    b.HasKey("EventID");

                    b.HasIndex("VenueID");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventID = 1,
                            Description = "A conference showcasing the latest in technology.",
                            EventDate = new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventName = "Tech Conference 2025",
                            VenueID = 1
                        },
                        new
                        {
                            EventID = 2,
                            Description = "A beautiful evening wedding reception.",
                            EventDate = new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventName = "Wedding Reception",
                            VenueID = 2
                        },
                        new
                        {
                            EventID = 3,
                            Description = "A live music festival featuring multiple artists.",
                            EventDate = new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventName = "Music Festival",
                            VenueID = 3
                        });
                });

            modelBuilder.Entity("EventManagerMVC.Models.Venue", b =>
                {
                    b.Property<int>("VenueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VenueID"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("VenueName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("VenueID");

                    b.ToTable("Venues");

                    b.HasData(
                        new
                        {
                            VenueID = 1,
                            Capacity = 500,
                            ImageURL = "https://example.com/venue1.jpg",
                            IsActive = true,
                            Location = "123 Main Street, City Center",
                            VenueName = "Grand Conference Hall"
                        },
                        new
                        {
                            VenueID = 2,
                            Capacity = 300,
                            ImageURL = "https://example.com/venue2.jpg",
                            IsActive = true,
                            Location = "456 Sunset Blvd, Downtown",
                            VenueName = "Skyline Banquet"
                        },
                        new
                        {
                            VenueID = 3,
                            Capacity = 700,
                            ImageURL = "https://example.com/venue3.jpg",
                            IsActive = true,
                            Location = "789 Beach Road, Seaside",
                            VenueName = "Ocean View Pavilion"
                        });
                });

            modelBuilder.Entity("EventManagerMVC.Models.Booking", b =>
                {
                    b.HasOne("EventManagerMVC.Models.Event", "Event")
                        .WithMany("EventBookings")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventManagerMVC.Models.Event", b =>
                {
                    b.HasOne("EventManagerMVC.Models.Venue", "Venue")
                        .WithMany("HostedEvents")
                        .HasForeignKey("VenueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("EventManagerMVC.Models.Event", b =>
                {
                    b.Navigation("EventBookings");
                });

            modelBuilder.Entity("EventManagerMVC.Models.Venue", b =>
                {
                    b.Navigation("HostedEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
