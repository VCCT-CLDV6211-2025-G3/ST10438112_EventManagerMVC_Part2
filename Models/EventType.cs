/*
 * @author: Kylan Frittelli (ST10438112)
 * @file: EventType.cs
 * @since: 01/06/2025
 * @purpose: Defines EventType model for EventManager project
 */

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

//-------------namespace--------//
namespace EventManagerMVC.Models
{
    //--------------------EventType class-------------------//
    public class EventType
    {
        [Key]
        public int EventTypeID { get; set; }

        [Required]
        public string TypeName { get; set; }

        // Navigation property for related events
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
    //--------------------------------//
}
//----------------------------------//

// END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>


/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 01 June 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 01 June 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */
