/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       Venue.cs                                      
 *@Updated:    22/03/2025                                    
 *@Purpose:    Defines Venue model for EventManager project  
 *              (CLDV6211 POE – Part 1)                         
 */

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

//-------------namespace--------//
namespace EventManagerMVC.Models
{
    //--------------------venue class-------------------//
    public class Venue
    {
        [Key] // Unique identifier for the venue
        public int VenueID { get; set; }

        [Required, StringLength(255)]
        public required string VenueName { get; set; }

        [Required, StringLength(500)]
        public required string Location { get; set; }

        [Required]
        public int Capacity { get; set; }

        // Optional URL for a venue image (can be used in the UI)
        public string? ImageURL { get; set; }

        // Flag to indicate whether the venue is currently active or retired
        public bool IsActive { get; set; } = true;

        // Navigation property: A venue can host multiple events
        public ICollection<Event> HostedEvents { get; set; } = new List<Event>();

        // Computed property to easily reference Venue display info
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DisplayVenue => $"{VenueName} ({Location})";
        //----------------------------//
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