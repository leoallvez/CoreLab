using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Models.Cadastros;
using Universidade.Data.DAL.Cadastros;

namespace Universidade.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;
        private DepartamentoDAL departamentoDAL;
        private InstituicaoDAL instituicaoDAL;

        public DepartamentoController(IESContext context)
        {
            this._context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await departamentoDAL.OrderByName().ToListAsync());
        }

        public IActionResult Create()
        {
            var instituicoes = instituicaoDAL.OrderByName().ToList();

            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione" });
            ViewBag.Instituicoes = instituicoes;
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
                    await departamentoDAL.CreateOrUpdate(departamento);
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
            ViewResult departamentoView = (ViewResult) await GetDepartamentoViewById(id);

            Departamento departamento = (Departamento)departamentoView.Model;

            if(departamento == null)
            {
                return NotFound();
            }

           ViewBag.Instituicoes = new SelectList(instituicaoDAL.OrderByName(), "InstituicaoID", "Nome", departamento.InstituicaoID);

            return departamentoView;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID", "InstituicaoID", "Nome")] Departamento departamento)
        {
            if(id != departamento.DepartamentoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await departamentoDAL.CreateOrUpdate(departamento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartamentoExists(departamento.DepartamentoID))
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
            ViewBag.Instituicoes = new SelectList(instituicaoDAL.OrderByName(), "InstituicaoID", "Nome", departamento.InstituicaoID);
            return View(departamento);
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetDepartamentoViewById((long)id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            return await GetDepartamentoViewById((long)id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var departamento = await departamentoDAL.Delete((long)id);
            TempData["Message"] = "Departamento" + departamento.Nome.ToUpper() + " foi removido.";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DepartamentoExists(long? id)
        {
            return await departamentoDAL.Find((long)id) != null;
        }

        private async Task<IActionResult> GetDepartamentoViewById(long? id)
        {
            if(!id.HasValue)
            {
                return NotFound();
            }

            var departamento = await departamentoDAL.Find((long)id);

            if(departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }
       
    }
}