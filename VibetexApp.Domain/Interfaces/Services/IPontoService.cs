using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;

namespace VibetexApp.Domain.Interfaces.Services
{
    public interface IPontoService
    {
        List<Ponto> ConsultarPontos();
        List<Ponto> ConsultarPontosPorData(DateTime dataMin, DateTime dataMax, Guid pontoId);
    }

}
