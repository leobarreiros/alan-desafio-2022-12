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
            decimal valorCotadoEmDolar, 
            decimal valorOriginal, 
            decimal valorComTaxa)
        {
            Cliente = cliente;
            ValorCotadoEmDolar = valorCotadoEmDolar;
            ValorOriginal = valorOriginal;
            ValorComTaxa = valorComTaxa;
        }

        public ClienteResponseDto Cliente { get; set; }
        public decimal ValorCotadoEmDolar { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorComTaxa { get; set; }
    }
}
