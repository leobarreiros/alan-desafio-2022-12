using System.Net.Http.Headers;
using Amazon.DynamoDBv2.DataModel;
using Commands.Clientes.CadastrarCliente;
using Commands.Clientes.CadastrarCliente.Validators;
using Commands.Cotacoes.SolicitarCotacao;
using Commands.Cotacoes.SolicitarCotacaoParaCliente.Validators;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Repositories.AWS.Clientes;
using Integrations.Cotacoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queries.Cotacoes.ObterCotacao;
using static Integrations.DefaultsHttpClients;

namespace Infra.DI
{
    public static class ApiDeMoedasServiceCollectionExtensions
    {
        public static void AddApiDeMoedasServices(this IServiceCollection services, ConfigurationManager config)
        {
            AddDomainServices(services);
            AddRepositories(services);
            AddHandlers(services);
            AddCotacaoClient(services, config.GetSection(COTACAO_CLIENT));
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped(x => 
                        new CadastrarClienteHandler(
                            x.GetRequiredService<IClienteRepository>(),
                            new CadastrarClienteRequestValidator()));

            services.AddScoped(x =>
                        new ObterUltimaCotacaoHandler(
                            x.GetRequiredService<IPedidoCotacaoService>()));

            services.AddScoped(x =>
                        new SolicitarCotacaoParaClienteHandler(
                            x.GetRequiredService<IPedidoCotacaoService>(),
                            new SolicitarCotacaoParaClienteRequestValidator()));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository>(x =>
                        new ClienteRepository(
                            x.GetRequiredService<IDynamoDBContext>()));
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddScoped<IPedidoCotacaoService>(x =>
                        new PedidoCotacaoService(
                            x.GetRequiredService<ICotacoesClient>(),
                            x.GetRequiredService<IClienteRepository>()));
        }

        private static void AddCotacaoClient(IServiceCollection services, IConfigurationSection config)
        {
            services.AddHttpClient(COTACAO_CLIENT)
                .ConfigureHttpClient(
                    b =>
                    {
                        b.BaseAddress = new Uri(config.GetValue<string>("url"));
                        b.DefaultRequestHeaders.Accept.Clear();
                        b.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    });

            services.AddScoped<ICotacoesClient>(x =>
                new CotacoesClient(
                    x.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(COTACAO_CLIENT)));
        }
    }
}
