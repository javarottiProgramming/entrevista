using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;

namespace Projeto.Pages.Usuarios
{
    public class InsertModel : PageModel
    {
        private readonly ILogger<InsertModel> _logger;
        private readonly ICrudService<Usuario> _crudService;

        [BindProperty]
        public Usuario Usuario { get; set; }
        public string Erro { get; set; }

        public InsertModel(ILogger<InsertModel> logger, ICrudService<Usuario> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }


        public void OnGet()
        {
            this.Usuario = new Usuario();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _crudService.Insert(this.Usuario);

                TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
                _logger.LogInformation("Usuário cadastrado com sucesso");

                return RedirectToPage("./Index");
            }
            catch (System.Exception ex)
            {
                this.Erro = ex.Message;
                _logger.LogError(ex, "Erro ao cadastrar Uusuário");
            }

            return Page();
        }
    }
}