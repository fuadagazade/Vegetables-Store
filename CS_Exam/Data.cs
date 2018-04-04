using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace CS_Exam
{
    static class Data
    {
        public static Dictionary<VegetableName, Stack<Vegetable>> Vegetables;
        public static Dictionary<VegetableName, List<int>> VegetablesState;
        public static Dictionary<VegetableName, List<int>> VegetablesStats;

        public static Queue<Customer> Customers;

        public static int Days = 0;
        public static int Rank = 400;
        public static int Capital = 5000;
        public static int OldCapital = 0;
        public static int Range = 0;
        public static int CustomersCount = 0;
        public static int soldAVG = 0;
        public static StoreStatus Status = 0;
    }
}
