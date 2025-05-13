/* 
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       Booking.cs                                    
 *@Updated:    22/03/2025                                    
 *@Purpose:    Represents a booking made for an event        
 *              (CLDV6211 POE – Part 1)                        
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//-------------namespace--------//
namespace EventManagerMVC.Models
{
    //--------------------Booking class-------------------//
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        // Foreign key pointing to the related event
        [ForeignKey("Event")]
        public int EventID { get; set; }

        // Navigation property to the associated event
        public Event? Event { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Confirmed"; //i added an additional confirmed status (recomended from ChatGPT) to show the status of the booking
    }
    //--------------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 04 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 04 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */