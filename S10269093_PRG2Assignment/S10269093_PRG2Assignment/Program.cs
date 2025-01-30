using S10269093_PRG2Assignment;
using System.Runtime.CompilerServices;

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


            // Add to the flight dictionary
            if (fcode == "CFFT")
            {
                Flight flight = new CFFTFlight(fNum, fOrigin, fDest, eDepart_Arrival);
                flightDict[fNum] = flight;
            }
            else if (fcode == "DDJB")
            {
                Flight flight = new DDJBFlight(fNum, fOrigin, fDest, eDepart_Arrival);
                flightDict[fNum] = flight;
            }
            else if (fcode == "LWTT")
            {
                Flight flight = new LWTTFlight(fNum, fOrigin, fDest, eDepart_Arrival);
                flightDict[fNum] = flight;
            }
            else
            {
                Flight flight = new NORMFlight(fNum, fOrigin, fDest, eDepart_Arrival);
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

                // Add to the boardingGate dictionary
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
        bool gateFound = false;
        Flight? selectedFlight = null;
        BoardingGate? selectedGate = null;
        try
        {


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

            foreach (KeyValuePair<string, BoardingGate> bg in boardingGateDict)
            {
                if (gateName == bg.Value.GateName)
                {
                    selectedGate = bg.Value;
                    Console.WriteLine(bg.Value.ToString());
                    gateFound = true;
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
            if (flightFound && gateFound)
            {
                selectedGate.Flight = selectedFlight;
                Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {gateName}!");
            }

            else
            {
                if (!flightFound)
                {
                    Console.WriteLine("Unable to find flight information.");
                }
                if (!gateFound)
                {
                    Console.WriteLine("Unable to find boarding gate information.");
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }




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

            if (specialReqCode == "None")
            {
                Flight newFlight = new NORMFlight(flightNumber!, origin!, destination!, expectedTime);
                flightDict[flightNumber!] = newFlight;
            }
            else if (specialReqCode == "CFFT")
            {
                Flight newFlight = new CFFTFlight(flightNumber!, origin!, destination!, expectedTime);
                flightDict[flightNumber!] = newFlight;
            }
            else if (specialReqCode == "DDJB")
            {
                Flight newFlight = new DDJBFlight(flightNumber!, origin!, destination!, expectedTime);
                flightDict[flightNumber!] = newFlight;
            }
            else if (specialReqCode == "LWTT")
            {
                Flight newFlight = new LWTTFlight(flightNumber!, origin!, destination!, expectedTime);
                flightDict[flightNumber!] = newFlight;
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

        // Converts the sorted result back into a dictionary
        var sortedFlights = flightDict.OrderBy(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
        Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-25} {5,-11} {6,-15}",
            "FlightNumber", "Airline Name", "Origin", "Destination", "ExpectedTime", "Status", "Boarding Gate");
        foreach (var f in sortedFlights)
        {
            string airlineCode = f.Value.FlightNumber.Split(' ')[0];
            if (airlineDict.ContainsKey(airlineCode))
            {
                Airline a = airlineDict[airlineCode];
                BoardingGate? assignedGate = null;
                foreach (var bg in boardingGateDict)
                {
                    if (bg.Value.Flight != null && bg.Value.Flight.FlightNumber == f.Value.FlightNumber)
                    {
                        assignedGate = bg.Value;  // Store the assigned gate
                        break;
                    }
                }

                // The ?. operator checks if assignedGate is not null. If assignedGate is not null, it accesses the GateName property.
                // The ?? operator checks if the left-hand side expression (assignedGate?.GateName) is null. If it is, it assigns the value on the right-hand side ("Unassigned").
                string gateName = assignedGate?.GateName ?? "Unassigned";
                Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-25} {5,-11} {6,-15}",
                        f.Value.FlightNumber, a.Name, f.Value.Origin, f.Value.Destination, f.Value.ExpectedTime, f.Value.Status, gateName);

            }
        }
    }

    else if (option == "9")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Display the total fee per airline for the day");
        Console.WriteLine("=============================================");

        bool allFlightsAssigned = true;

        // Check if all flights have boarding gates assigned
        foreach (KeyValuePair<string, Flight> f in flightDict)
        {
            BoardingGate? assignedGate = null;

            foreach (var bg in boardingGateDict)
            {
                if (bg.Value.Flight?.FlightNumber == f.Value.FlightNumber)
                {
                    assignedGate = bg.Value;  // Store the assigned gate
                    break;
                }
            }

            if (assignedGate == null)
            {
                Console.WriteLine($"Flight {f.Value.FlightNumber} does not have a boarding gate assigned.");
                allFlightsAssigned = false;
            }
        }

        // Display message depending on whether all flights have gates assigned
        if (allFlightsAssigned)
        {
            Console.WriteLine("All flights have boarding gates assigned.");
        }
        else
        {
            Console.WriteLine("Please ensure all flights have their boarding gates assigned before proceeding.");
        }

        Console.WriteLine();
        DisplayAirlines();
        Console.Write("Which airline do you want to calculate total fees (Input Airline Code): ");
        string? choice = Console.ReadLine();

        Terminal terminal = new Terminal("Terminal 5", airlineDict, flightDict, boardingGateDict);
        //terminal.PrintAirlineFees();

        double subtotalFees = 0;
        double totalDiscounts = 0;

        double fees = 0;
        int totalFlights = flightDict.Count;
        int flightsWithNoRequestCodes = 0;
        int flightsDuringPromoTimes = 0;
        int flightsFromPromoOrigins = 0;
        foreach (KeyValuePair<string, Flight> f in flightDict)
        {
            string airlineCode = f.Value.FlightNumber.Split(' ')[0];

            

            if (airlineCode == choice)
            {

                Airline sqAirline = airlineDict[airlineCode];
                double flightFee = f.Value.CalculateFees(); 
                //double discount = sqAirline.CalculateFees();

                Console.WriteLine(sqAirline);
                //double finalFee = flightFee - discount; 

                subtotalFees += flightFee;
                //totalDiscounts += discount;

                if ((f.Value is not CFFTFlight) && (f.Value is not DDJBFlight) && (f.Value is not LWTTFlight))
                {
                    flightsWithNoRequestCodes++;
                }

                // Check for flights during promotional times
                if (f.Value.ExpectedTime.Hour < 11 || f.Value.ExpectedTime.Hour > 21)
                {
                    flightsDuringPromoTimes++;
                }

                // Check for flights from specific origins
                if (f.Value.Origin == "Dubai (DXB)" || f.Value.Origin == "Bangkok (BKK)" || f.Value.Origin == "Tokyo (NRT)")
                {
                    flightsFromPromoOrigins++;
                }
                Console.WriteLine($"Flight: {f.Value.FlightNumber}, Fee: ${flightFee:F2}");
            }
        }
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
        Console.WriteLine(subtotalFees);
        //Console.WriteLine($"Subtotal Fees for Airline SQ: ${subtotalFees:F2}");

    }

    else
    {
        Console.WriteLine("Pls try again.");
    }
}
