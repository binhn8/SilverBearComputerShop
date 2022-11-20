using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Models;
using SilverBearComputerShop.Repositories;

namespace SilverBearComputerShop.Controllers
{
	public class ComputersController : Controller
    {
        private readonly IRepository<Computer, int> computerRepository;
        public ComputersController(IRepository<Computer, int> computerRepository)
        {
            this.computerRepository = computerRepository;
        }

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
            var computers = computerRepository.GetByTextThenOrder(searchString, sortOrder);

            int pageSize = 3;
            return View(await PaginatedList<Computer>.CreateAsync(computers, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var computer = await computerRepository.GetDetailById((int)id);

            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Weight,Title,Description")] Computer computer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await computerRepository.Insert(computer);
                    await computerRepository.Save();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(computer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await computerRepository.GetById((int)id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

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
                    var computerObject = await computerRepository.GetById(id);
                    computerObject.Title = computer.Title;
                    computerObject.Description = computer.Description;
                    computerObject.Weight = computer.Weight;
                    await computerRepository.Save();
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

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await computerRepository.GetById((int)id);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await computerRepository.Delete(id);
                await computerRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ComputerExists(int id)
        {
            return computerRepository.GetById(id)!=null;
        }
    }
}
