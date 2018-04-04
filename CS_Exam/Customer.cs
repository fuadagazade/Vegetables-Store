using System;

namespace CS_Exam
{
    [Serializable]
    public class Customer
    {
        public VegetableName Desired { get; private set; }

        public Customer()
        {
            Random rnd = new Random();
            Desired = (VegetableName)(rnd.Next(0, Enum.GetNames(typeof(VegetableName)).Length - 1));
        }

        public VegetableName NewDesire()
        {
            Random rnd = new Random();
            Desired = (VegetableName)(rnd.Next(0, Enum.GetNames(typeof(VegetableName)).Length - 1));
            return Desired;
        }
    }
}