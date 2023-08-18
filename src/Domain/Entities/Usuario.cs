using Domain.Dto;
using FluentValidation;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Usuario : Base
    {
        public string? Nome { get;  set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Senha { get; set; }
    }

    public class UsuarioValidation : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .EmailAddress().WithMessage("O campo Email está em um formato inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("O campo Senha é obrigatório.")
                .MaximumLength(8).WithMessage("O campo Senha deve ter no máximo 8 caracteres.");
        }
    }
}
