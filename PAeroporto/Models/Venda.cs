using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Venda
    {
        public String ID { get; set; }
        public DateTime DataVenda { get; set; }
        public Passageiro CPFPassageiro { get; set; }
        public float ValorTotal { get; set; }

    }
}
