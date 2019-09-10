using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorDeSeñales
{
    class SeñalExponencial : Señal
    {
        public double Alpha { get; set; }
        public SeñalExponencial()
        {
            Muestras = new List<Muestra>();
            Alpha = Alpha;
        }

        public SeñalExponencial(double alpha)
        {
            Alpha = alpha;
            Muestras = new List<Muestra>();
        }

        override public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Math.Exp(Alpha * tiempo);
            return resultado;
        }
    }
}
