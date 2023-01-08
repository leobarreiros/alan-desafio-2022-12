using Amazon.DynamoDBv2.DataModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.AWS.Clientes
{
    [DynamoDBTable(CLIENTE_MODEL_TABLE_NAME)]
    internal class DynamoDbClienteModel
    {
        private const string CLIENTE_MODEL_TABLE_NAME = "clientes";
        private const string CLIENTE_MODEL_PROPERTY_ID = "id";
        private const string CLIENTE_MODEL_PROPERTY_NOME = "nome";
        private const string CLIENTE_MODEL_PROPERTY_EMAIL = "email";
        private const string CLIENTE_MODEL_PROPERTY_MULTIPLICADOR_BASE = "multiplicadorBase";

        public const string CLIENTE_MODEL_SCAN_EMAIL = "Email";

        [DynamoDBHashKey(CLIENTE_MODEL_PROPERTY_ID)]
        public string Id { get; set; }
        
        [DynamoDBProperty(CLIENTE_MODEL_PROPERTY_NOME)]
        public string Nome { get; set; }
        
        [DynamoDBProperty(CLIENTE_MODEL_PROPERTY_EMAIL)]
        public string Email { get; set; }
        
        [DynamoDBProperty(CLIENTE_MODEL_PROPERTY_MULTIPLICADOR_BASE)]
        public decimal MultiplicadorBase { get; set; }

        public static DynamoDbClienteModel Map(Cliente cliente)
        {
            return new DynamoDbClienteModel()
            {
                Id = cliente.Id.ToString(),
                Nome = cliente.Nome,
                Email = cliente.Email,
                MultiplicadorBase = cliente.MultiplicadorBase,
            };
        }

        public static Cliente Map(DynamoDbClienteModel cliente)
        {
            return Cliente.Criar(
                Guid.Parse(cliente.Id),
                cliente.Nome,
                cliente.Email,
                cliente.MultiplicadorBase
            );
        }

        public Cliente Map()
        {
            return Map(this);
        }
    }
}
