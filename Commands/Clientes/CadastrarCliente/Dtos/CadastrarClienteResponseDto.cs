using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Clientes.CadastrarCliente.Dtos
{
    public class CadastrarClienteResponseDto
    {
        public CadastrarClienteResponseDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
