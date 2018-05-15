using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Models.Cadastros;

namespace Universidade.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;

        public DepartamentoController(IESContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.Include(m => m.Instituicao)
                .OrderBy(c => c.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            var instiuicoes = _context.Instituicoes.OrderBy(i => i.Nome).ToList();

            instiuicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione" });
            ViewBag.Instituicoes = instiuicoes;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome", "InstituicaoID")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }

            return View(departamento);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);

            if(departamento == null)
            {
                return NotFound();
            }

           ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);

            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID", "InstituicaoID", "Nome")] Departamento departamento)
        {
            if(id != departamento.DepartamentoID)
            {
                return NotFound();
            }

            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID ?? null);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);

            _context.Instituicoes.Where(i => i.InstituicaoID == departamento.InstituicaoID).Load();

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);

            _context.Instituicoes.Where(i => i.InstituicaoID == departamento.InstituicaoID).Load();

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            _context.Departamentos.Remove(departamento);

            TempData["Message"] = "Departamento" + departamento.Nome.ToUpper() + " foi removido.";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(long? id)
        {
            return _context.Departamentos.Any(e => e.DepartamentoID == id);
        }
       
    }
}