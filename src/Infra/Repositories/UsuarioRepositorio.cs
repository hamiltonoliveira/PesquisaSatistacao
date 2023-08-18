using Domain.Entities;
using Infra.Data;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio, IDisposable
    {
        protected readonly DataContext _db;
        public UsuarioRepositorio(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Usuario>> GetAllAsync(int Page, int PageSize)
        {
            if (Page == 0)
                Page = 1;
            var retorna = (from cust in _db.Usuario orderby cust.Nome select cust).Where(x => x.Nome != null)
            .Skip(Page - 1).Take(PageSize).ToListAsync();
            return retorna.Result;
        }

        public async Task<Usuario> GetGuidAsync(string guid)
        {
            return await _db.Usuario.FindAsync(guid);
        }

        public async Task<Usuario> GetIdAsync(int id)
        {
            return await _db.Usuario.FindAsync(id);
        }

        public async Task<Usuario> InsertAsync(Usuario entity)
        {
                var verificaUsuario = _db.Usuario.SingleOrDefault(x => x.Email == entity.Email);

                if (verificaUsuario == null)
                {
                    _db.Usuario.Add(entity);
                    await _db.CommitAsync();
                }

            return entity;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            try
            {
                var recebe = _db.Usuario.FindAsync(entity.Id);
                if (recebe != null)
                {
                    _db.Update(entity);
                    _db.Entry(entity).State = EntityState.Modified;
                    await _db.CommitAsync();
                }
            }
            finally { Dispose(); }
        }

        public IEnumerable<Usuario> Where(Expression<Func<Usuario, bool>> expression)
        {
            return  _db.Set<Usuario>().Where(expression).ToList();
        }
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            GC.SuppressFinalize(this);
        }

    }
}