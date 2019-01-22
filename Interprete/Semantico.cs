using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interprete
{
    class Semantico
    {
        
        public List<Instruccion> AnalizadorSem(List<Instruccion> lista)
        {
            List<Canvas> puntos = new List<Canvas>();
            int contador = 0;
            int contadorError = 0;
            int pX;
            int pY;
            int pRadio;
            string nombre = "";
            for(int i = 0; i < lista.Count; i++)
            {
                contador = 0;
                contadorError = 0;
                switch (lista[i].instruccion)
                {
                    case "dibujar":
                        nombre = lista[i].cara.Nombre;
                        pX = lista[i].cara.X;
                        pY = lista[i].cara.Y;
                        pRadio = lista[i].cara.Radio;
                        for (int x = 0; x < puntos.Count; x++)
                        {
                            if(pX > puntos[x].X1 && pX < puntos[x].X2 && pY > puntos[x].Y1 && pY < puntos[x].Y2)
                            {
                                contadorError++;
                            }
                        }
                        if (contadorError == 0)
                        {
                            for (int j = 0; j < lista.Count; j++)
                            {
                                if (i > j)
                                {
                                    if (lista[j].cara != null)
                                    {
                                        if (nombre == lista[j].cara.Nombre && lista[j].cara.Existe == true)
                                        {
                                            contador++;
                                        }
                                    }
                                }
                            }
                            if (contador < 1)
                            {
                                Canvas c = new Canvas(pX, pY, (pX + (2 * pRadio)), (pY + (2 * pRadio)));
                                puntos.Add(c);
                                lista[i].mensaje = "No se encontraron errores sintacticos";
                                lista[i].estado = true;
                            }
                            else
                            {
                                lista[i].mensaje = "Nombre de cara " + nombre + "ya está asignado a otra cara.";
                                lista[i].estado = false;
                            }
                        }
                        else
                        {
                            lista[i].cara.Existe = false;
                            lista[i].mensaje = "Ya existe una cara en ese espacio, cambia las coordenadas";
                            lista[i].estado = false;
                        }
                        break;
                    case "eliminar":
                        for (int j = 0; j < lista.Count; j++)
                        {
                            if (i > j)
                            {
                                if(lista[j].cara != null)
                                { 
                                    if (lista[i].nombre == lista[j].cara.Nombre)
                                    {
                                        lista[j].cara.Existe = false;
                                        lista[i].cara = lista[j].cara;
                                    }
                                }
                            }
                        }
                        if(lista[i].cara != null)
                        {
                            lista[i].mensaje = "No se encontraron errores sintacticos";
                            lista[i].estado = true;
                        }
                        else
                        {
                            lista[i].mensaje = "La cara "+lista[i].nombre+" no existe";
                            lista[i].estado = false;
                        }
                        break;
                    case "dormir":
                        lista[i].mensaje = "Proceso durmiendo por peticion del usuario";
                        lista[i].estado = true;
                        break;
                    case "error":
                        
                        lista[i].estado = true;
                        break;
                    case "cambiar":
                        break;
         
                }
            }
            return lista;
        }
    }
}
