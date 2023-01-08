
namespace Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos
{
    public class ClienteResponseDto
    {
        public ClienteResponseDto(string nome, string email, Guid id)
        {
            Nome = nome;
            Email = email;
            Id = id;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
