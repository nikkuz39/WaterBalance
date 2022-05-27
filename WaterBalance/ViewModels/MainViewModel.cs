using WaterBalance.Services;
using System.Windows;

namespace WaterBalance.ViewModels
{
    public sealed class MainViewModel : ObservableObj
    {
        public RelayCommand ConsumptionTypeViewCommand { get; set; }
        public ConsumptionTypeViewModel ConsumptionTypeVM { get; set; }

        public RelayCommand TableToFillViewCommand { get; set; }
        public TableToFillViewModel TableToFillVM { get; set; }


        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            ConsumptionTypeVM = new ConsumptionTypeViewModel();
            ConsumptionTypeViewCommand = new RelayCommand(o =>
            {
                CurrentView = ConsumptionTypeVM;
            });


            TableToFillVM = new TableToFillViewModel();            
            TableToFillViewCommand = new RelayCommand(o =>
            {
                CurrentView = TableToFillVM;
            });
        }

        private RelayCommand closeMainWindow;
        public RelayCommand CloseMainWindow
        {
            get
            {
                return closeMainWindow ??
                    (closeMainWindow = new RelayCommand(o =>
                    {
                        Application.Current.MainWindow.Close();
                    }));
            }
        }
    }
}
