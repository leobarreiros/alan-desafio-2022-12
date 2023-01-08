using Domain.Models.Enums;

namespace Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos
{
    public class SolicitarCotacaoParaClienteRequestDto
    {
        public SolicitarCotacaoParaClienteRequestDto(Guid clienteId, Moeda de, Moeda para, decimal valor)
        {
            ClienteId = clienteId;
            De = de;
            Para = para;
            ValorCotadoEmReais = valor;
        }

        public Moeda De { get; set; }
        public Moeda Para { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorCotadoEmReais { get; set; }
    }
}
