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
        void InicioExpediente(Guid usuarioId);
        void Pausa(Guid usuarioId);
        void VoltaDaPausa(Guid usuarioId);
        void FimDoExpediente(Guid usuarioId);
        List<Usuario> ConsultarUsuarios();
        Usuario? ConsultarPorId(Guid id);
    }
}
