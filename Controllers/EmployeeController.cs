using Hr_Vacancy_Managment.Data;
using Hr_Vacancy_Managment.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hr_Vacancy_Managment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;
        public EmployeeController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Employee emp,string ReturnUrl)
        {
          var result = _context.Employees.Where(x => x.Employee_Email == emp.Employee_Email && x.Employee_Password == emp.Employee_Password).FirstOrDefault();
            if(result != null)
            {
                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, result.Employee_Name)
    };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user with the authentication cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Employee");
        }
    }
}
