using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Lagash
{
    class ControladorParquimetroConcreto : IControladorParquimetro
    {
        public ControladorParquimetroConcreto(ServicioExterno servicioExterno)
        {
            this.servicioExterno = servicioExterno; // Interactuo con sistema externo a traves de una interfaz, para evitar dependencias directas con sistemas externos.
        }

        ServicioExterno servicioExterno;
        string direccion;
        string patenteActual;
        int minutosEstacionado;
        int centavosPorHora;

        public int CentavosPorHora
        {
            get
            {
                return this.centavosPorHora;
            }

            set
            {
                this.centavosPorHora = value;
            }
        }

        public int MinutosEstacionado
        {
            get
            {
                return this.minutosEstacionado;
            }
        }

        public string Patente
        {
            get
            {
                return this.patenteActual;
            }
        }

        public void AutoDetectado(string patente)
        {
            this.patenteActual = patente;
            this.minutosEstacionado = 0;
        }

        public void AvanzarMinuto()
        {
            this.minutosEstacionado ++;
        }

        public void EstacionamientoFinalizado()
        {
            double pesosAPagar = CalcularPesosAPagar();

            string cuerpoEmail = "Usted debe abonar la suma de $" + pesosAPagar.ToString() + " por el tiempo que permaneció estacionado en " + this.direccion + ".";
            string email = servicioExterno.ObtenerEmailPorPatente(this.patenteActual);
            servicioExterno.EnviarEmail("Notificación pago de estacionamiento", cuerpoEmail, email);

            // Estacionamiento vacio
            this.patenteActual = null;
            this.minutosEstacionado = 0;
        }

        private double CalcularPesosAPagar()
        {
            double horas = this.minutosEstacionado / 60;
            double horasTotal = Math.Ceiling((horas)); // Redondeo para arriba
            double pesosAPagar = (CentavosPorHora * horasTotal) / 100;
            return pesosAPagar;
        }
    }
}
