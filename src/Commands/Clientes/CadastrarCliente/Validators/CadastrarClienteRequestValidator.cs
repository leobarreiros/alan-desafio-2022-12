using System;
using System.Collections.Generic;
using Commands.Clientes.CadastrarCliente.Dtos;
using FluentValidation;
using FluentValidation.Results;
using static Shared.FluentValidation.Extensions.CustomFluentValidation;
using static Shared.Extensions.FluentValidation.DefaultsValidationMessages;
using Shared.FluentValidation.Extensions;
using Shared.FluentValidation;

namespace Commands.Clientes.CadastrarCliente.Validators
{
    public class CadastrarClienteRequestValidator : BaseFluentValidation<CadastrarClienteRequestDto>
    {
        public const string MULTIPLICADOR_BASE_MAXIMO = "Multiplicador base maior que o permitido";
        public const string MULTIPLICADOR_BASE_MINIMO = "Multiplicador base menor que o permitido";

        public CadastrarClienteRequestValidator()
        {
            this.RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(CAMPO_VAZIO)
                .MaximumLength(50)
                .WithMessage(TAMANHO_MAXIMO)
                .IsAlphaNumeric()
                .WithMessage(CARACTERES_INVALIDOS);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(CAMPO_VAZIO)
                .MaximumLength(50)
                .WithMessage(TAMANHO_MAXIMO)
                .EmailAddress()
                .WithMessage(EMAIL_INVALIDO);

            RuleFor(x => x.MultiplicadorBase)
                .GreaterThan(1)
                .WithMessage(MULTIPLICADOR_BASE_MINIMO)
                .LessThan(2)
                .WithMessage(MULTIPLICADOR_BASE_MAXIMO);
        }
    }
}
