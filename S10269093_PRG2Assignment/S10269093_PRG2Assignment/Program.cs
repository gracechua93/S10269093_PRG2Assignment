using S10269093_PRG2Assignment;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

//==========================================================
// Student Number: S10269093
// Student Name: Grace Chua
// Partner Name: Dalton Seah
//==========================================================

// Dictionaries
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
Terminal terminal = new Terminal("Terminal 5", airlineDict, flightDict, boardingGateDict);

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
        Console.WriteLine("9. Display Total Fees For Airline");
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
            return;
        }

       
        bool gateAssigned = false;
        BoardingGate? selectedGate = null;

        // The loop asks the user for a boarding gate name until a valid, unassigned gate is found.
        do
        {
            bool gateFound = false;
            foreach (KeyValuePair<string, BoardingGate> bg in boardingGateDict)
            {
                if (gateName == bg.Value.GateName)
                {
                    gateFound = true;

                    if (bg.Value.Flight != null) // Check if gate is already assigned
                    {
                        Console.WriteLine($"Boarding Gate {gateName} is already assigned to Flight {bg.Value.Flight.FlightNumber}.");
                        Console.WriteLine("Please enter a different gate.");
                        Console.Write("Enter Boarding Gate Name: ");
                        gateName = Console.ReadLine();
                    }
                    else
                    {
                        selectedGate = bg.Value;
                        Console.WriteLine(bg.Value.ToString());
                        gateAssigned = true;  // Valid gate found, exit loop
                        break;
                    }

                }
            }

            if (!gateFound)
            {
                Console.WriteLine("Unable to find boarding gate information.");
                break;
            }
            
        } while (!gateAssigned);

        if (gateAssigned)
        {
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
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                }
            }

            selectedGate.Flight = selectedFlight;
            Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {selectedGate.GateName}!");
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
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                Console.WriteLine("Invalid flight number. Please try again.");
                continue;
            }

            // Check if the first two characters are letters
            if (flightNumber.Length < 5 || !char.IsLetter(flightNumber[0]) || !char.IsLetter(flightNumber[1]))
            {
                Console.WriteLine("Flight number must start with two letters (e.g., 'SQ').");
                continue;
            }

            // Check if there is exactly one space after the two letters
            if (flightNumber[2] != ' ')
            {
                Console.WriteLine("Flight number must have a space after the two letters.");
                continue;
            }

            // Check if the remaining part (after the space) is 3 digits
            string numberPart = flightNumber.Substring(3).Trim(); // Remove the first two letters and space
            if (numberPart.Length < 3 || numberPart.Length > 3 || !int.TryParse(numberPart, out _))
            {
                Console.WriteLine("Flight number must be followed by 3 digits (e.g., '115' ).");
                continue;
            }

            Console.Write("Enter Origin: ");
            string? origin = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(origin))
            {
                Console.WriteLine("Invalid origin. Please try again.");
                continue;
            }

            Console.Write("Enter Destination: ");
            string? destination = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Invalid destination. Please try again.");
                continue;
            }

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
        DisplayAirlineFlights(terminal);
    }

    else if (option == "6")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayAirlines();
        ModifyFlightDetails(flightDict, airlineDict);
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

    // Advanced Feature 2
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
            continue;
        }

        Console.WriteLine();
        DisplayAirlines();
        Console.Write("Which airline do you want to calculate total fees (Input Airline Code): ");
        string? choice = Console.ReadLine();


        if (choice != null && airlineDict.ContainsKey(choice))
        {
            Dictionary<string, Flight> selectedFlights = new Dictionary<string, Flight>();

            foreach (KeyValuePair<string, Flight> f in flightDict)
            {
                string airlineCode = f.Value.FlightNumber.Split(' ')[0];

                if (airlineCode == choice)
                {
                    selectedFlights[f.Key] = f.Value;
                }
            }

            Airline selectedAirline = new Airline(airlineDict[choice].Name, airlineDict[choice].Code, selectedFlights);
            Console.WriteLine($"The total fees for the selected airline for today is ${selectedAirline.CalculateFees():F2}.");
        }
    }

    else
    {
        Console.WriteLine("Pls try again.");
    }
}

