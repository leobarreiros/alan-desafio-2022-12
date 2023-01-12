using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PedidoCotacao
    {
        private const string CLIENTE_NAO_INFORMADO = "Cliente não informado";
        private const string COTACAO_NAO_INFORMADA = "Cotação não informada";
        private const string VALOR_SOLICITADO_NEGATIVO = "Valor do pedido não pode ser negativo";

        public PedidoCotacao(
            Cliente cliente,
            Cotacao cotacao,
            decimal valorSolicitado,
            decimal multiplicadorCorrente,
            DateTime solicitadoEm)
        {
            Cliente = cliente;
            Cotacao = cotacao;
            ValorSolicitado = valorSolicitado;
            MultiplicadorCorrente = multiplicadorCorrente;
            SolicitadoEm = solicitadoEm;
        }

        public Cliente Cliente { get; private set; }
        public Cotacao Cotacao { get; private set; }
        public decimal ValorSolicitado { get; private set; }
        public decimal ValorDoPedido { get { return Math.Round(ValorSolicitado / Cotacao.Valor, 2); } }
        public decimal ValorDoPedidoComTaxa { get { return Math.Round(ValorSolicitado / CotacaoComTaxa, 2); } }
        public decimal CotacaoComTaxa { get { return Cotacao.Valor * MultiplicadorCorrente; } }
        public decimal MultiplicadorCorrente { get; private set; }
        public DateTime SolicitadoEm { get; private set; }
        public static ValueResult<PedidoCotacao> Solicitar(Cliente cliente, Cotacao cotacao, decimal valor)
        {
            if (cliente == null)
                return ValueResult<PedidoCotacao>.Faliure(CLIENTE_NAO_INFORMADO);

            if (cotacao == null)
                return ValueResult<PedidoCotacao>.Faliure(COTACAO_NAO_INFORMADA);

            if (valor < 0)
                return ValueResult<PedidoCotacao>.Faliure(VALOR_SOLICITADO_NEGATIVO);

            return ValueResult<PedidoCotacao>.Success(
                new PedidoCotacao(cliente, cotacao, valor, cliente.MultiplicadorBase, DateTime.UtcNow));
        }
    }
}
