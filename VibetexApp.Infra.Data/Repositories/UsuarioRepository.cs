using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Data.Contexts;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Interfaces.Repositories;

namespace VibetexApp.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Add(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(usuario);
                dataContext.SaveChanges();
            }
        }

        public Usuario? GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Usuario>()
                    .Include(u => u.Pontos) 
                    .FirstOrDefault(u => u.Id == id);
            }
        }

        public ICollection<Usuario> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Usuario>()                  
                    .Include(u => u.Pontos) 
                    .ToList();
            }
        }
    }
}
