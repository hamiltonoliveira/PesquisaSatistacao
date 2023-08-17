using Domain.Dto;

namespace Infra.Interfaces
{
    public interface IAutenticarRepositorio
    {
        Task<TokensDTO> GerarToKen(string GuidId);
    }
}
