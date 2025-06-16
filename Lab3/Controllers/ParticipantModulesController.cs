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
    public class ParticipantModulesController : Controller
    {
        private readonly ApplicationContext _context;

        public ParticipantModulesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: ParticipantModules
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ParticipantModules
                .Include(p => p.Module)
                .Include(p => p.Participant);
            return View(await applicationContext.ToListAsync());
        }

        // GET: ParticipantModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var participantModule = await _context.ParticipantModules
                .Include(p => p.Module)
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (participantModule == null)
                return NotFound();

            return View(participantModule);
        }

        // GET: ParticipantModules/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id");
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id");
            return View();
        }

        // POST: ParticipantModules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParticipantId,ModuleId")] ParticipantModule participantModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participantModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", participantModule.ModuleId);
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id", participantModule.ParticipantId);
            return View(participantModule);
        }

        // GET: ParticipantModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var participantModule = await _context.ParticipantModules.FindAsync(id);
            if (participantModule == null)
                return NotFound();

            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", participantModule.ModuleId);
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id", participantModule.ParticipantId);
            return View(participantModule);
        }

        // POST: ParticipantModules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParticipantId,ModuleId")] ParticipantModule participantModule)
        {
            if (id != participantModule.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participantModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantModuleExists(participantModule.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", participantModule.ModuleId);
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "Id", "Id", participantModule.ParticipantId);
            return View(participantModule);
        }

        // GET: ParticipantModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var participantModule = await _context.ParticipantModules
                .Include(p => p.Module)
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (participantModule == null)
                return NotFound();

            return View(participantModule);
        }

        // POST: ParticipantModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participantModule = await _context.ParticipantModules.FindAsync(id);
            if (participantModule != null)
            {
                _context.ParticipantModules.Remove(participantModule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantModuleExists(int id)
        {
            return _context.ParticipantModules.Any(e => e.Id == id);
        }
    }
}
