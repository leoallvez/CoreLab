using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Cadastros
{
    public class Instituicao
    {
        public long InstituicaoID { get; set; }
        [Display(Name= "Nome Instituicao")]
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
