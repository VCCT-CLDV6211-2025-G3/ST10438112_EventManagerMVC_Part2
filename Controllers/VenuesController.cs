/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       VenuesController.cs                           
 *@Created:    22/03/2025
 *@Updated:    04/05/2025
 * - Added image upload functionality using Azure Blob Storage
 * 
 *@Purpose:    Handles CRUD operations for venues.           
 *             Scaffolded using Visual Studio tools.
 *             Views were also scaffolded using Visual Studio.
 *               
 */

using EventManagerMVC.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagerMVC.Data;
using EventManagerMVC.Models;

//-------------namespace--------//
namespace EventManagerMVC.Controllers
{
    //--------------------VenuesController class-------------------//
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobStorageService _blobService;

        //constructor----------------//
        public VenuesController(ApplicationDbContext context, IBlobStorageService blobService)
        {
            _context = context;
            _blobService = blobService;
        }
        //----------------------------//

        // GET: Venues
        //Index method----------------//
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }
        //----------------------------//

        // GET: Venues/Details/5
        //Details method----------------//
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
             }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }
        //----------------------------//

        // GET: Venues/Create
        //Create method----------------//
        public IActionResult Create()
        {
            return View();
        }
        //----------------------------//

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Create method----------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueID,VenueName,Location,Capacity,ImageURL")] Venue venue, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var imageUrl = await _blobService.UploadFileBlobAsync(file.OpenReadStream(), file.FileName, "eventimages");
                        venue.ImageURL = imageUrl;
                    }

                    if (string.IsNullOrWhiteSpace(venue.ImageURL))
                    {
                        TempData["ErrorMessage"] = "Image upload failed or no image was provided.";
                        return View(venue);
                    }

                    _context.Add(venue);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while uploading the image. Please try again.";
                    return View(venue);
                }
            }

            return View(venue);
        }
        //----------------------------//

        // GET: Venues/Edit/5
        //Edit method----------------//
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }
        //-----------------------------//

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Edit method----------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueID,VenueName,Location,Capacity,ImageURL")] Venue venue, IFormFile file)
        {
            if (id != venue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        try
                        {
                            var imageUrl = await _blobService.UploadFileBlobAsync(file.OpenReadStream(), file.FileName, "eventimages");
                            venue.ImageURL = imageUrl;

                            if (string.IsNullOrWhiteSpace(venue.ImageURL))
                            {
                                TempData["ErrorMessage"] = "Image upload failed or no image was returned.";
                                return View(venue);
                            }
                        }
                        catch (Exception)
                        {
                            TempData["ErrorMessage"] = "An error occurred while uploading the image. Please try again.";
                            return View(venue);
                        }
                    }

                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(venue);
        }
        //----------------------------//

        // GET: Venues/Delete/5
        //Delete method----------------//
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }
        //----------------------------//

        // POST: Venues/Delete/5
        //Delete method----------------//
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Events)
                .FirstOrDefaultAsync(m => m.VenueID == id);

            if (venue != null && venue.Events.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete this venue because it has associated events.";
                return RedirectToAction(nameof(Index));
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //--------------------------//
        //---------------------------///
        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueID == id);
        }
        //---------------------------//
    }
    //---------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/*
 * Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 20 March 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 14 March 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */