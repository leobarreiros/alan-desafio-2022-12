using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cotacoes.Dtos
{
    public class CotacaoDto
    {
        public string Code { get; set; }
        public string Codein { get; set; }
        public string Name { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal VarBid { get; set; }
        public decimal pctChange { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
    }
}
