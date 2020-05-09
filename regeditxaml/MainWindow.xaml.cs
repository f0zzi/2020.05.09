using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace regeditxaml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> keys;
        public RegistryKey run;
        public MainWindow()
        {
            InitializeComponent();
            keys = new ObservableCollection<string>();
            lbKeys.ItemsSource = keys;
            RegistryKey currentKey = Registry.CurrentUser;
            run = currentKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            GetValues();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.exe | *.exe";
            if(openFileDialog.ShowDialog() == true)
            {
                run.SetValue(openFileDialog.SafeFileName, openFileDialog.FileName);
                GetValues();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            run.DeleteValue(lbKeys.SelectedItem.ToString());
            GetValues();
        }
        private void GetValues()
        {
            var items = run.GetValueNames();
            keys.Clear();
            foreach (var item in items)
            {
                keys.Add(item);
            }
        }
    }
}
