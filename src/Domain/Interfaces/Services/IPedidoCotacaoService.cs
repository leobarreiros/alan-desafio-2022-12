using Domain.Models;
using Domain.Models.Enums;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IPedidoCotacaoService
    {
        ValueTask<ValueResult<PedidoCotacao>> ConsultarCotacaoAsync(
            Moeda de,
            Moeda para,
            Guid clienteId,
            CancellationToken cancellationToken);

        ValueTask<ValueResult<PedidoCotacao>> SolicitarCotacaoAsync(
            Moeda de,
            Moeda para,
            Guid clienteId,
            decimal valorSolicitado,
            CancellationToken cancellationToken);
    }
}
