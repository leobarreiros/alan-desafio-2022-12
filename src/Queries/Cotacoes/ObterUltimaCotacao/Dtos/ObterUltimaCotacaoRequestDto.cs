using Domain.Models.Enums;

namespace Queries.Cotacoes.ObterCotacao.Dtos
{
    public class ObterUltimaCotacaoRequestDto
    {
        public ObterUltimaCotacaoRequestDto(Guid clienteId, Moeda de, Moeda para)
        {
            ClienteId = clienteId;
            De = de;
            Para = para;
        }

        public Moeda De { get; set; }
        public Moeda Para { get; set; }
        public Guid ClienteId { get; set; }
    }
}
