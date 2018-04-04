using System;
using System.Timers;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace CS_Exam
{
    class Store
    {
        public int Range { get; private set; }

        private Timer dayTimer;
        private Timer weekTimer;
        private Timer epidemTimer;
        private Timer UITimer;

        public delegate void StateDownDelegate();
        public event StateDownDelegate StateDownEvent;

        private DailySerialize day;

        int rangeCount;

        public Store()
        {
            Data.Vegetables = new Dictionary<VegetableName, Stack<Vegetable>>();
            Data.VegetablesState = new Dictionary<VegetableName, List<int>>();
            Data.VegetablesStats = new Dictionary<VegetableName, List<int>>();

            Data.Customers = new Queue<Customer>();
            Range = Enum.GetNames(typeof(VegetableName)).Length;

            dayTimer = new Timer(24 * 500);
            weekTimer = new Timer(7 * 24 * 500);
            UITimer = new Timer(24 * 500);
            epidemTimer = new Timer();
            epidemTimer.Enabled = false;
            epidemTimer.AutoReset = false;
        }

        private void Sort()
        {
            for (int i = 0; i < Data.Vegetables.Count; ++i)
            {
                int freshCount = 0, normalCount = 0, rottenCount = 0, toxicCount = 0;

                if (Data.Vegetables.ContainsKey((VegetableName)i) == true)
                {
                    List<Vegetable> old = new List<Vegetable>(Data.Vegetables[(VegetableName)i]);

                    Stack<Vegetable> freshStack = new Stack<Vegetable>();
                    Stack<Vegetable> normalStack = new Stack<Vegetable>();

                   while(Data.Vegetables[(VegetableName)i].Count > 0)
                    {
                        Vegetable v = Data.Vegetables[(VegetableName)i].Pop();

                        if (v.State == VegetableState.Fresh)
                        {
                            freshStack.Push(v);
                            ++freshCount;
                        }
                        else if (v.State == VegetableState.Normal)
                        {
                            normalStack.Push(v);
                            ++normalCount;
                        }
                        else if (v.State == VegetableState.Rotten)
                        {
                            ++rottenCount;
                            Data.VegetablesStats[(VegetableName)i][1]++;
                        }
                        else
                        {
                            ++toxicCount;
                            Data.VegetablesStats[(VegetableName)i][1]++;
                        }
                    }

                    while (freshStack.Count > 0)
                    {
                        Data.Vegetables[(VegetableName)i].Push(freshStack.Pop());
                    }

                    while (normalStack.Count > 0)
                    {
                        Data.Vegetables[(VegetableName)i].Push(normalStack.Pop());
                    }

                    Data.VegetablesState[(VegetableName)i].Clear();
                    Data.VegetablesState[(VegetableName)i].Add(freshCount);
                    Data.VegetablesState[(VegetableName)i].Add(normalCount);
                    Data.VegetablesState[(VegetableName)i].Add(rottenCount);
                    Data.VegetablesState[(VegetableName)i].Add(toxicCount);
                }
            }

            if (epidemTimer.Enabled == false && Data.Days %7 != 0)
            {
                Data.Status = StoreStatus.Sorted;
            }
        }

        private void Buy()
        {
            Data.OldCapital = Data.Capital;
            Random rnd = new Random();
            int rangeOld = rangeCount;
            rangeCount = 0;
            int ReservedCapital = (int)(Data.Capital * 0.1);

            for (int i = 0; i < Range; ++i)
            {
                int count = Data.CustomersCount == 0 || rangeOld == 0 ? rnd.Next(10, 30) : rnd.Next(Data.CustomersCount / 10, Data.CustomersCount/2);

                int vegetablesCount = 0;

                int price = Vegetable.VegetablePrice((VegetableName)i);

                Stack<Vegetable> tmp = new Stack<Vegetable>();

                int toxic = rnd.Next(0, 100);

                if(Data.Vegetables.ContainsKey((VegetableName)i) == true)
                {
                    Data.VegetablesStats[(VegetableName)i][2] = 0;
                }

                if (Data.Capital < ReservedCapital + price)
                {
                    continue;
                }

                rangeCount++;

                for (int j = 0; j < count; ++j)
                {
                    if (Data.Capital < ReservedCapital + price)
                    {
                        break;
                    }

                    Vegetable vegetable = new Vegetable((VegetableName)i);

                    StateDownEvent += vegetable.StateDownEvent;

                    if (Data.Vegetables.ContainsKey((VegetableName)i) == true)
                    {
                        Data.Vegetables[(VegetableName)i].Push(vegetable);
                    }
                    else
                    {
                        tmp.Push(vegetable);
                    }

                    if (toxic == 6)
                    {
                        Data.Capital -= price;
                    }

                    Data.Capital -= price;
                    ++vegetablesCount;
                }

                if (Data.Vegetables.ContainsKey((VegetableName)i) != true || Data.Vegetables[(VegetableName)i].Count == 0)
                {
                    Data.Vegetables.Add((VegetableName)i, tmp);
                    Data.VegetablesState.Add((VegetableName)i, new List<int>());
                    Data.VegetablesStats.Add((VegetableName)i, new List<int>());
                    Data.VegetablesStats[(VegetableName)i].Add(0);
                    Data.VegetablesStats[(VegetableName)i].Add(0);
                    Data.VegetablesStats[(VegetableName)i].Add(0);
                }

                Data.VegetablesStats[(VegetableName)i][2] = vegetablesCount;
            }

            Data.Range = rangeCount;

            if (epidemTimer.Enabled == false)
            {
                Data.Status = StoreStatus.Buy;
            }
        }

        private void CustomerBuy()
        {
            while(Data.Customers.Count > 0)
            {
                Customer customer = Data.Customers.Dequeue();

                if (Data.Vegetables.ContainsKey(customer.Desired) && Data.Vegetables[customer.Desired].Count > 0)
                {
                    Vegetable Taken = Data.Vegetables[customer.Desired].Pop();
                    do
                    {
                        if (Taken.State == VegetableState.Fresh || Taken.State == VegetableState.Normal)
                        {
                            Data.Capital += Taken.SellPrice();
                            ++Data.Rank;
                            Data.VegetablesStats[(VegetableName)customer.Desired][0]++;
                        }
                        else if (Taken.State == VegetableState.Rotten)
                        {
                            Taken = Data.Vegetables[customer.Desired].Pop();
                            Data.VegetablesStats[(VegetableName)customer.Desired][1]++;
                        }
                        else if (Taken.State == VegetableState.Toxic)
                        {
                            Data.Rank -= 2;
                            Data.VegetablesStats[(VegetableName)customer.Desired][1]++;
                        }
                    } while (Taken.State == VegetableState.Rotten);
                }
                else
                {
                    Data.Rank -= 1;
                }
            }
        }

        private void VegetablesStateDown()
        {
            if (StateDownEvent != null)
            {
                StateDownEvent.Invoke();
            }
        }

        private void AddCustomers()
        {
            VegetableName prev = 0;
            for (int i = 0; i < (Data.Rank /10); ++i)
            {
                Customer customer = new Customer();

                VegetableName Desire;
                while (true)
                {
                    Desire = customer.NewDesire();
                    if (Desire == prev)
                        continue;
                    else
                    {
                        prev = Desire;
                        break;
                    }
                }
                Data.Customers.Enqueue(customer);
            }
                Data.CustomersCount = Data.Customers.Count;
        }

        private void OneDay(Object source, ElapsedEventArgs e)
        {
            Random rnd = new Random();

            Data.Days += 1;

            if (epidemTimer.Enabled == false)
            {
                Data.Rank += 5;
            }

            if(rnd.Next(0, 100) == 25 && !epidemTimer.Enabled)
            {
                Epidem();
            }

            VegetablesStateDown();
            Sort();

            if (epidemTimer.Enabled == false)
            {
                AddCustomers();
                CustomerBuy();
            }

            if (Data.Rank <= 0 || (Data.Days > 0 && Data.Range  == 0))
            {
                Bankrupt();
            }

            day = new DailySerialize();
            SaveData();
        }

        private void SaveData()
        {
            XmlSerializer xs = new XmlSerializer(day.GetType());

            using (FileStream fs = new FileStream(@"..\..\DailyData.xml", FileMode.Create))
            {
                xs.Serialize(fs, day);
            }
        }

        private void OneWeek(Object source, ElapsedEventArgs e)
        {
            Buy();
        }

        private void EndEpidem(Object source, ElapsedEventArgs e)
        {
            epidemTimer.Enabled = false;
        }

        private void UpdateUI(Object source, ElapsedEventArgs e)
        {
            UI.Visible();
        }

        private void Epidem()
        {
            Random rnd = new Random();
            epidemTimer.Interval = rnd.Next(7, 14) * dayTimer.Interval;
            epidemTimer.Elapsed += EndEpidem;
            Data.Status = StoreStatus.Epidem;
            epidemTimer.Start();
        }

        private void Bankrupt()
        {
            Data.Status = StoreStatus.Bankrupt;
            dayTimer.Stop();
            weekTimer.Stop();
            UITimer.Stop();
        }

        public void OpenStore()
        {
            UI.Visible();

            Buy();
            
            dayTimer.Elapsed += OneDay;
            weekTimer.Elapsed += OneWeek;
            UITimer.Elapsed += UpdateUI;

            dayTimer.Start();
            weekTimer.Start();
            UITimer.Start();
        }
    }
}