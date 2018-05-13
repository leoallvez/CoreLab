using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universidade.Models;

namespace Universidade.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var instituicoes = new Instituicao[]
            {
                new Instituicao {Nome="UniParaná", Endereco="Párana"},
                new Instituicao {Nome="UniAcre", Endereco="Acre"}
            };

            foreach (var i in instituicoes)
            {
                context.Instituicoes.Add(i);
            }

            context.SaveChanges();

            var departamento = new Departamento[]
            {
                new Departamento {Nome="Ciência da Computação", InstituicaoID=1},
                new Departamento {Nome="Ciência de Alimentos", InstituicaoID=2}
            };

            foreach(Departamento d in departamento)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();
        }
    }
}
