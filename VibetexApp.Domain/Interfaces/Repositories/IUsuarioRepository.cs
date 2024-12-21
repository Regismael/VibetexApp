using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibetexApp.Domain.Entities;

namespace VibetexApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        Usuario? GetById(Guid id);
        ICollection<Usuario> GetAll();
    }
}
