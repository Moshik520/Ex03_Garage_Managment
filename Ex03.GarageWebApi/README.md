# Garage Management Web API

An **ASP.NET Core Web API (.NET 8)** layer over the existing Garage Management
business logic. It exposes the garage operations (add vehicle, list, fuel,
charge, inflate, change status, view details) as a REST API documented with
**Swagger / OpenAPI**.

The business logic itself is **not duplicated**: this project compiles the same
source files from the existing `Ex03.GarageLogic` project (via linked
compilation in the `.csproj`), so there is a single source of truth and the
original projects are left untouched.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Running

```bash
cd Ex03.GarageWebApi
dotnet run
```

Then open Swagger UI in the browser:

```
http://localhost:5241/swagger
```

The garage state is held in memory (a singleton) for the lifetime of the
running server.

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| `GET`  | `/api/garage/vehicle-types` | List supported vehicle types |
| `GET`  | `/api/garage/vehicle-types/{type}/required-fields` | Fields needed to add that type |
| `GET`  | `/api/garage/vehicles?status=` | List vehicles, optionally filtered by status |
| `GET`  | `/api/garage/vehicles/{license}` | Full details of a vehicle |
| `POST` | `/api/garage/vehicles` | Add a vehicle |
| `PUT`  | `/api/garage/vehicles/{license}/status` | Change a vehicle's status |
| `POST` | `/api/garage/vehicles/{license}/inflate` | Inflate all wheels to maximum |
| `POST` | `/api/garage/vehicles/{license}/fuel` | Fuel a fuel-based vehicle |
| `POST` | `/api/garage/vehicles/{license}/charge` | Charge an electric vehicle |

### Adding a vehicle

`POST /api/garage/vehicles` takes the vehicle type, license and model, plus a
`properties` dictionary holding the type-specific values. Use
`GET /api/garage/vehicle-types/{type}/required-fields` first to discover which
keys a given type expects.

```json
{
  "vehicleType": "FuelCar",
  "licenseId": "111",
  "modelName": "Mazda3",
  "properties": {
    "VehicleOwnerName": "Moshik",
    "VehicleOwnerPhoneNumber": "0501234567",
    "TierModel": "Michelin",
    "CurrAirPressure": "30",
    "EnergyPercentage": "50",
    "CarColor": "1",
    "NumOfDoors": "4"
  }
}
```

Invalid values return `400` with a dictionary of field errors. Unknown licenses
return `404`, and adding a duplicate license returns `409`.

The `Ex03.GarageWebApi.http` file contains ready-to-run sample requests.
