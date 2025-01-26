using System;

namespace S10269093_PRG2Assignment
{
    class Flight
    {
        // properties
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        // constructors
        public Flight() { }

        public Flight(string fN, string o, string d, DateTime et)
        {
            FlightNumber = fN;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
        }

        public virtual double CalculateFees()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Flight Number: " + FlightNumber +
                "\nOrigin: " + Origin + "\nDestination: " +
                Destination + "\nExpected Time: " + ExpectedTime + "\nStatus: " + Status;
        }

    }
}
