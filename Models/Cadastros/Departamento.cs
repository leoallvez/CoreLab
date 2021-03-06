﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Cadastros
{
    public class Departamento
    {
        public long DepartamentoID { get; set; }

        [Display(Name = "Nome Departamento")]
        public string Nome { get; set; }

        public long? InstituicaoID { get; set; }
        public Instituicao Instituicao { get; set; }
        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
