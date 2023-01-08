using Integrations.Cotacoes.Dtos;
using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cotacoes
{
    public class CotacoesClient : ICotacoesClient
    {
        private const string ERRO_AO_OBTER_COTACAO = "Ocorreu um erro ao obter a cotação";
        private readonly HttpClient _client;
        public CotacoesClient(HttpClient client)
        {
            _client = client;
        }

        public async ValueTask<ValueResult<CotacaoResponseDto>> GetCotacaoAsync(string de, string para, CancellationToken cancellationToken)
        {
            var result = new CotacaoResponseDto();
            var url = new StringBuilder(de).Append("-").Append(para);
            
            var jsonSettings = new JsonSerializerSettings()
            {
                Error = (sender, errorArgs) =>
                {
                    errorArgs.ErrorContext.Handled = true;
                }
            };

            var response = await _client.GetAsync(url.ToString(), cancellationToken);
            var responseString = response.Content != null 
                                    ? await response.Content.ReadAsStringAsync() 
                                    : ERRO_AO_OBTER_COTACAO;

            if (!response.IsSuccessStatusCode)
                return ValueResult<CotacaoResponseDto>.Faliure(responseString);                       

            result.Cotacoes = JsonConvert.DeserializeObject<List<CotacaoDto>>(responseString, jsonSettings);

            return ValueResult<CotacaoResponseDto>.Success(result);
        }
    }
}
