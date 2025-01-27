using System;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string fN, string o, string d, DateTime et, string s) : base(fN, o, d, et, s) { }

        public override double CalculateFees()
        {
            double fees = 300;
            if (Origin == "Singapore (SIN)")
            {
                return fees += 500;
            }
            else if (Destination == "Singapore (SIN)")
            {
                return fees += 800;
            }

            return 0;
        }

        public override string ToString()
        {
            return base.ToString() + "Normal Flight";
        }
    }
}
