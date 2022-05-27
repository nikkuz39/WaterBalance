using System.Windows;
using WaterBalance.Models;

namespace WaterBalance.View
{
    public partial class ConsumerWindow : Window
    {
        public Consumer Consumer { get; private set; }
        public ConsumerWindow(Consumer consumer)
        {
            InitializeComponent();

            Consumer = consumer;
            this.DataContext = Consumer;
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
