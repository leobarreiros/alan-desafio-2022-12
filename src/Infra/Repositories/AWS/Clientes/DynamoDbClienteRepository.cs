using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Interfaces.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.AWS.Clientes
{
    internal class ClienteRepository : IClienteRepository
    {
        private readonly IDynamoDBContext _dbContext;

        public ClienteRepository(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SalvarAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            await _dbContext.SaveAsync(DynamoDbClienteModel.Map(cliente), cancellationToken);
        }

        public async Task<Cliente> ObterClienteAsync(string email, CancellationToken cancellationToken)
        {
            var query = _dbContext.ScanAsync<DynamoDbClienteModel>(                    
                new List<ScanCondition>()
                {
                    new ScanCondition(
                        DynamoDbClienteModel.CLIENTE_MODEL_SCAN_EMAIL,
                        ScanOperator.Equal,
                        email)
                });

            var result = await query.GetRemainingAsync();

            return result != null && result.Any() 
                    ? result.FirstOrDefault().Map() 
                    : null;
        }

        public async Task<Cliente> ObterClienteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _dbContext.LoadAsync<DynamoDbClienteModel>(id.ToString(), cancellationToken);
                        
            return result != null 
                    ? result.Map()
                    : null;
        }
    }
}
