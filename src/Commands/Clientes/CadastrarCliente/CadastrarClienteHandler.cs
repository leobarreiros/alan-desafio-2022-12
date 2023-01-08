using Commands.Clientes.CadastrarCliente.Dtos;
using Commands.Clientes.CadastrarCliente.Validators;
using Domain.Interfaces.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.ResponseHandlers.ResponseHandler;

namespace Commands.Clientes.CadastrarCliente
{
    public class CadastrarClienteHandler
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly CadastrarClienteRequestValidator _requestValidator;
        public CadastrarClienteHandler(

            IClienteRepository clienteRepository, 
            CadastrarClienteRequestValidator validator)
        {
            _clienteRepository = clienteRepository;
            _requestValidator = validator;
        }
        public async Task<CadastrarClienteResponseDto> Handle(
            CadastrarClienteRequestDto request, 
            CancellationToken cancellationToken)
        {
            var requestValidatorResult = _requestValidator.TryValidate(request);

            if (!requestValidatorResult.IsValid)
                return BadRequest<CadastrarClienteResponseDto>(requestValidatorResult);

            var clienteExistente = await _clienteRepository.ObterClienteAsync(request.Email, cancellationToken);

            if(clienteExistente != null)
                return Created(new CadastrarClienteResponseDto(clienteExistente.Id));

            var cliente = Cliente.Criar(request.Nome, request.Email, request.MultiplicadorBase);

            await _clienteRepository.SalvarAsync(cliente, cancellationToken);

            return Created(new CadastrarClienteResponseDto(cliente.Id));
        }
    }
}
