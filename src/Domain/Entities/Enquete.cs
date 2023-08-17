using Domain.Enum;

namespace Domain.Entities
{
    public class Enquete :Base
    {
        public string Nome { get; private set; }
        public SatisfacaoNivel SatisfacaoNivel { get; private set; }
        public Enquete(string nome, SatisfacaoNivel satisfacaoNivel)
        {
            Nome = nome;
            SatisfacaoNivel = satisfacaoNivel;
        }
    }
}
