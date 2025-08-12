using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Infrastructure.Exceptions;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Pages.Clientes
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;
        private readonly ICrudService<Cliente> _crudService;

        [BindProperty]
        public Cliente Cliente { get; set; }
        public string Erro { get; set; }

        public DeleteModel(ILogger<DeleteModel> logger, ICrudService<Cliente> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public void OnGet(int id)
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            this.Cliente = _crudService.GetById(id);
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new BusinessException("Parametro inválido");
                }

                _crudService.DeleteById(id);

                TempData["SuccessMessage"] = "Cliente deletado com sucesso!";

                return RedirectToPage("./Index");
            }
            catch (System.Exception ex)
            {

                this.Erro = ex.Message;
            }

            return Page();
        }
    }
}
