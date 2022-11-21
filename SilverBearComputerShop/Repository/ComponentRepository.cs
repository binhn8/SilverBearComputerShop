using SilverBearComputerShop.Data;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilverBearComputerShop.Repositories
{
    public class ComponentRepository : IRepository<Component, int>
    {
        private readonly ComputerShopContext context;

		public ComponentRepository(ComputerShopContext context)
		{
			this.context = context;
		}
		public async Task<IEnumerable<Component>> GetAll()
        {
            return await context.Component.Include(c => c.ComponentType).ToListAsync();
        }

        public async Task<Component> GetById(int id)
        {
            return await context.Component.FindAsync(id);
        }

        public async Task<Component> GetDetailById(int id)
        {
            return await context.Component
               .Include(c => c.ComponentType)
               .FirstOrDefaultAsync(m => m.ID == id);
        }

        public IQueryable<Component> GetByTextThenOrder(string searchString, string sortOrder)
        {
            IQueryable<Component> computers;
            if (!string.IsNullOrEmpty(searchString))
            {
                computers = context.Component.Where(c => c.Name.Contains(searchString));
               
            }
            else
            {
                computers=  context.Component;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    computers = computers.OrderByDescending(s => s.Name);
                    break;
                default:
                    computers = computers.OrderBy(s => s.Name);
                    break;
            }
            return computers;
        }

        public async Task<IEnumerable<Component>> GetByText(string searchString)
        {
            return await context.Component.Where(c => c.Name.Contains(searchString))
                .ToListAsync();
        }

        public async Task<Component> Insert(Component entity)
        {
            await context.Component.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var computer = await context.Component.FindAsync(id);
            if (computer != null)
            {
                context.Remove(computer);
            }
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
	}
}
