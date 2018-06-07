using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Discente;
using System.Threading.Tasks;
using Universidade.Data;
using Universidade.Data.DAL.Discente;

namespace Universidade.Controllers
{
    public class AcademicoController : Controller
    {
        private readonly IESContext _context;
        private AcademicoDAL academicoDAL;

        public AcademicoController(IESContext context)
        {
            this._context = context;
            academicoDAL = new AcademicoDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await academicoDAL.OrderByName().ToListAsync());
        }

        private async Task<IActionResult> GetViewById(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var academico = await academicoDAL.Find((long)id);

            if(academico == null)
            {
                return NotFound();
            }

            return View(academico);
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetViewById((long)id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            return await GetViewById((long)id);
        }

        public IActionResult Create()
        {
            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistroAcademico ", "Nome", "Nascimento")] Academico academico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await academicoDAL.CreateOrUpdate(academico);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {

                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }

            return View(academico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var academico = await academicoDAL.Delete((long)id);
            TempData["Message"] = "Academico " + academico.Nome.ToUpper() + " foi removido.";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            return await GetViewById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("AcademicoID", "RegistroAcademico ", "Nome", "Nascimento")] Academico academico)
        {
            if (id != academico.AcademicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await academicoDAL.CreateOrUpdate(academico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicoExists(academico.AcademicoID))
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

            return View(academico);
        }

        private async Task<bool> AcademicoExists(long? id)
        {
            return await academicoDAL.Find((long)id) != null;
        }
    }
}