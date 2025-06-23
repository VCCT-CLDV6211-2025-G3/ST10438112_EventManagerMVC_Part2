/*
 * @Author: Kylan Frittelli ST10438112
 * @Since[Updated]: 01/03/525
 * @Function: ApplicationDbContext for CLDV6211 POE Part 1
 */

using Microsoft.EntityFrameworkCore;
using EventManagerMVC.Models;

//----------------------------namespace-----------------//
namespace EventManagerMVC.Data //uses the Data folder
{
    //--ApplicationDbContext class-----------------//
    public class ApplicationDbContext : DbContext //inherits from DbContext
    {
        //constructor-----------------//
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //uses the options parameter

        public DbSet<Venue> Venues { get; set; } //DbSet for Venue
        public DbSet<Event> Events { get; set; } //DbSet for Event
        public DbSet<Booking> Bookings { get; set; } //DbSet for Booking

        public DbSet<EventType> EventTypes { get; set; }

        //protected override method-----------------//
        protected override void OnModelCreating(ModelBuilder modelBuilder) //overrides the OnModelCreating method
        {
            base.OnModelCreating(modelBuilder); //calls the base method

         modelBuilder.Entity<Venue>()
           .HasIndex(v => v.VenueName)
           .IsUnique();

           modelBuilder.Entity<Venue>()
               .Property(v => v.ImageURL)
               .HasDefaultValue("/images/placeholder.png");

            modelBuilder.Entity<Event>()
               .Property(e => e.ImageURL)
               .HasDefaultValue("/images/placeholder.png");

            //------ Seed Venues ------
            modelBuilder.Entity<Venue>().HasData(
                new Venue { VenueID = 4, VenueName = "Selva Gardina", Location = "Italy", Capacity = 250, ImageURL = "<placeholderURL>" },
                new Venue { VenueID = 5, VenueName = "Vatican", Location = "Rome, Italy", Capacity = 9000, ImageURL = "<placeholderURL>" },
                new Venue { VenueID = 6, VenueName = "Table Mountain", Location = "Cape Town, South Africa", Capacity = 50, ImageURL = "<placeholderURL>" },
                new Venue { VenueID = 7, VenueName = "Buckingham Palace", Location = "London, UK", Capacity = 1000, ImageURL = "<placeholderURL>" },
                new Venue { VenueID = 8, VenueName = "Varsity College", Location = "Newlands, Cape Town", Capacity = 40, ImageURL = "<placeholderURL>" }
            );

            //------ Seed Event Types ------
            modelBuilder.Entity<EventType>().HasData(
                new EventType { EventTypeID = 1, TypeName = "Competition" },
                new EventType { EventTypeID = 2, TypeName = "Conference" },
                new EventType { EventTypeID = 3, TypeName = "Tourist Activity" },
                new EventType { EventTypeID = 4, TypeName = "Fundraiser" },
                new EventType { EventTypeID = 5, TypeName = "Corporate" }
            );
            //------ Seed Events ------
            modelBuilder.Entity<Event>().HasData(
                new Event { EventID = 4, EventName = "Snow Skiing Competition", VenueID = 4, EventDate = new DateTime(2026, 7, 31), Description = "A fun activity for hotel guests", EventTypeID = 3 },
                new Event { EventID = 5, EventName = "Pope's Christmas Address", VenueID = 5, EventDate = new DateTime(2026, 12, 25), Description = "An annual speech to commemorate the birth of Christ", EventTypeID = 2 },
                new Event { EventID = 6, EventName = "Sunday Paragliding", VenueID = 6, EventDate = new DateTime(2026, 1, 7), Description = "An exciting afternoon experience for booked customers", EventTypeID = 3},
                new Event { EventID = 7, EventName = "His Majesty's Cancer Fundraiser", VenueID = 7, EventDate = new DateTime(2025, 6, 12), Description = "A public event where the King meets the public around the Palace", EventTypeID = 4},
                new Event { EventID = 8, EventName = "Cloud Development Team Death Match 2027", VenueID = 8, EventDate = new DateTime(2027, 6, 17), Description = "High stakes competition where groups of students compete to deploy an SaaS infrastructure under 60 minutes", EventTypeID = 1 }
            );

            //------ Seed Bookings ------
            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingID = 4, BookingDate = new DateTime(2025, 3, 31), EventID = 4 },
                new Booking { BookingID = 5, BookingDate = new DateTime(2025, 11, 30), EventID = 5 },
                new Booking { BookingID = 6, BookingDate = new DateTime(2025, 1, 3), EventID = 6 },
                new Booking { BookingID = 7, BookingDate = new DateTime(2025, 5, 1), EventID = 7 },
                new Booking { BookingID = 8, BookingDate = new DateTime(2027, 5, 31), EventID = 8 }
            );

            
        }
        //----------------------------//
    }
    //--------------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 01 June 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 01 June 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */