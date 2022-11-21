using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Data;
using SilverBearComputerShop.Models;
using SilverBearComputerShop.Repositories;

namespace SilverBearComputerShop.Controllers
{
	public class ComponentsController : Controller
    {
        private readonly IRepository<Component, int> componentRepository;
        private readonly IRepository<ComponentType, int> componentTypeRepository;
        public ComponentsController(IRepository<Component, int> componentRepository
                                    , IRepository<ComponentType, int> componentTypeRepository
                                    , ComputerShopContext context)
        {
            this.componentRepository = componentRepository;
            this.componentTypeRepository = componentTypeRepository;
        }
     
        public async Task<IActionResult> Index()
        {
            var component = await componentRepository.GetAll();
            return View(component);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await componentRepository.GetDetailById((int)id);
            
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        public IActionResult Create()
        {
            IEnumerable<ComponentType> conponentType = componentTypeRepository.GetAll().Result;
            ViewData["ComponentTypeId"] = new SelectList(conponentType, "ID", "Type");
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ComponentTypeId")] Component component)
        {
            if (ModelState.IsValid)
            {
                await componentRepository.Insert(component);
                await componentRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            var conponentType = await componentTypeRepository.GetAll();
            ViewData["ComponentTypeId"] = new SelectList(conponentType, "ID", "Type", component.ComponentTypeId);
            return View(component);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var component = await componentRepository.GetById((int)id);
            if (component == null)
            {
                return NotFound();
            }

            var conponentType = await componentTypeRepository.GetAll();
            ViewData["ComponentTypeId"] = new SelectList(conponentType, "ID", "Type", component.ComponentTypeId);
            return View(component);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ComponentTypeId")] Component component)
        {
            if (id != component.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var componentObject = await componentRepository.GetById(id);
                    componentObject.Name = component.Name;
                    componentObject.ComponentTypeId = component.ComponentTypeId;
                    componentObject.ComponentType = component.ComponentType;
                    await componentRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.ID))
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
            var conponentType = await componentTypeRepository.GetAll();
            ViewData["ComponentTypeId"] = new SelectList(conponentType, "ID", "Type", component.ComponentTypeId);
            return View(component);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var component = await componentRepository.GetById((int)id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await componentRepository.Delete(id);
            await componentRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(int id)
        {
            return componentRepository.GetById(id) != null;
        }
    }
}
