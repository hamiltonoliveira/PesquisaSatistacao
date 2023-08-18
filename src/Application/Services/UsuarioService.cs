using Application.Interfaces;
using Domain.Entities;
using Domain.ViewModels;
using Infra.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usersRepositorio;
        public UsuarioService(IUsuarioRepositorio usersRepositorio)
        {
            _usersRepositorio = usersRepositorio;
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

        public async Task<MensagemView> InsertAsync(Usuario entity)
        {
            var mensagemView = new MensagemView();

            var retorno = await _usersRepositorio.InsertAsync(entity);

            if (retorno != null)
            {
                mensagemView.Sucesso = false;
                mensagemView.Mensagem = "Usuário cadastrado.";
            }
            return mensagemView;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            await _usersRepositorio.UpdateAsync(entity);
        }

        public List<Usuario> Where(Expression<Func<Usuario, bool>> expression)
        {
            return  _usersRepositorio.Where(expression).ToList();
        }
    }
}
