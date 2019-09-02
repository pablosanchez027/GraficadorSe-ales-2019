using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorDeSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoinicial = double.Parse(txtTiempoInicial.Text);
            double tiempofinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciamuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            //SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);
            //SeñalParabolica señal = new SeñalParabolica();
            FuncionSigno señal = new FuncionSigno();

            double periodioMuestreo = 1.0 / frecuenciamuestreo;
            double amplitudMaxima = 0.0;

            plnGrafica.Points.Clear();

            for(double i = tiempoinicial; i <= tiempofinal; i += periodioMuestreo)
            {
                double valorMuestra = señal.evaluar(i);
                if (Math.Abs(valorMuestra) > amplitudMaxima)
                {
                    amplitudMaxima = Math.Abs(valorMuestra);
                }
                Muestra muestra = new Muestra(i, señal.evaluar(i));
                señal.Muestras.Add(muestra);
            }

            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(adaptarCoordenadas(muestra.X, muestra.Y, tiempoinicial, amplitudMaxima));
            }

            lblLimiteSuperior.Text = amplitudMaxima.ToString();
            lblLmiteInferior.Text = "-" + amplitudMaxima.ToString();

            plnEjeX.Points.Clear();
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoinicial, 0.0, tiempoinicial, amplitudMaxima));
            plnEjeX.Points.Add(adaptarCoordenadas(tiempofinal, 0.0, tiempoinicial, amplitudMaxima));

            plnEjeY.Points.Clear();
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima, tiempoinicial, amplitudMaxima));
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, -amplitudMaxima, tiempoinicial, amplitudMaxima));

        }
        public Point adaptarCoordenadas(double x, double y, double tiempoinicial, double amplitudMaxima)
        {
            return new Point((x - tiempoinicial) * srcGrafica.Width, (-1 * (y * (((srcGrafica.Height / 2.0) - 25) / amplitudMaxima))) + (srcGrafica.Height / 2));
        }
    }
}
