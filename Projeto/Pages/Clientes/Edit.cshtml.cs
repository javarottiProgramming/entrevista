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
    public class EditModel : PageModel
    {

        private readonly ILogger<EditModel> _logger;
        private readonly ICrudService<Cliente> _crudClienteService;
        private readonly ICrudService<Usuario> _crudUsuarioService;

        [BindProperty]
        public Cliente Cliente { get; set; }
        
        [BindProperty]
        public List<SelectListItem> Usuarios { get; set; }

        public string Erro { get; set; }

        public EditModel(ILogger<EditModel> logger, ICrudService<Cliente> crudClienteService, ICrudService<Usuario> crudUsuarioService)
        {
            _logger = logger;
            _crudClienteService = crudClienteService;
            _crudUsuarioService = crudUsuarioService;
        }

        public void OnGet(int id)
        {
            this.Cliente = _crudClienteService.GetById(id);
            
            var usuarios = _crudUsuarioService.GetAll()
                        .Select(x =>  new SelectListItem { Text = x.Nome, Value = x.Id.ToString() });

            usuarios.Where(x => x.Value == this.Cliente.UsuarioResponsavelId.ToString())
                    .ToList()
                    .ForEach(x => x.Selected = true);

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

                _crudClienteService.Update(this.Cliente);

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
