using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Estudiantes.Config
{
    internal class Conexion
    {
        private SqlConnection con = new SqlConnection("Server =DORIAN;Database=Gestión de Escuelas; uid=sa ; pwd=12345");
        
        public SqlConnection AbrirConexion()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            return con;
        }
        public SqlConnection CerrarConexion()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return con;
        }
    
    }
}
