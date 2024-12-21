using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Data.Contexts;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;
using VibetexApp.Domain.Interfaces.Repositories;

namespace VibetexApp.Infra.Data.Repositories
{
    public class PontoRepository : IPontoRepository
    {
        public void Add(Ponto ponto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(ponto);  
                dataContext.SaveChanges();  
            }
        }
        public ICollection<Ponto> GetByUsuarioId(Guid usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Ponto>()
                    .Where(p => p.UsuarioId == usuarioId)
                    .ToList();
            }
        }

        public ICollection<Ponto> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Ponto>()
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Ponto> Get(DateTime dataMin, DateTime dataMax, Guid pontoId)
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Set<Ponto>().AsQueryable();

                if (dataMin != DateTime.MinValue)
                {
                    query = query.Where(p => p.InicioExpediente >= dataMin);
                }

                if (dataMax != DateTime.MinValue)
                {
                    query = query.Where(p => p.InicioExpediente <= dataMax);
                }

                if (pontoId != Guid.Empty)
                {
                    query = query.Where(p => p.Id == pontoId);
                }

                return query.ToList();
            }
        }

    }
}
