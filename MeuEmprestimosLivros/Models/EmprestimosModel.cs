using System.ComponentModel.DataAnnotations;

namespace MeuEmprestimosLivros.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digete o nome do Recebedor!")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "Digete o nome do Fornencedor!")]
        public string Fornecedor { get; set; }
        [Required(ErrorMessage = "Digete o nome do Livro empréstado!")]
        public string LivroEmprestado { get; set; }
        public DateTime DataUltimaAtualizacao { get;  set; }
    }
}
