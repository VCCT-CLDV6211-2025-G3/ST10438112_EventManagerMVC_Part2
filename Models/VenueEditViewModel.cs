/*
 * @file VenueEditViewModel.cs
 * @author Kylan Frittelli (ST10438112)
 * @created 01/06/2025
 * @function: ViewModel for editing an existing venue in the EventManager project
 */



using System.ComponentModel.DataAnnotations;

namespace EventManagerMVC.Models
{
    public class VenueEditViewModel
    {
        public int VenueID { get; set; }
        [Required, StringLength(255)]
        public string VenueName { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000)]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Replace Image (optional)")]
        public IFormFile? ImageUpload { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 23 June 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 23 June 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */