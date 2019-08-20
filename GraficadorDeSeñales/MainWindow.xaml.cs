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

            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);

            double periodioMuestreo = 1.0 / frecuenciamuestreo;

            plnGrafica.Points.Clear();
            for(double i = tiempoinicial; i <= tiempofinal; i += periodioMuestreo)
            {
                plnGrafica.Points.Add(adaptarCoordenadas(i,señal.evaluar(i)));
            }

        }
        public Point adaptarCoordenadas(double x, double y)
        {
            return new Point(x * srcGrafica.Width, (-1 * ( y * ((srcGrafica.Height/2.0) -25 ) )) + (srcGrafica.Height / 2.0) );
        }
    }
}
