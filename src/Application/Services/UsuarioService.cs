using Domain.Entities;
using Infra.Interfaces;

namespace Application.Services
{
    public class UsuarioService : IUsuarioRepositorio
    {
        private readonly IUsuarioRepositorio _usersRepositorio;

        public UsuarioService(IUsuarioRepositorio usersRepositorio)
        {
            _usersRepositorio = usersRepositorio;
        }
        public async Task DeleteAsync(int Id)
        {
            await _usersRepositorio.DeleteAsync(Id);
        }

        public async Task<List<Usuario>> GetAllAsync(int Page, int PageSize)
        {
            return await _usersRepositorio.GetAllAsync(Page, PageSize);
        }

        public async Task<Usuario> GetGuidAsync(string guid)
        {
            return await _usersRepositorio.GetGuidAsync(guid);
        }

        public async Task<Usuario> GetIdAsync(int id)
        {
            return await _usersRepositorio.GetIdAsync(id);
        }

        public async Task<Usuario> InsertAsync(Usuario entity)
        {
            return await _usersRepositorio.InsertAsync(entity);
        }

        public async Task UpdateAsync(Usuario entity)
        {
            await _usersRepositorio.UpdateAsync(entity);
        }

       
       
    }
}
