/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       Event.cs                                      
 *@Updated:    22/03/2025                                    
 *@Purpose:    Represents an Event linked to a Venue         
 *              (CLDV6211 POE – Part 1)                        
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//-------------namespace--------//
namespace EventManagerMVC.Models
{
    //--------------------Event class-------------------//
    public class Event
    {
        [Key] // Auto-incremented primary key
        public int EventID { get; set; }

        [Required, StringLength(255)]
        public required string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        //<removed> VenueID property to link to the Venue table

        // Navigation property back to the venue
        public Venue? Venue { get; set; }

        // Navigation: A single event can have many bookings
        public ICollection<Booking> EventBookings { get; set; } = new List<Booking>();
    }
    //--------------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies, Hangzshou: Posts & Telecom Press.
 * OpenAI, 2025. chatgpt.com. [Online] 
   Available at: https://openai.com/chatgpt/
   [Accessed 20 March 2025].
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer's Guide to Azure, Redmond: Microsoft Press
 * Github Inc, 2025. Github Copilot. [Online] 
   Available at: https://github.com
   [Accessed 14 March 2025].
 * Varsity Collage, 2025. INSY6112 Module Manual, Cape Town: Independent Institute of Education.
 */