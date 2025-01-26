using System;

namespace S10269093_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight() : base() { }

        public DDJBFlight(string fN, string o, string d, DateTime et, string s, double rF) : base(fN, o, d, et)
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

            RequestFee = 300;
            fees += RequestFee;
            return fees;

        }

        public override string ToString()
        {
            return base.ToString() + "DDJB Flight";
        }
    }
}
