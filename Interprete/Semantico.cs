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
                        if (pX < 436 && pY < 392 && pRadio < 196 && pRadio > 49 && pX +(2*pRadio) < 436 && pY + (2 * pRadio) < 392)
                        {
                            for (int x = 0; x < lista.Count; x++)
                            {

                                if (lista[x].cara != null)
                                {
                                    int x1 = lista[x].cara.X;
                                    int y1 = lista[x].cara.Y;
                                    int x2 = lista[x].cara.X + (2 * lista[x].cara.Radio);
                                    int y2 = lista[x].cara.Y + (2 * lista[x].cara.Radio);
                                    int rradio = (2 * lista[x].cara.Radio);
                                    if (i > x)
                                    {
                                        if (pX >= x1 && pX <= x2 && pY >= y1 && pY <= y2 && lista[x].cara.Existe == true)
                                        {
                                            contadorError++;
                                        }
                                        if (pX + rradio >= x1 && pX + rradio <= x2 && pY + rradio >= y1 && pY + rradio <= y2 && lista[x].cara.Existe == true)
                                        {
                                            contadorError++;
                                        }
                                        if (pX + rradio >= x1 && pX + rradio <= x2 && pY >= y1 && pY <= y2 && lista[x].cara.Existe == true)
                                        {
                                            contadorError++;
                                        }
                                        if (pX >= x1 && pX <= x2 && pY + rradio >= y1 && pY + rradio <= y2 && lista[x].cara.Existe == true)
                                        {
                                            contadorError++;
                                        }
                                        if (pX <= x2 && pY <= y2 && pX + rradio >= x1 && pY + rradio >= y1 && lista[x].cara.Existe == true)
                                        {
                                            contadorError++;
                                        }
                                    }
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
                        }
                        else
                        {
                            if(pX > 436)
                            {
                                lista[i].cara.Existe = false;
                                lista[i].mensaje = "Coordenada X fuera de los límites";
                                lista[i].estado = false;
                            }
                            else
                            {
                                if (pY > 392)
                                {
                                    lista[i].cara.Existe = false;
                                    lista[i].mensaje = "Coordenada Y fuera de los límites";
                                    lista[i].estado = false;
                                }
                                else
                                {
                                    if (pRadio < 50)
                                    {
                                        lista[i].cara.Existe = false;
                                        lista[i].mensaje = "Radio muy pequeño";
                                        lista[i].estado = false;
                                    }
                                    else
                                    {
                                        lista[i].cara.Existe = false;
                                        lista[i].mensaje = "Radio muy grande";
                                        lista[i].estado = false;
                                    }
                                }
                            }
                            
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
                        for (int j = 0; j < lista.Count; j++)
                        {
                            if (i > j)
                            {
                                if (lista[j].cara != null)
                                {
                                    if (lista[i].nombre == lista[j].cara.Nombre)
                                    {
                                        
                                        lista[i].cara = lista[j].cara;
                                        lista[j].cara.Existe = false;
                                    }
                                }
                            }
                        }
                        if (lista[i].cara != null)
                        {
                            lista[i].mensaje = "No se encontraron errores sintacticos";
                            lista[i].estado = true;
                        }
                        else
                        {
                            lista[i].mensaje = "La cara " + lista[i].nombre + " no existe";
                            lista[i].estado = false;
                        }
                        break;
         
                }
            }
            return lista;
        }

        public bool ErrorExiste(List<Instruccion> lista, int linea)
        {
            bool bandera = false;
            for(int i = 0; i < lista.Count; i++)
            {
                if(lista[i].instruccion != null)
                {
                    if (lista[i].instruccion == "error" && lista[i].linea == linea)
                    {
                        bandera = true;
                    }
                }
            }
            return bandera;
        }
    }
}
