using System;

namespace CS_Exam
{
    [Serializable]
    public class Vegetable : IComparable
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public VegetableState State { get; private set; }
        private static int prev;

        public Vegetable(VegetableName vn)
        {
            Name = vn.ToString();
            Price = VegetablePrice(vn);
            State = VegetableState.Fresh;
            prev = 0;
        }

        public static int VegetablePrice(VegetableName vn)
        {
            switch (vn)
            {
                case VegetableName.Cabbage: return 17;
                case VegetableName.Turnip: return 12;
                case VegetableName.Radish: return 11;
                case VegetableName.Carrot: return 7;
                case VegetableName.Parsnip: return 19;
                case VegetableName.Beetroot: return 26;
                case VegetableName.Lettuce: return 13;
                case VegetableName.Beans: return 20;
                case VegetableName.Peas: return 24;
                case VegetableName.Potato: return 5;
                case VegetableName.Eggplant: return 9;
                case VegetableName.Tomato: return 7;
                case VegetableName.Cucumber: return 6;
                case VegetableName.Pumpkin: return 25;
                case VegetableName.Onion: return 4;
                case VegetableName.Garlic: return 5;
                case VegetableName.Leek: return 15;
                case VegetableName.Pepper: return 10;
            }
            return 0;
        }

        public int SellPrice()
        {
            return Price + (int)(Price * 0.4);
        }

        public void StateDown()
        {
            if (State < VegetableState.Rotten)
                ++State;
        }

        public void StateDownEvent()
        {
            Random rnd = new Random();
            int a;
            while (true)
            {
                a = rnd.Next(12);
                if (a == prev)
                    continue;
                else
                {
                    prev = a;

                    if (a % 4 == 0)
                    {
                        StateDown();
                    }
                    break;
                }
            }
        }

        public void StateToxic()
        {
            State = VegetableState.Toxic;
        }

        public void Info()
        {
            Console.WriteLine($"Vegetable Name : {Name} \t\tPrice : {Price} \t\tState : {State}");
        }

        public int CompareTo(object obj)
        {
            return this.State.CompareTo((obj as Vegetable).State);
        }
    }
}