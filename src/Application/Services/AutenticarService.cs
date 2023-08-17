using Application.Interfaces;
using Domain.Dto;
using Infra.Interfaces;


namespace Application.Services
{
    public class AutenticarService : IAutenticarService
    {
        private readonly IAutenticarRepositorio _autenticarRepositorio;
        public AutenticarService(IAutenticarRepositorio autenticarRepositorio)
        {
            _autenticarRepositorio = autenticarRepositorio;
        }

        public async Task<TokensDTO> GerarToKen(string Email)
        {
            TokensDTO tokens = await _autenticarRepositorio.GerarToKen(Email);
            return tokens;
        }
    }
}
