using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Voo
    {
        public string ID { get; set; }
        public string Destino { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public Aeronave InscAeronave { get; set; }
        public int AssentosOcupados { get; set; }

    }
}
