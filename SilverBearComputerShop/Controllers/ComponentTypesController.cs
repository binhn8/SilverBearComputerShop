using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;
using SilverBearComputerShop.Repositories;

namespace SilverBearComputerShop.Controllers
{
	public class ComponentTypesController : Controller
    {
        private readonly IRepository<ComponentType, int> componentTypeRepository;
        private readonly ComputerShopContext _context;
        public ComponentTypesController(IRepository<ComponentType, int> componentTypeRepository, ComputerShopContext context)
        {
            this.componentTypeRepository = componentTypeRepository;
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var component = await componentTypeRepository.GetAll();
            return View(component);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await componentTypeRepository.GetDetailById((int)id);

            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type")] ComponentType componentType)
        {
            if (ModelState.IsValid)
            {
                await componentTypeRepository.Insert(componentType);
                await componentTypeRepository.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(componentType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await componentTypeRepository.GetById((int)id);
            if (componentType == null)
            {
                return NotFound();
            }
            return View(componentType);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type")] ComponentType componentType)
        {
            if (id != componentType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var componentTypeObject = await componentTypeRepository.GetById(id);
                    componentTypeObject.Type = componentType.Type;
                    await componentTypeRepository.Save();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.ID))
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
            return View(componentType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await componentTypeRepository.GetById((int)id);

            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await componentTypeRepository.Delete(id);
            await componentTypeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(int id)
        {
            return componentTypeRepository.GetById(id) != null;
        }
    }
}
