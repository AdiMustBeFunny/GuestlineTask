# What is it?

This is a project for job interview.
It consists of console application that enables user to check availability count of certain room type in a hotel

# How to run it?

When you open the folder in a console run

```
dotnet run --project ./Guestline.ConsoleApp/Guestline.ConsoleApp.csproj -- --hotels ./Guestline.ConsoleApp/SampleData/hotels.json --bookings ./Guestline.ConsoleApp/SampleData/bookings.json
```

Options **--hotels** and **--bookings** are mandatory and should point to .json files with data.

# Tests

Run the following command in the console to run tests

```
dotnet test
```