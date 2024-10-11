using System.Collections.Generic;
using System;
using System.Linq;

namespace Biblioteca2.Models
{

    public class UsuarioService
    {

        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public Usuario Listar(int Id){
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(Id);
            }
        }
        public void IncluirUsuario(Usuario novoUsuario){
             using (BibliotecaContext bc = new BibliotecaContext())
             {
                bc.Usuarios.Add(novoUsuario);
                bc.SaveChanges();
             }
            
        }
        public void editarUsuario(Usuario userEditado){
             using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario u = bc.Usuarios.Find(userEditado);
                u.Login = userEditado.Login;
                u.Nome = userEditado.Nome;
                u.Senha = userEditado.Senha;;
                u.Tipo = userEditado.Tipo;
            }
        }

        public void excluirUsuario(int id)
        {
             using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
    }
}