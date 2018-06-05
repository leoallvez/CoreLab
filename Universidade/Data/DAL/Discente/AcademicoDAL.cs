using Models.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidade.Data.DAL.Discente
{
    public class AcademicoDAL
    {
        private IESContext _context;

        public AcademicoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Academico> OrderByName()
        {
            return _context.Academicos.OrderBy(m => m.Nome);
        }

        public async Task<Academico> Find(long id)
        {
            return await _context.Academicos.FindAsync(id);
        }

        public async Task<Academico> CreateOrUpdate(Academico academico)
        {
            if(!academico.AcademicoID.HasValue)
            {
                _context.Academicos.Add(academico);
            }
            else
            {
                _context.Update(academico);
            }
            await _context.SaveChangesAsync();
            return academico;
        }

        public async Task<Academico> Delete(long id)
        {
           Academico academico = await Find(id);
            _context.Academicos.Remove(academico);
            await _context.SaveChangesAsync();
            return academico;
        }
    }
}
