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


        public void InicioExpediente(Guid usuarioId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var pontoAtivo = _pontoRepository.GetAll()
                    .FirstOrDefault(p => p.UsuarioId == usuarioId && p.FimExpediente == null);

                if (pontoAtivo == null)
                {
                    var ponto = new Ponto
                    {
                        Id = Guid.NewGuid(),
                        UsuarioId = usuario.Id,
                        // Deixamos InicioExpediente como null, até que o expediente realmente comece
                        Usuario = usuario
                    };
                    _pontoRepository.Add(ponto);
                }
                else
                {
                    throw new Exception("Já existe um ponto de expediente em andamento.");
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public void FimDoExpediente(Guid usuarioId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var ponto = _pontoRepository.GetAll()
                    .Where(p => p.UsuarioId == usuarioId && p.FimExpediente == null)
                    .OrderByDescending(p => p.InicioExpediente)
                    .FirstOrDefault();

                if (ponto != null)
                {
                    ponto.FimExpediente = DateTime.Now;  // Aqui, a data será atribuída
                    _pontoRepository.Add(ponto);
                }
                else
                {
                    throw new Exception("Ponto de expediente não encontrado.");
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public void Pausa(Guid usuarioId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var pontoEmPausa = _pontoRepository.GetAll()
                    .FirstOrDefault(p => p.UsuarioId == usuarioId && p.RetornoPausa == null);

                if (pontoEmPausa == null)
                {
                    var ponto = new Ponto
                    {
                        Id = Guid.NewGuid(),
                        UsuarioId = usuario.Id,
                        InicioPausa = DateTime.Now,  // A data será atribuída aqui
                        Usuario = usuario
                    };
                    _pontoRepository.Add(ponto);
                }
                else
                {
                    throw new Exception("O usuário já está em pausa.");
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public void VoltaDaPausa(Guid usuarioId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario != null)
            {
                var ponto = _pontoRepository.GetAll()
                    .FirstOrDefault(p => p.UsuarioId == usuarioId && p.RetornoPausa == null);

                if (ponto != null)
                {
                    ponto.RetornoPausa = DateTime.Now;  // A data será atribuída aqui
                    _pontoRepository.Add(ponto);
                }
                else
                {
                    throw new Exception("Ponto de pausa não encontrado.");
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }


        public List<Usuario> ConsultarUsuarios()
        {
            return _usuarioRepository.GetAll().ToList();
        }

        public Usuario? ConsultarPorId(Guid id)
        {
            return _usuarioRepository.GetById(id);
        }

    }
}
