using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGate { get; set; } = new Dictionary<string, BoardingGate>();
        //public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal() { }

        public Terminal(string t, Dictionary<string, Airline> a, Dictionary<string, Flight> f, Dictionary<string, BoardingGate> bg)
        {
            TerminalName = t;
            Airlines = a;
            Flights = f;
            BoardingGate = bg;
        }

        // methods
        public bool AddAirline(Airline a)
        {
            if (!Airlines.ContainsKey(a.Code))
            {
                Airlines[a.Code] = a;
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate gate)
        {
            if (!BoardingGate.ContainsKey(gate.GateName))
            {
                BoardingGate[gate.GateName] = gate;
                return true;
            }
            return false;
        }

        public Airline GetAirlineFromFlight(Flight f)
        {
            foreach (Airline a in Airlines.Values)
            {
                if (a.Flights.ContainsKey(f.FlightNumber))
                {
                    return a;
                }
            }

            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (Airline a in Airlines.Values)
            {
                Console.WriteLine($"Airline: {a.Name}, Fees: {a.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            return "Terminal: " + TerminalName;
        }
    }
}
