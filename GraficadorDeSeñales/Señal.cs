using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorDeSeñales
{
    abstract class Señal
    {
        public List<Muestra> Muestras { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }
        public double AmplitudMaxima { get; set; }
        public abstract double evaluar(double tiempo);

        public void construirSeñal()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;

            for (double i = TiempoInicial; i <= TiempoFinal; i += periodoMuestreo)
            {
                double muestra = evaluar(i);
                Muestras.Add(new Muestra(i, muestra));

                if (Math.Abs(muestra) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra);
                }
            }
        }
    }
}