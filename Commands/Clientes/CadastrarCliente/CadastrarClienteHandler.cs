using Commands.Clientes.CadastrarCliente.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Clientes.CadastrarCliente
{
    public class CadastrarClienteHandler
    {
        public async Task<CadastrarClienteResponseDto> Handle(CadastrarClienteRequestDto request)
        {
            return new CadastrarClienteResponseDto(Guid.NewGuid());
        }
    }
}
