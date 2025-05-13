/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       EventsController.cs                           
 *@Created:    22/03/2025
 *@Updated:    04/05/2025
 * - Added image upload functionality using Azure Blob Storage
 * 
 *@Purpose:    Manages event creation, editing, deletion,    
 *             and details. Scaffolded via Visual Studio.
 *             Views were also scaffolded using Visual Studio.
 */

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
using EventManagerMVC.Services;


//-------------namespace--------//
namespace EventManagerMVC.Controllers
{
    //--------------------EventsController class-------------------//
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobStorageService _blobService;

        //constructor----------------//
        public EventsController(ApplicationDbContext context, IBlobStorageService blobService)
        {
            _context = context;
            _blobService = blobService;
        }
        //----------------------------//

        // GET: Events
        //Index method----------------//
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(e => e.Venue);
            return View(await applicationDbContext.ToListAsync());
        }
        //----------------------------//

        // GET: Events/Details/5
        //Details method----------------//
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }
        //----------------------------//

        // GET: Events/Create
        //Create method----------------//
        public IActionResult Create()
        {
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location");
            return View();
        }
        //----------------------------//

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Create method----------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,EventName,EventDate,EventTime,Description,VenueID,ImageURL")] Event @event, IFormFile file)
        {
            if (ModelState.IsValid)
            {
               
                if (_context.Events.Any(e =>
                    e.VenueID == @event.VenueID &&
                    e.EventDate == @event.EventDate &&
                    e.EventTime == @event.EventTime))
                {
                    TempData["ErrorMessage"] = "This venue is already booked for the selected date and time.";
                    return View(@event);
                }

                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var imageUrl = await _blobService.UploadFileBlobAsync(file.OpenReadStream(), file.FileName, "eventimages");
                        @event.ImageURL = imageUrl;
                    }

                    if (string.IsNullOrWhiteSpace(@event.ImageURL))
                    {
                        TempData["ErrorMessage"] = "Image upload failed or no image was provided.";
                        return View(@event);
                    }

                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while uploading the image. Please try again.";
                    return View(@event);
                }
            }

            return View(@event);
        }
        //----------------------------//

        // GET: Events/Edit/5
        //Edit method----------------//
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location");
            return View(@event);
        }
        //----------------------------//

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Edit method----------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,EventName,EventDate,EventTime,Description,VenueID,ImageURL")] Event @event, IFormFile file)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Events.Any(e =>
                    e.EventID != @event.EventID &&
                    e.VenueID == @event.VenueID &&
                    e.EventDate == @event.EventDate &&
                    e.EventTime == @event.EventTime))
                {
                    TempData["ErrorMessage"] = "This venue is already booked for the selected date and time.";
                    return View(@event);
                }

                try
                {
                    if (file != null && file.Length > 0)
                    {
                        try
                        {
                            var imageUrl = await _blobService.UploadFileBlobAsync(file.OpenReadStream(), file.FileName, "eventimages");
                            @event.ImageURL = imageUrl;

                            if (string.IsNullOrWhiteSpace(@event.ImageURL))
                            {
                                TempData["ErrorMessage"] = "Image upload failed or no image was returned.";
                                return View(@event);
                            }
                        }
                        catch (Exception)
                        {
                            TempData["ErrorMessage"] = "An error occurred while uploading the image. Please try again.";
                            return View(@event);
                        }
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(@event);
        }

        //----------------------------//

        // GET: Events/Delete/5
        //Delete method----------------//
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }
        //----------------------------//

        // POST: Events/Delete/5
        //Delete method----------------//
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //this method inclues preventing deletion if there are bookings linked to the event
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (@event != null && @event.Bookings.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete this event because it has active bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //--------------------------------//

        //-----EventExists method-----//
        //this method checks if an event exists in the database
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id); //this returns true if the event exists
        }
        //----------------------------//
    }
    //----------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 04 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 04 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */
