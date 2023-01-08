using Shared.ResponseHandlers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Clientes.CadastrarCliente.Dtos
{
    public class CadastrarClienteResponseDto : ResponseHandlerDto
    {
        public CadastrarClienteResponseDto()
        {

        }

        public CadastrarClienteResponseDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
