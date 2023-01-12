using Commands.Clientes.CadastrarCliente;
using Commands.Clientes.CadastrarCliente.Dtos;
using Commands.Clientes.CadastrarCliente.Validators;
using Domain.Interfaces.Repositories;
using Domain.Models;
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

namespace UnitTests.Lib.App.Commands.Clientes
{
    public class CadastrarClienteHandlerUnitTest
    {
        private readonly Mock<IClienteRepository> _clienteRepository;
        private readonly CadastrarClienteHandler _handler;

        private const string TAG_EMAIL = "Email";
        private const string TAG_NOME = "Nome";
        private const string TAG_MULTIPLICADOR = "MultiplicadorBase";

        public CadastrarClienteHandlerUnitTest()
        {
            _clienteRepository = new Mock<IClienteRepository>();
            _handler = new CadastrarClienteHandler(_clienteRepository.Object, new CadastrarClienteRequestValidator());
        }

        [Fact]
        public async Task Incluir_cliente_com_sucesso()
        {
            var request = new CadastrarClienteRequestDto()
            {
                Email = "teste@teste.com",
                Nome = "teste",
                MultiplicadorBase = 1.1M
            };

            using (var cts = new CancellationTokenSource())
            {
                var result = await _handler.Handle(request, cts.Token);
                result.Status.Should().Be(HttpStatusCode.Created);
            }
        }

        [Fact]
        public async Task Incluir_cliente_ja_existe()
        {
            var cliente = Cliente.Criar("teste", "teste@teste.com", 1.1M);
            var request = new CadastrarClienteRequestDto()
            {
                Email = "teste@teste.com",
                Nome = "teste",
                MultiplicadorBase = 1.1M
            };

            _clienteRepository
                .Setup(x => x.ObterClienteAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(cliente);

            using (var cts = new CancellationTokenSource())
            {
                var result = await _handler.Handle(request, cts.Token);
                result.Status.Should().Be(HttpStatusCode.Created);
                result.Id.Should().Be(cliente.Id);
            }
        }

        [Theory]
        [MemberData(nameof(DadosEntradaInvalidosParaCadastrarCliente))]
        public async Task validar_dados_de_entrada(CadastrarClienteRequestDto request, string mensagemErro, string tag)
        {
            using (var cts = new CancellationTokenSource())
            {
                var result = await _handler.Handle(request, cts.Token);
                result.Status.Should().Be(HttpStatusCode.BadRequest);
                result.Errors.Should().Contain(erro => erro.Tag == tag && erro.Description == mensagemErro);
            }
        }

        public static IEnumerable<object[]> DadosEntradaInvalidosParaCadastrarCliente() =>
            new List<object[]>()
            {
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "testeteste.com",
                        Nome = "teste",
                        MultiplicadorBase = 1.1M
                    },
                    EMAIL_INVALIDO,
                    TAG_EMAIL
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "",
                        Nome = "teste",
                        MultiplicadorBase = 1.1M
                    },
                    CAMPO_VAZIO,
                    TAG_EMAIL
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = string.Empty.PadLeft(51, 'A'),
                        Nome = "teste",
                        MultiplicadorBase = 1.1M
                    },
                    TAMANHO_MAXIMO,
                    TAG_EMAIL
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "teste@teste.com",
                        Nome = "teste@123",
                        MultiplicadorBase = 1.1M
                    },
                    CARACTERES_INVALIDOS,
                    TAG_NOME
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "teste@teste.com",
                        Nome = "",
                        MultiplicadorBase = 1.1M
                    },
                    CAMPO_VAZIO,
                    TAG_NOME
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "teste@teste.com",
                        Nome = string.Empty.PadLeft(51, 'A'),
                        MultiplicadorBase = 1.1M
                    },
                    TAMANHO_MAXIMO,
                    TAG_NOME
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "teste@teste.com",
                        Nome = "teste",
                        MultiplicadorBase = -1.1M
                    },
                    CadastrarClienteRequestValidator.MULTIPLICADOR_BASE_MINIMO,
                    TAG_MULTIPLICADOR
                },
                new object[]
                {
                    new CadastrarClienteRequestDto()
                    {
                        Email = "teste@teste.com",
                        Nome = "teste",
                        MultiplicadorBase = 9
                    },
                    CadastrarClienteRequestValidator.MULTIPLICADOR_BASE_MAXIMO,
                    TAG_MULTIPLICADOR
                }
            };
    }
}
