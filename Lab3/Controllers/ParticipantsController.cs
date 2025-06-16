using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab3;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly ApplicationContext _context;

        public ParticipantsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Participants
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Participants.Include(p => p.Division);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Participants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var participant = await _context.Participants
                .Include(p => p.Division)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (participant == null)
                return NotFound();

            return View(participant);
        }

        // GET: Participants/Create
        public IActionResult Create()
        {
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id");
            return View();
        }

        // POST: Participants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,DivisionId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", participant.DivisionId);
            return View(participant);
        }

        // GET: Participants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
                return NotFound();

            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", participant.DivisionId);
            return View(participant);
        }

        // POST: Participants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,DivisionId")] Participant participant)
        {
            if (id != participant.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantExists(participant.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", participant.DivisionId);
            return View(participant);
        }

        // GET: Participants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var participant = await _context.Participants
                .Include(p => p.Division)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (participant == null)
                return NotFound();

            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant != null)
            {
                _context.Participants.Remove(participant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participants.Any(e => e.Id == id);
        }
    }
}
