﻿using System;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight() : base() { }

        public LWTTFlight(string fN, string o, string d, DateTime et, string s) : base(fN, o, d, et, s)
        {
            RequestFee = 500;
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

        public override string ToString()
        {
            return base.ToString() + "\nSpecial Request Code: " + "LWTT";
        }
    }
}
