using Domain.Dto;
using Domain.Enum;
using FluentValidation;

namespace Domain.Entities
{
    public class Enquete :Base
    {
        public string Nome { get;  set; }
        public SatisfacaoNivel SatisfacaoNivel { get;  set; }
     
        public class EnqueteValidation : AbstractValidator<EnqueteDTO>
        {
            public EnqueteValidation()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage("O campo Nome é obrigatório.");
                RuleFor(x => x.SatisfacaoNivel).NotNull().WithMessage("O campo SatisfacaoNivel não pode ser nulo");
            }
        }
    }
}
