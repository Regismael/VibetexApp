using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibetexApp.Domain.Dtos
{
    public class ConsultarUsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? TipoPerfil { get; set; } // Representação como string para facilitar leitura
        public List<ConsultarPontoResponseDto>? Pontos { get; set; } // Lista de pontos associados
    }
}

