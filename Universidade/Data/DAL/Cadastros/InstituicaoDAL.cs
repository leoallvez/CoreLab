using Microsoft.EntityFrameworkCore;
using Models.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidade.Data.DAL.Cadastros
{
    public class InstituicaoDAL
    {
        private IESContext _context;

        public InstituicaoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Instituicao> ObterInstituicoesClassificadasPorNome()
        {
            return _context.Instituicoes.OrderBy(i => i.Nome);
        }

        public async Task<Instituicao> ObterInstituicaoPorId(long id)
        {
            return await _context.Instituicoes
                .Include(i => i.Departamentos)
                .SingleOrDefaultAsync(m => m.InstituicaoID == id);
        }

        public async Task<Instituicao> GravarInstituicao(Instituicao instituicao)
        {
            if(instituicao.InstituicaoID > 0)
            {
                _context.Instituicoes.Add(instituicao);

            }
            else
            {
                _context.Instituicoes.Update(instituicao);
            }

            await _context.SaveChangesAsync();
            return instituicao;
        }

        public async Task<Instituicao> ElimitarInstituicaoPorId(long id)
        {
            Instituicao instituicao = await ObterInstituicaoPorId(id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return instituicao;
        }
    }
}
