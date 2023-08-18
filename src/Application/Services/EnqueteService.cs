using Application.Interfaces;
using Domain.Entities;
using Domain.ViewModels;
using Infra.Interfaces;
using Infra.Repositories;
using System.Linq.Expressions;

namespace Application.Services
{
    public class EnqueteService : IEnqueteService
    {
        private readonly IEnqueteRepositorio _EnqueteRepositorio;

        public EnqueteService(IEnqueteRepositorio enqueteRepositorio)
        {
            _EnqueteRepositorio = enqueteRepositorio;
        }

        public async Task<List<Enquete>> GetAllAsync(int Page, int PageSize)
        {
            return await _EnqueteRepositorio.GetAllAsync(Page, PageSize);
        }

        public async Task<Enquete> GetGuidAsync(string guid)
        {
            return await _EnqueteRepositorio.GetGuidAsync(guid);
        }

        public async Task<Enquete> GetIdAsync(int id)
        {
            return await _EnqueteRepositorio.GetIdAsync(id);
        }

        public async Task<MensagemView> InsertAsync(Enquete entity)
        {
            var mensagemView = new MensagemView();

            try
            {
                var verificaos20doDia = _EnqueteRepositorio.Where(x => x.Criado.Date == entity.Criado.Date);

                if (verificaos20doDia.Count() >= 2)
                {
                    mensagemView.Sucesso = false;
                    mensagemView.Mensagem = "Limite diário de enquetes atingido - 20 alcançado.";
                }
                else
                {
                    await _EnqueteRepositorio.InsertAsync(entity);
                    mensagemView.Sucesso = true;
                    mensagemView.Mensagem = "Enquete inserida com sucesso!";
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ex.Message.ToString();
                mensagemView.Sucesso = false;
                mensagemView.Mensagem = "Ocorreu um erro ao inserir a enquete: " + errorResponse;
            }

            return mensagemView;
        }

        public async Task UpdateAsync(Enquete entity)
        {
            await _EnqueteRepositorio.UpdateAsync(entity);
        }

        public List<Enquete> Where(Expression<Func<Enquete, bool>> expression)
        {
            return _EnqueteRepositorio.Where(expression).ToList();
        }
    }
}
