using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Lagash
{
    public interface IControladorParquimetro
    {
        string Patente
        {
            get;
        }

        int MinutosEstacionado
        {
            get;
        }

        int CentavosPorHora{
            get;
            set;
        }

        void AutoDetectado(string patente);

        void AvanzarMinuto();

        void EstacionamientoFinalizado();
    }
}
