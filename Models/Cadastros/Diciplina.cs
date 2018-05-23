using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Cadastros
{
    public class Diciplina
    {
        public long? DiciplinaID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<CursoDisciplina> CursosDiciplinas { get; set; } 
    }
}
