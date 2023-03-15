using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

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
            progressStart();
            

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
        int dagen = 1;
        double afrekenTotaal = 0;
        private void btToevoegen_Click(object sender, RoutedEventArgs e)
        {
            TextBlock fiets = new TextBlock();
            TextBlock prijs = new TextBlock();
            TextBlock verzekering = new TextBlock();
            TextBlock service = new TextBlock();
            int bestelPrijs = 0;
            try
            {
                
                dagen = Convert.ToInt16(txtDagen.Text);
                if (dagen >= 1 && cmbFietsen.SelectedIndex >= 1)
                {
                    bestelPrijs = (int)prijsfietsList[cmbFietsen.SelectedIndex - 1];
                        // + (int)prijsService[cmbService.SelectedIndex - 1] + (int)prijsverzekeringlist[cmbVerzekering.SelectedIndex - 1]) * dagen).ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                    fiets.Text = fietsList[cmbFietsen.SelectedIndex - 1];
                    if (cmbVerzekering.SelectedIndex >= 1)
                    {
                        verzekering.Text = verzekeringList[cmbVerzekering.SelectedIndex - 1];
                        bestelPrijs += (int)prijsverzekeringlist[cmbVerzekering.SelectedIndex - 1];
                    }
                    else
                    {
                        verzekering.Text = "Geen verzekering";
                    }
                    if (cmbService.SelectedIndex >= 1)
                    {
                        service.Text = ServiceList[cmbService.SelectedIndex - 1];
                        bestelPrijs += (int)prijsService[cmbService.SelectedIndex - 1];
                    }
                    else
                    {
                        service.Text = "Geen service";
                    }
                    bestelPrijs = bestelPrijs * dagen;
                    
                    prijs.Text = bestelPrijs.ToString("C", new System.Globalization.CultureInfo("nl-NL"));
                    btAfrekenen.IsEnabled = true;

                }
                else
                {
                    throw new Exception("werkt niet");
                }
                
                
            }
            catch
            {
                MessageBox.Show("Selecteer een of meerdere fiets(en) en een of meerdere dag(en).");
                cmbFietsen.SelectedIndex = 0;
                cmbService.SelectedIndex = 0;
                cmbVerzekering.SelectedIndex = 0;
                txtDagen.Text = "";
                return;
            }
            //ListBoxItem bestelling = new ListBoxItem();
            

            fiets.Padding = new Thickness(10);
            prijs.Padding = new Thickness(10);
            verzekering.Padding = new Thickness(10);
            service.Padding = new Thickness(10);

            fiets.FontSize = 15;
            prijs.FontSize = 15;
            verzekering.FontSize = 15;
            service.FontSize = 15;

            double neededWidth = gdBestelling.ActualWidth * 0.20;
            fiets.Width = neededWidth;
            prijs.Width = neededWidth;
            verzekering.Width = neededWidth;
            service.Width = neededWidth;
            
            Button remove = new Button();
            
            remove.Width = neededWidth;
            remove.Content = "X";
            remove.FontSize = 20;
            remove.BorderBrush = Brushes.Transparent;
            remove.Foreground = Brushes.Red;
            remove.Background = Brushes.Transparent;
            


            StackPanel bestelling = new StackPanel();
            bestelling.Orientation = Orientation.Horizontal;
            bestelling.Children.Add(fiets);
            bestelling.Children.Add(verzekering);
            bestelling.Children.Add(service);
            bestelling.Children.Add(prijs);
            bestelling.Children.Add(remove);
            remove.Click += Remove_Click;




            //lbBestelling.Items.Add(fiets + prijs + verzekering + service);
            try
            {
                afrekenTotaal = afrekenTotaal + bestelPrijs;
            }
            catch
            {
                MessageBox.Show("Er is iets misgegaan het uitrekenen van de prijs. probeer het opniew.");
            }
            spBestellingen.Children.Add(bestelling);
            btAfrekenen.Content =  "Afrekenen " + afrekenTotaal.ToString("C", new System.Globalization.CultureInfo("nl-NL"));

            
        }
        bool afgerekent = false;
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            //Pak de stackpanel die verwijderd wordt
            Button btn = sender as Button;
            StackPanel sp = btn.Parent as StackPanel;
            TextBlock tb = sp.Children[3] as TextBlock;
            
            //Pakt de 2 getallen die van elkaar af worden gehaald
            string a = tb.Text.ToString();
            string[] subs = a.Split(' ');
            double prijs = Convert.ToDouble(subs[1]);
            a = btAfrekenen.Content.ToString();
            string[] subs2 = a.Split(' ');
            double prijsRemove = Convert.ToDouble(subs2[2]);
            afrekenTotaal = prijsRemove - prijs;
            //haalt de prijs van het totaal af en als het minder dan nul is word de hele tekst verwijderd en verwijderd ook de hele stackpanel en 
            if (afrekenTotaal != 0) 
            {
                btAfrekenen.Content = "Afrekenen " + ((afrekenTotaal).ToString("C", new System.Globalization.CultureInfo("nl-NL")));
            }
            else
            {
                btAfrekenen.Content = "Afrekenen";
            }
            
            StackPanel lb = sp.Parent as StackPanel;
            lb.Children.Remove(sp);

        }
        private void btVolgendeKlant_Click(object sender, RoutedEventArgs e)
        {
            
            if (afgerekent == false)
            {
                MessageBoxResult result = MessageBox.Show("U heeft nog niet afgerekent. Weet u zeker dat u wilt anuleeren?", "Weet u het zeker", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    spBestellingen.Children.Clear();
                    cmbFietsen.SelectedIndex = 0;
                    cmbService.SelectedIndex = 0;
                    cmbVerzekering.SelectedIndex = 0;
                    txtDagen.Text = "";
                    btAfrekenen.Content = "Afrekenen";
                    afrekenTotaal = 0;
                    afgerekent = false;
                }
            }
            else
            {
                spBestellingen.Children.Clear();
                cmbFietsen.SelectedIndex = 0;
                cmbService.SelectedIndex = 0;
                cmbVerzekering.SelectedIndex = 0;
                txtDagen.Text = "";
                afrekenTotaal = 0;
                btAfrekenen.Content = "Afrekenen";
                afgerekent = false;
            }
            
        }

        private void btAfrekenen_Click(object sender, RoutedEventArgs e)
        {
            
            btAfrekenen.Content = "Afrekenen";
            afgerekent = true;
            Contant win2 = new Contant(afrekenTotaal);
            win2.Show();
        }
        DispatcherTimer progress = new DispatcherTimer();
        private void progressStart()
        {
            DispatcherTimer progress = new DispatcherTimer();
            progress.Interval = TimeSpan.FromSeconds(1);
            progress.Tick += timer_Tick;
            progress.Start();
            sluitenIn = 60;
        }
        int sluitenIn = 60;
        void timer_Tick(object sender, EventArgs e)
        {
            
            
                sluitenIn--;
                pgrbSluiten.Value = sluitenIn;
                tbSluitenOver.Text = $"Sluit over: {sluitenIn} seconden";
                

            
            if(sluitenIn == 0)
            {
                /*Contant win = new Contant(afrekenTotaal);
                win.Close();
                this.Close();*/
                System.Environment.Exit(0);
            }
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sluitenIn = 61;
        }
    }
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

}
