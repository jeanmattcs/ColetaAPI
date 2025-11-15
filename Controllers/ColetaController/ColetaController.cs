using ColetaAPI.DTOs;
using ColetaAPI.Models;
using ColetaAPI.Service.ColetaService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColetaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionInterface _collectionInterface;
        public CollectionController(ICollectionInterface collectionInterface)
        {
            _collectionInterface = collectionInterface;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<CollectionResponseDto>>>> GetCollection()
        {
            return Ok(await _collectionInterface.GetCollection());
        }
        [HttpGet("GetSingle/{id}")]
        public async Task<ActionResult<ServiceResponse<CollectionResponseDto>>> GetSingleCollection(int id)
        {
            return Ok(await _collectionInterface.GetSingleCollection(id));
        }
        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<CollectionResponseDto>>>> AddCollection(CreateCollectionDto dto)
        {
            var collection = new CollectionModel
            {
                LocationId = dto.LocationId,
                OrderDate = dto.OrderDate,
                Collected = dto.Collected
            };
            return Ok(await _collectionInterface.AddCollection(collection));
        }
        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<CollectionResponseDto>>> UpdateCollection(UpdateCollectionDto dto)
        {
            var collection = new CollectionModel
            {
                ID = dto.Id,
                LocationId = dto.LocationId,
                OrderDate = dto.OrderDate,
                Collected = dto.Collected
            };
            return Ok(await _collectionInterface.UpdateCollection(collection));
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<CollectionResponseDto>>>> DeleteCollection(int id)
        {
            return Ok(await _collectionInterface.DeleteCollection(id));
        }
    }
}

