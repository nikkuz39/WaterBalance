using System.ComponentModel.DataAnnotations.Schema;
using WaterBalance.Services;

namespace WaterBalance.Models
{
    public class Consumer : ObservableObj
    {
        public int Id { get; set; }        
        public string? ConsumerName { get; set; }
        private double unit { get; set; }
        public double Unit
        {
            get => unit;
            set
            {
                if (value <= 0)
                    unit = 0;
                else
                    unit = value;

                OnPropertyChanged("Unit");
            }
        }
        public string? UnitName { get; set; }

        private int consumptionRatePerDay;
        public int ConsumptionRatePerDay
        {
            get => consumptionRatePerDay;
            set
            {
                if (value <= 0)
                    consumptionRatePerDay = 0;
                else
                    consumptionRatePerDay = value;

                OnPropertyChanged("ConsumptionRatePerDay");
            }
        }

        public bool? HotWaterBool { get; set; } 

        private int? hotWaterConsumption;
        public int? HotWaterConsumption
        {
            get => hotWaterConsumption;
            set
            {
                if (value <= 0)
                    hotWaterConsumption = null;
                else
                    hotWaterConsumption = value;

                OnPropertyChanged("HotWaterConsumption");
            }
        }

        private int? wastewaterCollection;
        public int? WastewaterCollection
        {
            get => wastewaterCollection;
            set
            {
                if (value <= 0)
                    wastewaterCollection = null;
                else
                    wastewaterCollection = value;

                OnPropertyChanged("WastewaterCollection");
            }
        }

        private int? duration;
        public int? Duration
        {
            get => duration;
            set
            {
                if (value <= 0)
                    duration = null;
                else
                    duration = value;

                OnPropertyChanged("Duration");
            }
        }

        public string? NormativeDocument { get; set; }        

        public DailyUseHotWater? DailyUseHotWater { get; set; }
        public DailyUseColdWater? DailyUseColdWater { get; set; }
        public DailyWastewaterCollection? DailyWastewaterCollection { get; set; }


        [NotMapped]
        public int? LineNumber { get; set; }
    }
}