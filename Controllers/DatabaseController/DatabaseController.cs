using ColetaAPI.DTOs;
using ColetaAPI.Models;
using ColetaAPI.Service.DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace ColetaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseInterface _databaseInterface;

        public DatabaseController(IDatabaseInterface databaseInterface)
        {
            _databaseInterface = databaseInterface;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<DatabaseDto>>> GetAllData()
        {
            return Ok(await _databaseInterface.GetAllData());
        }
    }
}


