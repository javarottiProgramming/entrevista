using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICrudService<Tarefa> _crudService;

        [BindProperty]
        public List<Tarefa> Tarefas { get; set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            ICrudService<Tarefa> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public void OnGet()
        {
            this.Tarefas = _crudService.GetAll();
        }

        public ActionResult OnPost()
        {
            _crudService.Update(this.Tarefas);

            return Page();
        }
    }
}
