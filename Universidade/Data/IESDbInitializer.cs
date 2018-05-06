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
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var departamento = new Departamento[]
            {
                new Departamento {Nome="Ciência da Computação"},
                new Departamento {Nome="Ciência de Alimentos"}
            };

            foreach(Departamento d in departamento)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();
        }
    }
}
