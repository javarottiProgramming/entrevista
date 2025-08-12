using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;

namespace Projeto.Pages.Clientes
{
    public class EditModel : PageModel
    {

        private readonly ILogger<EditModel> _logger;
        private readonly ICrudService<Cliente> _crudService;

        [BindProperty]
        public Cliente Cliente { get; set; }
        public string Erro { get; set; }

        public EditModel(ILogger<EditModel> logger, ICrudService<Cliente> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public void OnGet(int id)
        {
            this.Cliente = _crudService.GetById(id);
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _crudService.Update(this.Cliente);

                TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";

                _logger.LogInformation("Cliente com ID {Id} atualizado com sucesso", this.Cliente.Id);

                return RedirectToPage("./Index");
            }
            catch (System.Exception ex)
            {
                this.Erro = ex.Message;
                _logger.LogError(ex, "Erro ao atualizar cliente com ID {Id}", this.Cliente.Id);
            }

            return Page();
        }
    }
}
