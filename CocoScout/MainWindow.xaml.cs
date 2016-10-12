using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //string dir = @"%USERPROFILE%\.aws";
            //bool exists = Directory.Exists(dir);
            //if (exists)
            //    Directory.CreateDirectory(dir);
            //File.Copy("credentials.csv", @"%USERPROFILE%\.aws\");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDataWindow window2 = new AddDataWindow();
            window2.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TeamUserControl.Delete();
        }

        AmazonDynamoDBClient client;
        AmazonDynamoDBConfig adcConfig = new AmazonDynamoDBConfig()
        {
            ServiceURL = "s3.amazonaws.com",
            RegionEndpoint = Amazon.RegionEndpoint.USWest2
        };
        Amazon.DynamoDBv2.DocumentModel.Table table;
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            client = new AmazonDynamoDBClient(adcConfig);
            try
            {
                table = Amazon.DynamoDBv2.DocumentModel.Table.LoadTable(client, "ScoutingDatabaseTest");
                foreach (TeamStats stats in TeamUserControl.items)
                {
                    Document doc = new Document();

                    doc["TeamNumber"] = stats.TeamNumber;
                    doc["MatchNumber"] = stats.MatchNumber;
                    doc["AutoScore"] = stats.AutoScore;
                    doc["TeleOpScore"] = stats.TeleOpScore;

                    table.PutItem(doc);
                }
            }
            catch (Exception ex) { }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {

            client = new AmazonDynamoDBClient(adcConfig);
            try
            {
                table = Amazon.DynamoDBv2.DocumentModel.Table.LoadTable(client, "ScoutingDatabaseTest");
                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("TeamNumber", ScanOperator.IsNotNull);

                Search search = table.Scan(scanFilter);

                List<Document> documentList = new List<Document>();
                do
                {
                    documentList = search.GetNextSet();
                    foreach (var document in documentList)
                    {
                        try
                        {
                            TeamStats team = new TeamStats()
                            {
                                TeamNumber = (int)document["TeamNumber"],
                                MatchNumber = (int)document["MatchNumber"],
                                
                            };

                            TeamUserControl.items.Add(team);
                            Console.WriteLine("TN: " + document["TeamNumber"]);
                        }
                        catch (KeyNotFoundException ex) { }
                    }
                } while (!search.IsDone);
            }
            catch (Exception ex) { }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".scout";
            dlg.Filter = "Scout Files (.scout)|*.scout";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    Console.WriteLine("Loading file from " + dlg.FileName);
                    path = dlg.FileName;
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<TeamStats>));
                    System.IO.StreamReader file = new System.IO.StreamReader(@"" + dlg.FileName);
                    TeamUserControl.items.Clear();
                    ObservableCollection<TeamStats> x = reader.Deserialize(file) as ObservableCollection<TeamStats>;
                    foreach(TeamStats p in x)
                    {
                        TeamUserControl.items.Add(p);
                    }
                    file.Close();
                }
                catch (InvalidOperationException ex) { MessageBox.Show("File not found. \n" + ex.Message); }
                catch (AccessViolationException) { }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        string path;

        private void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = ""; //Default file name, left blank to force user to put in name
            dlg.DefaultExt = ".scout"; //Sets the default extension
            dlg.Filter = "Scout Files (.scout)|*.scout"; //Forces user to use XML

            //Shows dialog box and sets variable for whether or not user cancelled
            bool? result = dlg.ShowDialog();

            //Process save file dialog box results
            if (result == true)
            {
                try
                {
                    //Get path
                    path = dlg.FileName;
                    //Write filename to console
                    Console.WriteLine("Saving file to " + dlg.FileName);
                    System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<TeamStats>));
                    System.IO.FileStream file = System.IO.File.Create(path);
                    writer.Serialize(file, TeamUserControl.items);
                    file.Close();
                }
                catch (InvalidOperationException) { MessageBox.Show("Storage is either full or file exists and is read only."); }
                catch (System.IO.PathTooLongException) { MessageBox.Show("Path too long."); }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Saving file to " + path);
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<TeamStats>));
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, TeamUserControl.items);
                file.Close();
            }
            else
            {
                SaveAs();
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

    }
}
