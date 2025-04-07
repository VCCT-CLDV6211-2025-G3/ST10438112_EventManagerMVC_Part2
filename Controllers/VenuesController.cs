/*
 *@Author:     Kylan Frittelli (ST10438112)                  
 *@File:       VenuesController.cs                           
 *@Created:    22/03/2025                                    
 *@Purpose:    Handles CRUD operations for venues.           
 *             Scaffolded using Visual Studio tools.
 *             Views were also scaffolded using Visual Studio.
 *               
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

//-------------namespace--------//
namespace EventManagerMVC.Controllers
{
    //--------------------VenuesController class-------------------//
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        //constructor----------------//
        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("VenueID,VenueName,Location,Capacity,ImageURL")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("VenueID,VenueName,Location,Capacity,ImageURL")] Venue venue)
        {
            if (id != venue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
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
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
            }

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