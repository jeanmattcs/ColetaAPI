using ColetaAPI.DTOs;
using ColetaAPI.Models;
using ColetaAPI.Service.LocalizacaoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColetaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationInterface _locationInterface;

        public LocationController(ILocationInterface locationInterface)
        {
            _locationInterface = locationInterface;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<LocationResponseDto>>>> GetLocations()
        {
            return Ok(await _locationInterface.GetLocations());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServiceResponse<LocationResponseDto>>> GetLocationById(int id)
        {
            return Ok(await _locationInterface.GetLocationById(id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<LocationResponseDto>>>> AddLocation(CreateLocationDto dto)
        {
            var location = new LocationModel
            {
                Description = dto.Description
            };
            return Ok(await _locationInterface.AddLocation(location));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<LocationResponseDto>>> UpdateLocation(UpdateLocationDto dto)
        {
            var location = new LocationModel
            {
                ID = dto.Id,
                Description = dto.Description
            };
            return Ok(await _locationInterface.UpdateLocation(location));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<LocationResponseDto>>>> DeleteLocation(int id)
        {
            return Ok(await _locationInterface.DeleteLocation(id));
        }
    }
}


