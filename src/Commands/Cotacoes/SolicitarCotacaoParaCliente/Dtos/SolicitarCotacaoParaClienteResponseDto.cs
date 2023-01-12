using Shared.ResponseHandlers.Dtos;

namespace Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos
{
    public class SolicitarCotacaoParaClienteResponseDto : ResponseHandlerDto
    {
        public SolicitarCotacaoParaClienteResponseDto()
        {

        }
        public SolicitarCotacaoParaClienteResponseDto(
            ClienteResponseDto cliente, 
            decimal valorCotadoEmReais, 
            decimal valorOriginal, 
            decimal valorComTaxa)
        {
            Cliente = cliente;
            ValorCotadoEmReais = valorCotadoEmReais;
            ValorOriginal = valorOriginal;
            ValorComTaxa = valorComTaxa;
        }

        public ClienteResponseDto Cliente { get; set; }
        public decimal ValorCotadoEmReais { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorComTaxa { get; set; }
    }
}
