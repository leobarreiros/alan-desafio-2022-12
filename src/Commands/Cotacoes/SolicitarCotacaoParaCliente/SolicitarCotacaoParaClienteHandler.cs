using Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos;
using Commands.Cotacoes.SolicitarCotacaoParaCliente.Validators;
using Domain.Interfaces.Services;
using static Shared.ResponseHandlers.ResponseHandler;

namespace Commands.Cotacoes.SolicitarCotacao
{
    public class SolicitarCotacaoParaClienteHandler
    {
        private readonly IPedidoCotacaoService _pedidoCotacaoService;
        private readonly SolicitarCotacaoParaClienteRequestValidator _requestValidator;

        public SolicitarCotacaoParaClienteHandler(
            IPedidoCotacaoService pedidoCotacaoService,
            SolicitarCotacaoParaClienteRequestValidator validator)
        {
            _requestValidator = validator;
            _pedidoCotacaoService = pedidoCotacaoService;
        }

        public async Task<SolicitarCotacaoParaClienteResponseDto> Handle(
            SolicitarCotacaoParaClienteRequestDto request,
            CancellationToken cancellationToken)
        {
            var requestValidatorResult = _requestValidator.TryValidate(request);

            if (!requestValidatorResult.IsValid)
                return BadRequest<SolicitarCotacaoParaClienteResponseDto>(requestValidatorResult);

            var pedidoResult = await _pedidoCotacaoService.SolicitarCotacaoAsync(
                    request.De, request.Para, request.ClienteId, request.ValorCotadoEmReais, cancellationToken);

            if (pedidoResult.Failed)
                return BadRequest<SolicitarCotacaoParaClienteResponseDto>(pedidoResult.Error);

            return OK(new SolicitarCotacaoParaClienteResponseDto(
                        new ClienteResponseDto(
                            pedidoResult.Value.Cliente.Nome,
                            pedidoResult.Value.Cliente.Email,
                            pedidoResult.Value.Cliente.Id),
                        pedidoResult.Value.ValorDoPedido,
                        pedidoResult.Value.Cotacao.Valor,
                        pedidoResult.Value.CotacaoComTaxa));
        }
    }
}
