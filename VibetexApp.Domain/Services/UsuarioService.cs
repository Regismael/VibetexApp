using System;
using System.Collections.Generic;
using VibetexApp.Domain.Dtos;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;
using VibetexApp.Domain.Enum;
using VibetexApp.Domain.Interfaces.Repositories;
using VibetexApp.Domain.Interfaces.Services;

namespace VibetexApp.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        private IPontoRepository _pontoRepository;  

        public UsuarioService(IUsuarioRepository usuarioRepository, IPontoRepository pontoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _pontoRepository = pontoRepository;
        }

        public (CriarUsuarioResponseDto, Guid) CriarUsuario(Usuario usuario, int tipoPerfil)
        {
            // Gera um novo Guid para o usuário
            usuario.Id = Guid.NewGuid();

            usuario.TipoPerfil = (TipoPerfil)tipoPerfil;

            // Garantir que o TipoPerfil não seja nulo
            if (usuario.TipoPerfil == null)
            {
                throw new ArgumentException("O TipoPerfil não pode ser nulo.");
            }

            // Criação do DTO de resposta
            var response = new CriarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoPerfil = (TipoPerfil)tipoPerfil
            };

            // Adiciona o usuário no repositório
            _usuarioRepository.Add(usuario);

            // Criação do ponto associado ao usuário
            var ponto = new Ponto
            {
                Id = Guid.NewGuid(), // Novo Guid para o ponto
                UsuarioId = usuario.Id,  // Usa o Id gerado do usuário
                HorasTrabalhadas = TimeSpan.FromHours(8),
                HorasExtras = TimeSpan.FromHours(2),
                HorasDevidas = TimeSpan.FromHours(1),
                Observacoes = "Nenhuma observação",
                Latitude = "-22.861564",
                Longitude = "-43.23105"
            };

            // Adiciona o ponto no repositório
            _pontoRepository.Add(ponto);

            // Retorna a tupla com o DTO e o Id
            return (response, usuario.Id);
        }


        public Guid InicioExpediente(Guid usuarioId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var ponto = new Ponto
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuario.Id,
                    InicioExpediente = DateTime.Now,
                    HorasTrabalhadas = TimeSpan.FromHours(8),
                    HorasExtras = TimeSpan.FromHours(2),
                    HorasDevidas = TimeSpan.FromHours(1),
                    Observacoes = "Nenhuma observação",
                    Latitude = "-22.861564",
                    Longitude = "-43.23105"
                };
                _pontoRepository.Add(ponto);

                return ponto.Id;
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public void FimDoExpediente(Guid usuarioId, Guid pontoId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var ponto = _pontoRepository.GetById(pontoId);

                ponto.UsuarioId = usuario.Id;
                ponto.FimExpediente = DateTime.Now;
               
                _pontoRepository.Update(ponto);
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public void Pausa(Guid usuarioId, Guid pontoId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var ponto = _pontoRepository.GetById(pontoId);

                ponto.UsuarioId = usuario.Id;
                ponto.InicioPausa = DateTime.Now;

                _pontoRepository.Update(ponto);
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public void VoltaDaPausa(Guid usuarioId, Guid pontoId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var ponto = _pontoRepository.GetById(pontoId);

                ponto.UsuarioId = usuario.Id;
                ponto.RetornoPausa = DateTime.Now;

                _pontoRepository.Update(ponto);
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }



        public List<ConsultarUsuarioResponseDto> ConsultarUsuarios()
        {
            // Busca todos os usuários no repositório
            var usuarios = _usuarioRepository.GetAll().ToList();

            // Transforma os usuários na lista de DTOs
            var usuariosDto = usuarios.Select(u => new ConsultarUsuarioResponseDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                TipoPerfil = u.TipoPerfil.ToString(),
                Pontos = _pontoRepository.GetAll()
                            .Where(p => p.UsuarioId == u.Id)
                            .Select(p => new ConsultarPontoResponseDto
                            {
                                Id = p.Id,
                                InicioExpediente = p.InicioExpediente,
                                FimExpediente = p.FimExpediente,
                                InicioPausa = p.InicioPausa,
                                RetornoPausa = p.RetornoPausa,
                                HorasTrabalhadas = p.HorasTrabalhadas,
                                HorasExtras = p.HorasExtras,
                                HorasDevidas = p.HorasDevidas,
                                Observacoes = p.Observacoes,
                                Latitude = p.Latitude,
                                Longitude = p.Longitude
                            })
                            .ToList()
            }).ToList();

            return usuariosDto;
        }


        public ConsultarUsuarioResponseDto? ConsultarPorId(Guid id)
        {
            // Busca o usuário no repositório
            var usuario = _usuarioRepository.GetById(id);

            // Verifica se o usuário existe
            if (usuario == null)
            {
                return null;
            }

            // Mapeia o usuário para o DTO
            var usuarioDto = new ConsultarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoPerfil = usuario.TipoPerfil.ToString(),
                Pontos = _pontoRepository.GetAll()
                            .Where(p => p.UsuarioId == usuario.Id)
                            .Select(p => new ConsultarPontoResponseDto
                            {
                                Id = p.Id,
                                InicioExpediente = p.InicioExpediente,
                                FimExpediente = p.FimExpediente,
                                InicioPausa = p.InicioPausa,
                                RetornoPausa = p.RetornoPausa,
                                HorasTrabalhadas = p.HorasTrabalhadas,
                                HorasExtras = p.HorasExtras,
                                HorasDevidas = p.HorasDevidas,
                                Observacoes = p.Observacoes,
                                Latitude = p.Latitude,
                                Longitude = p.Longitude
                            })
                            .ToList()
            };

            return usuarioDto;
        }

  
    }
}
