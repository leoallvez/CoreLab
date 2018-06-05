using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Models.Cadastros;
using System.Threading.Tasks;
using Models.Discente;

namespace Universidade.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options) { }
        //Os nome da propriedades serão os nomes das tabelas.
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoDisciplina> Disciplinas { get; set; }
        public DbSet<Academico> Academicos { get; set; }

        //Sobrescrita da tabela em relação ao atributo de contexto.
        /**
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departamento>().ToTable("Departamentos");
            modelBuilder.Entity<Departamento>().ToTable("Instituicoes");
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoDisciplina>()
                .HasKey(cd => new { cd.CursoID, cd.DisciplinaID });

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(c => c.Curso)
                .WithMany(cd => cd.CursosDiciplinas)
                .HasForeignKey(c => c.CursoID);

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(d => d.Diciplina)
                .WithMany(cd => cd.CursosDiciplinas)
                .HasForeignKey(d => d.DisciplinaID);
        }
    }
}
