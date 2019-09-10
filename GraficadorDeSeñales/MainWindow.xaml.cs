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
            /*double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);*/
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaDeMuestreo = double.Parse(txtFrecuenciaDeMuestreo.Text);

            Señal señal;
            switch (cbTipoSeñal.SelectedIndex)
            {
                case 0:
                    señal = new SeñalParabolica();
                    break;
                case 1: //Senoidal
                    double amplitud = double.Parse(
                        ( (ConfiguraciónSeñalSenoidal) (panelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(
                        ((ConfiguraciónSeñalSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(
                        ((ConfiguraciónSeñalSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                case 2: //Exponencial
                    double alpha = double.Parse(
                        ((ConfiguraciónSeñalExponencial)(panelConfiguracion.Children[0])).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    break;
                default:
                    señal = null;
                    break;
            }

            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaDeMuestreo;

            señal.construirSeñal();

            double amplitudMaxima = señal.AmplitudMaxima;

            plnGrafica.Points.Clear();

            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(adaptarCoordenadas(muestra.X, muestra.Y, tiempoInicial, amplitudMaxima));
            }

            lblLimiteSuperior.Text = amplitudMaxima.ToString();
            lblLmiteInferior.Text = "-" + amplitudMaxima.ToString();

            plnEjeX.Points.Clear();
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoInicial, 0.0, tiempoInicial, amplitudMaxima));
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoFinal, 0.0, tiempoInicial, amplitudMaxima));

            plnEjeY.Points.Clear();
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima, tiempoInicial, amplitudMaxima));
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, -amplitudMaxima, tiempoInicial, amplitudMaxima));

        }

        public Point adaptarCoordenadas(double x, double y, double tiempoInicial, double amplitudMaxima)
        {

            return new Point((x - tiempoInicial) * srcGrafica.Width, (-1 * (y * (((srcGrafica.Height / 2.0) - 25) / amplitudMaxima))) + (srcGrafica.Height / 2));
        }

        private void CbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion.Children.Clear();
            switch (cbTipoSeñal.SelectedIndex)
            {
                case 0: //Parabolica
                    break;
                case 1: //Senoidal
                    panelConfiguracion.Children.Add(new ConfiguraciónSeñalSenoidal());
                    break;
                case 2: //Exponencial
                    panelConfiguracion.Children.Add(new ConfiguraciónSeñalExponencial());
                    break;
                default:
                    break;
            }
        }
    }
}