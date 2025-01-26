using System;
using System.Collections.Generic;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class Airline
    {
        // properties
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline() { }
        public Airline(string c, string n)
        {
            Name = n;
            Code = c;
        }

        // methods
        public bool AddFlight(Flight f)
        {
            if (!Flights.ContainsKey(f.FlightNumber))
            {
                Flights.Add(f.FlightNumber, f);
                return true;
            }
            return false;
        }

        public bool RemoveFlight(Flight f)
        {
            return Flights.Remove(f.FlightNumber);
        }

        public double CalculateFees()
        {
            double fees = 0;
            foreach (Flight flight in Flights.Values)
            {
                fees += flight.CalculateFees();
            }
            return fees;
        }

        public override string ToString()
        {
            return "Airline Name: " + Name + "\nAirline Code: " + Code;
        }
    }
}
