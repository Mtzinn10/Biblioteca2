using System.Collections.Generic;

namespace Biblioteca2.Models
{
    public class CadEmprestimoViewModel
    {
        public ICollection<Livro> Livros { get; set; }
        public Emprestimo Emprestimo { get; set; }
    }
}