using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Cadastros
{
    public class Curso
    {
        public long? CursoID { get; set; }
        public string Nome { get; set; }
        public long? DepartamentoID { get; set; }
        public Departamento departamento { get; set; }

        public virtual ICollection<CursoDisciplina> CursosDiciplinas { get; set; }
    }
}
