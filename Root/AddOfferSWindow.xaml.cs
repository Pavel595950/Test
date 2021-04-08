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
using System.Windows.Shapes;

namespace Root
{
    /// <summary>
    /// Логика взаимодействия для AddOfferSWindow.xaml
    /// </summary>
    public partial class AddOfferSWindow : Window
    {
        public Offers CurrentOffer { get; set; }
        public IEnumerable<Cities> CitiesList { get; set; }
        public IEnumerable<Streets> StreetsList { get; set; }

        public AddOfferSWindow(Offers Offer)
        {

            InitializeComponent();
            DataContext = this;
            CurrentOffer = Offer;
            CitiesList = Core.Root.Cities.ToArray();
            StreetsList = Core.Root.Streets.ToArray();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // опять же всю работу с БД заворачиваем в try..catch
            try
            {
                if (CurrentOffer.Agents == null)
                    throw new Exception("Не выбран город");

                if (CurrentOffer.Apartments == null)
                    throw new Exception("Не выбрана улица");

                if (CurrentOffer.Clients == null)
                    throw new Exception("Не выбрана улица");

                if (CurrentOffer.Id == 0)
                    Core.Root.Offers.Add(CurrentOffer);

                Core.Root.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
