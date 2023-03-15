using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace De_Fluitende_Fietser_Dekstop
{
    /// <summary>
    /// Interaction logic for Contant.xaml
    /// </summary>
    public partial class Contant : Window
    {

        public Contant(double afrekenTotaal)
        {
            InitializeComponent();
            timerStart(afrekenTotaal);
            
        }
        double bedragNodig = 0;
        private void timerStart(double bedrag)
        {
            bedragNodig = bedrag;
            tbNodig.Text = bedrag.ToString("C", new System.Globalization.CultureInfo("nl-NL"));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            
        }
        double sum = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                double[] listAlltb = 
                {
                    Convert.ToInt32(tb100euro.Text) * 100,
                    Convert.ToInt32(tb50euro.Text) * 50,
                    Convert.ToInt32(tb20euro.Text) * 20, 
                    Convert.ToInt32(tb10euro.Text) * 10,
                    Convert.ToInt32(tb5euro.Text) * 5,
                    Convert.ToInt32(tb2euro.Text) * 2,
                    Convert.ToInt32(tb1euro.Text) * 1,
                    Convert.ToInt32(tb50cent.Text) * 0.5,
                    Convert.ToInt32(tb20cent.Text) * 0.2,
                    Convert.ToInt32(tb5cent.Text) * 0.05,
                    Convert.ToInt32(tb2cent.Text) * 0.02,
                    Convert.ToInt32(tb1cent.Text) * 0.01 
                };
                sum = 0;
                foreach (double bill in listAlltb)
                {
                    sum += bill;
                }
                tbBetaald.Text = sum.ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                if( sum < bedragNodig)
                {
                    tbBetaald.Foreground = Brushes.Red;
                }
                else
                {
                    tbBetaald.Foreground = Brushes.LightGreen;
                    tbOver.Text = (sum - bedragNodig).ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                }
            }
            catch
            {
                return;
            }
            
        }
       


        private void btSluitaf_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btBerekenBriefjes_Click(object sender, RoutedEventArgs e)
        {
            spMuntenTerug.Children.Clear();
            double bedragTerug = sum - bedragNodig;

            double[] listAllBriefjes = new double[] { 100, 50, 20, 10, 5, 2, 1, 0.5, 0.2, 0.05, 0.02, 0.01 };
            int[] listBriefjesHoeveelheid = new int[listAllBriefjes.Length];
            for (int i = 0; i < listAllBriefjes.Length; i++)
            {
                listBriefjesHoeveelheid[i] = Convert.ToInt32(Math.Floor(bedragTerug / listAllBriefjes[i]));
                bedragTerug -= listBriefjesHoeveelheid[i] * listAllBriefjes[i];
            }
            for (int i = 0; i < listAllBriefjes.Length; i++)
            {
                if (listBriefjesHoeveelheid[i] > 0)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = listBriefjesHoeveelheid[i] + " x " + listAllBriefjes[i].ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                    tb.Margin = new Thickness(0, 0, 0, 5);
                    spMuntenTerug.Children.Add(tb);
                }
            }
        }
    }
}
