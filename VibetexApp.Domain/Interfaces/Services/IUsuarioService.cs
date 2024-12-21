using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibetexApp.Domain.Dtos;
using VibetexApp.Domain.Entities;

namespace VibetexApp.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        (CriarUsuarioResponseDto, Guid) CriarUsuario(Usuario usuario, int tipoPerfil);
        Guid InicioExpediente(Guid usuarioId);
        void Pausa(Guid usuarioId, Guid pontoId);
        void VoltaDaPausa(Guid usuarioId, Guid pontoId);
        void FimDoExpediente(Guid usuarioId, Guid pontoId);
        List<ConsultarUsuarioResponseDto> ConsultarUsuarios();
        ConsultarUsuarioResponseDto? ConsultarPorId(Guid id);
    }
}
