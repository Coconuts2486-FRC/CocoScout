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

namespace CocoScout
{
    /// <summary>
    /// Interaction logic for AddDataWindow.xaml
    /// </summary>
    public partial class AddDataWindow : Window
    {
        TeamStats TeamStats;

        public AddDataWindow()
        {
            TeamStats = new TeamStats();
            InitializeComponent();
            this.DataContext = TeamStats;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(TeamStats.AutoScore);
            Console.WriteLine(TeamStats.TeleOpScore);
            Console.WriteLine(TeamStats.MatchNumber);
            Console.WriteLine(TeamStats.TeamNumber);
            TeamUserControl.items.Add(TeamStats);
            this.Close();
        }

        private void TextUpdated(object sender, TextChangedEventArgs e)
        {

        }
    }
}
