﻿using System;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight() : base() { }
        public CFFTFlight(string fN, string o, string d, DateTime et, string s, double rF) : base(fN, o, d, et)
        {
            RequestFee = rF;
        }

        public override double CalculateFees()
        {
            double fees = 0;
            if (Origin == "Singapore (SIN)")
            {
                fees = 500 + 300;
            }
            else if (Destination == "Singapore (SIN)")
            {
                fees = 800 + 300;
            }

            RequestFee = 150;
            fees += RequestFee;
            return fees;

        }

        public override string ToString()
        {
            return base.ToString() + "CFFT Flight";
        }
    }
}
