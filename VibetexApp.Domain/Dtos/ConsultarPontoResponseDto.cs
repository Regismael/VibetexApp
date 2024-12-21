using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibetexApp.Domain.Dtos
{
    public class ConsultarPontoResponseDto
    {
        public Guid Id { get; set; }
        public DateTime? InicioExpediente { get; set; }
        public DateTime? FimExpediente { get; set; }
        public DateTime? InicioPausa { get; set; }
        public DateTime? RetornoPausa { get; set; }
        public TimeSpan HorasTrabalhadas { get; set; }
        public TimeSpan HorasExtras { get; set; }
        public TimeSpan HorasDevidas { get; set; }
        public string? Observacoes { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public Guid UsuarioId { get; set; } // R
    }
}
