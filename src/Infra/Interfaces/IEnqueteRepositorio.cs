using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IEnqueteRepositorio : IRepositorio<Enquete>
    {
        //Task<List<Usuario>> WhereAsync(Expression<Func<Usuario, bool>> expression);
    }
}
