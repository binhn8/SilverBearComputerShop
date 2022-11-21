using SilverBearComputerShop.Data;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilverBearComputerShop.Repositories
{
    public class ComponentTypeRepository : IRepository<ComponentType, int>
    {
        private readonly ComputerShopContext context;
		
        public ComponentTypeRepository(ComputerShopContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ComponentType>> GetAll()
        {
            return await context.ComponentType.ToListAsync();
        }

        public async Task<ComponentType> GetById(int id)
        {
            return await context.ComponentType.FindAsync(id);
        }

        public async Task<ComponentType> GetDetailById(int id)
        {
            return await context.ComponentType
               .FirstOrDefaultAsync(m => m.ID == id);
        }

        public IQueryable<ComponentType> GetByTextThenOrder(string searchString, string sortOrder)
        {
            IQueryable<ComponentType> componentType;
            if (!string.IsNullOrEmpty(searchString))
            {
                componentType = context.ComponentType.Where(c => c.Type.Contains(searchString));
               
            }
            else
            {
                componentType=  context.ComponentType;
            }

            switch (sortOrder)
            {
                case "type_desc":
                    componentType = componentType.OrderByDescending(s => s.Type);
                    break;
                default:
                    componentType = componentType.OrderBy(s => s.Type);
                    break;
            }
            return componentType;
        }

        public async Task<IEnumerable<ComponentType>> GetByText(string searchString)
        {
            return await context.ComponentType.Where(c => c.Type.Contains(searchString))
                .ToListAsync();
        }

        public async Task<ComponentType> Insert(ComponentType entity)
        {
            await context.ComponentType.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var componentType = await context.ComponentType.FindAsync(id);
            if (componentType != null)
            {
                context.Remove(componentType);
            }
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
	}
}
