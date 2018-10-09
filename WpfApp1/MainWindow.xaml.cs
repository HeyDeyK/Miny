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
        int PocetPoli = 8;
        int PocetBomb = 5;
        int[,] PoleMin = new int[8,8];
        List<string> BombList = new List<string>();
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
            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            DynamicGrid.ShowGridLines = true;
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
                    Btn.Click += myButton_Click;
                    Grid.SetRow(Btn, j);
                    Grid.SetColumn(Btn, k);
                    DynamicGrid.Children.Add(Btn);
                    Console.WriteLine(j + ":" + k);
                    MujCTR++;
                }
            }
        }
        void myButton_Click(object sender, RoutedEventArgs e)
        {
            Button _btn = sender as Button;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);
            MessageBox.Show("řádek: "+_row+" sloupec: " + _column);
            SpawnMine(_row, _column);
        }
        void SpawnMine(int selRadek,int selSloupec)
        {
            if(Spawn)
            {
                string myString = selRadek + "" + selSloupec;
                var match = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(myString));
                if (match != null)
                {
                    MessageBox.Show("BOMBA");
                }
                else
                {
                    //MessageBox.Show(myString);
                    int FoundBombs = 0;
                    Console.WriteLine("Hledam bomby");
                    for (int j = 0; j < 3; j++)
                    {
                        
                        for (int k = 0; k < 3; k++)
                        {
                            
                            string PoleOkoli = ((selRadek - 1) + j) + "" + ((selSloupec - 1) + k);
                            var BombaOkoli = BombList.FirstOrDefault(stringToCheck => stringToCheck.Contains(PoleOkoli));
                            Console.WriteLine(PoleOkoli);
                            if (BombaOkoli != null)
                            {
                                Console.WriteLine("Nalezena bomba");

                                FoundBombs++;
                            }
                        }

                    }
                    Grid DynamicGrid = test;
                    Button Btn = new Button();
                    Btn.Content = "" + FoundBombs;
                    Grid.SetRow(Btn, selRadek);
                    Grid.SetColumn(Btn, selSloupec);
                    DynamicGrid.Children.Add(Btn);
                    Console.WriteLine("Pocet bomb:" + FoundBombs);
                }
            }
            else
            {
                Spawn = true;
                Random rnd = new Random();
                for (int j = 0; j < PocetBomb; j++)
                {

                    int radek = rnd.Next(0, PocetPoli);
                    int sloupec = rnd.Next(0, PocetPoli);

                    if (selRadek == radek && selSloupec == sloupec)
                    {

                    }
                    else
                    {
                        BombList.Add(radek + "" + sloupec);
                    }
                }
                foreach (string prime in BombList)
                {
                    System.Console.WriteLine(prime);
                }

            }

        }
    }
}
