using System;
using System.Timers;

namespace CS_Exam
{
    static class UI
    {
        const string LeftTop = "╔";
        const string RightTop = "╗";
        const string LeftBottom = "╚";
        const string RightBottom = "╝";
        const string HorizontalTop = "╦";
        const string HorizontalBottom = "╩";
        const string VerticalLeft = "╠";
        const string MiddleJoint = "╬";
        const string VerticalRight = "╣";
        const string HorizontalLine = "═";
        const string VerticalLine = "║";

        private static int OldRank = Data.Rank;
        private static int OldCustomers = 0;
        private static int OldRange = 0;

        private static Timer update = new Timer(1000);

        private static void Header()
        {
            for (int i = 0; i < 100; ++i)
            {
                if (i == 0) Console.Write(LeftTop);
                else if (i == 12 || i == 26 || i == 45 || i == 66 || i == 85) Console.Write(HorizontalTop);
                else if (i == 99) Console.WriteLine(RightTop);
                else Console.Write(HorizontalLine);
            }
            Console.WriteLine($"{VerticalLine} VEG Store {VerticalLine} RANK : {Data.Rank, 4} {VerticalLine} CAPITAL : {Data.Capital, 6} {VerticalLine} PRODUCT RANGE : {Data.Range, 2} {VerticalLine} CUSTOMERS : {Data.CustomersCount, 4} {VerticalLine} DAYS : {Data.Days, 4} {VerticalLine}");
            for (int i = 0; i < 100; ++i)
            {
                if (i == 0) Console.Write(LeftBottom);
                else if (i == 12 || i == 26 || i == 45 || i == 66 || i == 85) Console.Write(HorizontalBottom);
                else if (i == 99) Console.WriteLine(RightBottom);
                else Console.Write(HorizontalLine);
            }
        }

        private static void Subheader()
        {
            for (int i = 0; i < 100; ++i)
            {
                if (i == 0) Console.Write(LeftTop);
                else if (i == 33 || i == 77) Console.Write(HorizontalTop);
                else if ( i == 99) Console.WriteLine(RightTop);
                else Console.Write(HorizontalLine);
            }
            Console.WriteLine($"{VerticalLine} {" ",13} BUY {" ",12} {VerticalLine}  {" ",8}  STATE AFTER SORTING  {" ",8}  {VerticalLine} {" ",5}STATISTICS{" ",4} {VerticalLine}");
            for (int i = 0; i < 100; ++i)
            {
                if (i == 0) Console.Write(VerticalLeft);
                else if (i == 33 || i == 77) Console.Write(MiddleJoint);
                else if(i == 21 || i == 40 || i == 50 || i == 59 || i == 68 || i == 88) Console.Write(HorizontalTop);
                else if (i == 99) Console.WriteLine(VerticalRight);
                else Console.Write(HorizontalLine);
            }
            Console.WriteLine($"{VerticalLine}    PRODUCT NAME    {VerticalLine}   COUNT   {VerticalLine}  ALL {VerticalLine}  FRESH  {VerticalLine} NORMAL {VerticalLine} ROTTEN {VerticalLine}  TOXIC {VerticalLine}   SOLD   {VerticalLine}   TURF   {VerticalLine}");
            for (int i = 0; i < 100; ++i)
            {
                if (i == 0) Console.Write(VerticalLeft);
                else if (i == 21 || i == 33 || i == 40 || i == 50 || i == 59 || i == 68 || i == 77 || i == 88) Console.Write(MiddleJoint);
                else if (i == 99) Console.WriteLine(VerticalRight);
                else Console.Write(HorizontalLine);
            }
        }

        private static void Body()
        {
            for(int j = 0; j < 18; ++j)
            {
                if (j == 17)
                {
                    for (int i = 0; i < 100; ++i)
                    {
                        if (i == 0) Console.Write(LeftBottom);
                        else if (i == 21 || i == 33 || i == 40 || i == 50 || i == 59 || i == 68 || i == 77 || i == 88) Console.Write(HorizontalBottom);
                        else if (i == 99) Console.WriteLine(RightBottom);
                        else Console.Write(HorizontalLine);
                    }
                }
                else
                {
                    int count = 0;
                    int fresh = 0;
                    int normal = 0;
                    int rotten = 0;
                    int toxic = 0;
                    int sold = 0;
                    int turf = 0;
                    int buyyed = 0;

                    if (Data.Vegetables.ContainsKey((VegetableName)j) == true)
                    {
                        count = Data.Vegetables[(VegetableName)j].Count;
                        fresh = Data.VegetablesState[(VegetableName)j][0];
                        normal = Data.VegetablesState[(VegetableName)j][1];
                        rotten = Data.VegetablesState[(VegetableName)j][2];
                        toxic = Data.VegetablesState[(VegetableName)j][3];
                        sold = Data.VegetablesStats[(VegetableName)j][0];
                        turf = Data.VegetablesStats[(VegetableName)j][1];
                        buyyed = Data.VegetablesStats[(VegetableName)j][2];
                    }

                    Console.WriteLine($"{VerticalLine}{(VegetableName)j, -20}{VerticalLine}{"+" + buyyed, 11}{VerticalLine}{count,6}{VerticalLine}{fresh,9}{VerticalLine}{normal,8}{VerticalLine}{rotten,8}{VerticalLine}{toxic,8}{VerticalLine}{sold,10}{VerticalLine}{turf,10}{VerticalLine}");
                    if(j != 16)
                    {
                        for (int i = 0; i < 100; ++i)
                        {
                            if (i == 0) Console.Write(VerticalLeft);
                            else if (i == 21 || i == 33 || i == 40 || i == 50 || i == 59 || i == 68 || i == 77 || i == 88) Console.Write(MiddleJoint);
                            else if (i == 99) Console.WriteLine(VerticalRight);
                            else Console.Write(HorizontalLine);
                        }
                    }
                }
            }
        }

