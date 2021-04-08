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
    /// Логика взаимодействия для AddAgentsWindow.xaml
    /// </summary>
    public partial class AddAgentsWindow : Window
    {

        public Agents CurrentAgent { get; set; }
     


       
        
           


            public AddAgentsWindow(Agents Agent)
        {
                InitializeComponent();
                DataContext = this;
                CurrentAgent = Agent;
            }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

    
    }
}
