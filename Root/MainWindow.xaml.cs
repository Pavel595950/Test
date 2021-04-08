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

namespace Root
{

    public partial class Apartments
    {
        public bool TotalAreaBigger50
        {
            get
            {
                return TotalArea > 50;
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        

        public MainWindow()
        {
            InitializeComponent();
            // PasswordVisibility = true;
            DataContext = this;
        }
       

        private void AppartmentsButton_Click(object sender, RoutedEventArgs e)
        {
            var NewApartmentWindow = new ApartmentS();
            NewApartmentWindow.ShowDialog();
            
        }

        private void AgentsButton_Click(object sender, RoutedEventArgs e)
        {
            var NewApartmentWindow = new AgentS();
            NewApartmentWindow.ShowDialog();
        }

        private void OffersButton_Click(object sender, RoutedEventArgs e)
        {
            var NewApartmentWindow = new OfferS();
            NewApartmentWindow.ShowDialog();
        }
    }
}
