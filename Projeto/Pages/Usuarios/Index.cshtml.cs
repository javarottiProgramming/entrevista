using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICrudService<Usuario> _crudService;

        [BindProperty]
        public List<Usuario> Usuarios { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICrudService<Usuario> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public void OnGet()
        {
            bool admin = User.Claims.FirstOrDefault(claims => claims.Type == "IsAdmin").Value.Equals("true");
            if (!admin)
            {
                Response.Redirect("/");
            }

            this.Usuarios = _crudService.GetAll();
        }
    }
}
