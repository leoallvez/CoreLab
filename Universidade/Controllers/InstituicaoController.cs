using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Universidade.Models;

namespace Universidade.Controllers
{
    public class InstituicaoController : Controller
    {
        private static IList<Instituicao> Instituicoes = new List<Instituicao>()
        {
            new Instituicao()
            {
                InstituicaoID = 1,
                Nome = "UniParaná",
                Endereco = "Paraná"

            },
            new Instituicao()
            {
                InstituicaoID = 2,
                Nome = "UniSanta",
                Endereco = "Santa Catarina"

            },
            new Instituicao()
            {
                InstituicaoID = 3,
                Nome = "UniSãoPaulo",
                Endereco = "São Paulo"

            },
            new Instituicao()
            {
                InstituicaoID = 4,
                Nome = "UniSulgrandense",
                Endereco = "Rio Grande do Sul"

            },
            new Instituicao()
            {
                InstituicaoID = 5,
                Nome = "UniCarioca",
                Endereco = "Rio de Janeiro"

            }
        };

        public IActionResult Index()
        {
            return View(Instituicoes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instituicao instituicao)
        {
            Instituicoes.Add(instituicao);
            instituicao.InstituicaoID = Instituicoes.Select(
                i => i.InstituicaoID).Max() + 1;

            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            return View(Instituicoes.Where(
                i => i.InstituicaoID == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instituicao i)
        {
            Instituicoes.Remove(Instituicoes
                .Where(m => m.InstituicaoID == i.InstituicaoID).First());

            Instituicoes.Add(i);

            return RedirectToAction("Index");
        }

        public IActionResult Details(long id)
        {
            return View(Instituicoes.Where(i => 
                    i.InstituicaoID == id).First());
        }

        public IActionResult Delete(long id)
        {
            return View(Instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Instituicao i)
        {
            return RedirectToAction("Index");
        }
    }
}