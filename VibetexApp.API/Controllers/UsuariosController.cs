using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using VibetexApp.Domain.Dtos;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace VibetexApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Criar novo usuário
        [HttpPost("criar")]
        [ProducesResponseType(typeof(CriarUsuarioResponseDto), 201)]
        public IActionResult Criar([FromBody] CriarUsuarioRequestDto dto)
        {
            try
            {
                // Converte o DTO para a entidade Usuario
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha
                };

                // Chama o serviço para criar o usuário, passando o tipo de perfil
                _usuarioService.CriarUsuario(usuario, (int)dto.TipoPerfil);

                // Retorna o status 201 com a resposta do serviço
                return StatusCode(201, usuario);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        // Consultar todos os usuários
        [HttpGet]
        [ProducesResponseType(typeof(List<Usuario>), 200)]
        public IActionResult ConsultarUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.ConsultarUsuarios();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        // Consultar usuário por ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(404)]
        public IActionResult ConsultarPorId(Guid id)
        {
            try
            {
                var usuario = _usuarioService.ConsultarPorId(id);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        // Iniciar expediente
        [HttpPost("{usuarioId}/inicio-expediente")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult InicioExpediente(Guid usuarioId)
        {
            try
            {
                _usuarioService.InicioExpediente(usuarioId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        // Finalizar expediente
        [HttpPost("{usuarioId}/fim-expediente")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult FimDoExpediente(Guid usuarioId)
        {
            try
            {
                _usuarioService.FimDoExpediente(usuarioId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        // Registrar pausa
        [HttpPost("{usuarioId}/pausa")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Pausa(Guid usuarioId)
        {
            try
            {
                _usuarioService.Pausa(usuarioId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        // Retornar da pausa
        [HttpPost("{usuarioId}/volta-da-pausa")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult VoltaDaPausa(Guid usuarioId)
        {
            try
            {
                _usuarioService.VoltaDaPausa(usuarioId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
