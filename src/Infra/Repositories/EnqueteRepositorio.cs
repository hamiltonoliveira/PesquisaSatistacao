using Azure;
using Domain.Entities;
using Infra.Data;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repositories
{
    public class EnqueteRepositorio : IEnqueteRepositorio, IDisposable
    {
        protected readonly DataContext _db;
        public EnqueteRepositorio(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Enquete>> GetAllAsync(int Page, int PageSize)
        {
            if (Page == 0)
                Page = 1;
            var retorna = await (from cust in _db.Enquete orderby cust.Nome select cust)
               .Where(x => x.Nome != null)
               .Skip((Page - 1) * PageSize)
               .Take(PageSize)
               .ToListAsync();
            return retorna;
        }

        public async Task<Enquete> GetGuidAsync(string guid)
        {
            return await _db.Enquete.FindAsync(guid);
        }

        public async Task<Enquete> GetIdAsync(int id)
        {
            return await _db.Enquete.FindAsync(id);
        }

        public async Task<Enquete> InsertAsync(Enquete entity)
        {
            var mensagem = string.Empty; 
            try
            {
                var verificaEnquete = _db.Enquete.SingleOrDefault(x => x.Nome == entity.Nome);

                if (verificaEnquete == null)
                {
                    var retorno = _db.Enquete.Add(entity);
                    await _db.CommitAsync();
                    return entity;  
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message.ToString();
            }

            return  null;  
        }

        public async Task UpdateAsync(Enquete entity)
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

        public IEnumerable<Enquete> Where(Expression<Func<Enquete, bool>> expression)
        {
            return  _db.Set<Enquete>().Where(expression).ToList();
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