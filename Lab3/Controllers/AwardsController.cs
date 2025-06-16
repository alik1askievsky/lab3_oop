using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab3;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class AwardsController : Controller
    {
        private readonly ApplicationContext _context;

        public AwardsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Awards
        public async Task<IActionResult> Index()
        {
            var awards = _context.Awards.Include(a => a.Participant);
            return View(await awards.ToListAsync());
        }

        // GET: Awards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var award = await _context.Awards
                .Include(a => a.Participant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (award == null)
                return NotFound();

            return View(award);
        }

        // GET: Awards/Create
        public IActionResult Create()
        {
            ViewData["ParticipantId"] = new SelectList(
                _context.Participants
                        .Select(p => new
                        {
                            Id = p.Id,
                            FullName = p.Name + " " + p.Surname
                        }),
                "Id", "FullName");

            return View();
        }

        // POST: Awards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParticipantId")] Award award)
        {
            if (ModelState.IsValid)
            {
                _context.Add(award);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParticipantId"] = new SelectList(
                _context.Participants
                        .Select(p => new
                        {
                            Id = p.Id,
                            FullName = p.Name + " " + p.Surname
                        }),
                "Id", "FullName", award.ParticipantId);

            return View(award);
        }

        // GET: Awards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var award = await _context.Awards.FindAsync(id);
            if (award == null)
                return NotFound();

            ViewData["ParticipantId"] = new SelectList(
                _context.Participants
                        .Select(p => new
                        {
                            Id = p.Id,
                            FullName = p.Name + " " + p.Surname
                        }),
                "Id", "FullName", award.ParticipantId);

            return View(award);
        }

        // POST: Awards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ParticipantId")] Award award)
        {
            if (id != award.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(award);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwardExists(award.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParticipantId"] = new SelectList(
                _context.Participants
                        .Select(p => new
                        {
                            Id = p.Id,
                            FullName = p.Name + " " + p.Surname
                        }),
                "Id", "FullName", award.ParticipantId);

            return View(award);
        }

        // GET: Awards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var award = await _context.Awards
                .Include(a => a.Participant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (award == null)
                return NotFound();

            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AwardExists(int id)
        {
            return _context.Awards.Any(e => e.Id == id);
        }
    }
}
