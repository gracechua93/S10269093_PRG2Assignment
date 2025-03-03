﻿using System;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight() : base() { }

        public DDJBFlight(string fN, string o, string d, DateTime et) : base(fN, o, d, et)
        {
            RequestFee = 300;
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

            fees += RequestFee;
            return fees;

        }
        public int CompareTo(DDJBFlight f)
        {
            return ExpectedTime.CompareTo(f.ExpectedTime);
        }

        public override string ToString()
        {
            return base.ToString() + "\nSpecial Request Code: " + "DDJB";
        }
    }
}
