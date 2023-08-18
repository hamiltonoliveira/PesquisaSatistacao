using System.Linq.Expressions;

namespace Infra.Interfaces
{
    public interface IRepositorio<Tentity> where Tentity : class
    {
        Task<Tentity> GetIdAsync(int id);
        Task<Tentity> GetGuidAsync(string guid);
        Task<List<Tentity>> GetAllAsync(int Page, int PageSize);
        Task<Tentity> InsertAsync(Tentity entity);
        Task UpdateAsync(Tentity entity);
        IEnumerable<Tentity> Where(Expression<Func<Tentity, bool>> expression);
    }
}
