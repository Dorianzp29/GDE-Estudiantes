
namespace Estudiantes.Controllers
{
    using System.Collections.Generic;
    using Estudiantes.Models;
    internal class EstudiantesController
    {
        private EstudianteModel modeloEstudiante = new EstudianteModel();

        public List<EstudianteModel> todos()
        {
            return modeloEstudiante.todos();
        }
        public EstudianteModel uno(EstudianteModel estudiante)
        {
            return modeloEstudiante.uno(estudiante);
        }
        public string insertar(EstudianteModel estudiante)
        {
            return modeloEstudiante.insertar(estudiante);
        }
        public string actualizar(EstudianteModel estudiante)
        {
            return modeloEstudiante.actualizar(estudiante);
        }
        public string eliminar(EstudianteModel estudiante)
        {
            return modeloEstudiante.eliminar(estudiante);
        }
    }
}
