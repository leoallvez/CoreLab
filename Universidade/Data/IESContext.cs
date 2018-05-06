using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Universidade.Models;
using System.Threading.Tasks;

namespace Universidade.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options){ }
        //Os nome da propriedades serão os nomes das tabelas.
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }

        //Sobrescrita da tabela em relação ao atributo de contexto.
        /**
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departamento>().ToTable("Departamentos");
            modelBuilder.Entity<Departamento>().ToTable("Instituicoes");
        }
        */



    }
}
