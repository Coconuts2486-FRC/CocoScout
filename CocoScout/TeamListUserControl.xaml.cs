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

namespace CocoScout
{
    /// <summary>
    /// Interaction logic for TeamListUserControl.xaml
    /// </summary>
    public partial class TeamListUserControl : UserControl
    {
        public TeamListUserControl()
        {
            InitializeComponent();
            List<User> items = new List<User>();
            items.Add(new User() { Test = 1, TeamNumber = 2486, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new User() { Test = 1, TeamNumber = 2486, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new User() { Test = 1, TeamNumber = 2, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            lvUsers.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Test");
            view.GroupDescriptions.Add(groupDescription);
        }

        public class User
        {
            public int Test { get; set; }

            public int TeamNumber { get; set; }

            public int MatchNumber { get; set; }

            public int TeleOpScore { get; set; }

            public int AutoScore { get; set; }
        }
    }
}
