/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       Venue.cs                                      
 *@Updated:    01/06/2025                                    
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

        [Range(1, 10000, ErrorMessage = "Capacity must be between 1 and 10,000.")]
        public int Capacity { get; set; }
        public string? ImageURL { get; set; }

        //flags to indicate whether the venue is currently active/retired
        public bool IsAvailable { get; set; } //added new property to indicate if the venue is available for booking


        //venue can host multiple events (navigation property)
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DisplayVenue => $"{VenueName} ({Location})";
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