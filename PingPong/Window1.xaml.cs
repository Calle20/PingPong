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
using System.Windows.Shapes;

namespace PingPong
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            foreach (object k in Enum.GetValues(typeof(Key)))
            {
                CBKeys1Down.Items.Add(k);
                CBKeys1Up.Items.Add(k);
                CBKeys2Down.Items.Add(k);
                CBKeys2Up.Items.Add(k);
            }
            CBKeys1Down.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player1Down);
            CBKeys1Up.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player1Up);
            CBKeys2Down.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player2Down);
            CBKeys2Up.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player2Up);
        }
        private void CBKeys1Up_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Key = (int)Enum.Parse(typeof(Key), CBKeys1Up.SelectedItem.ToString());
            Properties.Settings.Default.Player1Up = Key;
            Properties.Settings.Default.Save();
        }
        private void CBKeys2Down_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Key = (int)Enum.Parse(typeof(Key), CBKeys2Down.SelectedItem.ToString());
            Properties.Settings.Default.Player2Down = Key;
            Properties.Settings.Default.Save();
        }
        private void CBKeys2Up_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Key = (int)Enum.Parse(typeof(Key), CBKeys2Up.SelectedItem.ToString());
            Properties.Settings.Default.Player2Up = Key;
            Properties.Settings.Default.Save();
        }
        private void CBKeys1Down_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Key = (int)Enum.Parse(typeof(Key), CBKeys1Down.SelectedItem.ToString());
            Properties.Settings.Default.Player1Down = Key;
            Properties.Settings.Default.Save();
        }
        private void bntOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Player1Down = 26;
            Properties.Settings.Default.Player1Up = 24;
            Properties.Settings.Default.Player2Down = 76;
            Properties.Settings.Default.Player2Up = 82;
            CBKeys1Down.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player1Down);
            CBKeys1Up.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player1Up);
            CBKeys2Down.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player2Down);
            CBKeys2Up.Text = Enum.GetName(typeof(Key), Properties.Settings.Default.Player2Up);
        }
    }
}
