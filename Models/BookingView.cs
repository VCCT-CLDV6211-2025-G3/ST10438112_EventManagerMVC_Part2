/* @author Kylan Frittelli (ST10438112)
 * @file BookingView.cs
 * @created 02/05/2025
 * @function: Defines the BookingViewModel class for the EventManager project of my POE Part 2.
 *            this model is used to represent the data for a booking view, to allow for views to be created and displayed
 */

//-------------namespace--------//
namespace EventManagerMVC.ViewModels
{
    //--------------------BookingViewModel class-------------------//
    public class BookingViewModel
    {
        public int BookingID { get; set; }
        public string EventName { get; set; }
        public DateTime BookingDate { get; set; }
    }
    //--------------------------------//
}
//---------------------------------//
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 02 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 02 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */