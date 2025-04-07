/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       HomeController.cs                             
 *@Created:    22/03/2025                                    
 *@Purpose:    Default controller for navigation & error     
 *             handling. Created automatically by the        
 *             ASP.NET Core project template.
 *             Views were also scaffolded using Visual Studio.
 *               
 */


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventManagerMVC.Models;

//-------------namespace--------//
namespace EventManagerMVC.Controllers;

//--------------HomeController class----------------//
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    //constructor----------------//
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    //----------------------------//

    //Index method----------------//
    public IActionResult Index()
    {
        return View();
    }
    //----------------------------//
    //Privacy method----------------//
    public IActionResult Privacy()
    {
        return View();
    }

    //Error method----------------//
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    //----------------------------//
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
