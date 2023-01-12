using Commands.Cotacoes.SolicitarCotacao;
using Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos;
using Commands.Cotacoes.SolicitarCotacaoParaCliente.Validators;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Shared.Extensions.FluentValidation.DefaultsValidationMessages;

namespace UnitTests.Lib.App.Commands.Cotacoes
{
    public class SolicitarCotacaoParaClienteHandlerUnitTest
    {
        private readonly Mock<IPedidoCotacaoService> _pedidoCotacaoService;
        private readonly SolicitarCotacaoParaClienteHandler _handler;

        private const string TAG_VALOR = "ValorCotadoEmReais";

        public SolicitarCotacaoParaClienteHandlerUnitTest()
        {
            _pedidoCotacaoService = new Mock<IPedidoCotacaoService>();
            _handler = new SolicitarCotacaoParaClienteHandler(
                _pedidoCotacaoService.Object, 
                new SolicitarCotacaoParaClienteRequestValidator());
        }

        [Fact]
        public async Task Solicitar_cotacao_com_sucesso()
        {
            var cliente = Cliente.Criar("teste", "teste@teste", 1.1M);
            var request = new SolicitarCotacaoParaClienteRequestDto(cliente.Id, Moeda.USD, Moeda.BRL, 1000);

            var cotacao = Cotacao.Criar(request.De, request.Para, 5.4M).Value;

            _pedidoCotacaoService
                .Setup(x => x.SolicitarCotacaoAsync(
                    It.Is<Moeda>(de => de == request.De),
                    It.Is<Moeda>(para => para == request.Para),
                    It.IsAny<Guid>(),
                    It.IsAny<decimal>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(PedidoCotacao.Solicitar(cliente, cotacao, request.ValorCotadoEmReais));

            using (var cts = new CancellationTokenSource())
            {
                var result = await _handler.Handle(request, cts.Token);
                result.Status.Should().Be(HttpStatusCode.OK);

                result.ValorOriginal.Should().Be(Math.Round(request.ValorCotadoEmReais / cotacao.Valor, 2));
                result.ValorComTaxa.Should().Be(Math.Round(request.ValorCotadoEmReais / (cotacao.Valor * cliente.MultiplicadorBase), 2));
                result.ValorCotadoEmReais.Should().Be(request.ValorCotadoEmReais);
                result.Cliente.Nome.Should().Be(cliente.Nome);
                result.Cliente.Email.Should().Be(cliente.Email);
                result.Cliente.Id.Should().Be(cliente.Id);                
            }
        }

        [Theory]
        [MemberData(nameof(DadosEntradaInvalidosParaSolicitarCotacao))]
        public async Task validar_dados_de_entrada(SolicitarCotacaoParaClienteRequestDto request, string mensagemErro, string tag)
        {
            using (var cts = new CancellationTokenSource())
            {
                var result = await _handler.Handle(request, cts.Token);
                result.Status.Should().Be(HttpStatusCode.BadRequest);
                result.Errors.Should().Contain(erro => erro.Tag == tag && erro.Description == mensagemErro);
            }
        }

        public static IEnumerable<object[]> DadosEntradaInvalidosParaSolicitarCotacao() =>
            new List<object[]>()
            {
                new object[]
                {
                    new SolicitarCotacaoParaClienteRequestDto(Guid.NewGuid(), Moeda.USD, Moeda.BRL, -100),
                    VALOR_MENOR_ZERO,
                    TAG_VALOR
                },
                new object[]
                {
                    new SolicitarCotacaoParaClienteRequestDto(Guid.NewGuid(), Moeda.USD, Moeda.BRL, 10000000001),
                    SolicitarCotacaoParaClienteRequestValidator.MUITO_RICO,
                    TAG_VALOR
                },
                new object[]
                {
                    new SolicitarCotacaoParaClienteRequestDto(Guid.NewGuid(), Moeda.USD, Moeda.USD, 100),
                    COTACAO_DE_PARA_INVALIDA,
                    ""
                }
            };
    }
}
