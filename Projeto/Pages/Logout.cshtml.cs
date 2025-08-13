using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projeto.Pages
{
    [ValidateAntiForgeryToken]
    public class LogoutModel : PageModel
    {

        private readonly ILogger<LogoutModel> _logger;
        private readonly ICrudService<Usuario> _crudService;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LogoutModel(ILogger<LogoutModel> logger, ICrudService<Usuario> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
    }
}