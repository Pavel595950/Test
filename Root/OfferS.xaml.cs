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
using System.Windows.Shapes;

namespace Root
{
    /// <summary>
    /// Логика взаимодействия для OfferS.xaml
    /// </summary>
    public partial class OfferS : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private IEnumerable<Offers> _OfferSList;

        public List<Streets> StreetsList { get; set; }


        public IEnumerable<Offers> OfferSList
        {
            get
            {
                return _OfferSList;
            }
            set
            {
                _OfferSList = value;
                // при изменении списка перерисуется DataGrid
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OfferSList"));
                }
            }
        }
        public OfferS()
        {
            InitializeComponent();
            DataContext = this;
            OfferSList = Core.Root.Offers.ToArray();
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var NewOfferWindow = new AddOfferSWindow(new Offers());
            if (NewOfferWindow.ShowDialog() == true)
            {
                OfferSList = Core.Root.Offers.ToArray();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var EditOfferWindow = new AddOfferSWindow(OfferSDataGrid.SelectedItem as Offers);
            if (EditOfferWindow.ShowDialog() == true)
            {
                OfferSList = Core.Root.Offers.ToArray();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var Offer = OfferSDataGrid.SelectedItem as Offers;
            // если объект недвижимости учавствует в каких-то предложениях, 
            // список предложений буде не пустой (магия внешних ключей)
            
            // процесс удаления заворачиваем в try..catch
            try
            {
                Core.Root.Offers.Remove(Offer);
                Core.Root.SaveChanges();
                OfferSList = Core.Root.Offers.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении объекта недвижимости: {ex.Message}");
            }
        }
    }
}

