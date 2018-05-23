using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Cadastros
{
    public class CursoDisciplina
    {
        public long? CursoID { get; set; }
        public Curso Curso { get; set; }
        public long? DisciplinaID { get; set; }
        public Diciplina Diciplina { get; set; }
    }
}
