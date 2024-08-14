
namespace Estudiantes.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Estudiantes.Config;
    using System.Data;
    using System.Windows.Forms;

    internal class EstudianteModel
    {
        public int estudiante_id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string grado { get; set; }

        List<EstudianteModel> ListaEstudiantes = new List<EstudianteModel>();

        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();



        public List<EstudianteModel> todos()
        {
            string cadena = "SELECT * FROM Estudiantes";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow estudiante in tabla.Rows)
            {
                EstudianteModel nuevoestudiante = new EstudianteModel
                {
                    estudiante_id = Convert.ToInt32(estudiante["estudiante_id"]),
                    nombre = estudiante["nombre"].ToString(),
                    apellido = estudiante["apellido"].ToString(),
                    fecha_nacimiento = Convert.ToDateTime(estudiante["fecha_nacimiento"]),
                    grado = estudiante["grado"].ToString()
                };
                ListaEstudiantes.Add(nuevoestudiante);
            }
            conexion.CerrarConexion();
            return ListaEstudiantes;
        }

        public EstudianteModel uno(EstudianteModel estudiante)
        {
            string cadena = "SELECT * FROM Estudiantes WHERE estudiante_id = @estudiante_id";
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            EstudianteModel estudianteregresa = new EstudianteModel
            {
                estudiante_id = Convert.ToInt32(lector["estudiante_id"]),
                nombre = lector["nombre"].ToString(),
                apellido = lector["apellido"].ToString(),
                fecha_nacimiento = Convert.ToDateTime(lector["fecha_nacimiento"]),
                grado = lector["grado"].ToString()
            };

            conexion.CerrarConexion();
            return estudianteregresa;
        }

        public string insertar(EstudianteModel estudiante)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "INSERT INTO Estudiantes (nombre, apellido, fecha_nacimiento, grado) VALUES (@nombre, @apellido, @fecha_nacimiento, @grado)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", estudiante.nombre);
                cmd.Parameters.AddWithValue("@apellido", estudiante.apellido);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", estudiante.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@grado", estudiante.grado);
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public string actualizar(EstudianteModel estudiante)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "UPDATE Estudiantes SET nombre = @nombre, apellido = @apellido, fecha_nacimiento = @fecha_nacimiento, grado = @grado WHERE estudiante_id = @estudiante_id";
              
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", estudiante.nombre);
                cmd.Parameters.AddWithValue("@apellido", estudiante.apellido);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", estudiante.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@grado", estudiante.grado);
                cmd.Parameters.AddWithValue("@estudiante_id", estudiante.estudiante_id);

                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public string eliminar(EstudianteModel estudiante)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "DELETE FROM Estudiantes WHERE estudiante_id = @estudiante_id";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@estudiante_id", estudiante.estudiante_id);
                
                
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }




    }

}