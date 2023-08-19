using Domain.Dto;
using Domain.Entities;
using Domain.Helpers;
using Infra.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            List<Usuario> usuario = _userRepository.Where(x => x.Email == email && x.Ativo == true).ToList();

            var _GuidI = string.Empty;
            var _Email = string.Empty;

            foreach (var item in usuario)
            {
                _Email = item.Email;
                _GuidI = item.GuidI;
            }

            var _Token = generateJwtToken(_Email, _GuidI,"Usuario");
            var _TokenRefresh = generateJwtTokenRefresh(_Email, _GuidI, "Usuario");

            return new TokensDTO()
            {
                Token = _Token,
                TokenRefresh = _TokenRefresh,
            };
        }

        private string generateJwtToken(string email, string guid, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string oculto = CodigoCripto.Cripto();
            var key = Encoding.ASCII.GetBytes(oculto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Email", email),
                    new Claim("Guid", guid),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string generateJwtTokenRefresh(string email, string guid, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string oculto = CodigoCripto.Cripto();
            var key = Encoding.ASCII.GetBytes(oculto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Email", email),
                    new Claim("Guid", guid),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}