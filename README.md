# Garage Management System

An object-oriented Garage Management system written in **C#**, demonstrating
inheritance, polymorphism, custom exceptions and a clean separation between
business logic and presentation.

## Projects

| Project | Description |
|---------|-------------|
| **Ex03.GarageLogic** | Core business logic — vehicles, energy sources, wheels, the garage facade and validation. No UI dependencies. |
| **Ex03.ConsoleUI** | Console-based user interface driving the garage. |
| **Ex03.GarageWebApi** | ASP.NET Core (.NET 8) REST API + Swagger over the same business logic. See [its README](Ex03.GarageWebApi/README.md). |

## Domain

Supported vehicles: fuel & electric cars, fuel & electric motorcycles, and
trucks. Each vehicle has wheels (with air pressure), an energy source
(fuel engine or battery) and type-specific properties. The garage tracks every
vehicle's owner and service status (in process / ready / paid) and supports
fueling, charging, inflating wheels and querying by status.

## Running

- **Console:** open `Ex03_Garage_Managment.sln` in Visual Studio and run
  `Ex03.ConsoleUI`.
- **Web API:** `cd Ex03.GarageWebApi && dotnet run`, then browse to
  `http://localhost:5241/swagger`.
