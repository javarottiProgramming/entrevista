using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;

namespace Projeto.Pages.Clientes
{
    public class InsertModel : PageModel
    {
        private readonly ILogger<InsertModel> _logger;
        private readonly ICrudService<Cliente> _crudService;

        [BindProperty]
        public Cliente Cliente { get; set; }
        public string Erro { get; set; }

        public InsertModel(ILogger<InsertModel> logger, ICrudService<Cliente> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }


        public void OnGet()
        {
            this.Cliente = new Cliente();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _crudService.Insert(this.Cliente);

                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";

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
