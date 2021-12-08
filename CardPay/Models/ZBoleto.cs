using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class ZBoleto
    {
        public ZBoleto()
        {
            this.DataEmissao = DateTime.Now;
        }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string NumerBoleto { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }

        public string CodigoBarras { get; set; }
        public string LinhaDigitavel { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }

    }
}
