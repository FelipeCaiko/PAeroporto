using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class ItemVenda
    {
        public int ID { get; set; }
        public PassagemVoo IDPassagem { get; set; }
        public Venda IDVenda { get; set; }
        public float ValorUnit { get; set; }
    }
}
