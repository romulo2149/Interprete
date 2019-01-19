using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interprete
{
    class Instruccion
    {
        private string instruccion;
        private int tiempo;
        private string nombre;
        private string cambio;
        private Carita cara;
        private bool estado;
        private string mensaje;
        public Instruccion(string instruccion, int tiempo, Carita cara, bool estado, string mensaje, string nombre, string cambio)
        {
            this.Instrucciones = instruccion;
            this.Tiempo = tiempo;
            this.Cara = cara;
            this.Estado = estado;
            this.Mensaje = mensaje;
            this.Nombre = nombre;
            this.Cambio = cambio;
        }

        public string Instrucciones { get => instruccion; set => instruccion = value; }
        public Carita Cara { get => cara; set => cara = value; }
        public int Tiempo { get => tiempo; set => tiempo = value; }
        public bool Estado { get => estado; set => estado = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Cambio { get => cambio; set => cambio = value; }
    }
}
