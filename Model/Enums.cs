using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTaste.Model
{
    [Serializable()]
    public class Enums
    {
        public static string Format(string name, bool reverse = false)
        {
            if (reverse)
            {
                name = name.Replace("#", "H_");
                name = name.Replace(" ", "_");
            }
            else
            {
                name = name.Replace("H_", "#");
                name = name.Replace("_", " ");
            }
            return name;
        }

        public enum Moagem
        {
            Chemex = 0,
            Coador = 1,
            Espresso = 2,
            French_Press = 3,
            Grãos = 4,
            Hário_V60 = 5,
            Moka = 6,
        }

        public enum Origem
        {
            Arábica = 0,
            Robusta = 1
        }

        public enum Marca
        {
            Nossa_Casa_Café = 0
        }

        public enum Torra
        {
            Clara = 0,
            Média_Clara = 3,
            Média = 1,
            Média_Escura = 4,
            Escura = 2,
        }

        public enum Acidez
        {
            Equilibrada = 0,
            Suave = 1,
            Delicada = 2,
            Cítrica = 3,
            Prolongada = 4,
        }

        public enum AfterTaste
        {
            Marcante = 0,
            Arredondado = 1,
            Duradouro = 2,
            Prazeroso = 3,
        }

        public enum Corpo
        {
            Alto = 0,
            Denso = 1,
            Balanceado = 2,
            Aveludado = 3,
        }

        public enum Sabor
        {
            Cacau = 0,
            Nozes = 1,
            Frutas = 2,
            Frutas_Secas = 3,
            Chocolate = 4,
            Caramelo = 5,
            Castanha = 6,
            Suave = 7,
        }

        public enum Humor
        {
            Agitado = 0,
            Indiferente = 1,
            Mal_Humorado = 2,
            Feliz = 3,
            Ansiado = 4,
            Irritado = 5,
            Eufórico = 6,
        }

        public enum Cor
        {
            H_edb860 = 0,
            H_ba8231 = 1,
            H_946a2d = 2,
            H_755421 = 3,
            H_694a1b = 4,
            H_533913 = 5,
            H_472b0d = 6,
            H_3c240a = 7,
            H_000000 = 8
        }

        public enum Crema
        {
            Espessa = 0,
            Normal = 1,
            Pouca = 2,
            Nenhuma = 3
        }

        public enum Xicara
        {
            Dellanno = 0,
            Mug = 1
        }
    }
}