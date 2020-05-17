// Author: Edward Champa
// Updated: 12 May 2020

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace SpiderWeb2._0
{

    public partial class FolderShortcuts : System.Windows.Window, INotifyPropertyChanged
    {
        // Inherits Folder Shortcuts Button Assignment From Main Window
        public System.Windows.Controls.Button FolderShortcutsButton;

        // Builds Object For Collection Of Filepaths
        public class Drives
        {
            public Drives(string Filepath)
            {
                this.Filepath = Filepath;
            }
            public string Filepath { get; set; }
        }

        // Builds Collection For Filepaths
        private ObservableCollection<Drives> _drivesList;

        public ObservableCollection<Drives> DrivesList
        {
            get => _drivesList;
            set
            {
                _drivesList = value;
                OnPropertyChanged();
            }
        }

        public FolderShortcuts()
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
            DrivesList = new ObservableCollection<Drives>();
            using (StreamReader reader = new StreamReader(@"..\..\Data\SDrives.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    DrivesList.Add(new Drives(line));
                }
            }
        }

        // Adds User Submitted Filepath To Text File Source
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string path = FilepathTextBox.Text;
            if (Directory.Exists(FilepathTextBox.Text))
            {
                using (StreamWriter writer = new StreamWriter(@"..\..\Data\SDrives.txt", append: true))
                {
                    writer.WriteLine(path);
                }
                DrivesList.Add(new Drives(path));
            }
            else
            {
                System.Windows.MessageBox.Show(@"" + FilepathTextBox.Text + " is not a valid filepath.");
            }
        }

        // Closes Folder Shortcut Window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderShortcutsButton.IsEnabled = true;
            this.Close();
        }

        // Opens BrowserDialog To Search For Folder
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
            };

            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilepathTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        // Clears Filepath Box When User Clicks TextBox
        private void FilepathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (System.Windows.Controls.TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= FilepathTextBox_GotFocus;
        }

        // Launches Filepath In Explorer When User Double Clicks
        private void Do_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cellInfo = SharedDriveDataGrid.CurrentCell;
            {
                var column = cellInfo.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var element = new FrameworkElement() { DataContext = cellInfo.Item };
                    BindingOperations.SetBinding(element, TagProperty, column.Binding);
                    var cellValue = element.Tag;
                    if (Directory.Exists(@"" + cellValue))
                    {
                        Process.Start(@"" + cellValue);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(@"" + cellValue + " is not a valid filepath.");
                    }
                }
            }

        }

        // Implementation Of Delete Command When User Selects Row To Delete In DataGrid
        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(parameter =>
        {
            if (parameter is Drives drives)
            {
                DrivesList.Remove(drives);
                using (StreamWriter writer = new StreamWriter(@"..\..\Data\SDrives.txt", append: false))
                {
                    foreach (Drives drive in DrivesList)
                    {
                        writer.WriteLine(drive.Filepath);
                    }
                }
            }
        }));
    }
}
