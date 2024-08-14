
namespace Estudiantes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Estudiantes.Models;
    using Estudiantes.Controllers;
    public partial class Form1 : Form
    {
        EstudiantesController estudiantesController = new EstudiantesController();
        
        public int codigoEstudiante = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarLista();
        }

        public void cargarLista()
        {
            lstEstudiantes.Items.Clear();
            List<EstudianteModel> estudiantes = estudiantesController.todos();

            var estudiantesFormateados = estudiantes.Select(estudiante => new
            {
                Display = $"{estudiante.nombre} {estudiante.apellido} - {estudiante.fecha_nacimiento.ToShortDateString()} - {estudiante.grado}",
                Value = estudiante.estudiante_id
            }).ToList();

            lstEstudiantes.DataSource = estudiantesFormateados;
            lstEstudiantes.DisplayMember = "Display";
            lstEstudiantes.ValueMember = "Value";
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            txt_Nombre.Text = "";
            txt_Apellido.Text = "";
            txt_Fecha.Text = "";
            txt_Grado.Text = "";
            codigoEstudiante = 0;
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_Nombre.Text = "";
            txt_Apellido.Text = "";
            txt_Fecha.Text = "";
            txt_Grado.Text = "";
            lstEstudiantes.SelectedIndex = -1;
            codigoEstudiante = 0;
        }

        private void btn_Grabar_Click(object sender, EventArgs e)
        {
            string respuesta = "";

            EstudianteModel estudiante = new EstudianteModel
            {
                estudiante_id = codigoEstudiante,
                nombre = txt_Nombre.Text,
                apellido = txt_Apellido.Text,
                fecha_nacimiento = Convert.ToDateTime(txt_Fecha.Text),
                grado = txt_Grado.Text
            };

            try
            {
                if (codigoEstudiante == 0)
                {
                    respuesta = estudiantesController.insertar(estudiante);
                }
                else
                {
                    respuesta = estudiantesController.actualizar(estudiante);
                }

                MessageBox.Show(respuesta);
                cargarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }

            codigoEstudiante = 0;
            txt_Nombre.Enabled = false;
            txt_Apellido.Enabled = false;
            txt_Fecha.Enabled = false;
            txt_Grado.Enabled = false;
            txt_Nombre.Text = "";
            txt_Apellido.Text = "";
            txt_Fecha.Text = "";
            txt_Grado.Text = "";
        }

        private void lstEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //codigoEstudiante = Convert.ToInt32 (lstEstudiantes.SelectedValue.ToString());
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (codigoEstudiante != 0)
            {
                EstudianteModel estudiante = new EstudianteModel
                {
                    estudiante_id = codigoEstudiante
                };

                DialogResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string respuesta = estudiantesController.eliminar(estudiante);
                    if (respuesta == "ok")
                    {
                        MessageBox.Show("Se eliminó con éxito");
                        cargarLista();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar: " + respuesta);
                    }
                }
                else
                {
                    MessageBox.Show("Se canceló la eliminación");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante para eliminar");
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (lstEstudiantes.SelectedItem != null)
            {
                txt_Nombre.Enabled = true;
                txt_Apellido.Enabled = true;
                txt_Fecha.Enabled = true;
                txt_Grado.Enabled = true;
                codigoEstudiante = Convert.ToInt16(lstEstudiantes.SelectedValue);
            }
        }

        private void lstEstudiantes_DoubleClick(object sender, EventArgs e)
        {
            if (lstEstudiantes.SelectedItem != null)
            {
              
                var selectedItem = (dynamic)lstEstudiantes.SelectedItem;
                int selectedId = selectedItem.Value;     
                List<EstudianteModel> estudiantes = new EstudiantesController().todos();
                EstudianteModel estudiante = estudiantes.FirstOrDefault(est => est.estudiante_id == selectedId);

                if (estudiante != null)
                {
                    
                    txt_Nombre.Text = estudiante.nombre;
                    txt_Apellido.Text = estudiante.apellido;
                    txt_Fecha.Text = estudiante.fecha_nacimiento.ToShortDateString();
                    txt_Grado.Text = estudiante.grado;
                    codigoEstudiante = estudiante.estudiante_id;
                }
            }
        }
    }
}
