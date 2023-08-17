using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<Tentity> where Tentity : class
    {
        Task<Tentity> GetIdAsync(int id);
        Task<Tentity> GetGuidAsync(string guid);
        Task<List<Tentity>> GetAllAsync(int Page, int PageSize);
        Task InsertAsync(Tentity entity);
        Task UpdateAsync(Tentity entity);
        Task DeleteAsync(int Id);
        IEnumerable<Tentity> Where(Expression<Func<Tentity, bool>> expression);
    }
}
