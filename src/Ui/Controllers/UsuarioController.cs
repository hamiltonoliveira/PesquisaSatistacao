using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SisTarefa.Ui.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IAutenticarService _autenticarService;
        private readonly IUsuarioService _usuarioService;
        protected readonly DataContext _db;

        private readonly IMapper _mapper;

        public UsersController(DataContext db, IMapper mapper, IAutenticarService autenticarService, IUsuarioService usuarioService)
        {
            _db = db;
            _mapper = mapper;
            _autenticarService = autenticarService;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("Criar")]
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        [ProducesResponseType(typeof(UsuarioDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Criar([FromBody] UsuarioDTO usuarioDto)
        {
            if (usuarioDto is null)
            {
                throw new ArgumentNullException(nameof(usuarioDto));
            }
            TokensDTO? tokens = null;
            Usuario user = _mapper.Map<Usuario>(usuarioDto);

            UsuarioValidation validator = new UsuarioValidation();
            var validationResult = validator.Validate(usuarioDto);

            List<string> errors = new List<string>();

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors.Add(failure.ErrorMessage);
                }
                var retorna = String.Join("| ", errors.ToArray());
                return Ok(retorna);
            }


            try
            {
                List<Usuario> usuario = _usuarioService.Where(x => x.Email == usuarioDto.Email);

                if (usuario.Count == 0)
                {
                    user.Senha = Criptograph.Encrypt(usuarioDto.Senha);
                    await _usuarioService.InsertAsync(user);
                    tokens = await _autenticarService.GerarToKen(usuarioDto.Email);
                }
                else
                {
                    return BadRequest("Usuário possue cadastro.");
                }
            }
            catch (DbUpdateException ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocorreu um erro ao criar o usuário.",
                    ErrorCode = "CREATE_USER_ERROR"
                };
                return BadRequest(errorResponse);
            }
            return Ok(tokens);
        }

      
        [AllowAnonymous]
        [HttpPost("RenovarToken")]
        [ProducesResponseType(typeof(IEnumerable<RenovarToken>), 200)]
        public async Task<IActionResult> RenovarToken([FromBody] RenovarToken renovarToken)
        {
            TokensDTO? tokens = null;
            try
            {
                tokens = await _autenticarService.GerarToKen(renovarToken.Email);
            }
            catch (DbUpdateException)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocorreu um erro ao criar o usuário.",
                    ErrorCode = "CREATE_USER_ERROR"
                };
                return BadRequest(errorResponse);
            }
            return Ok(tokens);
        }
    }
}
