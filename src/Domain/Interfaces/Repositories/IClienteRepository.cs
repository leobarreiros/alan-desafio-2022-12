using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task SalvarAsync(Cliente cliente, CancellationToken cancellationToken);
        Task<Cliente> ObterClienteAsync(string email, CancellationToken cancellationToken);
        Task<Cliente> ObterClienteAsync(Guid id, CancellationToken cancellationToken);
    }
}
