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
            while(ctrColumn<5)
            {
                ColumnDefinition Column = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(Column);
                RowDefinition gridRow1 = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(gridRow1);
                ctrColumn++;
            }
            for (int j = 0; j < 5; j++)
            {
                
                for (int k = 0; k < 5; k++)
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
        }
    }
}
