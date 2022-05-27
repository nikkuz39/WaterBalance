using WaterBalance.Services;

namespace WaterBalance.Models
{
    public class DailyWastewaterCollection : ObservableObj
    {
        public int Id { get; set; }

        private double? wastewaterCollectionPerDay;
        public double? WastewaterCollectionPerDay
        {
            get => wastewaterCollectionPerDay;
            set
            {
                if (value <= 0)
                    wastewaterCollectionPerDay = null;
                else
                    wastewaterCollectionPerDay = value;

                OnPropertyChanged("WastewaterCollectionPerDay");
            }
        }

        private double? wastewaterCollectionPerHour;
        public double? WastewaterCollectionPerHour
        {
            get => wastewaterCollectionPerHour;
            set
            {
                if (value <= 0)
                    wastewaterCollectionPerHour = null;
                else
                    wastewaterCollectionPerHour = value;

                OnPropertyChanged("WastewaterCollectionPerHour");
            }
        }

        private double? wastewaterCollectionPerSecond;
        public double? WastewaterCollectionPerSecond
        {
            get => wastewaterCollectionPerSecond;
            set
            {
                if (value <= 0)
                    wastewaterCollectionPerSecond = null;
                else
                    wastewaterCollectionPerSecond = value;

                OnPropertyChanged("WastewaterCollectionPerSecond");
            }
        }

        public int ConsumerId { get; set; }
        public Consumer? Consumer { get; set; }        
    }
}
