using Microsoft.EntityFrameworkCore;
using Models.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidade.Data.DAL.Cadastros
{
    public class DepartamentoDAL
    {
        private IESContext _context;

        public DepartamentoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Departamento> ObterDepartamentosClassificadasPorNome()
        {
            return _context.Departamentos
                .Include(i => i.Instituicao)
                .OrderBy(d => d.Nome);
        }

        public async Task<Departamento> ObterDepartamentoPorId(long id)
        {
            var departamento = await _context.Departamentos
                                             .SingleOrDefaultAsync(m => m.DepartamentoID == id);

            _context.Instituicoes
                    .Where(i => departamento.InstituicaoID == i.InstituicaoID)
                    .Load();

            return departamento;
        }

        public async Task<Departamento> GravarDepartamento(Departamento departamento)
        {
            if (departamento.DepartamentoID > 0)
            {
                _context.Departamentos.Add(departamento);

            }
            else
            {
                _context.Departamentos.Update(departamento);
            }

            await _context.SaveChangesAsync();
            return departamento;
        }

        public async Task<Departamento> ElimitarDepartamentoPorId(long id)
        {
            Departamento departamento = await ObterDepartamentoPorId(id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return departamento;
        }
    }
}
