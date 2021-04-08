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
    public partial class AgentS : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<Agents> _AgentsList;

        public IEnumerable<Agents> AgentsList
        {
            get
            {
                return _AgentsList;
            }
            set
            {
                _AgentsList = value;
                // при изменении списка перерисуется DataGrid
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AgentsList"));
                }
            }
        }
        public AgentS()
        {
            InitializeComponent();
            DataContext = this;
            AgentsList = Core.Root.Agents.ToArray();
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var NewAgentWindow = new AddAgentsWindow(new Agents());
            if (NewAgentWindow.ShowDialog() == true)
            {
                AgentsList = Core.Root.Agents.ToArray();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var EditApartmentWindow = new AddAgentsWindow(AgentsDataGrid.SelectedItem as Agents);
            if (EditApartmentWindow.ShowDialog() == true)
            {
                AgentsList = Core.Root.Agents.ToArray();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var Agent = AgentsDataGrid.SelectedItem as Agents;
            // если объект недвижимости учавствует в каких-то предложениях, 
            // список предложений буде не пустой (магия внешних ключей)
            if (Agent.Offers.Count > 0)
            {
                MessageBox.Show("Нельзя удалять объект недвижимости, на который есть предложение");
                return;
            }

            // процесс удаления заворачиваем в try..catch
            try
            {
                Core.Root.Agents.Remove(Agent);
                Core.Root.SaveChanges();
                AgentsList = Core.Root.Agents.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении объекта недвижимости: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


