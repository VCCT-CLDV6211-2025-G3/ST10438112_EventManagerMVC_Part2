/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       VenuesController.cs                           
 *@Created:    22/03/2025
 *@Updated:    23/06/2025
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
        public async Task<IActionResult> Index(string sortOrder)
        {
            var venuesQuery = from v in _context.Venues select v;
            venuesQuery = venuesQuery.OrderBy(v => v.VenueName);
            return View(await venuesQuery.ToListAsync());
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
            var model = new VenueCreateViewModel
            {
                IsAvailable = true
            };
            return View(model);
        }
        //----------------------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VenueCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = "/images/placeholder.png";

                if (model.ImageUpload != null && model.ImageUpload.Length > 0)
                {
                    try
                    {
                        imageUrl = await _blobService.UploadFileBlobAsync(
                            model.ImageUpload.OpenReadStream(),
                            model.ImageUpload.FileName,
                            "venueimages"
                        );
                    }
                    catch
                    {
                        // Fallback to placeholder if upload fails
                    }
                }

                var venue = new Venue
                {
                    VenueName = model.VenueName,
                    Location = model.Location,
                    Capacity = model.Capacity,
                    ImageURL = imageUrl,
                    IsAvailable = model.IsAvailable
                };

                bool alreadyExists = await _context.Venues
                .AnyAsync(v => v.VenueName.Trim().ToLower() == model.VenueName.Trim().ToLower());

                if (alreadyExists)
                {
                    ModelState.AddModelError(nameof(model.VenueName),
                        "A venue with this name already exists.");
                    return View(model);
                }

                _context.Add(venue);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Venue created successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //----------------------------//

        // GET: Venues/Edit/5
        //Edit method----------------//
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();

            var model = new VenueEditViewModel
            {
                VenueID = venue.VenueID,
                VenueName = venue.VenueName,
                Location = venue.Location,
                Capacity = venue.Capacity,
                ExistingImageUrl = venue.ImageURL,
                IsAvailable = venue.IsAvailable

            };
            return View(model);
        }
        //-----------------------------//

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Edit method----------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VenueEditViewModel model)
        {
            if (id != model.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var venue = await _context.Venues.FindAsync(id);
                if (venue == null)
                {
                    return NotFound();
                }

                venue.VenueName = model.VenueName;
                venue.Location = model.Location;
                venue.Capacity = model.Capacity;
                venue.IsAvailable = model.IsAvailable;

                if (model.ImageUpload != null && model.ImageUpload.Length > 0)
                {
                    try
                    {
                        venue.ImageURL = await _blobService.UploadFileBlobAsync(
                            model.ImageUpload.OpenReadStream(),
                            model.ImageUpload.FileName,
                            "venueimages"
                        );
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(venue.ImageURL))
                    {
                        venue.ImageURL = "/images/placeholder.png";
                    }
                }

                bool duplicate = await _context.Venues
                .AnyAsync(v => v.VenueName.Trim().ToLower() == model.VenueName.Trim().ToLower()
                   && v.VenueID != model.VenueID);

                if (duplicate)
                {
                    ModelState.AddModelError(nameof(model.VenueName),
                        "Another venue with this name already exists.");
                    //Retrieve the existing image URL to display in the view
                    model.ExistingImageUrl = (await _context.Venues
                                                .Where(v => v.VenueID == model.VenueID)
                                                .Select(v => v.ImageURL)
                                                .FirstOrDefaultAsync())
                                                ?? "/images/placeholder.png";
                    return View(model);
                }

                _context.Update(venue);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Venue updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            var storedVenue = await _context.Venues
                       .Where(v => v.VenueID == model.VenueID)
                       .Select(v => v.ImageURL)
                       .FirstOrDefaultAsync();

            model.ExistingImageUrl = storedVenue ?? "/images/placeholder.png";
            return View(model);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Events)
                .FirstOrDefaultAsync(m => m.VenueID == id);

            if (venue == null)
            {
                TempData["ErrorMessage"] = "Venue not found or already deleted.";
                return RedirectToAction(nameof(Index));
            }

            if (venue.Events.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete this venue because it has associated events.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Venue deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the venue: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
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
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 23 June 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 23 June 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */