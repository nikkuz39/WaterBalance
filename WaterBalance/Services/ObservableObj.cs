using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WaterBalance.Services
{
    public class ObservableObj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
