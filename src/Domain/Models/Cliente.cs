namespace Domain.Models
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public decimal MultiplicadorBase { get; private set; }

        public static Cliente Criar(string nome, string email, decimal multiplicadorBase)
        {
            return Criar(Guid.NewGuid(), nome, email, multiplicadorBase);
        }

        public static Cliente Criar(Guid id, string nome, string email, decimal multiplicadorBase)
        {
            return new Cliente()
            {
                Id = id,
                Nome = nome,
                Email = email,
                MultiplicadorBase = multiplicadorBase,
            };
        }
    }
}