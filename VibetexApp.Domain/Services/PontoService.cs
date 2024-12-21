using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Interfaces.Repositories;
using VibetexApp.Domain.Interfaces.Services;
using VibetexApp.Domain.Enum;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities; // Para acessar o Enum TipoPerfil

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
        public List<Ponto> ConsultarPontos()
        {
            // Primeiro, obtemos todos os usuários com o tipo de perfil "Colaborador" (TipoPerfil = 2)
            var colaboradores = _usuarioRepository.GetAll()
                                                   .Where(u => u.TipoPerfil == TipoPerfil.Colaborador)
                                                   .ToList();

            // Agora, pegamos todos os pontos relacionados a esses colaboradores
            var pontos = new List<Ponto>();

            foreach (var colaborador in colaboradores)
            {
                var pontosDoColaborador = _pontoRepository.GetByUsuarioId(colaborador.Id);
                pontos.AddRange(pontosDoColaborador); // Adiciona os pontos de cada colaborador
            }

            return pontos;
        }

        public List<Ponto> ConsultarPontosPorData(DateTime dataMin, DateTime dataMax, Guid pontoId)
        {
            // Obtém todos os pontos
            var pontos = _pontoRepository.GetAll();

            // Filtra os pontos pelo intervalo de datas e pelo pontoId
            var pontosFiltrados = pontos.Where(p => p.InicioExpediente >= dataMin && p.FimExpediente <= dataMax && p.Id == pontoId)
                                        .ToList();

            return pontosFiltrados;
        }
    }
}
