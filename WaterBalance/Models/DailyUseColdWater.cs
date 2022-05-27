using WaterBalance.Services;

namespace WaterBalance.Models
{
    public class DailyUseColdWater : ObservableObj
    {
        public int Id { get; set; }

        private double? сonsumptionPerDayColdWater;
        public double? ConsumptionPerDayColdWater
        {
            get => сonsumptionPerDayColdWater;
            set
            {
                if (value <= 0)
                    сonsumptionPerDayColdWater = null;
                else
                    сonsumptionPerDayColdWater = value;

                OnPropertyChanged("ConsumptionPerDayColdWater");
            }
        }

        private double? сonsumptionPerHourColdWater;
        public double? ConsumptionPerHourColdWater
        {
            get => сonsumptionPerHourColdWater;
            set
            {
                if (value <= 0)
                    сonsumptionPerHourColdWater = null;
                else
                    сonsumptionPerHourColdWater = value;

                OnPropertyChanged("ConsumptionPerHourColdWater");
            }
        }

        private double? сonsumptionPerSecondColdWater;
        public double? ConsumptionPerSecondColdWater
        {
            get => сonsumptionPerSecondColdWater;
            set
            {
                if (value <= 0)
                    сonsumptionPerSecondColdWater = null;
                else
                    сonsumptionPerSecondColdWater = value;

                OnPropertyChanged("ConsumptionPerSecondColdWater");
            }
        }

        public int ConsumerId { get; set; }
        public Consumer? Consumer { get; set; }
    }
}