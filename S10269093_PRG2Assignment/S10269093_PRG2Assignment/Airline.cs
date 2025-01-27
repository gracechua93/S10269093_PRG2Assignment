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
            int totalFlights = Flights.Count;
            int flightsWithNoRequestCodes = 0;
            int flightsDuringPromoTimes = 0;
            int flightsFromPromoOrigins = 0;

            foreach (Flight flight in Flights.Values)
            {
                fees += flight.CalculateFees();

                // Check for flights without special request codes
                if (!(flight is CFFTFlight) && !(flight is DDJBFlight) && !(flight is LWTTFlight))
                {
                    flightsWithNoRequestCodes++;
                }

                // Check for flights during promotional times
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                {
                    flightsDuringPromoTimes++;
                }

                // Check for flights from specific origins
                if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
                {
                    flightsFromPromoOrigins++;
                }
            }

            // Apply discounts
            double discount = 0;

            // For every 3 flights
            discount += (totalFlights / 3) * 350;

            // For each flight during promotional times
            discount += flightsDuringPromoTimes * 110;

            // For each flight from promotional origins
            discount += flightsFromPromoOrigins * 25;

            // For each flight with no request codes
            discount += flightsWithNoRequestCodes * 50;

            // Apply 3% discount if total flights > 5
            if (totalFlights > 5)
            {
                discount += fees * 0.03;
            }

            // Subtract total discount
            fees -= discount;

            return fees;
        }

        public override string ToString()
        {
            return "Airline Name: " + Name + "\nAirline Code: " + Code;
        }
    }
}
