using Integrations.Cotacoes.Dtos;
using Shared;

namespace Integrations.Cotacoes
{
    public interface ICotacoesClient
    {
        ValueTask<ValueResult<CotacaoResponseDto>> GetCotacaoAsync(string de, string para, CancellationToken cancellationToken);
    }
}
