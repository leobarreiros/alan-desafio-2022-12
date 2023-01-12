using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions.FluentValidation
{
    public static class DefaultsValidationMessages
    {
        public const string CAMPO_VAZIO = "Campo vazio";
        public const string TAMANHO_MAXIMO = "Tamanho máximo";
        public const string CARACTERES_INVALIDOS = "Caracteres inválidos";
        public const string EMAIL_INVALIDO = "Email inválido";
        public const string VALOR_MENOR_ZERO = "Valor menor ou igual a zero";
        public const string REQUEST_FORMATO_INVALIDO = 
            "Não foi possível receber o seu request. Verifique se todos os campos estão com os tipos de dados corretos.";
        public const string CLIENTE_NAO_LOCALIZADO = "Cliente não localizado";
        public const string COTACAO_NAO_OBTIDA = "Cotação não pode ser obtida";
        public const string COTACAO_DE_PARA_INVALIDA = "Cotações De e Para precisam ser diferentes";
    }
}
