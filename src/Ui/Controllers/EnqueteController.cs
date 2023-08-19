using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.ViewModels;
using FluentValidation;
using Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Domain.Entities.Enquete;

namespace SisTarefa.Ui.Controller
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EnqueteController : ControllerBase
    {

        private readonly IEnqueteService _EnqueteService;
        protected readonly DataContext _db;

        private readonly IMapper _mapper;

        public EnqueteController(DataContext db, IMapper mapper, IEnqueteService EnqueteService)
        {
            _db = db;
            _mapper = mapper;
            _EnqueteService = EnqueteService;
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost("Criar")]
        [ProducesResponseType(typeof(EnqueteDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Criar([FromBody] EnqueteDTO enqueteDTO)
        {
            if (enqueteDTO is null)
            {
                throw new ArgumentNullException(nameof(enqueteDTO));
            }

            var mensagemView = new MensagemView();


            Enquete enquete = _mapper.Map<Enquete>(enqueteDTO);

            EnqueteValidation validator = new EnqueteValidation();
            var validationResult = validator.Validate(enqueteDTO);

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
                mensagemView = await _EnqueteService.InsertAsync(enquete); 

                if (mensagemView.Sucesso)
                {
                    mensagemView.Mensagem = "Enquete registrada.";
                    return Ok(mensagemView.Mensagem); 
                }

            }
            catch (DbUpdateException ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocorreu um erro ao criar a Enquete.",
                    ErrorCode = "CREATE_ENQUETE_ERROR"
                };
                return BadRequest(errorResponse);
            }
            return Ok(mensagemView.Mensagem);  
        }
    }
}