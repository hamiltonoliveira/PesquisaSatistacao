using Domain.Entities;
using Domain.Helpers;
using Infra.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Domain.Dto.IAutenticarService;

namespace Infra.Repositories
{
    public class AutenticarRepositorio : IAutenticarRepositorio
    {
        private readonly IUsuarioRepositorio _userRepository;
        public AutenticarRepositorio(IUsuarioRepositorio userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<TokensDTO> GerarToKen(string email)
        {
            List<Usuario> usuario = _userRepository.Where(x => x.Email == email).ToList();

            var _GuidI = string.Empty;
            var _Email = string.Empty;

            foreach (var item in usuario)
            {
                _Email = item.Email;
                _GuidI = item.GuidI;
            }

            var _Token = generateJwtToken(_Email, _GuidI);
            var _TokenRefresh = generateJwtTokenRefresh(_Email, _GuidI);

            return new TokensDTO()
            {
                Token = _Token,
                TokenRefresh = _TokenRefresh,
            };
        }

        private string generateJwtToken(string email, string guid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string oculto = CodigoCripto.Cripto();
            var key = Encoding.ASCII.GetBytes(oculto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Email", email),
                    new Claim("Guid", guid),
                }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string generateJwtTokenRefresh(string guid, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string oculto = CodigoCripto.Cripto();
            var key = Encoding.ASCII.GetBytes(oculto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                   new Claim("Guid", guid),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}