using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
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

            double neededWidth = gdBestelling.ActualWidth * 0.25;
            fiets.Width = neededWidth;
            prijs.Width = neededWidth;
            verzekering.Width = neededWidth;
            service.Width = neededWidth;


            StackPanel bestelling = new StackPanel();
            bestelling.Orientation = Orientation.Horizontal;
            bestelling.Children.Add(fiets);
            bestelling.Children.Add(verzekering);
            bestelling.Children.Add(service);
            bestelling.Children.Add(prijs);

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
            if(Win32.GetIdleTime() > 500)
            {
                sluitenIn--;
                pgrbSluiten.Value = sluitenIn;
                tbSluitenOver.Text = $"Sluit over: {sluitenIn} seconden";
                

            }
            else
            {
                sluitenIn = 60;
                pgrbSluiten.Value = sluitenIn;
                tbSluitenOver.Text = $"Sluit over: {sluitenIn} seconden";
            }
            if(sluitenIn == 0)
            {
                this.Close();
            }
        }

    }
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    public class Win32
    {
        [DllImport("User32.dll")]
        public static extern bool LockWorkStation();

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        public static long GetLastInputTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }

            return lastInPut.dwTime;
        }
    }
}
