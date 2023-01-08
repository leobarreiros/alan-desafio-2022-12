using Domain.Models.Enums;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cotacao
    {
        public Moeda De { get; private set; }
        public Moeda Para { get; private set; }
        public decimal Valor { get; set; }

        public static ValueResult<Cotacao> Criar(Moeda de, Moeda para, decimal valor = 0)
        {
            if (de == para)
            {
                var mensagem = new StringBuilder("Cotação ").Append(de).Append(" para ").Append(para).Append(" inválida");
                
                return ValueResult<Cotacao>.Faliure(
                    string.Format(mensagem.ToString(), de.ToString(), para.ToString()));
            }

            return ValueResult<Cotacao>.Success(new Cotacao()
            {
                De = de,
                Para = para,
                Valor = valor
            });
        }
    }
}
