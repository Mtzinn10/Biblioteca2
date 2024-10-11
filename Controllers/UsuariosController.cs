using Biblioteca2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;


namespace Biblioteca2.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificacaoUserAdmin(this);

            return View(new UsuarioService().Listar());
        }
        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);

        }
        [HttpPost]

        public IActionResult editarUsuario(Usuario userEditado)
        {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }
        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificacaoUserAdmin(this);
            return View();

        }

        [HttpPost]

        public IActionResult RegistrarUsuarios(Usuario NovoUsuario)
        {
             Autenticacao.CheckLogin(this);
             Autenticacao.verificacaoUserAdmin(this);

             NovoUsuario.Senha = Criptografo.TextoCriptografado(NovoUsuario.Senha);

             UsuarioService us = new UsuarioService();
             us.IncluirUsuario(NovoUsuario);

             return RedirectToAction("cadastroRealizado");
        }

        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]

        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if(decisao=="EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão de Usuario" + new UsuarioService().Listar(id).Nome + "Realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else{
                ViewData["Mensagem"] = "Exclusão Cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }

    
        }
        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificacaoUserAdmin(this);
            return View();

        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();

        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

    }

}