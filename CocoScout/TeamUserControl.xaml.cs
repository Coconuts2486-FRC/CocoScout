using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace CocoScout
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TeamUserControl : UserControl
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        public static ObservableCollection<TeamStats> items;
        public TeamUserControl()
        {
            InitializeComponent();
            items = new ObservableCollection<TeamStats>();
#if DEBUG
            items.Add(new TeamStats() { TeamNumber = 2486, MatchNumber = 15, AutoScore = 250, TeleOpScore = 250, });
            items.Add(new TeamStats() { TeamNumber = 2486, MatchNumber = 10, AutoScore = 200, TeleOpScore = 20, });
            items.Add(new TeamStats() { TeamNumber = 2, MatchNumber = 10, AutoScore = 25, TeleOpScore = 2, });
            items.Add(new TeamStats() { TeamNumber = 50, MatchNumber = 1, AutoScore = 50, TeleOpScore = 5, });
            items.Add(new TeamStats() { TeamNumber = 500, MatchNumber = 13, AutoScore = 60, TeleOpScore = 75, });
#endif
            TeamView.ItemsSource = items;
            TeamView.Items.Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TeamView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("TeamNumber");
            view.SortDescriptions.Add(new SortDescription("TeamNumber", ListSortDirection.Ascending));
            view.GroupDescriptions.Add(groupDescription);
            view.Filter = UserFilter;
        }

        public static void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete that entry?", "Delete Item", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (stats != null)
                    items.Remove(stats);
                else
                    MessageBox.Show("Nothing is selected.");
            }
        }

        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as TeamStats).TeamNumber.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(TeamView.ItemsSource).Refresh();
        }

        private void GridViewColumnHeader_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                TeamView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            TeamView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry =
                    Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

            private static Geometry descGeometry =
                    Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                    : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (AdornedElement.RenderSize.Width < 20)
                    return;

                TranslateTransform transform = new TranslateTransform
                        (
                                AdornedElement.RenderSize.Width - 15,
                                (AdornedElement.RenderSize.Height - 5) / 2
                        );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                    geometry = descGeometry;
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }
        static TeamStats stats;
        private void TeamView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stats = TeamView.SelectedItem as TeamStats;
        }
    }
}

