using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Clientes.CadastrarCliente.Dtos
{
    public class CadastrarClienteRequestDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public decimal MultiplicadorBase { get; set; }
    }
}
