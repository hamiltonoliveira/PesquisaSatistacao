using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        //Task<List<Usuario>> WhereAsync(Expression<Func<Usuario, bool>> expression);
    }
}
