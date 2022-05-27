using System;
using System.Collections.ObjectModel;
using WaterBalance.Models;
using WaterBalance.Services;
using WaterBalance.View;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace WaterBalance.ViewModels
{
    public sealed class TableToFillViewModel : ObservableObj
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private DialogService dialogService;
        private SaveOpenData saveOpenData;
        private Consumer? sumConsumptionWaterAndWastewaterCollection { get; set; }
        public Consumer? SumConsumptionWaterAndWastewaterCollection
        {
            get => sumConsumptionWaterAndWastewaterCollection;
            set
            {
                sumConsumptionWaterAndWastewaterCollection = value;
                OnPropertyChanged("SumConsumptionWaterAndWastewaterCollection");
            }
        }
        public ObservableCollection<Consumer>? ConsumptionList { get; set; }
        public TableToFillViewModel()
        {
            ConsumptionList = new ObservableCollection<Consumer>();

            SumConsumptionWaterAndWastewaterCollection = new Consumer { ConsumerName = "Итого:" };
            DailyUseColdWater coldWater = new DailyUseColdWater();
            DailyUseHotWater hotWater = new DailyUseHotWater();
            DailyWastewaterCollection wastewaterCollection = new DailyWastewaterCollection();

            SumConsumptionWaterAndWastewaterCollection.DailyUseColdWater = coldWater;
            SumConsumptionWaterAndWastewaterCollection.DailyUseHotWater = hotWater;
            SumConsumptionWaterAndWastewaterCollection.DailyWastewaterCollection = wastewaterCollection;
        }

        private RelayCommand saveJsonDataCommand;
        public RelayCommand SaveJsonDataCommand
        {
            get
            {
                return saveJsonDataCommand ??
                    (saveJsonDataCommand = new RelayCommand(o =>
                    {
                        try
                        {
                            dialogService = new DialogService();
                            saveOpenData = new SaveOpenData();

                            if (dialogService.SaveFileDialog() == true)
                            {
                                saveOpenData.SaveJsonFile(dialogService.FilePath, ConsumptionList.ToList());
                            }                            
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Command 'SaveJsonDataCommand' / class 'TableToFillViewModel'");
                        }
                    }));
            }
        }

        private RelayCommand opeuJsonDataCommand;
        public RelayCommand OpenJsonDataCommand
        {
            get
            {
                return opeuJsonDataCommand ??
                    (opeuJsonDataCommand = new RelayCommand(o =>
                    {
                        try
                        {
                            dialogService = new DialogService();
                            saveOpenData = new SaveOpenData();

                            if (dialogService.OpenFileDialog() == true)
                            {
                                List<Consumer> data = saveOpenData.OpenJsonFile(dialogService.FilePath);
                                ConsumptionList.Clear();

                                foreach (var c in data)
                                    ConsumptionList.Add(c);

                                GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Command 'OpenJsonDataCommand' / class 'TableToFillViewModel'");
                        }
                    }));
            }
        }

        private RelayCommand printData;
        public RelayCommand PrintData
        {
            get
            {
                return printData ??
                    (printData = new RelayCommand(o =>
                    {
                        foreach (var i in ConsumptionList)
                        {
                            i.LineNumber = ConsumptionList.IndexOf(i) + 1;
                        }

                        ConsumptionList.Add(SumConsumptionWaterAndWastewaterCollection);

                        PrintDocument printDocument = new PrintDocument();
                        printDocument.PrintDoc(ConsumptionList.ToList());

                        ConsumptionList.Remove(SumConsumptionWaterAndWastewaterCollection);
                    }));
            }
        }

        private RelayCommand addConsumerToList;
        public RelayCommand AddConsumerToList
        {
            get
            {
                return addConsumerToList ??
                    (addConsumerToList = new RelayCommand(o =>
                    {
                        SelectTypeConsumptionWindow selectTypeConWin = new SelectTypeConsumptionWindow();

                        if (selectTypeConWin.ShowDialog() == true)
                        {
                            using (ApplicationContext context = new ApplicationContext())
                            {                                
                                try
                                {
                                    Consumer consumer = new Consumer();

                                    consumer = context.Consumers.Find(selectTypeConWin.Consumer.Id);

                                    if (consumer != null)
                                    {
                                        DailyUseColdWater coldWater = new DailyUseColdWater();
                                        DailyUseHotWater hotWater = new DailyUseHotWater();
                                        DailyWastewaterCollection wastewaterCollection = new DailyWastewaterCollection();

                                        consumer.ConsumerName = selectTypeConWin.Consumer.ConsumerName;
                                        consumer.UnitName = selectTypeConWin.Consumer.UnitName;
                                        consumer.Unit = selectTypeConWin.Consumer.Unit;

                                        if (selectTypeConWin.Consumer.HotWaterConsumption != 0 && selectTypeConWin.Consumer.HotWaterConsumption != null)
                                        {
                                            consumer.ConsumptionRatePerDay = selectTypeConWin.Consumer.ConsumptionRatePerDay - (int)selectTypeConWin.Consumer.HotWaterConsumption;
                                            consumer.HotWaterBool = true;
                                        }
                                        else
                                        {
                                            consumer.ConsumptionRatePerDay = selectTypeConWin.Consumer.ConsumptionRatePerDay;
                                            consumer.HotWaterBool = false;
                                        }

                                        consumer.HotWaterConsumption = selectTypeConWin.Consumer.HotWaterConsumption;
                                        consumer.WastewaterCollection = selectTypeConWin.Consumer.WastewaterCollection;
                                        consumer.Duration = selectTypeConWin.Consumer.Duration;
                                        consumer.NormativeDocument = selectTypeConWin.Consumer.NormativeDocument;

                                        consumer.DailyUseColdWater = coldWater;
                                        consumer.DailyUseHotWater = hotWater;
                                        consumer.DailyWastewaterCollection = wastewaterCollection;

                                        ConsumptionList.Add(consumer);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex, "Command 'AddConsumerToList' / class 'TableToFillViewModel'");
                                }
                            }
                        }
                    }));
            }
        }

        private RelayCommand deleteConsumerToList;
        public RelayCommand DeleteConsumerToList
        {
            get
            {
                return deleteConsumerToList ??
                    (deleteConsumerToList = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;

                        MessageBoxResult result = MessageBox.Show("Удалить строку?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            Consumer? consumer = selectedItem as Consumer;

                            ConsumptionList.Remove(consumer);
                            GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                        }
                    }));
            }
        }

        private RelayCommand deleteAllConsumerToList;
        public RelayCommand DeleteAllConsumerToList
        {
            get
            {
                return deleteAllConsumerToList ??
                    (deleteAllConsumerToList = new RelayCommand((selectedItem) =>
                    {
                        MessageBoxResult result = MessageBox.Show("Очистить таблицу?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            ConsumptionList.Clear();
                            GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                        }
                    }));
            }
        }

        private RelayCommand hotWaterCheckBoxUnchecked;
        public RelayCommand HotWaterCheckBoxUnchecked
        {
            get
            {
                return hotWaterCheckBoxUnchecked ??
                    (hotWaterCheckBoxUnchecked = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;                        
                       
                        Consumer? consumer = selectedItem as Consumer;

                        if (consumer.HotWaterBool != false && consumer.HotWaterConsumption >= 0)
                        {
                            consumer.ConsumptionRatePerDay += (int)consumer.HotWaterConsumption;
                            consumer.HotWaterConsumption = 0;
                        }
                    }));
            }
        }

        private RelayCommand hotWaterCheckBoxChecked;
        public RelayCommand HotWaterCheckBoxChecked
        {
            get
            {
                return hotWaterCheckBoxChecked ??
                    (hotWaterCheckBoxChecked = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;

                        Consumer? consumer = selectedItem as Consumer;

                        if (consumer.HotWaterBool != false)
                        {
                            using (ApplicationContext context = new ApplicationContext())
                            {
                                try
                                {
                                    Consumer consumerInDb = new Consumer();

                                    consumerInDb = context.Consumers.FirstOrDefault(i => i.Id == consumer.Id);

                                    if (consumerInDb != null)
                                    {
                                        consumer.ConsumptionRatePerDay -= (int)consumerInDb.HotWaterConsumption;
                                        consumer.HotWaterConsumption = consumerInDb.HotWaterConsumption;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex, "Command 'HotWaterCheckBoxChecked' / class 'TableToFillViewModel'");
                                }
                            }
                        }
                    }));
            }
        }

        private RelayCommand calculateColdWater;
        public RelayCommand CalculateColdWater
        {
            get
            {
                return calculateColdWater ??
                    (calculateColdWater = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;

                        Consumer? consumer = selectedItem as Consumer;

                        double[] data = new double[3];

                        if (consumer.Duration == null)
                        {
                            data = CalculateWaterConsumption(consumer.Unit, consumer.ConsumptionRatePerDay, 1);

                            consumer.DailyUseColdWater.ConsumptionPerDayColdWater = data[0];
                            consumer.DailyUseColdWater.ConsumptionPerHourColdWater = data[0];
                            consumer.DailyUseColdWater.ConsumptionPerSecondColdWater = data[2];

                            GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);

                            return;
                        }

                        data = CalculateWaterConsumption(consumer.Unit, consumer.ConsumptionRatePerDay, (int)consumer.Duration);

                        consumer.DailyUseColdWater.ConsumptionPerDayColdWater = data[0];
                        consumer.DailyUseColdWater.ConsumptionPerHourColdWater = data[1];
                        consumer.DailyUseColdWater.ConsumptionPerSecondColdWater = data[2];

                        GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                    }));
            }
        }

        private RelayCommand calculateHotWater;
        public RelayCommand CalculateHotWater
        {
            get
            {
                return calculateHotWater ??
                    (calculateHotWater = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;

                        Consumer? consumer = selectedItem as Consumer;

                        if (consumer.HotWaterConsumption != null)
                        {
                            double[] data = new double[3];

                            if (consumer.Duration == null)
                            {
                                data = CalculateWaterConsumption(consumer.Unit, (int)consumer.HotWaterConsumption, 1);

                                consumer.DailyUseHotWater.ConsumptionPerDayHotWater = data[0];
                                consumer.DailyUseHotWater.ConsumptionPerHourHotWater = data[0];
                                consumer.DailyUseHotWater.ConsumptionPerSecondHotWater = data[2];

                                GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);

                                return;
                            }

                            data = CalculateWaterConsumption(consumer.Unit, (int)consumer.HotWaterConsumption, (int)consumer.Duration);

                            consumer.DailyUseHotWater.ConsumptionPerDayHotWater = data[0];
                            consumer.DailyUseHotWater.ConsumptionPerHourHotWater = data[1];
                            consumer.DailyUseHotWater.ConsumptionPerSecondHotWater = data[2];

                            GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                        }
                        else
                        {
                            consumer.DailyUseHotWater.ConsumptionPerDayHotWater = null;
                            consumer.DailyUseHotWater.ConsumptionPerHourHotWater = null;
                            consumer.DailyUseHotWater.ConsumptionPerSecondHotWater = null;

                            GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                        }
                    }));
            }
        }

        private RelayCommand calculateWastewaterCollection;
        public RelayCommand CalculateWastewaterCollection
        {
            get
            {
                return calculateWastewaterCollection ??
                    (calculateWastewaterCollection = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;

                        Consumer? consumer = selectedItem as Consumer;

                        if (consumer.WastewaterCollection != null)
                        {
                            double[] data = new double[3];

                            if (consumer.Duration == null)
                            {
                                data = CalculateWaterConsumption(consumer.Unit, (int)consumer.WastewaterCollection, 1);

                                consumer.DailyWastewaterCollection.WastewaterCollectionPerDay = data[0];
                                consumer.DailyWastewaterCollection.WastewaterCollectionPerHour = data[0];
                                consumer.DailyWastewaterCollection.WastewaterCollectionPerSecond = data[2];

                                GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);

                                return;
                            }

                            data = CalculateWaterConsumption(consumer.Unit, (int)consumer.WastewaterCollection, (int)consumer.Duration);

                            consumer.DailyWastewaterCollection.WastewaterCollectionPerDay = data[0];
                            consumer.DailyWastewaterCollection.WastewaterCollectionPerHour = data[1];
                            consumer.DailyWastewaterCollection.WastewaterCollectionPerSecond = data[2];

                            GetDataConsumptionWaterAndWastewaterCollection(ConsumptionList);
                        }
                    }));
            }
        }        

        private double[] CalculateWaterConsumption(double totalConsumptionPerDay, int consumptionRatePerDay, int duration)
        {
            double[] data = new double[3];

            double cubicMetersPerDay = 0;
            double cubicMetersPerHour = 0;
            double cubicMetersPerSecond = 0;

            cubicMetersPerDay = (totalConsumptionPerDay * consumptionRatePerDay) / 1000;
            cubicMetersPerHour = cubicMetersPerDay / duration;
            cubicMetersPerSecond = cubicMetersPerHour / 60 / 60 * 1000;            

            data[0] = cubicMetersPerDay;
            data[1] = cubicMetersPerHour;
            data[2] = cubicMetersPerSecond;

            return data;
        }

        private void GetDataConsumptionWaterAndWastewaterCollection(ObservableCollection<Consumer> consumers)
        {
            SumConsumptionWaterAndWastewaterCollection.DailyUseColdWater.ConsumptionPerDayColdWater = consumers.Sum(s => s.DailyUseColdWater.ConsumptionPerDayColdWater);
            SumConsumptionWaterAndWastewaterCollection.DailyUseColdWater.ConsumptionPerHourColdWater = consumers.Sum(s => s.DailyUseColdWater.ConsumptionPerHourColdWater);
            SumConsumptionWaterAndWastewaterCollection.DailyUseColdWater.ConsumptionPerSecondColdWater = consumers.Sum(s => s.DailyUseColdWater.ConsumptionPerSecondColdWater);

            SumConsumptionWaterAndWastewaterCollection.DailyUseHotWater.ConsumptionPerDayHotWater = consumers.Sum(s => s.DailyUseHotWater.ConsumptionPerDayHotWater);
            SumConsumptionWaterAndWastewaterCollection.DailyUseHotWater.ConsumptionPerHourHotWater = consumers.Sum(s => s.DailyUseHotWater.ConsumptionPerHourHotWater);
            SumConsumptionWaterAndWastewaterCollection.DailyUseHotWater.ConsumptionPerSecondHotWater = consumers.Sum(s => s.DailyUseHotWater.ConsumptionPerSecondHotWater);

            SumConsumptionWaterAndWastewaterCollection.DailyWastewaterCollection.WastewaterCollectionPerDay = consumers.Sum(s => s.DailyWastewaterCollection.WastewaterCollectionPerDay);
            SumConsumptionWaterAndWastewaterCollection.DailyWastewaterCollection.WastewaterCollectionPerHour = consumers.Sum(s => s.DailyWastewaterCollection.WastewaterCollectionPerHour);
            SumConsumptionWaterAndWastewaterCollection.DailyWastewaterCollection.WastewaterCollectionPerSecond = consumers.Sum(s => s.DailyWastewaterCollection.WastewaterCollectionPerSecond);
        }
    }
}