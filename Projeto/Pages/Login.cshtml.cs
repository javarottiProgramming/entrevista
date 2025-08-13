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
    public class LoginModel : PageModel
    {

        private readonly ILogger<LoginModel> _logger;
        private readonly ICrudService<Usuario> _crudService;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(ILogger<LoginModel> logger, ICrudService<Usuario> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        public async Task OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuarios = _crudService.GetAll();
            var usuario = usuarios.FirstOrDefault(u => u.Login == UserName && u.Senha == Password);

            if (usuario != null && usuario.Id > 0)
            {
                if (!usuario.Ativo)
                {
                    Message = "Usuário inativo.";
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim("IsAdmin", usuario.Administrador ? "true" : "false")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    
                return RedirectToPage("/Index");
                
            }

            Message = "Usuário ou senha inválidos";

            return Page();
        }

    }
}
