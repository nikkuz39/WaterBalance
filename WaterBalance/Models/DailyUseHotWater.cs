using WaterBalance.Services;

namespace WaterBalance.Models
{
    public class DailyUseHotWater : ObservableObj
    {
        public int Id { get; set; }

        private double? сonsumptionPerDayHotWater;
        public double? ConsumptionPerDayHotWater
        {
            get => сonsumptionPerDayHotWater;
            set
            {
                if (value <= 0)
                    сonsumptionPerDayHotWater = null;
                else
                    сonsumptionPerDayHotWater = value;

                OnPropertyChanged("ConsumptionPerDayHotWater");
            }
        }

        private double? сonsumptionPerHourHotWater;
        public double? ConsumptionPerHourHotWater
        {
            get => сonsumptionPerHourHotWater;
            set
            {
                if (value <= 0)
                    сonsumptionPerHourHotWater = null;
                else
                    сonsumptionPerHourHotWater = value;

                OnPropertyChanged("ConsumptionPerHourHotWater");
            }
        }

        private double? сonsumptionPerSecondHotWater;
        public double? ConsumptionPerSecondHotWater
        {
            get => сonsumptionPerSecondHotWater;
            set
            {
                if (value <= 0)
                    сonsumptionPerSecondHotWater = null;
                else
                    сonsumptionPerSecondHotWater = value;

                OnPropertyChanged("ConsumptionPerSecondHotWater");
            }
        }

        public int ConsumerId { get; set; }
        public Consumer? Consumer { get; set; }        
    }
}