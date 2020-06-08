using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDAT1007AsgOneWebApp.Models;

namespace BDAT1007AsgOneWebApp.Controllers
{
    public class TicketOptionsController : Controller
    {
        private readonly BDAT1007Context _context;

        public TicketOptionsController(BDAT1007Context context)
        {
            _context = context;
        }

        // GET: TicketOptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketOptions.ToListAsync());
        }

        // GET: TicketOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOptions = await _context.TicketOptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketOptions == null)
            {
                return NotFound();
            }

            return View(ticketOptions);
        }

        // GET: TicketOptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,TeamName,League,Esource,Seatgeek,Ticketmaster,Stubhub")] TicketOptions ticketOptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketOptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketOptions);
        }

        // GET: TicketOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOptions = await _context.TicketOptions.FindAsync(id);
            if (ticketOptions == null)
            {
                return NotFound();
            }
            return View(ticketOptions);
        }

        // POST: TicketOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,TeamName,League,Esource,Seatgeek,Ticketmaster,Stubhub")] TicketOptions ticketOptions)
        {
            if (id != ticketOptions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketOptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketOptionsExists(ticketOptions.Id))
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
            return View(ticketOptions);
        }

        // GET: TicketOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOptions = await _context.TicketOptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketOptions == null)
            {
                return NotFound();
            }

            return View(ticketOptions);
        }

        // POST: TicketOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketOptions = await _context.TicketOptions.FindAsync(id);
            _context.TicketOptions.Remove(ticketOptions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketOptionsExists(int id)
        {
            return _context.TicketOptions.Any(e => e.Id == id);
        }
    }
}