void DisplayAirlineFlights(Terminal t)
{
    Console.Write("Enter Airline Code: ");
    string? airlineCode = Console.ReadLine();
    if (!airlineDict.ContainsKey(airlineCode))
    {
        Console.WriteLine("Airline not found.");
        return;
    }
    Airline selectedAirline = t.Airlines[airlineCode];
    foreach (var flight in flightDict.Values)
    {
        string airCode = flight.FlightNumber.Substring(0, 2); // Extract the 2-letter airline code

        if (airlineDict.ContainsKey(airCode))
        {
            airlineDict[airCode].Flights[flight.FlightNumber] = flight; // Add the flight to the airline's Flights dictionary
        }
    }

    Console.WriteLine($"Flights operated by {selectedAirline.Name}:");
    foreach (Flight flight in selectedAirline.Flights.Values)
    {
        Console.WriteLine($"{flight.FlightNumber} - {flight.Origin} to {flight.Destination} at {flight.ExpectedTime}");
    }

    Console.Write("Enter Flight Number: ");
    string? flightNum = Console.ReadLine();
    
    if (!selectedAirline.Flights.ContainsKey(flightNum))
    {
        Console.WriteLine("Flight not found. Try again.");
        return;
    }
    Flight selectedFlight = selectedAirline.Flights[flightNum];

    Console.WriteLine(selectedFlight);
    // Find the first boarding gate assigned to the selected flight
    // FirstOrDefault returns the first element that matches the given condition.
    BoardingGate? assignedGate = terminal.BoardingGate.Values.FirstOrDefault(g => g.Flight != null && g.Flight.FlightNumber == selectedFlight.FlightNumber);
    if (assignedGate != null)
    {
        Console.WriteLine($"Boarding Gate: {assignedGate.GateName}");
    }
    else
    {
        Console.WriteLine("Boarding Gate: Unassigned");
    }
}

