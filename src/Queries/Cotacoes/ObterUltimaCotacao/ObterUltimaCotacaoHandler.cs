using Queries.Cotacoes.ObterCotacao.Dtos;
using static Shared.ResponseHandlers.ResponseHandler;
using Domain.Interfaces.Services;

namespace Queries.Cotacoes.ObterCotacao
{
    public class ObterUltimaCotacaoHandler
    {
        private readonly IPedidoCotacaoService _pedidoCotacaoService;

        public ObterUltimaCotacaoHandler(
            IPedidoCotacaoService pedidoCotacaoService)
        {
            _pedidoCotacaoService = pedidoCotacaoService;
        }

        public async Task<ObterUltimaCotacaoResponseDto> Handle(
            ObterUltimaCotacaoRequestDto request, 
            CancellationToken cancellationToken)
        {
            var pedidoResult = await _pedidoCotacaoService.ConsultarCotacaoAsync(
                                    request.De, request.Para, request.ClienteId, cancellationToken);

            if (pedidoResult.Failed)
                return BadRequest<ObterUltimaCotacaoResponseDto>(pedidoResult.Error);

            return OK(new ObterUltimaCotacaoResponseDto(
                        pedidoResult.Value.Cotacao.Valor, 
                        pedidoResult.Value.CotacaoComTaxa));
        }
    }
}
