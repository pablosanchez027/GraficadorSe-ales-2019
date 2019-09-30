﻿using System;
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
            double periodoMuestreo =
                1 / FrecuenciaMuestreo;

            Muestras.Clear();

            for (double i = TiempoInicial;
                i <= TiempoFinal;
                i += periodoMuestreo)
            {
                double muestra = evaluar(i);

                Muestras.Add(
                    new Muestra(i, muestra));

                if (Math.Abs(muestra) >
                    AmplitudMaxima)
                {
                    AmplitudMaxima =
                        Math.Abs(muestra);
                }

            }
        }

        public static Señal escalarAmplitud(
            Señal señalOriginal, double factorEscala)
        {

            SeñalResultante resultado =
                new SeñalResultante();
            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo =
                señalOriginal.FrecuenciaMuestreo;

            foreach (var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y * factorEscala;
                resultado.Muestras.Add(
                    new Muestra(
                        muestra.X,
                        nuevoValor)
                    );
                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima =
                        Math.Abs(nuevoValor);
                }
            }

            return resultado;
        }

        public static Señal desplazarAmplitud(
            Señal señalOriginal, double cantidadDesplazamiento)
        {

            SeñalResultante resultado =
                new SeñalResultante();
            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo =
                señalOriginal.FrecuenciaMuestreo;

            foreach (var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y + cantidadDesplazamiento;
                resultado.Muestras.Add(
                    new Muestra(
                        muestra.X,
                        nuevoValor)
                    );
                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima =
                        Math.Abs(nuevoValor);
                }
            }

            return resultado;
        }
    }

}