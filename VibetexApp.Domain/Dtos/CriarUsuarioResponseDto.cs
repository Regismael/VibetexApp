using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibetexApp.Domain.Enum;

namespace VibetexApp.Domain.Dtos
{
    public class CriarUsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public TipoPerfil TipoPerfil { get; set; }

    }
}
