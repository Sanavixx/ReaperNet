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

    public partial class Window : System.Windows.Window, INotifyPropertyChanged
    {
        public class Drives
        {
            public Drives(string Filepath)
            {
                this.Filepath = Filepath;
            }

            public string Filepath { get; set; }

        }

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

        public Window()
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

        //For Dynamic Changes To Binding Source
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //Loads DataGrid With Text File Source
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

        //Adds User Submitted Filepath To Text File Source
        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            string path = FilepathTextBox.Text;
            using (StreamWriter writer = new StreamWriter(@"..\..\Data\SDrives.txt", append: true))
            {
                writer.WriteLine(path);
            }
            DrivesList.Add(new Drives(path));
        }

        // Closes Folder Shortcut Window
        private void SD_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Opens BrowserDialog To Search For Folder
        private void Browse_Btn_Click(object sender, RoutedEventArgs e)
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
            var cellInfo = SDDataGrid.CurrentCell;
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

        //Implementation Of Delete Command When User Selects Row To Delete In DataGrid
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
