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
    /// Логика взаимодействия для ApartmentS.xaml
    /// </summary>
    public partial class ApartmentS : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public List<Streets> StreetsList { get; set; }

        public int SelectedRows
        {
            get
            {
                return ApartmentsList.Count();
            }
        }

        public int TotalRows
        {
            get
            {
                return _ApartmentsList.Count();
            }
        }

        private void Invalidate()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsList"));
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedRows"));
                PropertyChanged(this, new PropertyChangedEventArgs("TotalRows"));
            }
        }



        private bool _SortAsc = true;
        public bool SortAsc
        {
            get
            {
                return _SortAsc;
            }
            set
            {
                _SortAsc = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsList"));
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortAsc = (sender as RadioButton).Tag.ToString() == "1";
        }

        private string _SearchFilter = "";
        public string SearchFilter
        {
            get
            {

                return _SearchFilter;

            }
            set
            {
                _SearchFilter = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsList"));
                }
            }
        }

        private void SearchFilter_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
        }

        private IEnumerable<Apartments> _ApartmentsList;

        private int _StreetFilterValue = 0;
        public int StreetFilterValue
        {
            get
            {
                return _StreetFilterValue;
            }
            set
            {
                _StreetFilterValue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsList"));
                }
            }
        }

        private void StreetFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StreetFilterValue = (StreetFilter.SelectedItem as Streets).Id;
        }

        public IEnumerable<Apartments> ApartmentsList
        {
            get
            {
                var res = _ApartmentsList;

                // у объекта "Все улицы" Id=0, т.к. он взят не из базы, а создан в приложении
                // если выбрана улица, то выбираем только объекты с такой улицей
                if (_StreetFilterValue > 0)
                    res = res.Where(ai => ai.StreetsId == _StreetFilterValue);

                if (SearchFilter != "")
                    res = res.Where(ai => ai.Streets.Name.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0);

                if (SortAsc) res = res.OrderBy(ai => ai.Rooms);
                else res = res.OrderByDescending(ai => ai.Rooms);

                return res;
                
            }

            set
            {
                _ApartmentsList = value;
                // при изменении списка перерисуется DataGrid
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsList"));
                }
            }
        }
        public ApartmentS()
        {
            InitializeComponent();
            DataContext = this;
            ApartmentsList = Core.Root.Apartments.ToArray();
            StreetsList = Core.Root.Streets.ToList();
            StreetsList.Insert(0, new Streets { Name = "Все улицы" });
        }


        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var NewApartmentWindow = new AddApatmentsWindow(new Apartments());
            if (NewApartmentWindow.ShowDialog() == true)
            {
                ApartmentsList = Core.Root.Apartments.ToArray();
            }
        }
         
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var EditApartmentWindow = new AddApatmentsWindow(ApartmentsDataGrid.SelectedItem as Apartments);
            if (EditApartmentWindow.ShowDialog() == true)
            {
                ApartmentsList = Core.Root.Apartments.ToArray();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var Apartment = ApartmentsDataGrid.SelectedItem as Apartments;
            // если объект недвижимости учавствует в каких-то предложениях, 
            // список предложений буде не пустой (магия внешних ключей)
            if (Apartment.Offers.Count > 0)
            {
                MessageBox.Show("Нельзя удалять объект недвижимости, на который есть предложение");
                return;
            }

            // процесс удаления заворачиваем в try..catch
            try
            {
                Core.Root.Apartments.Remove(Apartment);
                Core.Root.SaveChanges();
                ApartmentsList = Core.Root.Apartments.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении объекта недвижимости: {ex.Message}");
            }
        }
    }
}
