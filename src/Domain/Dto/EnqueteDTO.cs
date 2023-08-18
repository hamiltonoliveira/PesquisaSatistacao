using Domain.Enum;

namespace Domain.Dto
{
    public class EnqueteDTO
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        public string? SatisfacaoNivel { get; set; }
    }
}
