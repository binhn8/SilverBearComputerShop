using SilverBearComputerShop.Data;
using Microsoft.EntityFrameworkCore;
using SilverBearComputerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilverBearComputerShop.Repositories
{
    public class ComputerRepository : IRepository<Computer, int>
    {
        private readonly ComputerShopContext context;

		public ComputerRepository(ComputerShopContext context)
		{
			this.context = context;
		}
		public async Task<IEnumerable<Computer>> GetAll()
        {
            return await context.Computer.ToListAsync();
        }

        public async Task<Computer> GetById(int id)
        {
            return await context.Computer.FindAsync(id);
        }

        public async Task<Computer> GetDetailById(int id)
        {
            return await context.Computer
                   .Include(s => s.ComputerComponent)
                   .ThenInclude(e => e.Component)
                   .ThenInclude(d => d.ComponentType)
                   .AsNoTracking()
                   .FirstOrDefaultAsync(m => m.ID == id);
        }

        public IQueryable<Computer> GetByTextThenOrder(string searchString, string sortOrder)
        {
            IQueryable<Computer> computers;
            if (!string.IsNullOrEmpty(searchString))
            {
                computers = context.Computer.Where(c => c.Title.Contains(searchString));
               
            }
            else
            {
                computers=  context.Computer;
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
            return computers;
        }

        public async Task<IEnumerable<Computer>> GetByText(string searchString)
        {
            return await context.Computer.Where(c => c.Description.Contains(searchString))
                .ToListAsync();
        }

        public async Task<Computer> Insert(Computer entity)
        {
            await context.Computer.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var computer = await context.Computer.FindAsync(id);
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
