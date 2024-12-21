using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Interfaces.Repositories;
using VibetexApp.Domain.Interfaces.Services;
using VibetexApp.Domain.Enum;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;
using VibetexApp.Domain.Dtos; // Para acessar o Enum TipoPerfil

namespace VibetexApp.Domain.Services
{
    public class PontoService : IPontoService
    {
        private readonly IPontoRepository _pontoRepository; // Dependência do repositório de pontos
        private readonly IUsuarioRepository _usuarioRepository; // Dependência do repositório de usuários

        // Construtor que recebe as dependências do repositório de pontos e de usuários
        public PontoService(IPontoRepository pontoRepository, IUsuarioRepository usuarioRepository)
        {
            _pontoRepository = pontoRepository;
            _usuarioRepository = usuarioRepository;
        }

        // Implementação do método ConsultarPontos
        public List<ConsultarPontoResponseDto> ConsultarPontos()
        {
            // Primeiro, obtemos todos os usuários com o tipo de perfil "Colaborador" (TipoPerfil = 2)
            var colaboradores = _usuarioRepository.GetAll()
                                                   .Where(u => u.TipoPerfil == TipoPerfil.Colaborador)
                                                   .ToList();

            // Agora, pegamos todos os pontos relacionados a esses colaboradores
            var pontosDto = new List<ConsultarPontoResponseDto>();

            foreach (var colaborador in colaboradores)
            {
                var pontosDoColaborador = _pontoRepository.GetByUsuarioId(colaborador.Id);

                // Mapeia os pontos para o DTO
                foreach (var ponto in pontosDoColaborador)
                {
                    var pontoDto = new ConsultarPontoResponseDto
                    {
                        Id = ponto.Id,
                        InicioExpediente = ponto.InicioExpediente,
                        FimExpediente = ponto.FimExpediente,
                        InicioPausa = ponto.InicioPausa,
                        RetornoPausa = ponto.RetornoPausa,
                        HorasTrabalhadas = ponto.HorasTrabalhadas,
                        HorasExtras = ponto.HorasExtras, // Defina a lógica para horas extras se necessário
                        HorasDevidas = ponto.HorasDevidas, // Defina a lógica para horas devidas se necessário
                        Observacoes = ponto.Observacoes,
                        Latitude = ponto.Latitude,
                        Longitude = ponto.Longitude,
                        UsuarioId = ponto.UsuarioId
                    };

                    // Adiciona o DTO mapeado à lista
                    pontosDto.Add(pontoDto);
                }
            }

            // Retorna a lista de DTOs mapeados
            return pontosDto;
        }

        // Método de consulta filtrando por intervalo de datas e pontoId
        public List<ConsultarPontoResponseDto> ConsultarPontosPorData(DateTime dataMin, DateTime dataMax)
        {
            // Obtém todos os pontos
            var pontos = _pontoRepository.GetAll();

            // Filtra os pontos pelo intervalo de datas e pelo pontoId
            var pontosFiltrados = pontos.Where(p => p.InicioExpediente >= dataMin && p.FimExpediente <= dataMax)
                                        .ToList();

            // Mapeia os pontos filtrados para DTOs
            var pontosDto = pontosFiltrados.Select(p => new ConsultarPontoResponseDto
            {
                Id = p.Id,
                InicioExpediente = p.InicioExpediente,
                FimExpediente = p.FimExpediente,
                InicioPausa = p.InicioPausa,
                RetornoPausa = p.RetornoPausa,
                // Você pode adicionar aqui a lógica para calcular as horas trabalhadas, extras e devidas.
                HorasTrabalhadas = p.HorasTrabalhadas,
                HorasExtras = p.HorasExtras,
                HorasDevidas = p.HorasDevidas,
                Observacoes = p.Observacoes,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                UsuarioId = p.UsuarioId
            }).ToList();

            return pontosDto;
        }

    }
}
