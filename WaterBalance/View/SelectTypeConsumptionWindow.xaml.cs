using System.Windows;
using WaterBalance.Models;

namespace WaterBalance.View
{
    public partial class SelectTypeConsumptionWindow : Window
    {
        private Consumer? consumer;
        public Consumer? Consumer { get { return consumer = consumersList.SelectedItem as Consumer; } }
        public SelectTypeConsumptionWindow()
        {
            InitializeComponent();
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}