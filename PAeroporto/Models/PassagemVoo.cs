using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class PassagemVoo
    {
        public string ID { get; set; }
        public Voo IDVoo { get; set; }
        public DateTime DataUltimaOperacao { get; set; }
        public float Valor { get; set; }
        public char Situacao { get; set; }
    }
}
