using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projeto.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        private const string _USERNAME = "admin";
        private const string _PASSWORD = "admin";

        public LoginModel()
        {
        }

        public async Task OnGet()
        {
            UserName = _USERNAME;
            Password = _PASSWORD;

            await HttpContext.SignOutAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            if (UserName == _USERNAME && Password == _PASSWORD)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName)
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
