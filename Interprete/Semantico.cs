using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interprete
{
    class Semantico
    {

        public List<Instruccion> AnalizadorSem(List<Instruccion> lista)
        {
            int contador = 0;
            string nombre = "";
            for(int i = 0; i < lista.Count; i++)
            {
                contador = 0;
                switch (lista[i].Instrucciones)
                {
                    case "dibujar":
                        nombre = lista[i].Cara.Nombre;
                        for (int j = 0; j < lista.Count; j++)
                        {
                            if (i > j)
                            {
                                if (nombre == lista[j].Cara.Nombre)
                                {
                                    contador++;
                                }
                            }
                        }
                        if (contador < 1)
                        {
                            lista[i].Mensaje = "No se encontraron errores sintacticos";
                            lista[i].Estado = true;
                        }
                        else
                        {
                            lista[i].Mensaje = "Nombre de cara " + nombre + "ya está asignado a otra cara.";
                            lista[i].Estado = false;
                        }
                        break;
                    case "eliminar":
                        break;
                    case "dormir":
                        break;
                    case "cambiar":
                        break;
         
                }
            }
            return lista;
        }
    }
}
