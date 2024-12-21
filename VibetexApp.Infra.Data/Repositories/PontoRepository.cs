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

        public Ponto? GetById(Guid pontoId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Ponto>()
                    .Where(p => p.Id == pontoId)
                    .FirstOrDefault();
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

                // Filtra pela data mínima, se fornecida
                if (dataMin != DateTime.MinValue)
                {
                    query = query.Where(p => p.InicioExpediente >= dataMin);
                }

                // Filtra pela data máxima, se fornecida
                if (dataMax != DateTime.MinValue)
                {
                    query = query.Where(p => p.InicioExpediente <= dataMax);
                }

                // Filtra pelo ID do ponto, se fornecido
                if (pontoId != Guid.Empty)
                {
                    query = query.Where(p => p.Id == pontoId);
                }

                // Ordena os pontos pela data de início do expediente (crescente)
                query = query.OrderBy(p => p.FimExpediente);

                // Retorna a lista ordenada de pontos
                return query.ToList();
            }
        }


        public void Update(Ponto ponto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(ponto);
                dataContext.SaveChanges();
            }
        }
    }
}
