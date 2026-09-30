using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
namespace Practica4CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            string Apellidos = tbApellidos.Text;
            string telefono = tbTel.Text;
            string edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string genero = "";
            if (rbM.Checked)
            {
                genero = "Masculino";
            }
            else if (rbF.Checked)
            {
                genero = "Femenino";
            }
            else if (rbOtro.Checked)
            {
                genero = "Otro";
            }

            string mensaje = $"Nombres : {nombre}\n Apellidos : {Apellidos}\n" +
                $"Telefono : {telefono}\n Edad : {edad}  años\n Estatura : {estatura} cm\n\r Genero : {genero}";

            //Ruta de almacenamiento TXT
            string rutaFile = "C:\\Users\\umb3r\\source\\repos\\registros.txt";
            //verificacion de existencia del archivo
            bool ArchivoExiste = File.Exists(rutaFile);
            //instanciación de StreamWriter para escribir en el archivo
            using (StreamWriter Escritor = new StreamWriter(rutaFile, true))
            {
                if (ArchivoExiste)
                {
                    Escritor.WriteLine();
                }
                Escritor.WriteLine(mensaje);
            }



            MessageBox.Show(mensaje, "Registros de usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            

        }
    }
}
*/

namespace Practica4CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener datos eliminando espacios en blanco innecesarios
            string nombre = tbNombre.Text.Trim();
            string apellidos = tbApellidos.Text.Trim();
            string telefono = tbTel.Text.Trim();
            string edadStr = tbEdad.Text.Trim();
            string estaturaStr = tbEstatura.Text.Trim();
            string genero = "";

            if (rbM.Checked)
            {
                genero = "Masculino";
            }
            else if (rbF.Checked)
            {
                genero = "Femenino";
            }
            else if (rbOtro.Checked)
            {
                genero = "Otro";
            }

            // Validacion de campos vacios
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(edadStr) ||
                string.IsNullOrWhiteSpace(estaturaStr) || string.IsNullOrEmpty(genero))
            {
                MessageBox.Show("Por favor, completa todos los campos del formulario.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validacion numerica para edad, estatura y telefono
            if (!int.TryParse(edadStr, out int edad) || !float.TryParse(estaturaStr, out float estatura) || !long.TryParse(telefono, out _))
            {
                MessageBox.Show("La edad, estatura y teléfono deben contener valores numéricos válidos.", "Error de formato",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string mensaje = $"Nombres: {nombre}\nApellidos: {apellidos}\n" +
                             $"Teléfono: {telefono}\nEdad: {edad} años\nEstatura: {estatura} cm\nGénero: {genero}";

            // Ruta dinamica en la carpeta del proyecto para evitar errores si cambia el usuario o la PC
            string rutaFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "registros.txt");

            // Bloque try-catch para manejar excepciones al interactuar con el archivo de texto
            try
            {
                using (StreamWriter escritor = new StreamWriter(rutaFile, true))
                {
                    escritor.WriteLine(mensaje);
                    escritor.WriteLine(new string('-', 30));
                }

                MessageBox.Show("Datos guardados con éxito:\n\n" + mensaje, "Registro exitoso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar el archivo: {ex.Message}", "Error de archivo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo auxiliar para limpiar las cajas de texto y controles despues de guardar
        private void LimpiarCampos()
        {
            tbNombre.Clear();
            tbApellidos.Clear();
            tbTel.Clear();
            tbEdad.Clear();
            tbEstatura.Clear();
            rbM.Checked = false;
            rbF.Checked = false;
            rbOtro.Checked = false;
        }
    }
}   