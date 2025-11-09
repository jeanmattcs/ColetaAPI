using ColetaAPI.DTOs;
using ColetaAPI.Models;
using ColetaAPI.Service.LocalizacaoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColetaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly ILocalizacaoInterface _localizacaoInterface;

        public LocalizacaoController(ILocalizacaoInterface localizacaoInterface)
        {
            _localizacaoInterface = localizacaoInterface;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<LocalizacaoResponseDto>>>> GetLocalizacoes()
        {
            return Ok(await _localizacaoInterface.GetLocalizacoes());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServiceResponse<LocalizacaoResponseDto>>> GetLocalizacaoById(int id)
        {
            return Ok(await _localizacaoInterface.GetLocalizacaoById(id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<LocalizacaoResponseDto>>>> AddLocalizacao(CreateLocalizacaoDto dto)
        {
            var localizacao = new LocalizacaoModel
            {
                Descricao = dto.Descricao
            };
            return Ok(await _localizacaoInterface.AddLocalizacao(localizacao));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<LocalizacaoResponseDto>>> UpdateLocalizacao(UpdateLocalizacaoDto dto)
        {
            var localizacao = new LocalizacaoModel
            {
                ID = dto.Id,
                Descricao = dto.Descricao
            };
            return Ok(await _localizacaoInterface.UpdateLocalizacao(localizacao));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<LocalizacaoResponseDto>>>> DeleteLocalizacao(int id)
        {
            return Ok(await _localizacaoInterface.DeleteLocalizacao(id));
        }
    }
}