void ModifyFlightDetails(Dictionary<string, Flight> flightDict, Dictionary<string, Airline> airlineDict)
{
    Console.Write("Enter Airline Code: ");
    string? airlineCode = Console.ReadLine();
    if (!airlineDict.ContainsKey(airlineCode))
    {
        Console.WriteLine("Airline not found.");
        return;
    }
    Airline selectedAirline = airlineDict[airlineCode];
    foreach (var flight in flightDict.Values)
    {
        string airCode = flight.FlightNumber!.Substring(0, 2); // Extract the 2-letter airline code

        if (airlineDict.ContainsKey(airCode))
        {
            airlineDict[airCode].Flights[flight.FlightNumber] = flight; // Add the flight to the airline's Flights dictionary
        }
    }
    Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
            "FlightNumber", "Airline Name", "Origin", "Destination", "ExpectedTime");
    foreach (Flight f in selectedAirline.Flights.Values)
    {
        Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
            f.FlightNumber, selectedAirline.Name, f.Origin, f.Destination, f.ExpectedTime);
    }

    Console.Write("Choose an existing Flight to modify or delete: ");
    string? flightNum = Console.ReadLine();
    if (!flightDict.ContainsKey(flightNum))
    {
        Console.WriteLine("Flight not found.");
        return;
    }


    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ModifyFlight(selectedAirline.Flights[flightNum], flightDict);
            break;

        case "2":
            DeleteFlight(flightNum, selectedAirline, flightDict);
            break;

        default:
            Console.WriteLine("Invalid option selected.");
            break;
    }
}
void ModifyFlight(Flight flight, Dictionary<string, Flight> flightDict)
{

    Console.WriteLine("1. Modify Basic Information");
    Console.WriteLine("2. Modify Status");
    Console.WriteLine("3. Modify Special Request Code");
    Console.WriteLine("4. Modify Boarding Gate");
    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine()?.Trim();

    switch (choice)
    {
        case "1":
            ModifyBasicInfo(flight);
            break;
        case "2":
            ModifyStatus(flight);
            break;
        case "3":
            ModifySpecialRequest(flight);
            break;
        case "4":
            ModifyBoardingGate(flight);
            break;
        default:
            Console.WriteLine("Invalid option selected.");
            return;
    }
    // Update the flight in the main dictionary
    flightDict[flight.FlightNumber!] = flight;
    Console.WriteLine("Flight details updated successfully!");

}
void ModifyBasicInfo(Flight flight)
{
    try
    {
        Console.Write("Enter new Origin: ");
        string? origin = Console.ReadLine();
        flight.Origin = origin;
        Console.Write("Enter new Destination: ");
        string? destination = Console.ReadLine();
        flight.Destination = destination;
        Console.Write("Enter New Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        flight.ExpectedTime = DateTime.Parse(Console.ReadLine());

        Console.WriteLine(flight);
        BoardingGate? assignedGate = terminal.BoardingGate.Values.FirstOrDefault(g => g.Flight != null && g.Flight.FlightNumber == flight.FlightNumber);
        if (assignedGate != null)
        {
            Console.WriteLine($"Boarding Gate: {assignedGate.GateName}");
        }
        else
        {
            Console.WriteLine("Boarding Gate: Unassigned");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid date/time format. Please use dd/MM/yyyy HH:mm");
    }
}

void ModifyStatus(Flight f)
{
    Console.WriteLine("1. On Time");
    Console.WriteLine("2. Delayed");
    Console.WriteLine("3. Cancelled");
    Console.WriteLine("4. Boarding");
    Console.WriteLine("5. Departed");
    Console.Write("Choose new status: ");

    string? choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            f.Status = "On Time";
            break;
        case "2":
            f.Status = "Delayed";
            break;
        case "3":
            f.Status = "Cancelled";
            break;
        case "4":
            f.Status = "Boarding";
            break;
        case "5":
            f.Status = "Departed";
            break;
        default:
            Console.WriteLine("Invalid status selected.");
            return;
    }
}

void ModifySpecialRequest(Flight f)
{
    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
    string? specialReqCode = Console.ReadLine();

    Flight? updateFlight = null;
    if (specialReqCode == "None")
    {
        updateFlight = new NORMFlight(f.FlightNumber!, f.Origin!, f.Destination!, f.ExpectedTime);
        
    }
    else if (specialReqCode == "CFFT")
    {
        updateFlight = new CFFTFlight(f.FlightNumber!, f.Origin!, f.Destination!, f.ExpectedTime);
        
    }
    else if (specialReqCode == "DDJB")
    {
        updateFlight = new DDJBFlight(f.FlightNumber!, f.Origin!, f.Destination!, f.ExpectedTime);
        
    }
    else if (specialReqCode == "LWTT")
    {
        updateFlight = new LWTTFlight(f.FlightNumber!, f.Origin!, f.Destination!, f.ExpectedTime);
        
    }
    else
    {
        Console.WriteLine("Invalid Special Request Code.");
        return;
    }
    if (updateFlight != null)
    {
        flightDict[f.FlightNumber!] = updateFlight;
        Console.WriteLine(updateFlight);
    }

}

void ModifyBoardingGate(Flight f)
{
    // Retrieve the existing boarding gate for the given flight
    BoardingGate? assignedGate = boardingGateDict.Values.FirstOrDefault(g => g.Flight != null && g.Flight.FlightNumber == f.FlightNumber);
    if (assignedGate != null)
    {
        Console.WriteLine($"Current Boarding Gate: {assignedGate.GateName}");
    }
    else
    {
        Console.WriteLine("Boarding Gate: Unassigned");
    }

    Console.Write("Enter new Boarding Gate (press Enter to keep current value): ");
    string? newGate = Console.ReadLine()?.Trim();

    if (!string.IsNullOrEmpty(newGate))
    {
        // If a boarding gate is assigned, update it; otherwise, add a new one
        if (assignedGate != null)
        {
            // Modify the existing assigned gate (if applicable)
            assignedGate.GateName = newGate;
            Console.WriteLine($"Boarding Gate updated to: {assignedGate.GateName}");
        }
        else
        {
            // If no gate was assigned, create and assign a new BoardingGate
            BoardingGate newBoardingGate = new BoardingGate
            {
                GateName = newGate,
                Flight = f
            };
            boardingGateDict[f.FlightNumber!] = newBoardingGate;  // Add to the dictionary
            Console.WriteLine($"New Boarding Gate assigned: {newBoardingGate.GateName}");
        }
    }
    else
    {
        Console.WriteLine("No changes made to the Boarding Gate.");
    }
}


void DeleteFlight(string flightNum, Airline airline, Dictionary<string, Flight> flightDict)
{
    Flight flightToDelete = airline.Flights[flightNum];
    Console.Write($"Are you sure you want to delete Flight {flightNum}? [Y/N]: ");
    string? confirmDelete = Console.ReadLine().ToUpper();

    if (confirmDelete == "Y")
    {
        bool airlineRemoval = airline.RemoveFlight(flightToDelete);
        bool dictionaryRemoval = flightDict.Remove(flightNum);

        if (airlineRemoval && dictionaryRemoval)
        {
            Console.WriteLine("Flight deleted successfully.");
        }
        else
        {
            Console.WriteLine("Error occurred during deletion.");
            if (!airlineRemoval)
                Console.WriteLine("- Failed to remove from airline's flights.");
            if (!dictionaryRemoval)
                Console.WriteLine("- Failed to remove from main flight dictionary.");
        }
    }
    else
    {
        Console.WriteLine("Deletion canceled.");
    }
}

    