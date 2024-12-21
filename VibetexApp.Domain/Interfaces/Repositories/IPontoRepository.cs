using System;
using System.Collections.Generic;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;

namespace VibetexApp.Domain.Interfaces.Repositories
{
    public interface IPontoRepository
    {
        void Add(Ponto ponto);

        void Update(Ponto ponto);
        List<Ponto> Get(DateTime dataMin, DateTime dataMax, Guid pontoId);
        ICollection<Ponto> GetByUsuarioId(Guid usuarioId);  
        ICollection<Ponto> GetAll();
        Ponto? GetById(Guid id);
    }
}
