using System.Collections.Generic;
using WaterBalance.Models;
using WaterBalance.Services;
using WaterBalance.View;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace WaterBalance.ViewModels
{
    public sealed class ConsumptionTypeViewModel : ObservableObj
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private List<Consumer>? consumers;
        public List<Consumer>? Consumers
        {
            get => consumers;
            set
            {
                consumers = value;
                OnPropertyChanged("Consumers");
            }
        }

        private List<Consumer> LoadAllConsumers()
        {
            List<Consumer> consumers = new List<Consumer>();

            using (ApplicationContext context = new ApplicationContext())
            {
                try
                {
                    consumers = context.Consumers.ToList();
                    return consumers;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Method 'LoadAllConsumers' / class 'ConsumptionTypeViewModel'");
                    return consumers;
                }
            }
        }

        public ConsumptionTypeViewModel()
        {
            Consumers = LoadAllConsumers();
        }

        private RelayCommand addConsumerCommand;
        public RelayCommand AddConsumerCommand
        {
            get
            {
                return addConsumerCommand ??
                    (addConsumerCommand = new RelayCommand(o =>
                    {
                        ConsumerWindow consumerWindow = new ConsumerWindow(new Consumer());

                        if (consumerWindow.ShowDialog() == true)
                        {
                            Consumer consumer = consumerWindow.Consumer;

                            using (ApplicationContext context = new ApplicationContext())
                            {
                                try
                                {
                                    context.Consumers.Add(consumer);
                                    context.SaveChanges();

                                    Consumers = LoadAllConsumers();
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex, "Command 'AddConsumerCommand' / class 'ConsumptionTypeViewModel'");
                                }
                            }
                        }
                    }));
            }
        }

        private RelayCommand editConsumerCommand;
        public RelayCommand EditConsumerCommand
        {
            get
            {
                return editConsumerCommand ??
                    (editConsumerCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) 
                            return;

                        Consumer? consumer = selectedItem as Consumer;

                        Consumer conWin = new Consumer()
                        {
                            Id = consumer.Id,
                            ConsumerName = consumer.ConsumerName,
                            UnitName = consumer.UnitName,
                            Unit = consumer.Unit,
                            ConsumptionRatePerDay = consumer.ConsumptionRatePerDay,
                            HotWaterConsumption = consumer.HotWaterConsumption,
                            WastewaterCollection = consumer.WastewaterCollection,
                            Duration = consumer.Duration,
                            NormativeDocument = consumer.NormativeDocument
                        };

                        ConsumerWindow consumerWindow = new ConsumerWindow(conWin);

                        if (consumerWindow.ShowDialog() == true)
                        {
                            using (ApplicationContext context = new ApplicationContext())
                            {
                                try
                                {
                                    consumer = context.Consumers.Find(consumerWindow.Consumer.Id);

                                    if (consumer != null)
                                    {
                                        consumer.ConsumerName = consumerWindow.Consumer.ConsumerName;
                                        consumer.UnitName = consumerWindow.Consumer.UnitName;
                                        consumer.Unit = consumerWindow.Consumer.Unit;
                                        consumer.ConsumptionRatePerDay = consumerWindow.Consumer.ConsumptionRatePerDay;
                                        consumer.HotWaterConsumption = consumerWindow.Consumer.HotWaterConsumption;
                                        consumer.WastewaterCollection = consumerWindow.Consumer.WastewaterCollection;
                                        consumer.Duration = consumerWindow.Consumer.Duration;
                                        consumer.NormativeDocument = consumerWindow.Consumer.NormativeDocument;

                                        context.Entry(consumer).State = EntityState.Modified;
                                        context.SaveChanges();

                                        Consumers = LoadAllConsumers();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex, "Command 'EditConsumerCommand' / class 'ConsumptionTypeViewModel'");
                                }
                            }
                        }
                    }));
            }
        }

        private RelayCommand deleteConsumerCommand;
        public RelayCommand DeleteConsumerCommand
        {
            get
            {
                return deleteConsumerCommand ??
                    (deleteConsumerCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;

                        Consumer? consumer = selectedItem as Consumer;

                        using (ApplicationContext context = new ApplicationContext())
                        {
                            try
                            {
                                context.Consumers.Remove(consumer);
                                context.SaveChanges();

                                Consumers = LoadAllConsumers();                                
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Command 'DeleteConsumerCommand' / class 'ConsumptionTypeViewModel'");
                            }
                        }
                    }));
            }
        }
    }
}