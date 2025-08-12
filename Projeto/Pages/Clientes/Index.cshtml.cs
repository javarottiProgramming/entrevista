using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Projeto.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICrudService<Cliente> _crudService;

        [BindProperty]
        public List<Cliente> Clientes { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICrudService<Cliente> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public void OnGet()
        {
            this.Clientes = _crudService.GetAll();

        }
    }
}
