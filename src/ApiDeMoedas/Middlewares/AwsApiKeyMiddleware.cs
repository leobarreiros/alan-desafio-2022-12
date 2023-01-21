using Amazon.Extensions.NETCore.Setup;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Shared;
using System.Text.Encodings.Web;
using System.Linq;

namespace ApiDeMoedas.Middlewares
{
    public class AwsApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "apikey";
        private const string APIKEY_NAO_ENCONTRADA = "ApiKey não encontrada";
        private const string APIKEY_NAO_AUTORIZADA = "Acesso não autorizado";
        private const string AWS_SECTION = "AWS";
        private const string AWS_USER = "user";
        private const string FALHA_CONFIGURACAO_CREDENCIAIS = "Credenciais de acesso não configuradas corretamente";

        public AwsApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEY, out
                    var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(APIKEY_NAO_ENCONTRADA);
                return;
            }

            var validAccessKeyResult = await ValidateAccessKey(context, extractedApiKey);

            if (validAccessKeyResult.Failed)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(validAccessKeyResult.Error);
                return;
            }

            await _next(context);
        }

        private async ValueTask<ValueResult> ValidateAccessKey(HttpContext context, string apikey)
        {
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            
            var awsIAMClient = context.RequestServices.GetRequiredService<IAmazonIdentityManagementService>();
            var keysRequest = new ListAccessKeysRequest();
            keysRequest.UserName = appSettings.GetSection(AWS_SECTION).GetValue<string>(AWS_USER);

            var accessKeyList = await awsIAMClient.ListAccessKeysAsync(keysRequest);

            if (accessKeyList?.AccessKeyMetadata == null)
                return ValueResult.Faliure(FALHA_CONFIGURACAO_CREDENCIAIS);

            if (!accessKeyList.AccessKeyMetadata.Any(key => key.AccessKeyId == apikey))
            {
                return ValueResult.Faliure(APIKEY_NAO_AUTORIZADA);
            }

            return ValueResult.Success();
        }
    }
}
