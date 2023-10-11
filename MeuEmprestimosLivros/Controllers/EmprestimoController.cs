using ClosedXML.Excel;
using MeuEmprestimosLivros.Data;
using MeuEmprestimosLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MeuEmprestimosLivros.Controllers
{
    public class EmprestimoController : Controller
    {

        readonly private ApplicationDbContext _db;
        public EmprestimoController(ApplicationDbContext db)
        {
                _db = db;
        }
        public IActionResult Index()
        {
            
            IEnumerable<EmprestimosModel> emprestimos = _db.Emprestimos2;


            return View(emprestimos);
        }
        [HttpGet] 
        public IActionResult Editar(int? id) 
        { 
            if (id == null || id == null)
            {
                return NotFound();
            }
            EmprestimosModel emprestimosModel = _db.Emprestimos2.FirstOrDefault(x => x.Id == id);


            if(emprestimosModel == null)
            {
                return NotFound();
            }

            return View(emprestimosModel);
        }
        [HttpGet]
        public IActionResult Excluir(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimos = _db.Emprestimos2.FirstOrDefault(x => x.Id == id);

            if (emprestimos == null)
            {
                return NotFound();
            }
            return View(emprestimos);
        }

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workbook = new XLWorkbook() )
            {
                workbook.AddWorksheet(dados, "Dados Empréstimos");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }

            }
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();


            dataTable.TableName = "Dados empréstimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof (string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));


            var dados = _db.Emprestimos2.ToList();
            if(dados.Count > 0)
            {
                dados.ForEach(emprestimos =>
                {
                    dataTable.Rows.Add(emprestimos.Recebedor, emprestimos.Fornecedor, emprestimos.LivroEmprestado, emprestimos.DataUltimaAtualizacao);
                });
            }

            return dataTable;
        }

        [HttpGet]
        public IActionResult cadastrar()
        {
            return View();
        }
        [HttpPost]
       public IActionResult Cadastrar(EmprestimosModel emprestimos) 
        {
            if (ModelState.IsValid)
            {
                emprestimos.DataUltimaAtualizacao = DateTime.Now;

                _db.Emprestimos2.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com socesso!"; 

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimos)
        {
            if(ModelState.IsValid)
            {
                var emprestimoDB = _db.Emprestimos2.Find(emprestimos.Id);

                emprestimoDB.Fornecedor = emprestimos.Fornecedor;
                emprestimoDB.Recebedor = emprestimos.Recebedor;
                emprestimoDB.LivroEmprestado = emprestimos.LivroEmprestado;

                _db.Emprestimos2.Update(emprestimoDB);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição realizado com socesso!";

                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a eição!";

            return View(emprestimos);
            //teste
        }
        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimos)
        { 
            if(emprestimos == null)
            {
                return NotFound();
            }
            _db.Emprestimos2.Remove(emprestimos);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Remoção realizada com socesso!";

            return RedirectToAction("Index");

        }



    }

}
