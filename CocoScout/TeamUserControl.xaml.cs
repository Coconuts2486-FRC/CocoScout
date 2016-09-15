using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace CocoScout
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TeamUserControl : UserControl
    {
        public TeamUserControl()
        {
            InitializeComponent();
            List<TeamStats> items = new List<TeamStats>();
            items.Add(new TeamStats() { TeamNumber = 2486, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new TeamStats() { TeamNumber = 2486, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new TeamStats() { TeamNumber = 2, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new TeamStats() { TeamNumber = 50, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new TeamStats() { TeamNumber = 500, MatchNumber = 10, AutoScore = 250, TeleOpScore = 250, });
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
                return ((item as TeamStats).TeamNumber.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }
    }
}

