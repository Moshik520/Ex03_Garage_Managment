using System.Collections.Generic;
using System.Linq;
using GarageLogic;
using GarageWebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GarageWebApi.Controllers
{
    [ApiController]
    [Route("api/garage")]
    public class GarageController : ControllerBase
    {
        private readonly Garage r_Garage;

        public GarageController(Garage i_Garage)
        {
            r_Garage = i_Garage;
        }

        [HttpGet("vehicle-types")]
        public ActionResult<IEnumerable<string>> GetVehicleTypes()
        {
            return Ok(VehicleCreator.SupportedTypes);
        }

        [HttpGet("vehicle-types/{type}/required-fields")]
        public ActionResult<Dictionary<string, string>> GetRequiredFields(string type)
        {
            Vehicle vehicle = VehicleCreator.CreateVehicle(type, "TEMP", "TEMP");
            if (vehicle == null)
            {
                return BadRequest(UnsupportedTypeMessage(type));
            }

            Dictionary<string, string> fields = new Dictionary<string, string>();
            new VehicleData(vehicle).GenerateVehicleDataForUser(fields);

            return Ok(fields);
        }

        [HttpGet("vehicles")]
        public ActionResult<IEnumerable<VehicleSummaryDto>> GetVehicles([FromQuery] VehicleData.eVehicleStatus? status)
        {
            IEnumerable<VehicleData> contacts = status.HasValue
                ? r_Garage.GetContactsByStatus(status.Value)
                : r_Garage.VehiclesContact;

            return Ok(contacts.Select(VehicleSummaryDto.FromVehicleData));
        }

        [HttpGet("vehicles/{license}")]
        public ActionResult<Dictionary<string, string>> GetVehicle(string license)
        {
            Dictionary<string, string> data = r_Garage.GetVehicleDataInDictionary(license);
            if (data == null)
            {
                return NotFound(NotFoundMessage(license));
            }

            return Ok(data);
        }

        [HttpPost("vehicles")]
        public IActionResult AddVehicle([FromBody] AddVehicleRequest request)
        {
            if (r_Garage.FindVehicleDataByLicense(request.LicenseId) != null)
            {
                return Conflict($"A vehicle with license '{request.LicenseId}' already exists.");
            }

            Vehicle vehicle = VehicleCreator.CreateVehicle(request.VehicleType, request.LicenseId, request.ModelName);
            if (vehicle == null)
            {
                return BadRequest(UnsupportedTypeMessage(request.VehicleType));
            }

            VehicleData vehicleData = new VehicleData(vehicle);
            Dictionary<string, string> errors =
                vehicleData.InitValues(request.Properties ?? new Dictionary<string, string>());

            if (errors.Count > 0)
            {
                return BadRequest(new { errors });
            }

            r_Garage.AddVehicleContact(vehicleData);

            return CreatedAtAction(
                nameof(GetVehicle),
                new { license = request.LicenseId },
                VehicleSummaryDto.FromVehicleData(vehicleData));
        }

        [HttpPut("vehicles/{license}/status")]
        public IActionResult ChangeStatus(string license, [FromBody] ChangeStatusRequest request)
        {
            bool found = r_Garage.ChangeVehicleStatus(request.Status, license);
            if (!found)
            {
                return NotFound(NotFoundMessage(license));
            }

            return NoContent();
        }

        [HttpPost("vehicles/{license}/inflate")]
        public IActionResult Inflate(string license)
        {
            bool found = r_Garage.InflateVehicle(license);
            if (!found)
            {
                return NotFound(NotFoundMessage(license));
            }

            return NoContent();
        }

        [HttpPost("vehicles/{license}/fuel")]
        public IActionResult Fuel(string license, [FromBody] FuelRequest request)
        {
            try
            {
                bool found = r_Garage.FuelVehicle(license, request.Liters, request.FuelType);
                if (!found)
                {
                    return NotFound(NotFoundMessage(license));
                }

                return NoContent();
            }
            catch (ValueRangeException ex)
            {
                return BadRequest($"{ex.Message} must be between {ex.MinValue} and {ex.MaxValue}.");
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("vehicles/{license}/charge")]
        public IActionResult Charge(string license, [FromBody] ChargeRequest request)
        {
            try
            {
                bool found = r_Garage.ChargeVehicle(license, request.Minutes);
                if (!found)
                {
                    return NotFound(NotFoundMessage(license));
                }

                return NoContent();
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string NotFoundMessage(string i_License)
        {
            return $"No vehicle found with license '{i_License}'.";
        }

        private static string UnsupportedTypeMessage(string i_Type)
        {
            return $"Unsupported vehicle type '{i_Type}'. Supported types: {string.Join(", ", VehicleCreator.SupportedTypes)}.";
        }
    }
}
