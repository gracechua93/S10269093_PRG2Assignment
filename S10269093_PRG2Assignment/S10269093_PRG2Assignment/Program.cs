using S10269093_PRG2Assignment;

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

void DisplayAirlines()
{
    Console.WriteLine("{0,-15} {1,-15}",
        "Airline Code", "Airline Name");
    foreach (KeyValuePair<string, Airline> a in airlineDict)
    {
        Console.WriteLine("{0,-15} {1,-15}",
        a.Value.Code, a.Value.Name);
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
        "FlightNumber", "Airline Name", "Origin", "Destination", "ExpectedTime");
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
        Flight? selectedFlight = null;

        foreach (KeyValuePair<string, Flight> f in flightDict)
        {
            if (flightNum == f.Value.FlightNumber)
            {
                string airlineCode = f.Value.FlightNumber.Split(' ')[0];
                if (airlineDict.ContainsKey(airlineCode))
                {
                    Airline a = airlineDict[airlineCode];
                    selectedFlight = f.Value;
                    Console.WriteLine(selectedFlight);

                }
                flightFound = true;
                break;
            }
        }
        if (!flightFound)
        {
            Console.WriteLine("Unable to find flight information.");
        }

        foreach (KeyValuePair<string, BoardingGate> bg in boardingGateDict)
        {
            if (gateName == bg.Value.GateName)
            {
                Console.WriteLine(bg.Value.ToString());
                break;
            }
        }

        Console.Write("Would you like to update the status of the flight? (Y/N) ");
        string? updateStatus = Console.ReadLine();

        if (updateStatus == "Y")
        {
            Console.WriteLine("1. Delayed");
            Console.WriteLine("2. Boarding");
            Console.WriteLine("3. On Time");
            Console.Write("Please select the new status of the flight: ");
            string? newStatus = Console.ReadLine();
            if (newStatus == "1")
            {
                selectedFlight.Status = "Delayed";
            }
            else if (newStatus == "2")
            {
                selectedFlight.Status = "Boarding";
            }
            else if (newStatus == "3")
            {
                selectedFlight.Status = "On Time";
            }
        }
        Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {gateName}!");
        
    }
    else if (option == "4")
    {
        while (true)
        {

            Console.WriteLine("=============================================");
            Console.WriteLine("Create a new Flight");
            Console.WriteLine("=============================================");

            Console.Write("Enter Flight Number: ");
            string? flightNumber = Console.ReadLine();
            Console.Write("Enter Origin: ");
            string? origin = Console.ReadLine();
            Console.Write("Enter Destination: ");
            string? destination = Console.ReadLine();
            Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            DateTime expectedTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string? specialReqCode = Console.ReadLine();

            string status = "Scheduled";
            if (specialReqCode == "None")
            {
                Flight newFlight = new NORMFlight(flightNumber, origin, destination, expectedTime, status);
                flightDict[flightNumber] = newFlight;
            }
            else if (specialReqCode == "CFFT")
            {
                Flight newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime, status);
                flightDict[flightNumber] = newFlight;
            }
            else if (specialReqCode == "DDJB")
            {
                Flight newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, status);
                flightDict[flightNumber] = newFlight;
            }
            else if (specialReqCode == "LWTT")
            {
                Flight newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime, status);
                flightDict[flightNumber] = newFlight;
            }

            Console.WriteLine($"Flight {flightNumber} has been added!");
            Console.WriteLine("Would you like to add another flight? (Y/N) ");
            string? choice = Console.ReadLine();
            if (choice == "N")
            {
                break;
            }
            else if (choice == "Y")
            {
                continue;
            }
            else
            {
                Console.WriteLine("Invalid input. Pls try again.");
                break;
            }
        }

    }
    else if (option == "5")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayAirlines();
    }
    else if (option == "6")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayAirlines();
    }
    else if (option == "7")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");

        var sortedFlights = flightDict
            .OrderBy(f => f.Value.ExpectedTime).ToList();
        Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-25} {5,-15}",
            "FlightNumber", "Airline Name", "Origin", "Destination", "ExpectedTime", "Status");
        foreach (var flight in sortedFlights)
        {
            Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-25} {5,-15}",
            flight.Value.FlightNumber, 0, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime, flight.Value.Status);
        }
       

    }


    else
    {
        Console.WriteLine("Pls try again.");
    }
}
