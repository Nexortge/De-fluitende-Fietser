using System;
using System.Collections.Generic;
using System.Data;
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

namespace De_Fluitende_Fietser_Dekstop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ComboboxVul();
        }
        double[] prijsService = { 15, 10, 12.5, 20 };
        string[] ServiceList = { "Ophaalservice", "Regenpak", "Lunchpakket basis", "Lunchpakket uitgebreid" };
        double[] prijsverzekeringlist = { 5, 10, 5, 2.5 };
        string[] verzekeringList = { "Beschadegingen", "Diefstal", "Rechtsbijstand", "Ongevallen" };
        string[] fietsList = { "Aanhangfiets", "Driewielfiets", "Bakfiets", "Elektrische fiets", "Kinderfiets", "Ligfiets", "Oma fiets", "Racefiets", "Vouwfiets", "Speed pedelec", "Stadsfiets", "Zitfiets" };
        double[] prijsfietsList = { 20, 10, 35, 30, 15, 45, 12.50, 15, 10, 15, 12.50, 15 };
        private void ComboboxVul()
        {
            //Fietsen combobox

            //string[] fietsList = { "Aanhangfiets", "Driewielfiets", "Bakfiets", "Elektrische fiets", "Kinderfiets", "Ligfiets", "Oma fiets", "Racefiets", "Vouwfiets", "Speed pedelec", "Stadsfiets", "Zitfiets" };
            //double[] prijsfietsList = { 20, 10, 35, 30, 15, 45, 12.50, 15, 10, 15, 12.50, 15 };

            for (int i = 0; i < prijsfietsList.Length; i++)
            {
                string formattedString = prijsfietsList[i].ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                TextBlock fiets = new TextBlock();
                fiets.Text = $"{fietsList[i]} {formattedString} / dag";
                fiets.FontSize = 15;
                cmbFietsen.Items.Add(fiets);
            }

            //Verzekering combobox

            //double[] prijsverzekeringlist = { 5, 10, 5, 2.5 };
            //string[] verzekeringList = { "Beschadegingen", "Diefstal", "Rechtsbijstand", "Ongevallen" };
            for (int i = 0; i < verzekeringList.Length; i++)
            {
                string formattedString = prijsverzekeringlist[i].ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                TextBlock verzekering = new TextBlock();
                verzekering.Text = $"{verzekeringList[i]} {formattedString} / dag";
                if (verzekeringList[i] == "Diefstal")
                {
                    verzekering.Text = verzekering.Text + ", eigen risico";
                }
                verzekering.FontSize = 15;
                cmbVerzekering.Items.Add(verzekering);
            }

            //Service combobox

            //double[] prijsService = { 15, 10, 12.5, 20 };
            //string[] ServiceList = { "Ophaalservice", "Regenpak", "Lunchpakket basis", "Lunchpakket uitgebreid" };
            for (int i = 0; i < prijsService.Length; i++)
            {
                string formattedString = prijsService[i].ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                TextBlock Service = new TextBlock();
                Service.Text = $"{ServiceList[i]} {formattedString} / dag";
                Service.FontSize = 15;
                cmbService.Items.Add(Service);



            }
        }
        private void btToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //ListBoxItem bestelling = new ListBoxItem();
            TextBlock fiets = new TextBlock();
            TextBlock prijs = new TextBlock();
            TextBlock verzekering = new TextBlock();
            TextBlock service = new TextBlock();
            fiets.Text = fietsList[cmbFietsen.SelectedIndex - 1];
            prijs.Text = prijsfietsList[cmbFietsen.SelectedIndex - 1].ToString();
            verzekering.Text = verzekeringList[cmbVerzekering.SelectedIndex - 1];
            service.Text = ServiceList[cmbService.SelectedIndex - 1];
            fiets.Padding = new Thickness(10);
            prijs.Padding = new Thickness(10);
            verzekering.Padding = new Thickness(10);
            service.Padding = new Thickness(10);
            StackPanel bestelling = new StackPanel();
            bestelling.Orientation = Orientation.Horizontal;
            bestelling.Children.Add(fiets);
            bestelling.Children.Add(prijs);
            bestelling.Children.Add(verzekering);
            bestelling.Children.Add(service);
            
            //lbBestelling.Items.Add(fiets + prijs + verzekering + service);

            spBestellingen.Children.Add(bestelling);


        }
    }
}
