using Shared.ResponseHandlers.Dtos;

namespace Queries.Cotacoes.ObterCotacao.Dtos
{
    public class ObterUltimaCotacaoResponseDto : ResponseHandlerDto
    {
        public ObterUltimaCotacaoResponseDto()
        {

        }

        public ObterUltimaCotacaoResponseDto(decimal valorOriginal, decimal valorComTaxa)
        {
            ValorOriginal = valorOriginal;
            ValorComTaxa = valorComTaxa;
        }

        public decimal ValorOriginal { get; set; }
        public decimal ValorComTaxa { get; set; }
    }
}
