// Author: Edward Champa
// Updated: 12 May 2020

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SpiderWeb2._0
{

    public partial class ExternalLinks : System.Windows.Window, INotifyPropertyChanged
    {
        // Inherits External Links Button Assignment From Main Window
        public System.Windows.Controls.Button ShowExternalLinksButton;

        // Builds Object For Collection Of Links
        public class Links
        {
            public Links(string Link)
            {
                this.Link = Link;
            }
            public string Link { get; set; }
        }

        // Builds Collection For Links
        private ObservableCollection<Links> _linksList;

        public ObservableCollection<Links> LinksList
        {
            get => _linksList;
            set
            {
                _linksList = value;
                OnPropertyChanged();
            }
        }

        public ExternalLinks()
        {
            InitializeComponent();
            DataContext = this;
            MouseDown += Window_MouseDown;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                try
                {
                    DragMove();
                }
                catch
                {
                }
        }

        // For Dynamic Changes To Binding Source
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Loads DataGrid With Text File Source
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LinksList = new ObservableCollection<Links>();
            using (StreamReader reader = new StreamReader(@"..\..\Data\ELinks.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    LinksList.Add(new Links(line));
                }
            }
        }

        // Validates User Submitted Format Text For External Link
        public static bool ValidateUrl(string value, bool required)
        {
            value = value.Trim();
            if (required == false && value == "") return true;
            if (required && value == "") return false;

            Regex pattern = new Regex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");
            Match match = pattern.Match(value);
            if (match.Success == false) return false;
            return true;
        }

        // Adds User Submitted External Link To Text File Source
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string path = ExternalLinkTextBox.Text;
            bool isUrl = false;
            if (ValidateUrl(path, isUrl == true)) {
                using (StreamWriter writer = new StreamWriter(@"..\..\Data\ELinks.txt", append: true))
                {
                    writer.WriteLine(path);
                }
                LinksList.Add(new Links(path));
            }
            else
            {
                System.Windows.MessageBox.Show(@"" + ExternalLinkTextBox.Text + " is not a valid URL naming convention.");
            }
        }

        // Closes External Links Window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ShowExternalLinksButton.IsEnabled = true;
            this.Close();
        }

        // Clears External Link Box When User Clicks TextBox
        private void ExternalLinkTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (System.Windows.Controls.TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= ExternalLinkTextBox_GotFocus;
        }

        // Launches External Link In Browser When User Double Clicks
        private void Do_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cellInfo = ExternalLinksDataGrid.CurrentCell;
            {
                var column = cellInfo.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var element = new FrameworkElement() { DataContext = cellInfo.Item };
                    BindingOperations.SetBinding(element, TagProperty, column.Binding);
                    var cellValue = element.Tag;
                    string url = cellValue.ToString();
                    try
                    {
                        Process.Start("Chrome.exe", url);
                    }
                    catch
                    {
                        Process.Start("microsoft-edge:", url);
                    }
                }
            }
        }

        // Implementation Of Delete Command When User Selects Row To Delete In DataGrid
        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(parameter =>
        {
            if (parameter is Links links)
            {
                LinksList.Remove(links);
                using (StreamWriter writer = new StreamWriter(@"..\..\Data\ELinks.txt", append: false))
                {
                    foreach (Links link in LinksList)
                    {
                        writer.WriteLine(link.Link);
                    }
                }
            }
        }));
    }
}
