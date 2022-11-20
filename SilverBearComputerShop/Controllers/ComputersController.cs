using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;

namespace SilverBearComputerShop.Controllers
{
    public class ComputersController : Controller
    {
        private readonly ComputerShopContext _context;

        public ComputersController(ComputerShopContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index(
                string sortOrder,
                string currentFilter,
                string searchString,
                int? pageNumber)
        {
            ViewData["WeightSortParm"] = String.IsNullOrEmpty(sortOrder) ? "weight_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var computers = from s in _context.Computer
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                computers = computers.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
                
            }
			
			switch (sortOrder)
            {
                case "weight_desc":
                    computers = computers.OrderByDescending(s => s.Weight);
                    break;
                case "Title":
                    computers = computers.OrderBy(s => s.Title);
                    break;
                case "title_desc":
                    computers = computers.OrderByDescending(s => s.Title);
                    break;
                default:
                    computers = computers.OrderBy(s => s.Weight);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Computer>.CreateAsync(computers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                    .Include(s => s.ComputerComponent)
                    .ThenInclude(e => e.Component)
                    .ThenInclude(d=>d.ComponentType)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);

            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Weight,Title,Description")] Computer computer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(computer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(computer);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Weight,Title,Description")] Computer computer)
        {
            if (id != computer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.ID))
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
            return View(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                 .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (computer == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Computer.Remove(computer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ComputerExists(int id)
        {
            return _context.Computer.Any(e => e.ID == id);
        }
    }
}
