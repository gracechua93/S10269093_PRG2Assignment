﻿using S10269093_PRG2Assignment;

// Dictionaries
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

Console.WriteLine("Loading Airlines...");
LoadAirlines(airlineDict);
Console.WriteLine("8 Airlines Loaded!");
void LoadAirlines(Dictionary<string, Airline> airDict)
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? s = sr.ReadLine();

        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string airname = data[0];
            string aircode = data[1];

            // Add to the airline dictionary
            Airline airline = new Airline(aircode, airname);
            airlineDict[aircode] = airline;
        }
    }
}

Console.WriteLine("Loading Flights...");
LoadFlights(flightDict);
Console.WriteLine("30 Flights Loaded!");
void LoadFlights(Dictionary<string, Flight> fDict)
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? s = sr.ReadLine();

        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string fNum = data[0];
            string fOrigin = data[1];
            string fDest = data[2];
            DateTime eDepart_Arrival = DateTime.Parse(data[3]);
            string fcode = data[4];

            string status = "Scheduled";

            // Add to the flight dictionary
            if (fcode == "CFFT")
            {
                Flight flight = new CFFTFlight(fNum, fOrigin, fDest, eDepart_Arrival, status);
                flightDict[fNum] = flight;
            }
            else if (fcode == "DDJB")
            {
                Flight flight = new DDJBFlight(fNum, fOrigin, fDest, eDepart_Arrival, status);
                flightDict[fNum] = flight;
            }
            else if (fcode == "LWTT")
            {
                Flight flight = new LWTTFlight(fNum, fOrigin, fDest, eDepart_Arrival, status);
                flightDict[fNum] = flight;
            }
            else
            {
                Flight flight = new NORMFlight(fNum, fOrigin, fDest, eDepart_Arrival, status);
                flightDict[fNum] = flight;
            }
        }
    }
}

void DisplayFlight(Dictionary<string, Flight> fDict, Dictionary<string, Airline> airDict)
{
    LoadFlights(flightDict);
    LoadAirlines(airDict);
    Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
                "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> f in flightDict)
    {
        string airlineCode = f.Value.FlightNumber.Split(' ')[0];
        if (airlineDict.ContainsKey(airlineCode))
        {
            Airline a = airlineDict[airlineCode];
            Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
                f.Value.FlightNumber, a.Name, f.Value.Origin, f.Value.Destination, f.Value.ExpectedTime);
        }
    }
}

Console.WriteLine("Loading Boarding Gates...");
LoadBoardingGates(boardingGateDict);
Console.WriteLine("66 Boarding Gates Loaded!");

void LoadBoardingGates(Dictionary<string, BoardingGate> bgDict)
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
         string? s = sr.ReadLine();

         while ((s = sr.ReadLine()) != null)
         {
                string[] data = s.Split(',');
                string bGate = data[0];
                bool isDDJB = bool.Parse(data[1]);
                bool isCFFT = bool.Parse(data[2]);
                bool isLWTT = bool.Parse(data[3]);

                // Add to the flight dictionary
                BoardingGate boardingGate = new BoardingGate(bGate, isDDJB, isCFFT, isLWTT);
                bgDict[bGate] = boardingGate;
         }
    }
}

void DisplayBoardingGates(Dictionary<string, BoardingGate> bgDict)
{
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20}",
        "Gate Number", "DDJB", "CFFT", "LWTT");
    foreach (KeyValuePair<string, BoardingGate> bg in bgDict)
    {
            Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20}",
                bg.Value.GateName, bg.Value.SupportsDDJB, bg.Value.SupportsCFFT, bg.Value.SupportsLWTT);
    }
}

void DisplayMenu()
{
        Console.WriteLine("=============================================");
        Console.WriteLine("Welcome to Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("1. List All Flights");
        Console.WriteLine("2. List Boarding Gates");
        Console.WriteLine("3. Assign a Boarding Gate to a Flight");
        Console.WriteLine("4. Create Flight");
        Console.WriteLine("5. Display Airline Flights");
        Console.WriteLine("6. Modify Flight Details");
        Console.WriteLine("7. Display Flight Schedule");
        Console.WriteLine("0. Exit");
        Console.WriteLine();
        Console.WriteLine("Please select your option:");
}


while (true)
{
    Console.WriteLine();
    DisplayMenu();
    string? option = Console.ReadLine();

    if (option == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else if (option == "1")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayFlight(flightDict, airlineDict);
    }
    else if (option == "2")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayBoardingGates(boardingGateDict);
    }
    else if (option == "3")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");
        Console.Write("Enter the Flight Number: ");
        string? flightNum = Console.ReadLine();
        Console.Write("Enter Boarding Gate Name: ");
        string? gateName = Console.ReadLine();

        bool flightFound = false;
        Flight selectedFlight = new ();

        foreach (KeyValuePair<string, Flight> f in flightDict)
        {
            if (flightNum == f.Value.FlightNumber)
            {
                string airlineCode = f.Value.FlightNumber.Split(' ')[0];
                if (airlineDict.ContainsKey(airlineCode))
                {
                    Airline a = airlineDict[airlineCode];
                    selectedFlight = f.Value;

                    //Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
                    //    f.Value.FlightNumber, a.Name, f.Value.Origin, f.Value.Destination, f.Value.ExpectedTime);
                }
                flightFound = true;
                break;
            }
        }
        if (!flightFound)
        {
            Console.WriteLine("Unable to find flight information.");
        }

        Console.WriteLine("Flight Number: ", selectedFlight.FlightNumber);
        Console.WriteLine("Origin: ", selectedFlight.Origin);
        Console.WriteLine("Destination: ", selectedFlight.Destination);
        Console.WriteLine("Expected Time: ", selectedFlight.ExpectedTime);
        //Console.WriteLine("Special Request Code: ", );
        Console.WriteLine("Boarding Gate Name: ", gateName);




    }
}
