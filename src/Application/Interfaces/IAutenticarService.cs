using Domain.Dto;

namespace Application.Interfaces
{
    public interface IAutenticarService
    {
        Task<TokensDTO> GerarToKen(string GuidId);
    }
}
