/*
 *   @Author:     Kylan Frittelli (ST10438112)                  
 *   @File:       BookingsController.cs                         
 *   @Created:    22/03/2025
 *   @Updated:    04/05/2025
 *    - Added consolidated view for bookings
 *    
 *   @Purpose:    Manages bookings related to events.          
 *               Auto-generated through scaffolding.
 *               Views were also scaffolded using Visual Studio.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagerMVC.Data;
using EventManagerMVC.Models;
using EventManagerMVC.ViewModels;

//----------------------------namespace-----------------//
namespace EventManagerMVC.Controllers
{
    //--------------------BookingsController class-------------------//
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //constructor-----------------//
        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //----------------------------//

        // GET: Bookings
        //--------------------Index method-------------------//
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bookings.Include(b => b.Event);
            return View(await applicationDbContext.ToListAsync());
        }
        //--------------------------------//

        // GET: Bookings/Details/5
        //--------------------Details method-------------------//
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
        //--------------------------------//

        // GET: Bookings/Create
        //--------------------Create method-------------------//
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName");
            return View();
        }
        //--------------------------------//

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //--------------------Create method-------------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,BookingDate,EventID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var selectedEvent = await _context.Events
                    .Include(e => e.Venue)
                    .FirstOrDefaultAsync(e => e.EventID == booking.EventID);

                if (selectedEvent != null)
                {
                    var venueId = selectedEvent.VenueID;

                    var isDoubleBooked = await _context.Bookings
                        .Include(b => b.Event)
                        .AnyAsync(b => b.Event.VenueID == venueId &&
                                       b.BookingDate == booking.BookingDate &&
                                       b.EventID != booking.EventID); // different event

                    if (isDoubleBooked)
                    {
                        TempData["ErrorMessage"] = "This venue is already booked for another event at the selected time.";
                        return RedirectToAction(nameof(Create));
                    }
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            return View(booking);
        }
        //--------------------------------//

        // GET: Bookings/Edit/5
        //--------------------Edit method-------------------//
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            return View(booking);
        }
        //--------------------------------//

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //--------------------Edit method-------------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,BookingDate,EventID")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
                    {
                        return NotFound();
}
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            return View(booking);
        }
        //--------------------------------//

        // GET: Bookings/Delete/5
        //--------------------Delete method-------------------//
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
        //--------------------------------//

        // POST: Bookings/Delete/5
        //--------------------Delete method-------------------//
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //---------------------------------------------------//

        //--------BookingExists methpd-----------------//
        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
        //--------------------------------//

        //--------ConsolidatedView method-----------------//
        //this method retrieves all bookings and allows for searching by BookingID or EventName
        public async Task<IActionResult> ConsolidatedView(string searchTerm)
        {
            var bookingsQuery = _context.Bookings
                .Include(b => b.Event) // includes the related Event entity
                .Select(b => new BookingViewModel
                {
                    BookingID = b.BookingID,
                    EventName = b.Event.EventName,
                    BookingDate = b.BookingDate 
                }); 

            if (!string.IsNullOrEmpty(searchTerm))
            {//this filters the bookings based on the search term
                bookingsQuery = bookingsQuery.Where(b =>
                    b.BookingID.ToString().Contains(searchTerm) ||
                    b.EventName.Contains(searchTerm));
            }

            var filteredBookings = await bookingsQuery.ToListAsync(); //then it converts the query to a list

            return View(filteredBookings); //thus returns the filtered bookings to the view
        }
    }
        //--------------------------------//
    }
    //--------------------------------//

//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 04 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 04 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */