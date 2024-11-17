using Guestline.ConsoleApp;
using Guestline.ConsoleApp.BusinessLogic;
using Guestline.ConsoleApp.ConsoleParameters;
using Guestline.ConsoleApp.DataImporter;
using Guestline.ConsoleApp.UserInputParser;


var parametersParser = new ConsoleParametersParser();
ConsoleAppParameters parameters;

try
{
    parameters = parametersParser.Parse(args);
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to parse arguments. Message = {ex.Message}\nApplication will terminate");
    return;
}

var dataLoader = new DataLoader();
HotelDataSet hotelDataSet;

try
{
    hotelDataSet = dataLoader.LoadData(args[1], args[3]);
}
catch(Exception ex)
{
    Console.WriteLine($"Failed to load data. Message = {ex.Message}\nApplication will terminate");
    return;
}

Console.WriteLine($"Files loaded successfully");


var hotelService = new HotelService(hotelDataSet);
var inputParser = new InputParser();

Console.WriteLine($"Service initialized successfully");
Console.WriteLine($"Instructions");
Console.WriteLine($"------------------------------------");
Console.WriteLine($"1. Type in command in the following format \n'Availability(hotelId , yyyymmdd, roomType)' or\n'Availability(hotelId , yyyymmdd-yyyymmdd, roomType)' \nto check room availability");
Console.WriteLine($"------------------------------------");
Console.WriteLine($"2. Enter whitespace to exit the app");
Console.WriteLine($"------------------------------------");


while (true)
{
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line))
    {
        break;
    }

    try
    {
        var inputData = inputParser.Parse(line);
        var roomAvailabilityCount = hotelService.GetRoomAvailabilityCount(inputData.HotelId, inputData.RoomType, inputData.ArriveDate, inputData.DepartureDate);
        Console.WriteLine($"Number of available rooms: {roomAvailabilityCount}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        continue;
    }
}

Console.WriteLine("Application finished gracefully");