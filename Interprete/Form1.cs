using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interprete;

namespace Interprete
{
    public partial class Form1 : Form
    {

        String rutaArchivo;
        String nombre;
        String nombreInterprete = "Interprete Mamalon";
        Sintactico sintaxis = new Sintactico();

        public Form1()
        {
            InitializeComponent();                       
        }

        private void botonAbrirArchivo_Click(object sender, EventArgs e)
        {
            Stream Flujo;
            OpenFileDialog DialogoAbrirArchivo = new OpenFileDialog();
            DialogoAbrirArchivo.Filter = "(*.lt1)|*.lt1";
            if (DialogoAbrirArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                               
                if ( (Flujo = DialogoAbrirArchivo.OpenFile() )!= null )
                {
                    rutaArchivo = DialogoAbrirArchivo.FileName;
                    nombre = DialogoAbrirArchivo.SafeFileName;                    
                    String textoArchivo = File.ReadAllText(rutaArchivo);                  
                    areaEditor.Text = textoArchivo;
                    Form1.ActiveForm.Text = nombre + " - " + nombreInterprete; 
                    Flujo.Close();
                }
                else
                {
                    areaErrores.Text = "Error abrir Archivo";
                }            
            }
            else
            {
                areaErrores.Text = "Error al abrir dialogo abrir (valgame la redundancia xd)";
            }
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {            
            if ( (areaEditor.Text).Length == 0)
            {
                areaErrores.Text = "No hay codigo que guardar";
            }
            else
            {                      
                if (File.Exists(rutaArchivo))
                {
                    File.WriteAllText(rutaArchivo, areaEditor.Text);
                    areaErrores.Text = nombre + " Guardado con Exito";
                }
                else
                {
                    SaveFileDialog dialogoGuardar = new SaveFileDialog();
                    dialogoGuardar.AddExtension = true;
                    dialogoGuardar.DefaultExt = ".lt1";
                    dialogoGuardar.Filter = "(*.lt1)|*.lt1";
                    if (dialogoGuardar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        rutaArchivo = dialogoGuardar.FileName;
                        //Inicio Codigo para sacar Nombre (Eliminar si se encuentra otra manera)
                        String[] partesNombre = rutaArchivo.Split('\\');
                        nombre = partesNombre[partesNombre.Length - 1];
                        //Fin Codigo para sacar Nombre (Eliminar si se encuentra otra manera)
                        Form1.ActiveForm.Text = nombre + " - " + nombreInterprete;
                        File.WriteAllText(rutaArchivo, areaEditor.Text);
                    }
                    else
                    {
                        areaErrores.Text = "";
                    }
                }                                                                                                             
            }
        }

        private void botonEjecutar_Click(object sender, EventArgs e)
        {
            Respuesta respuesta = new Respuesta();
      
            if ((areaEditor.Text).Length == 0)
            {
                respuesta.estado = false;
                respuesta.Mensaje = "[•][Estado : Exito]\n" + "[Tipo : nulo] " + "\n[Descripcion]: No hay nada que analizar";
                areaErrores.Text = respuesta.Mensaje;
                textBox1.Text = "hola";

            }
            else
            {                
                respuesta = sintaxis.analizarSintaxis(areaEditor.Text);
                if (respuesta.list != null)
                {
                    for (int i = 0; i < respuesta.list.Count; i++)
                    {
                        switch (respuesta.list[i].instruccion)
                        {
                            case "dibujar":
                                if (respuesta.list[i].estado == true)
                                {
                                    Graphics g = panel1.CreateGraphics();
                                    if (respuesta.list[i].cara.Modo == "feliz")
                                    {

                                        Pen p = new Pen(Color.Black);
                                        SolidBrush s = new SolidBrush(Color.Red);
                                        g.DrawEllipse(p, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                        g.FillEllipse(s, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                    }
                                    if (respuesta.list[i].cara.Modo == "triste")
                                    {
                                        Pen p = new Pen(Color.Black);
                                        SolidBrush s = new SolidBrush(Color.Blue);
                                        g.DrawEllipse(p, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                        g.FillEllipse(s, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                    }
                                    if (respuesta.list[i].cara.Modo == "enojada")
                                    {
                                        Pen p = new Pen(Color.Black);
                                        SolidBrush s = new SolidBrush(Color.Yellow);
                                        g.DrawEllipse(p, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                        g.FillEllipse(s, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                    }
                                }
                                else
                                {
                                   
                                    textBox1.Text += textBox1.Text + respuesta.list[i].mensaje;
                                }
                                break;
                            case "eliminar":
                                if (respuesta.list[i].estado == true)
                                {
                                    Graphics g = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.White);
                                    SolidBrush s = new SolidBrush(Color.White);
                                    g.DrawEllipse(p, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                    g.FillEllipse(s, respuesta.list[i].cara.X, respuesta.list[i].cara.Y, respuesta.list[i].cara.Radio, respuesta.list[i].cara.Radio);
                                }
                                else
                                {
                                    textBox1.Text = textBox1.Text + respuesta.list[i].mensaje;
                                }
                                break;
                            case "dormir":
                                if (respuesta.list[i].estado == true)
                                {
                                    Thread.Sleep(1000 * (respuesta.list[i].tiempo));
                                    textBox1.Text = textBox1.Text + respuesta.list[i].mensaje;
                                }
                                break;
                            case "cambiar":
                                break;
                            case "error":
                                if (respuesta.list[i].estado == true)
                                {
                                    if (areaErrores.Text == "")
                                    {
                                        areaErrores.Text = respuesta.list[i].mensaje;
                                    }
                                    else
                                    {
                                        areaErrores.Text += Environment.NewLine;
                                        areaErrores.Text += "__________________________________________________________________";
                                        areaErrores.Text += Environment.NewLine;
                                        areaErrores.Text += respuesta.list[i].mensaje;
                                    }
                                }
                                break;

                        }
                    }
                }
                else
                {
                    areaErrores.Text = respuesta.Mensaje;
                }
            }
        }
                          
    

        private void botonNuevoArchivo_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.DefaultExt = ".lt1";
            dialogoGuardar.Filter = "(*.lt1)|*.lt1";
      
            
            if (dialogoGuardar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                                   
                rutaArchivo = dialogoGuardar.FileName;
                //Inicio Codigo para sacar Nombre (Eliminar si se encuentra otra manera)
                String[] partesNombre = rutaArchivo.Split('\\');
                nombre = partesNombre[partesNombre.Length - 1];
                //Fin Codigo para sacar Nombre (Eliminar si se encuentra otra manera)
                Form1.ActiveForm.Text = nombre + " - " + nombreInterprete;
                areaEditor.Text = "";
                File.WriteAllText(rutaArchivo,areaEditor.Text);
               

            }
            else
            {
                areaErrores.Text = "No se abrio dialogo guardar";
            }

          
        }


        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void areaEditor_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void areaEditor_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void areaEditor_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            areaErrores.Text = "";
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);

        }
    }
}
