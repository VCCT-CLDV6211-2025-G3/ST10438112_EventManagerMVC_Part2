/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       EventsController.cs                           
 *@Created:    22/03/2025
 *@Updated:    23/06/2025
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
        public async Task<IActionResult> Index(int? eventTypeId, DateTime? startDate, DateTime? endDate, bool? IsAvailable)
        {
            ViewBag.EventTypes = new SelectList(_context.EventTypes, "EventTypeID", "TypeName", eventTypeId);
            ViewBag.Venues = new SelectList(_context.Venues, "VenueID", "VenueName");
            ViewBag.IsAvailable = IsAvailable;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            var events = _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .AsQueryable();

            if (eventTypeId.HasValue && eventTypeId.Value > 0)
                events = events.Where(e => e.EventTypeID == eventTypeId.Value);

            if (startDate.HasValue)
                events = events.Where(e => e.EventDate >= startDate.Value);

            if (endDate.HasValue)
                events = events.Where(e => e.EventDate <= endDate.Value);

            if (IsAvailable.HasValue)
                events = events.Where(e => e.Venue.IsAvailable == IsAvailable.Value);

            return View(await events.ToListAsync());
        }
        //----------------------------//

        // GET: Events/Create
        //Create method----------------//
        public IActionResult Create()
        {
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName");
            ViewBag.EventTypes = new SelectList(_context.EventTypes, "EventTypeID", "TypeName");
            return View();
        }
        //----------------------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,EventName,EventDate,EventTime,Description,VenueID,EventTypeID,ImageURL")] Event @event, IFormFile file)
        {
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location");

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

                // Default placeholder in case no image is uploaded or upload fails
                string imageUrl = "/images/placeholder.png";

                try
                {
                    if (file != null && file.Length > 0)
                    {
                        imageUrl = await _blobService.UploadFileBlobAsync(file.OpenReadStream(), file.FileName, "eventimages");
                    }

                    @event.ImageURL = imageUrl;

                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Event created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while uploading the image. Please try again. (" + ex.Message + ")";
                    return View(@event); // user input preserved
                }
            }

            
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName");
            ViewBag.EventTypes = new SelectList(_context.EventTypes, "EventTypeID", "TypeName");
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
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", @event.VenueID);
            ViewBag.EventTypes = new SelectList(_context.EventTypes, "EventTypeID", "TypeName", @event.EventTypeID);
            return View(@event);
        }
        //----------------------------//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,EventName,EventDate,EventTime,Description,VenueID,EventTypeID,ImageURL")] Event @event, IFormFile file)
        {
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location");

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
                        var imageUrl = await _blobService.UploadFileBlobAsync(file.OpenReadStream(), file.FileName, "eventimages");
                        @event.ImageURL = imageUrl;
                    }
                    else if (string.IsNullOrWhiteSpace(@event.ImageURL))
                    {
                        @event.ImageURL = "/images/placeholder.png";
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Event updated successfully.";
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
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the event: " + ex.Message;
                    return View(@event);
                }
            }

            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", @event.VenueID);
            ViewBag.EventTypes = new SelectList(_context.EventTypes, "EventTypeID", "TypeName", @event.EventTypeID);
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
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 23 June 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 23 June 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */
