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

namespace WpfApp1
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool Spawn = false;
        int PocetPoli = 15;
        int PocetBomb = 20;
        int Cas = 0;
        int BombCTR = 20;
        int[,] PoleMin = new int[15,15];
        List<string> BombList = new List<string>();
        List<string> MarkedList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            VytvorPole();
            
            
        }
        public void VytvorPole()
        {
            int ctrColumn = 0;
            int ctrRow = 0;
            
            
            int MujCTR = 0;
            
            Grid DynamicGrid = test;
            DynamicGrid.Background = new SolidColorBrush(Colors.Gray);
            while(ctrColumn< PocetPoli)
            {
                ColumnDefinition Column = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(Column);
                RowDefinition gridRow1 = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(gridRow1);
                ctrColumn++;
            }
            for (int j = 0; j < PocetPoli; j++)
            {
                
                for (int k = 0; k < PocetPoli; k++)
                {
                    Label Hodnota = new Label();
                    Hodnota.VerticalAlignment = VerticalAlignment.Center;
                    Hodnota.HorizontalAlignment = HorizontalAlignment.Center;
                    Hodnota.Content = MujCTR;

                    Button Btn = new Button();
                    Btn.Background = new SolidColorBrush(Colors.DarkGray);
                    Btn.MouseRightButtonDown += myButton_RClick;
                    Btn.Click += myButton_Click;
                    Grid.SetRow(Btn, j);
                    Grid.SetColumn(Btn, k);
                    DynamicGrid.Children.Add(Btn);
                    MujCTR++;
                }
            }
        }
        void myButton_RClick(object sender, RoutedEventArgs e)
        {
            Button _btn = sender as Button;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);
            Grid DynamicGrid = test;
            Button Btn = new Button();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"Image\mina.png", UriKind.Relative));
            string VolnoOkoli1 = _row + "" + _column;
            var VolnoOkoliList1 = MarkedList.FirstOrDefault(stringToCheck => stringToCheck.Contains(VolnoOkoli1));
            if (VolnoOkoliList1 != null)
            {
                BombCTR++;
                Btn.Content = "";
                Btn.Background = new SolidColorBrush(Colors.DarkGray);
                BombCounterLabel.Content = "Počet min: " + BombCTR+"/20";
                MarkedList.Remove(_row + "" + _column);
            }
            else
            {
                BombCTR--;
                Btn.Content = image;
                BombCounterLabel.Content = "Počet min: "+BombCTR+"/20";
                MarkedList.Add(_row + "" + _column);

            }
            Btn.MouseRightButtonDown += myButton_RClick;
            Btn.Click += myButton_Click;
            Grid.SetRow(Btn, _row);
            Grid.SetColumn(Btn, _column);
            DynamicGrid.Children.Add(Btn);
        }
        void myButton_Click(object sender, RoutedEventArgs e)
        {

            Button _btn = sender as Button;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);
            //MessageBox.Show("řádek: "+_row+" sloupec: " + _column);
            SpawnMine(_row, _column);
        }
        void RevealPole(int selRadek, int selSloupec)
        {
            Grid DynamicGrid = test;
            Button Btn = new Button();
            Btn.Background = new SolidColorBrush(Colors.LightGray);
            Grid.SetRow(Btn, selRadek);
            Grid.SetColumn(Btn, selSloupec);
            DynamicGrid.Children.Add(Btn);
            /*
            for (int o = 0; o < 3; o++)
            {

                for (int p = 0; p < 3; p++)
                {
                    string VolnoOkoli1 = ((selRadek - 1) + o) + "" + ((selSloupec - 1) + p);
                    var VolnoOkoliList1 = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(VolnoOkoli1));
                    if (VolnoOkoliList1 == null)
                    {
                        RevealPole2(((selRadek - 1) + o), ((selSloupec - 1) + p));
                    }
                }
            }*/
            
            
        }
        void RevealPole2(int selRadek, int selSloupec)
        {
            if(selRadek<0)
            {
                selRadek = 0;
            }
            if(selSloupec<0)
            {
                selSloupec = 0;
            }
            Grid DynamicGrid = test;
            Button Btn = new Button();
            Btn.Background = new SolidColorBrush(Colors.LightGray);
            Grid.SetRow(Btn, selRadek);
            Grid.SetColumn(Btn, selSloupec);
            DynamicGrid.Children.Add(Btn);
        }
        void SpawnMine(int selRadek,int selSloupec)
        {
            if(Spawn)
            {
                string myString = selRadek + "" + selSloupec;
                var match = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(myString));
                if (match != null)
                {
                    MessageBox.Show("Konec HRY");

                    Spawn = false;
                    Cas = 0;
                    CasUkazatel();
                    Grid DynamicGrid = test;
                    DynamicGrid.Children.Clear();
                    MarkedList.Clear();
                    BombList.Clear();
                    int ctrColumn = 0;
                    int ctrRow = 0;


                    int MujCTR = 0;
                    
                    DynamicGrid.Background = new SolidColorBrush(Colors.Gray);
                    for (int j = 0; j < PocetPoli; j++)
                    {

                        for (int k = 0; k < PocetPoli; k++)
                        {
                            Label Hodnota = new Label();
                            Hodnota.VerticalAlignment = VerticalAlignment.Center;
                            Hodnota.HorizontalAlignment = HorizontalAlignment.Center;
                            Hodnota.Content = MujCTR;

                            Button Btn = new Button();
                            Btn.Background = new SolidColorBrush(Colors.DarkGray);
                            Btn.MouseRightButtonDown += myButton_RClick;
                            Btn.Click += myButton_Click;
                            Grid.SetRow(Btn, j);
                            Grid.SetColumn(Btn, k);
                            DynamicGrid.Children.Add(Btn);
                            MujCTR++;
                        }
                    }

                }
                else
                {
                    //MessageBox.Show(myString);
                    int FoundBombs = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        
                        for (int k = 0; k < 3; k++)
                        {
                            
                            string PoleOkoli = ((selRadek - 1) + j) + "" + ((selSloupec - 1) + k);
                            var BombaOkoli = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(PoleOkoli));
                            if (BombaOkoli != null)
                            {
                                FoundBombs++;
                            }
                            else
                            {
                                
                            }
                        }

                    }
                    if(FoundBombs==0)
                    {
                        
                        RevealPole(selRadek, selSloupec);
                        /*
                        for (int o = 0; o < 3; o++)
                        {

                            for (int p = 0; p < 3; p++)
                            {
                                string VolnoOkoli1 = ((selRadek - 1) + o) + "" + ((selSloupec - 1) + p);
                                var VolnoOkoliList1 = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(VolnoOkoli1));
                                if (VolnoOkoliList1 == null)
                                {
                                    RevealPole(((selRadek - 1) + o), ((selSloupec - 1) + p));
                                }
                            }
                        }*/
                    }
                    else
                    {
                        Grid DynamicGrid = test;
                        Button Btn = new Button();
                        Btn.Content = "" + FoundBombs;
                        if(FoundBombs==1)
                        {
                            Btn.Foreground = new SolidColorBrush(Colors.Blue);
                        }
                        else if(FoundBombs==2)
                        {
                            Btn.Foreground = new SolidColorBrush(Colors.Green);
                        }
                        Btn.Background = new SolidColorBrush(Colors.LightGray);
                        Grid.SetRow(Btn, selRadek);
                        Grid.SetColumn(Btn, selSloupec);
                        DynamicGrid.Children.Add(Btn);
                        Console.WriteLine("Pocet bomb:" + FoundBombs);
                    }
                    
                }
            }
            else
            {
                Spawn = true;
                CasUkazatel();
                int bombCTR = 0;
                Random rnd = new Random();
                while(true)
                {
                    if(bombCTR==PocetBomb)
                    {
                        break;
                    }
                    else
                    {
                        int radek = rnd.Next(0, PocetPoli);
                        int sloupec = rnd.Next(0, PocetPoli);
                        string VolnoOkoli1 = radek + "" + sloupec;
                        var VolnoOkoliList1 = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(VolnoOkoli1));
                        if(VolnoOkoliList1==null)
                        {
                            if (selRadek == radek && selSloupec == sloupec)
                            {

                            }
                            else
                            {
                                BombList.Add(radek + "" + sloupec);
                                bombCTR++;
                            }
                        }
                        
                    }
                }
                foreach (string prime in BombList)
                {
                    System.Console.WriteLine(prime);
                }
                SpawnMine(selRadek, selSloupec);
            }

        }
        public async void CasUkazatel()
        {
            while (Spawn)
            {
                await Task.Delay(1000);
                Cas++;
                Timer.Content = TimeSpan.FromSeconds(Cas).ToString(@"mm\:ss");
            }


        }
    }
}
