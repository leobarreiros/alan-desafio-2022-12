using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Integrations.Cotacoes;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.Extensions.FluentValidation.DefaultsValidationMessages;

namespace Domain.Services
{
    public class PedidoCotacaoService : IPedidoCotacaoService
    {
        private readonly ICotacoesClient _cotacoesClient;
        private readonly IClienteRepository _clienteRepository;

        private const string VALOR_SOLICITADO_INVALIDO = "Valor do pedido deve ser positivo";

        public PedidoCotacaoService(
            ICotacoesClient cotacoesClient,
            IClienteRepository clienteRepository)
        {
            _cotacoesClient = cotacoesClient;
            _clienteRepository = clienteRepository;
        }

        public async ValueTask<ValueResult<PedidoCotacao>> ConsultarCotacaoAsync(
            Moeda de, 
            Moeda para, 
            Guid clienteId, 
            CancellationToken cancellationToken)
        {
            return await ObterCotacao(de, para, clienteId, 0, cancellationToken);
        }

        public async ValueTask<ValueResult<PedidoCotacao>> SolicitarCotacaoAsync(
            Moeda de, 
            Moeda para, 
            Guid clienteId,
            decimal valorSolicitado,
            CancellationToken cancellationToken)
        {
            if (valorSolicitado <= 0)
                return ValueResult<PedidoCotacao>.Faliure(VALOR_SOLICITADO_INVALIDO);

            return await ObterCotacao(de, para, clienteId, valorSolicitado, cancellationToken);
        }

        private async ValueTask<ValueResult<PedidoCotacao>> ObterCotacao(
            Moeda de,
            Moeda para,
            Guid clienteId,
            decimal valorSolicitado,
            CancellationToken cancellationToken)
        {
            var cotacaoResult = Cotacao.Criar(de, para);

            if (cotacaoResult.Failed)
                return ValueResult<PedidoCotacao>.Faliure(cotacaoResult.Error);

            var cliente = await _clienteRepository.ObterClienteAsync(clienteId, cancellationToken);

            if (cliente == null)
                return ValueResult<PedidoCotacao>.Faliure(CLIENTE_NAO_LOCALIZADO);

            var cotacoesResult = await _cotacoesClient.GetCotacaoAsync(de.ToString(), para.ToString(), cancellationToken);

            if (cotacaoResult.Failed)
                return ValueResult<PedidoCotacao>.Faliure(cotacaoResult.Error);

            if (!cotacoesResult.Value.Cotacoes.Any())
                return ValueResult<PedidoCotacao>.Faliure(COTACAO_NAO_OBTIDA);

            var ultimaCotacao = cotacoesResult.Value.Cotacoes.First();

            cotacaoResult.Value.Valor = ultimaCotacao.Bid;

            return PedidoCotacao.Solicitar(cliente, cotacaoResult.Value, valorSolicitado);
        }
    }
}
