// Author: Edward Champa  
// Updated: 12 May 2020

using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SpiderWeb2._0
{
    public partial class Mainwindow : System.Windows.Window
    {
        public Mainwindow()
        {
            InitializeComponent();
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
            NavigationMenuPanel.Visibility = Visibility.Collapsed;
            MouseDown += Window_MouseDown;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        // Tray Icon Application Functions
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Start Button To Enter Application
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Visibility = Visibility.Visible;
            NavigationMenuPanel.Visibility = Visibility.Visible;
            await Task.Delay(200);
            OpeningBackground.Visibility = Visibility.Collapsed;
        }

        // Sets Tolltip Visibility For Navigation Panel
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NavigationMenuToggleButton.IsChecked == true)
            {
                NavigationMenuHomeToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuCompToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuSoftwareToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuActiveDirectoryToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuToolsToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuScriptsToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuSettingsToolTip.Visibility = Visibility.Collapsed;
                NavigationMenuAboutToolTip.Visibility = Visibility.Collapsed;
            }
            else
            {
                NavigationMenuHomeToolTip.Visibility = Visibility.Visible;
                NavigationMenuCompToolTip.Visibility = Visibility.Visible;
                NavigationMenuSoftwareToolTip.Visibility = Visibility.Visible;
                NavigationMenuActiveDirectoryToolTip.Visibility = Visibility.Visible;
                NavigationMenuToolsToolTip.Visibility = Visibility.Visible;
                NavigationMenuScriptsToolTip.Visibility = Visibility.Visible;
                NavigationMenuSettingsToolTip.Visibility = Visibility.Visible;
                NavigationMenuAboutToolTip.Visibility = Visibility.Visible;
            }

        }

        // Sets Background Opacity When Navigation Menu Panel Is Condensed/Expanded
        private void NavigationMenuToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            HomePageBackgroundImage.Opacity = 1;
            Comp_OSBackgroundImage.Opacity = 1;
            SoftwareInstallerBackgroundImage.Opacity = 1;
            ActiveDirectoryBackgroundImage.Opacity = 1;
            ToolsBackgroundImage.Opacity = 1;
            ScriptsBackgroundImage.Opacity = 1;
            SettingsBackgroundImage.Opacity = 1;
            AboutBackgroundImage.Opacity = 1;
        }

        private void NavigationMenuToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            HomePageBackgroundImage.Opacity = 0.2;
            Comp_OSBackgroundImage.Opacity = 0.2;
            SoftwareInstallerBackgroundImage.Opacity = 0.2;
            ActiveDirectoryBackgroundImage.Opacity = 0.2;
            ToolsBackgroundImage.Opacity = 0.2;
            ScriptsBackgroundImage.Opacity = 0.2;
            SettingsBackgroundImage.Opacity = 0.2;
            AboutBackgroundImage.Opacity = 0.2;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationMenuToggleButton.IsChecked = false;
        }

        // Open/Closes Release Notes Window
        private void ShowReleaseNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ReleaseNotesWindow.Visibility = Visibility.Visible;
            ShowReleaseNotesButton.IsEnabled = false;
        }

        private void CloseReleaseNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReleaseNotesButton.IsEnabled = true;
        }

        // Open/Closes Folder Locations Window
        private void ShowFolderShortcutsButton_Click(object sender, RoutedEventArgs e)
        {
            FolderShortcuts FolderLocationsWindow = new FolderShortcuts();
            this.ShowFolderShortcutsButton.IsEnabled = false;
            FolderLocationsWindow.FolderShortcutsButton = this.ShowFolderShortcutsButton;
            FolderLocationsWindow.Owner = this;
            if (FolderLocationsWindow.ShowDialog() == true) { }
        }

        private void ShowExternalLinksButton_Click(object sender, RoutedEventArgs e)
        {
            ExternalLinks ExternalLinksWindow = new ExternalLinks();
            this.ShowExternalLinksButton.IsEnabled = false;
            ExternalLinksWindow.ShowExternalLinksButton = this.ShowExternalLinksButton;
            ExternalLinksWindow.Owner = this;
            if (ExternalLinksWindow.ShowDialog() == true) { }
        }

        // Navigation Menu List Functions
        private void HomeButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Visible;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void HomeButtonTitleTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Visible;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void CompOSButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Visible;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void CompOSButtonTitleTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Visible;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void SoftwareInstallerButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Visible;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void SoftwareInstallerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Visible;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void ActiveDirectoryButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Visible;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void ActiveDirectoryTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Visible;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void ToolsButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Visible;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void ToolsTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Visible;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void ScriptsButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Visible;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void ScriptsTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Visible;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void SettingsButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Visible;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void SettingsTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Visible;
            AboutPage.Visibility = Visibility.Collapsed;
        }

        private void AboutButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Visible;
        }

        private void AboutTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            Comp_OSPage.Visibility = Visibility.Collapsed;
            SoftwareInstallerPage.Visibility = Visibility.Collapsed;
            ActiveDirectoryPage.Visibility = Visibility.Collapsed;
            ToolsPage.Visibility = Visibility.Collapsed;
            ScriptsPage.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
            AboutPage.Visibility = Visibility.Visible;
        }

        // Loads Local PC Name Into ComputerNameTextBox On Startup
        private void ComputerNameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComputerNameTextBox.Text = Environment.MachineName;
        }

        // Retrieves System Information For ComputerNameTextBox
        private void SystemTypeButton_Click(object sender, RoutedEventArgs e)
        {

            string pcName = ComputerNameTextBox.Text;
            ProgressBar Progress = CompOSProgressBar;
            Progress.Foreground = Brushes.DarkGreen;
            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
            DoubleAnimation doubleanimation = new DoubleAnimation(100, duration);
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(File.ReadAllText(@"C:\SpiderWeb2\SpiderWeb2.0\SpiderWeb2.0\Scripts\systemtype.ps1"), true).AddParameter("ComputerName", pcName).AddCommand("Out-String");
                try
                {
                    Collection<PSObject> results = ps.Invoke();
                    Progress.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
                    foreach (var test in results)
                    MainRichTextBox.AppendText(Environment.NewLine + Regex.Replace(test.ToString(), @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline));
                }
                catch (Exception f)
                {
                    Console.WriteLine(f.Message);
                }

            }
        }

        // Clears The MainRichTextBox
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MainRichTextBox.Document.Blocks.Clear();
        }
    }

    static class DesignModeTool
    {
        public static readonly DependencyProperty IsHiddenProperty =
            DependencyProperty.RegisterAttached("IsHidden",
                typeof(bool),
                typeof(DesignModeTool),
                new FrameworkPropertyMetadata(false,
                    new PropertyChangedCallback(OnIsHiddenChanged)));

        public static void SetIsHidden(FrameworkElement element, bool value)
        {
            element.SetValue(IsHiddenProperty, value);
        }

        public static bool GetIsHidden(FrameworkElement element)
        {
            return (bool)element.GetValue(IsHiddenProperty);
        }

        private static void OnIsHiddenChanged(DependencyObject d,
                                              DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d)) return;
            var element = (FrameworkElement)d;
            element.RenderTransform = (bool)e.NewValue
               ? new ScaleTransform(0, 0)
               : null;
        }
    }
}
