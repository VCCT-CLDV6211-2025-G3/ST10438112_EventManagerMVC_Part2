/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       Event.cs                                      
 *@Updated:    03/04/2025                                    
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
        
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EventTime { get; set; } //added an additional time property to show the time of the event (recomended from ChatGPT)

        [StringLength(1000)]
        public string? Description { get; set; }

        public string? ImageURL { get; set; }

        public int VenueID { get; set; }
        
        // Navigation property back to the venue
        public Venue? Venue { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
    //--------------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 20 March 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 14 March 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */