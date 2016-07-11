using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTaste.Model
{
    [Serializable()]
    public class Rotulo
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Enums.Marca Marca { get; set; }
        public List<Enums.Sabor> Sabores { get; set; }
        public List<Enums.Acidez> Acidez { get; set; }
        public List<Enums.Corpo> Corpo { get; set; }
        public List<Enums.AfterTaste> AfterTaste { get; set; }
        public List<Enums.Sabor> Aroma { get; set; }
        public Enums.Moagem Moagem { get; set; }
        public Enums.Torra Torra { get; set; }
        public Enums.Origem Origem { get; set; }
        public bool Especial { get; set; }
        public bool Premium { get; set; }
        public bool Premiado { get; set; }
    }
}
