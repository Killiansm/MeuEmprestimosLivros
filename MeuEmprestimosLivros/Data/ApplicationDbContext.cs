using MeuEmprestimosLivros.Controllers;
using MeuEmprestimosLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuEmprestimosLivros.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<EmprestimosModel> Emprestimos2 { get; set; }
        public IEnumerable<EmprestimoController> emprestimos { get; internal set; }
    }
}
