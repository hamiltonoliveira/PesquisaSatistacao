namespace Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime Criado { get; set; } = DateTime.UtcNow;
        public DateTime Alterado { get; set; } = DateTime.UtcNow;
        public string? GuidI { get; set; } = Guid.NewGuid().ToString();
        public Boolean Ativo { get; set; } = true;
    }
}
