using static Shared.FluentValidation.Extensions.CustomFluentValidation;
using static Shared.Extensions.FluentValidation.DefaultsValidationMessages;
using FluentValidation;
using Shared.FluentValidation;
using Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos;

namespace Commands.Cotacoes.SolicitarCotacaoParaCliente.Validators
{
    public class SolicitarCotacaoParaClienteRequestValidator : 
        BaseFluentValidation<SolicitarCotacaoParaClienteRequestDto>
    {
        private const string MUITO_RICO = "Você possuir tanto dinheiro assim?";

        public SolicitarCotacaoParaClienteRequestValidator()
        {
            this.RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ValorCotadoEmReais)
                .GreaterThan(0)
                .WithMessage(VALOR_MENOR_ZERO)
                .LessThan(10000000000)
                .WithMessage(MUITO_RICO);
        }
    }
}
