using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CS_Exam
{
    [Serializable]
    public class DailySerialize
    {
        public int Days, Rank, Capital, Range, CustomersCount, soldAVG;
        public StoreStatus Status;

        public DailySerialize()
        {
            Days = Data.Days;
            Rank = Data.Rank;
            Capital = Data.Capital;
            Range = Data.Range;
            CustomersCount = Data.CustomersCount;
            soldAVG = Data.soldAVG;
            Status = Data.Status;
        }
    }
}
