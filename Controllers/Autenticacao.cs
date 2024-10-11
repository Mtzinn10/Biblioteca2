using System.Collections.Generic;
using System.Linq;
using Biblioteca2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;


namespace Biblioteca2.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {  
            if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool verificacaoLogin(string login, string senha, Controller controller) 
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                verificacaoUserAdmin(bc);

                senha = Criptografo.TextoCriptografado(senha);

                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login == login && u.Senha ==senha);
                List<Usuario>ListarUsuarioEncontrado = UsuarioEncontrado.ToList();

                if (ListarUsuarioEncontrado.Count ==0)
                {
                    return false;

                }
                else
                {
                    controller.HttpContext.Session.SetString("Login",ListarUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome",ListarUsuarioEncontrado[0].Nome);
                        controller.HttpContext.Session.SetInt32("tipo",ListarUsuarioEncontrado[0].Tipo);
                        return true;
                }
            }
        }

            public static void verificacaoUserAdmin(BibliotecaContext bc)
        {
            IQueryable<Usuario> userEncontrado = bc.Usuarios.Where(u => u.Login == "admin");
            if (userEncontrado.ToList().Count == 0)
            {
                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.Senha = Criptografo.TextoCriptografado("admin");
                admin.Tipo = Usuario.ADMIN;
                admin.Nome = "Administrador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
        }

        public static void verificacaoUserAdmin(Controller controller)
        {
            if (!(controller.HttpContext.Session.GetInt32("tipo")==Usuario.ADMIN))
            {
                controller.Request.HttpContext.Response.Redirect("/Usuarios/NeedAdmin");
            }
            }

    }
}

