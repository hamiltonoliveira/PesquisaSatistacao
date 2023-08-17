using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Interfaces
{
    public interface IUsuarioRepositorio : IRepository<Usuario>
    {
        //Task<List<Usuario>> WhereAsync(Expression<Func<Usuario, bool>> expression);
    }
}
