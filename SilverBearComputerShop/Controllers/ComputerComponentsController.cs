//I dont refactor this class on purpose for the test. I will do it if this is real application

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;

namespace SilverBearComputerShop.Controllers
{
	public class ComputerComponentsController : Controller
    {
        private readonly ComputerShopContext _context;

        public ComputerComponentsController(ComputerShopContext context)
        {
            _context = context;
        }

        // GET: ComputerComponents
        public async Task<IActionResult> Index()
        {
            var computerShopContext = _context.ComputerComponent.Include(c => c.Component).Include(c => c.Computer);
            return View(await computerShopContext.ToListAsync());
        }

        // GET: ComputerComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computerComponent = await _context.ComputerComponent
                .Include(c => c.Component)
                .Include(c => c.Computer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (computerComponent == null)
            {
                return NotFound();
            }

            return View(computerComponent);
        }

        // GET: ComputerComponents/Create
        public IActionResult Create()
        {
            ViewData["ComponentID"] = new SelectList(_context.Component, "ID", "ID");
            ViewData["ComputerID"] = new SelectList(_context.Computer, "ID", "ID");
            return View();
        }

        // POST: ComputerComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ComputerID,ComponentID")] ComputerComponent computerComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computerComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentID"] = new SelectList(_context.Component, "ID", "ID", computerComponent.ComponentID);
            ViewData["ComputerID"] = new SelectList(_context.Computer, "ID", "ID", computerComponent.ComputerID);
            return View(computerComponent);
        }

        // GET: ComputerComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computerComponent = await _context.ComputerComponent.FindAsync(id);
            if (computerComponent == null)
            {
                return NotFound();
            }
            ViewData["ComponentID"] = new SelectList(_context.Component, "ID", "ID", computerComponent.ComponentID);
            ViewData["ComputerID"] = new SelectList(_context.Computer, "ID", "ID", computerComponent.ComputerID);
            return View(computerComponent);
        }

        // POST: ComputerComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ComputerID,ComponentID")] ComputerComponent computerComponent)
        {
            if (id != computerComponent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computerComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerComponentExists(computerComponent.ID))
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
            ViewData["ComponentID"] = new SelectList(_context.Component, "ID", "ID", computerComponent.ComponentID);
            ViewData["ComputerID"] = new SelectList(_context.Computer, "ID", "ID", computerComponent.ComputerID);
            return View(computerComponent);
        }

        // GET: ComputerComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computerComponent = await _context.ComputerComponent
                .Include(c => c.Component)
                .Include(c => c.Computer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (computerComponent == null)
            {
                return NotFound();
            }

            return View(computerComponent);
        }

        // POST: ComputerComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computerComponent = await _context.ComputerComponent.FindAsync(id);
            _context.ComputerComponent.Remove(computerComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerComponentExists(int id)
        {
            return _context.ComputerComponent.Any(e => e.ID == id);
        }
    }
}