        private static void Sidebar()
        {
            for(int j = 0; j < 42; ++j)
            {
                Console.SetCursorPosition(100, j);
                if(j == 0 || j == 3 || j == 8 || j == 39)
                {
                    for (int i = 0; i < 30; ++i)
                    {
                        if (i == 0) Console.Write(LeftTop);
                        else if (i == 29) Console.WriteLine(RightTop);
                        else Console.Write(HorizontalLine);
                    }
                }
                else if(j == 1)
                {
                    Console.Write($"{VerticalLine}        STORE STATE         {VerticalLine}");

                }
                else if(j == 4)
                {
                    Console.Write($"{VerticalLine}           STATUS           {VerticalLine}");
                }
                else if (j == 5)
                {
                    for (int i = 0; i < 30; ++i)
                    {
                        if (i == 0) Console.Write(VerticalLeft);
                        else if (i == 29) Console.WriteLine(VerticalRight);
                        else Console.Write(HorizontalLine);
                    }
                }
                else if (j == 6)
                {
                    Console.Write($"{VerticalLine}          ");

                    if (Data.Status == 0) Console.ForegroundColor = ConsoleColor.Black;
                    else if(Data.Status == StoreStatus.Buy) Console.ForegroundColor = ConsoleColor.Green;
                    else if (Data.Status == StoreStatus.Sorted) Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (Data.Status == StoreStatus.Epidem) Console.ForegroundColor = ConsoleColor.Red;
                    else if (Data.Status == StoreStatus.Bankrupt) Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write($"{Data.Status.ToString().ToUpper(),8}");
                    Console.ResetColor();
                    Console.Write($"          {VerticalLine}");
                }
                else if(j == 12)
                {
                    Console.Write($"{VerticalLine}            RANK            {VerticalLine}");
                }
                else if (j == 14)
                {
                    int res = Data.Rank - OldRank;
                    if(res >= 0)
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"+ {res, 4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                    else
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"- {res * (-1),4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                }
                else if (j == 18)
                {
                    Console.Write($"{VerticalLine}           CAPITAL          {VerticalLine}");
                }
                else if (j == 20)
                {
                    int res = Data.Capital - Data.OldCapital;
                    if (res >= 0)
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"+ {res,4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                    else
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"- {res * (-1),4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                }
                else if (j == 24)
                {
                    Console.Write($"{VerticalLine}            RANGE           {VerticalLine}");
                }
                else if (j == 26)
                {
                    int res = Data.Range - OldRange;
                    if (res >= 0)
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"+ {res,4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                    else
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"- {res * (-1),4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                }
                else if (j == 30)
                {
                    Console.Write($"{VerticalLine}          CUSTOMERS         {VerticalLine}");
                }
                else if (j == 32)
                {
                    int res = Data.CustomersCount - OldCustomers;
                    if (res >= 0)
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"+ {res,4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                    else
                    {
                        Console.Write($"{VerticalLine}           ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"- {res * (-1),4}");
                        Console.ResetColor();
                        Console.Write($"           {VerticalLine}");
                    }
                }
                else if (j == 40)
                {
                    Console.Write($"{VerticalLine}        Fuad Agazade        {VerticalLine}");
                }
                else if(j == 2 || j == 7|| j == 38 || j == 41)
                {
                    for (int i = 0; i < 30; ++i)
                    {
                        if (i == 0) Console.Write(LeftBottom);
                        else if (i == 29) Console.WriteLine(RightBottom);
                        else Console.Write(HorizontalLine);
                    }
                }
                else
                {
                    Console.Write($"{VerticalLine}                            {VerticalLine}");
                }
            }
        }

        public static void Visible()
        {
            Console.Clear();
            Header();
            Subheader();
            Body();
            Sidebar();

            OldRange = Data.Range;
            OldCustomers = Data.CustomersCount;
        }
    }
}
