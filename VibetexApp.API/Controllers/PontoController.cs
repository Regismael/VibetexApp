using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;
using VibetexApp.Domain.Interfaces.Services;

namespace VibetexApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoController : ControllerBase
    {
        private readonly IPontoService _pontoService;

        // Injeção de dependência do serviço de ponto
        public PontoController(IPontoService pontoService)
        {
            _pontoService = pontoService;
        }

        // Consultar todos os pontos
        [HttpGet]
        [ProducesResponseType(typeof(List<Ponto>), 200)]
        public IActionResult ConsultarPontos()
        {
            try
            {
                var pontos = _pontoService.ConsultarPontos();
                return Ok(pontos);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("por-data")]
        [ProducesResponseType(typeof(List<Ponto>), 200)]
        public IActionResult ConsultarPontosPorData([FromQuery] DateTime dataMin, [FromQuery] DateTime dataMax, [FromQuery] Guid pontoId)
        {
            try
            {
                var pontos = _pontoService.ConsultarPontosPorData(dataMin, dataMax, pontoId);
                return Ok(pontos);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
