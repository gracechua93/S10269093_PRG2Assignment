﻿using System;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string fN, string o, string d, DateTime et) : base(fN, o, d, et) { }

        public int CompareTo(NORMFlight f)
        {
            return ExpectedTime.CompareTo(f.ExpectedTime);
        }

        public override double CalculateFees()
        {
            double fees = 300;
            if (Origin == "Singapore (SIN)")
            {
                fees += 500;
            }
            else if (Destination == "Singapore (SIN)")
            {
                fees += 800;
            }

            return fees;
        }

        public override string ToString()
        {
            return base.ToString() + "\nSpecial Request Code: " + "None";
        }
    }
}
