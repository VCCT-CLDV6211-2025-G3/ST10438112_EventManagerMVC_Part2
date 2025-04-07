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

        // Optional: Add status to allow future logic (e.g., "Confirmed", "Cancelled")
        [StringLength(50)]
        public string Status { get; set; } = "Confirmed";
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