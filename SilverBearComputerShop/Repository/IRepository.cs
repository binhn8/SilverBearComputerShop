using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilverBearComputerShop.Repositories
{
    public interface IRepository<T1, T2> where T1:class
    {
        Task<IEnumerable<T1>> GetAll();
        Task<IEnumerable<T1>> GetByText(string searchString);
        IQueryable<T1> GetByTextThenOrder(string searchString, string sortOrder);
        Task<T1> GetById(T2 id);
        Task<T1> GetDetailById(T2 id);
        Task<T1> Insert(T1 entity);
        Task Delete(T2 id);
        Task Save();
    }
}
