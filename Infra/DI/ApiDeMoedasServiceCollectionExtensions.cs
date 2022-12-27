using System;
using Commands.Clientes.CadastrarCliente;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DI
{
    public static class ApiDeMoedasServiceCollectionExtensions
    {
        public static void AddApiDeMoedasServices(this IServiceCollection services)
        {
            services.AddScoped(x => new CadastrarClienteHandler());
        }
    }
}
