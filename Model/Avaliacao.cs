using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTaste.Model
{
    [Serializable()]
    public class Avaliacao
    {
        public int Codigo { get; set; }
        public String Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public Rotulo Rotulo { get; set; }
        public List<String> Percepcoes { get; set; }
        public Enums.Humor Humor { get; set; }
        public decimal Temperatura { get; set; }
        public int SegundosExtração { get; set; }
        public int DiasDesdeMoagem { get; set; }
        public Enums.Cor Cor { get; set; }
        public String Observacoes { get; set; }
        public Enums.Crema Crema { get; set; }
        public bool Latte { get; set; }
        public bool Duplo { get; set; }
        public Enums.Xicara Xicara { get; set; }
        public int NotaGeral { get; set; }
        public int NivelAfterTaste { get; set; }
    }
}
