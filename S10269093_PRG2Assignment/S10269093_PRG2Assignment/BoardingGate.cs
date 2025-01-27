using System;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

namespace S10269093_PRG2Assignment
{
    class BoardingGate
    {
        // properties
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight? Flight { get; set; }

        public BoardingGate() { }

        public BoardingGate(string gname, bool sCCFT, bool sDDJB, bool sLWTT)
        {
            GateName = gname;
            SupportsCFFT = sCCFT;
            SupportsDDJB = sDDJB;
            SupportsLWTT = sLWTT;
        }

        public double CalculatFees()
        {
            return 300;
        }

        public override string ToString()
        {
            return "GateName: " + GateName + "\nSupport CTTF: " + SupportsCFFT +
                "\n Support DDJB: " + SupportsDDJB + "\n Support LWTT: " + SupportsLWTT;
        }

    }
}
