using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            List<User> items = new List<User>();
            items.Add(new User() { TeamNumber = 2486, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new User() { TeamNumber = 2486, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new User() { TeamNumber = 2, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new User() { TeamNumber = 50, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new User() { TeamNumber = 500, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            lvUsers.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("TeamNumber");
            view.SortDescriptions.Add(new SortDescription("TeamNumber", ListSortDirection.Ascending));
            view.GroupDescriptions.Add(groupDescription);
            view.Filter = UserFilter;
        }

            private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as User).TeamNumber.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }
    }

        public class User
        {
            public int TeamNumber { get; set; }

            public int MatchNumber { get; set; }

            public int TeleOpScore { get; set; }

            public int AutoScore { get; set; }
        }
    }

