using ProjetTennis.DAO;
using ProjetTennis.Models;
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

namespace ProjetTennis_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();//Stop le programme quand on appuie sur le X 
        }
        private void Test(object sender, RoutedEventArgs e)
        {
        
            Tournament tournament = new Tournament();
            tournament.Play();
            this.Hide();
        }

        
    }
}
