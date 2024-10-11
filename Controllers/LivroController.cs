using Biblioteca2.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca2.Controllers
{
    public class LivroController : Controller
    {
        private string filtro;


        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            LivroService livroService = new LivroService();

            if(l.Id == 0)
            {
                livroService.Inserir(l);
            }
            else
            {
                livroService.Atualizar(l);
            }

            return RedirectToAction("Listagem");
        }

         public IActionResult Listagem(string tipoFiltro, string ItensPagina, int NumeroPagina, int PaginaAtual)
        {
            Autenticacao.CheckLogin(this);

            FiltrosLivros objFiltro = null;
            if(!string.IsNullOrEmpty(filtro));
            {
                objFiltro = new FiltrosLivros();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }
            ViewData["livrosPorPagina"] = (string.IsNullOrEmpty(ItensPagina) ? 10 : Int32.Parse(ItensPagina));
            ViewData["PaginaAtual"] = (PaginaAtual !=0 ? PaginaAtual : 1);
            
            LivroService livrosService = new LivroService();
            return View(livrosService.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }
    }
}