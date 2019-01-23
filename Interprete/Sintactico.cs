using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Interprete;

namespace Interprete
{
    class Sintactico
    {
        public Respuesta analizarSintaxis(string Codigo)
        {
            Lexico Analiza_Lexico = new Lexico();
            Respuesta respuesta = new Respuesta();
            Analiza_Lexico.Inicia();
            Analiza_Lexico.Analiza(Codigo);
            Semantico analiza_sem = new Semantico();
            List<Instruccion> listai = new List<Instruccion>();

            if (Analiza_Lexico.NoTokens > 0)
            {
                if (Analiza_Lexico.Lexema[0] == "Programa")
                {
                    if (Analiza_Lexico.Token[1] == "id")
                    {
                        if (Analiza_Lexico.Lexema[2] == "Inicio")
                        {
                            int valido = 3;
                            for (int i = valido; i < Analiza_Lexico.NoTokens - 1; i++)
                            {
                                if (Analiza_Lexico.Token[i] == "PalabraReservada" && Analiza_Lexico.Lexema[i] != "Programa" && Analiza_Lexico.Lexema[i] != "Inicio" && Analiza_Lexico.Lexema[i] != "Fin")
                                {

                                    switch (Analiza_Lexico.Lexema[i])
                                    {
                                        case "DibujarCara":

                                            if (Analiza_Lexico.Lexema[i + 1] == "(")
                                            {
                                                if (Analiza_Lexico.Token[i + 2] == "num")
                                                {
                                                    if (Analiza_Lexico.Lexema[i + 3] == ",")
                                                    {
                                                        if (Analiza_Lexico.Token[i + 4] == "num")
                                                        {
                                                            if (Analiza_Lexico.Lexema[i + 5] == ",")
                                                            {
                                                                if (Analiza_Lexico.Token[i + 6] == "num")
                                                                {
                                                                    if (Analiza_Lexico.Lexema[i + 7] == ",")
                                                                    {
                                                                        if (Analiza_Lexico.Token[i + 8] == "id")
                                                                        {
                                                                            if (Analiza_Lexico.Lexema[i + 9] == ",")
                                                                            {
                                                                                if (Analiza_Lexico.Token[i + 10] == "Modo")
                                                                                {
                                                                                    if (Analiza_Lexico.Lexema[i + 11] == ")")
                                                                                    {
                                                                                        int nombreToken = i + 8;
                                                                                        int xToken = i + 2;
                                                                                        int yToken = i + 4;
                                                                                        int modoToken = i + 10;
                                                                                        int radioToken = i + 6;
                                                                                        string nomC = Analiza_Lexico.Lexema[nombreToken];
                                                                                        int xC = Convert.ToInt32(Analiza_Lexico.Lexema[xToken]);
                                                                                        int yC = Convert.ToInt32(Analiza_Lexico.Lexema[yToken]);
                                                                                        int radioC = Convert.ToInt32(Analiza_Lexico.Lexema[radioToken]);
                                                                                        string modoC = Analiza_Lexico.Lexema[modoToken];
                                                                                        Carita car = new Carita(nomC, xC, yC, radioC, modoC, true);
                                                                                        Instruccion ins = new Instruccion();
                                                                                        ins.instruccion = "dibujar";
                                                                                        ins.cara = car;
                                                                                        listai.Add(ins);
                                                                                        i = i + 11;
                                                                                        if (i == Analiza_Lexico.NoTokens - 2) //solo una sentencia
                                                                                        {
                                                                                            respuesta.estado = true;
                                                                                            respuesta.Mensaje = "[●][Estado: Exito] \nNo se han encontrado Errores";
                                                                                            respuesta.list = analiza_sem.AnalizadorSem(listai);

                                                                                            return respuesta;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        respuesta.estado = false;
                                                                                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 11] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 11]) + "\n[Descripcion]: Se esperaba el elemento )";
                                                                                        Instruccion ins = new Instruccion();
                                                                                        ins.estado = false;
                                                                                        ins.instruccion = "error";
                                                                                        ins.mensaje = respuesta.Mensaje;
                                                                                        listai.Add(ins);
                                                                                        respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                                        i = i+10;
                                                                                      //  return respuesta;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    respuesta.estado = false;
                                                                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 10] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 10]) + "\n[Descripcion]: Se esperaba un Modo valido (triste, enojada, feliz, dormida, neutral)";
                                                                                    Instruccion ins = new Instruccion();
                                                                                    ins.estado = false;
                                                                                    ins.instruccion = "error";
                                                                                    ins.mensaje = respuesta.Mensaje;

                                                                                    listai.Add(ins);
                                                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                                    i = i + 9;
                                                                                 //   return respuesta;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                respuesta.estado = false;
                                                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 9] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 9]) + "\n[Descripcion]: Se esperaba el elemento ,";
                                                                                Instruccion ins = new Instruccion();
                                                                                ins.estado = false;
                                                                                ins.instruccion = "error";
                                                                                ins.mensaje = respuesta.Mensaje;
                                                                                listai.Add(ins);
                                                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                                i = i+8;
                                                                                //return respuesta;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            respuesta.estado = false;
                                                                            respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 8] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 8]) + "\n[Descripcion]: Se esperaba un tipo de nombre valido";
                                                                            Instruccion ins = new Instruccion();
                                                                            ins.estado = false;
                                                                            ins.instruccion = "error";
                                                                            ins.mensaje = respuesta.Mensaje;
                                                                            listai.Add(ins);
                                                                            respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                            i = i+7;
                                                                            //return respuesta;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        respuesta.estado = false;
                                                                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 7] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 7]) + "\n[Descripcion]: Se esperaba el elemento ,";
                                                                        Instruccion ins = new Instruccion();
                                                                        ins.estado = false;
                                                                        ins.instruccion = "error";
                                                                        ins.mensaje = respuesta.Mensaje;
                                                                        listai.Add(ins);
                                                                        respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                        i = i+6;
                                                                        //return respuesta;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    respuesta.estado = false;
                                                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 6] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 6]) + "\n[Descripcion]: Se esperaba un tipo numerico";
                                                                    Instruccion ins = new Instruccion();
                                                                    ins.estado = false;
                                                                    ins.instruccion = "error";
                                                                    ins.mensaje = respuesta.Mensaje;
                                                                    listai.Add(ins);
                                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                    i = i+5;
                                                                    //return respuesta;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                respuesta.estado = false;
                                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 5] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 5]) + "\n[Descripcion]: Se esperaba el elemento ,";
                                                                Instruccion ins = new Instruccion();
                                                                ins.estado = false;
                                                                ins.instruccion = "error";
                                                                ins.mensaje = respuesta.Mensaje;
                                                                listai.Add(ins);
                                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                i = i+4;
                                                                //return respuesta;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            respuesta.estado = false;
                                                            respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 4] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 4]) + "\n[Descripcion]: Se esperaba un tipo numerico";
                                                            Instruccion ins = new Instruccion();
                                                            ins.estado = false;
                                                            ins.instruccion = "error";
                                                            ins.mensaje = respuesta.Mensaje;
                                                            listai.Add(ins);
                                                            respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                            i = i+3;
                                                            //return respuesta;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        respuesta.estado = false;
                                                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 3] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 3]) + "\n[Descripcion]: Se esperaba el elemento ,";
                                                        Instruccion ins = new Instruccion();
                                                        ins.estado = false;
                                                        ins.instruccion = "error";
                                                        ins.mensaje = respuesta.Mensaje;
                                                        listai.Add(ins);
                                                        respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                        i = i+2;
                                                        //return respuesta;
                                                    }
                                                }
                                                else
                                                {
                                                    respuesta.estado = false;
                                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 2] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 2]) + "\n[Descripcion]: Se esperaba un tipo numerico";
                                                    Instruccion ins = new Instruccion();
                                                    ins.estado = false;
                                                    ins.instruccion = "error";
                                                    ins.mensaje = respuesta.Mensaje;
                                                    listai.Add(ins);
                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                    i = i+1;
                                                    //return respuesta;
                                                }
                                            }
                                            else
                                            {
                                                respuesta.estado = false;
                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 1] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 1]) + "\n[Descripcion]: Se esperaba el elemento (";
                                                Instruccion ins = new Instruccion();
                                                ins.estado = false;
                                                ins.instruccion = "error";
                                                ins.mensaje = respuesta.Mensaje;
                                                listai.Add(ins);
                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                i = i+0;
                                                //return respuesta;
                                            }
                                            break;

                                        case "EliminarCara":
                                            //Console.Out.WriteLine("Selecciono EliminarCara");
                                            if (Analiza_Lexico.Lexema[i + 1] == "(")
                                            {
                                                if (Analiza_Lexico.Token[i + 2] == "id")
                                                {
                                                    if (Analiza_Lexico.Lexema[i + 3] == ")")
                                                    {
                                                        Instruccion ins = new Instruccion();
                                                        ins.instruccion = "eliminar";
                                                        ins.nombre = Analiza_Lexico.Lexema[i + 2];
                                                        listai.Add(ins);
                                                        i = i + 3;
                                                        if (i == Analiza_Lexico.NoTokens - 2)
                                                        {
                                                            respuesta.estado = true;
                                                            respuesta.Mensaje = "[●][Estado: Exito] \nNo se han encontrado Errores";
                                                            respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                            return respuesta;
                                                            //Console.Out.WriteLine("No se han encontrado Errores");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        respuesta.estado = false;
                                                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 3] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 3]) + "\n[Descripcion]: Se esperaba el elemento )";
                                                        Instruccion ins = new Instruccion();
                                                        ins.estado = false;
                                                        ins.instruccion = "error";
                                                        ins.mensaje = respuesta.Mensaje;
                                                        listai.Add(ins);
                                                        respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                        i = i + 2;
                                                    }
                                                }
                                                else
                                                {
                                                    respuesta.estado = false;
                                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 2] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 2]) + "\n[Descripcion]: Se esperaba un tipo de nombre valido";
                                                    Instruccion ins = new Instruccion();
                                                    ins.estado = false;
                                                    ins.instruccion = "error";
                                                    ins.mensaje = respuesta.Mensaje;
                                                    listai.Add(ins);
                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                    i = i + 1;
                                                }
                                            }
                                            else
                                            {
                                                respuesta.estado = false;
                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 1] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 1]) + "\n[Descripcion]: Se esperaba el elemento (";
                                                Instruccion ins = new Instruccion();
                                                ins.estado = false;
                                                ins.instruccion = "error";
                                                ins.mensaje = respuesta.Mensaje;
                                                listai.Add(ins);
                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                i = i + 0;
                                            }
                                            break;
                                        case "Dormir":
                                            if (Analiza_Lexico.Lexema[i + 1] == "(")
                                            {
                                                if (Analiza_Lexico.Token[i + 2] == "num")
                                                {
                                                    if (Analiza_Lexico.Lexema[i + 3] == ")")
                                                    {
                                                        int t = Convert.ToInt32(Analiza_Lexico.Lexema[i + 2]);
                                                        Instruccion ins = new Instruccion();
                                                        ins.instruccion = "dormir";
                                                        ins.tiempo = t;
                                                        listai.Add(ins);
                                                        i = i + 3;
                                                        if (i == Analiza_Lexico.NoTokens - 2)
                                                        {
                                                            respuesta.estado = true;
                                                            respuesta.Mensaje = "[●][Estado: Exito] \nNo se han encontrado Errores";
                                                            respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                            return respuesta;
                                                            //Console.Out.WriteLine("No se han encontrado Errores");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        respuesta.estado = false;
                                                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 3] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 3]) + "\n[Descripcion]: Se esperaba el elemento )";
                                                        Instruccion ins = new Instruccion();
                                                        ins.estado = false;
                                                        ins.instruccion = "error";
                                                        ins.mensaje = respuesta.Mensaje;
                                                        listai.Add(ins);
                                                        respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                        i = i + 2;
                                                    }
                                                }
                                                else
                                                {
                                                    respuesta.estado = false;
                                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en:" + Analiza_Lexico.Lexema[i + 2] + " Linea:" + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 2]) + "\n[Descripcion]: Se esperaba un tipo numerico";
                                                    Instruccion ins = new Instruccion();
                                                    ins.estado = false;
                                                    ins.instruccion = "error";
                                                    ins.mensaje = respuesta.Mensaje;
                                                    listai.Add(ins);
                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                    i = i + 1;
                                                }
                                            }
                                            else
                                            {
                                                respuesta.estado = false;
                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 1] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 1]) + "\n[Descripcion]: Se esperaba el elemento (";
                                                Instruccion ins = new Instruccion();
                                                ins.estado = false;
                                                ins.instruccion = "error";
                                                ins.mensaje = respuesta.Mensaje;
                                                listai.Add(ins);
                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                i = i + 0;
                                            }
                                            break;
                                        case "CambiarModo":
                                            if (Analiza_Lexico.Lexema[i + 1] == "(")
                                            {
                                                if (Analiza_Lexico.Token[i + 2] == "id")
                                                {
                                                    if (Analiza_Lexico.Lexema[i + 3] == ",")
                                                    {
                                                        if (Analiza_Lexico.Token[i + 4] == "Modo")
                                                        {
                                                            if (Analiza_Lexico.Lexema[i + 5] == ")")
                                                            {
                                                                Instruccion ins = new Instruccion();
                                                                ins.instruccion = "cambiar";
                                                                ins.nombre = Analiza_Lexico.Lexema[i + 2];
                                                                ins.cambio = Analiza_Lexico.Lexema[i + 4];
                                                                listai.Add(ins);
                                                                i = i + 5;
                                                                if (i == Analiza_Lexico.NoTokens - 2)
                                                                {
                                                                    respuesta.estado = true;
                                                                    respuesta.Mensaje = "[●][Estado: Exito] \nNo se han encontrado Errores";
                                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                    return respuesta;
                                                                    //Console.Out.WriteLine("No se han encontrado Errores");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                respuesta.estado = false;
                                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 5] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 5]) + "\n[Descripcion]: Se esperaba el elemento )";
                                                                Instruccion ins = new Instruccion();
                                                                ins.estado = false;
                                                                ins.instruccion = "error";
                                                                ins.mensaje = respuesta.Mensaje;
                                                                listai.Add(ins);
                                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                                i = i + 4;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            respuesta.estado = false;
                                                            respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 4] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 4]) + "\n[Descripcion]: Se esperaba un tipo de nombre valido";
                                                            Instruccion ins = new Instruccion();
                                                            ins.estado = false;
                                                            ins.instruccion = "error";
                                                            ins.mensaje = respuesta.Mensaje;
                                                            listai.Add(ins);
                                                            respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                            i = i + 3;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        respuesta.estado = false;
                                                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 3] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 3]) + "\n[Descripcion]: Se esperaba el elemento ,";
                                                        Instruccion ins = new Instruccion();
                                                        ins.estado = false;
                                                        ins.instruccion = "error";
                                                        ins.mensaje = respuesta.Mensaje;
                                                        listai.Add(ins);
                                                        respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                        i = i + 2;
                                                    }
                                                }
                                                else
                                                {
                                                    respuesta.estado = false;
                                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 2] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 2]) + "\n[Descripcion]: Se esperaba un tipo de nombre valido";
                                                    Instruccion ins = new Instruccion();
                                                    ins.estado = false;
                                                    ins.instruccion = "error";
                                                    ins.mensaje = respuesta.Mensaje;
                                                    listai.Add(ins);
                                                    respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                    i = i + 1;
                                                }
                                            }
                                            else
                                            {
                                                respuesta.estado = false;
                                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i + 1] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i + 1]) + "\n[Descripcion]: Se esperaba el elemento (";
                                                Instruccion ins = new Instruccion();
                                                ins.estado = false;
                                                ins.instruccion = "error";
                                                ins.mensaje = respuesta.Mensaje;
                                                listai.Add(ins);
                                                respuesta.list = analiza_sem.AnalizadorSem(listai);
                                                i = i + 0;
                                            }
                                            break;
                                        default:
                                            Console.Out.WriteLine("Selecciono otro");
                                            break;
                                    }
                                }
                                else
                                {
                                    respuesta.estado = false;
                                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[i] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[i]) + "\n[Descripcion]: Instruccion incorrecta, se esperaba algun tipo de instruccion bien formada";
                                    return respuesta;
                                }
                            }

                            //Fin for

                            if (Analiza_Lexico.Lexema[Analiza_Lexico.NoTokens - 1] == "Fin")
                            {
                                respuesta.estado = true;
                                respuesta.Mensaje = "[●][Estado: Exito] \nNo se han encontrado Errores";
                                return respuesta;
                            } /////************************************ Aqui Fin
                            else
                            {
                                respuesta.estado = false;
                                respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[Analiza_Lexico.NoTokens - 1] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[Analiza_Lexico.NoTokens - 1]) + "\n[Descripcion]: Se esperaba la palabra reservada Fin al final del codigo";
                                return respuesta;
                            }

                        }
                        else
                        {
                            respuesta.estado = false;
                            respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[2] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[2]) + "\n[Descripcion]: Se esperaba la palabra reservada Inicio";
                            return respuesta;
                        }
                    }
                    else
                    {
                        respuesta.estado = false;
                        respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[1] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[1]) + "\n[Descripcion]: Hace falta un nombre valido despues de la palabra reservada Programa";
                        return respuesta;
                    }

                }
                else
                {
                    respuesta.estado = false;
                    respuesta.Mensaje = "[x][Estado : Error]\n" + "[Tipo : Sintactico] Error en: " + Analiza_Lexico.Lexema[0] + " Linea: " + numeroLinea(Codigo, Analiza_Lexico.Lexema[0]) + "\n[Descripcion]: Se esperaba la palabra reservada Programa";
                    return respuesta;
                }
            }
            else
            {
                respuesta.estado = false;
                respuesta.Mensaje = "[●][Estado: Exito] \nNo se han encontrado Errores";
                return respuesta;
            }
        }

        public int numeroLinea(string codigo, string lexema)
        {
            int nolinea = 1;
            string linea = "";
            string aux = "";
            if (lexema != null)
            {
                for (int i = 0; i < codigo.Length; i++)
                {
                    if (codigo[i] == '\n')
                    {
                        if (linea.Contains(lexema))
                        {
                            return nolinea;
                        }
                        nolinea = nolinea + 1;
                    }
                    else
                    {
                        aux = codigo[i].ToString();
                        linea = linea + aux;
                        if (i == codigo.Length - 1)
                        {
                            //Console.Out.WriteLine(linea);
                            //if (linea.Contains(lexema))
                            //{                            
                            return nolinea;
                            //}
                        }
                    }
                }
                return nolinea;
            }
            return nolinea;
        }
    }
}
