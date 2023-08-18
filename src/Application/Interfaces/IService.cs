using Domain.ViewModels;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IService<Tentity> where Tentity : class
    {
        Task<Tentity> GetIdAsync(int id);
        Task<Tentity> GetGuidAsync(string guid);
        Task<List<Tentity>> GetAllAsync(int Page, int PageSize);
        Task<MensagemView> InsertAsync(Tentity entity);
        Task UpdateAsync(Tentity entity);
        List<Tentity> Where(Expression<Func<Tentity, bool>> expression);
    }
}
