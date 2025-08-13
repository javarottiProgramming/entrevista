using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Pages.Clientes
{
    public class InsertModel : PageModel
    {
        private readonly ILogger<InsertModel> _logger;
        private readonly ICrudService<Cliente> _crudService;
        private readonly ICrudService<Usuario> _crudUsuarioService;


        [BindProperty]
        public Cliente Cliente { get; set; }

        [BindProperty]
        public List<SelectListItem> Usuarios { get; set; }

        public string Erro { get; set; }

        public InsertModel(ILogger<InsertModel> logger, ICrudService<Cliente> crudService, ICrudService<Usuario> crudUsuarioService)
        {
            _logger = logger;
            _crudService = crudService;
            _crudUsuarioService = crudUsuarioService;
        }


        public void OnGet()
        {
            this.Cliente = new Cliente();

            var usuarios = _crudUsuarioService.GetAll()
                        .Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() });

            this.Usuarios = usuarios.ToList();
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
                _logger.LogInformation("Cliente cadastrado com sucesso");

                return RedirectToPage("./Index");
            }
            catch (System.Exception ex)
            {
                this.Erro = ex.Message;
                _logger.LogError(ex, "Erro ao cadastrar cliente");
            }

            return Page();
        }
    }
}