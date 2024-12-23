using Hr_Vacancy_Managment.Data;
using Hr_Vacancy_Managment.Migrations;
using Hr_Vacancy_Managment.Models;
using Hr_Vacancy_Managment.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hr_Vacancy_Managment.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public UserController(DataContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            DepartmentAndVacancyViewModel vm = new DepartmentAndVacancyViewModel();
           var department = _context.Department.ToList();
            var vacancy = _context.Vacancy.ToList();
            vm.department = department;
            vm.vacancy = vacancy;
            return View(vm);
        }
        public IActionResult Jobs()
        {
          
            DepartmentAndVacancyViewModel vm = new DepartmentAndVacancyViewModel();
            var vacancies = _context.Vacancy.OrderByDescending(x => x.Vacancy_ID).ToList();
            vm.vacancy = vacancies;
            ViewBag.departments = new SelectList(_context.Department.ToList(),"Department_ID","Department_Name");
            return View(vm);
        }
        public IActionResult SearchByDepart(string did = null, List<int> timing =null)
        {
            DepartmentAndVacancyViewModel vm = new DepartmentAndVacancyViewModel();
            if(did != null && timing !=null && timing.Count > 0)
            {
              var FilteredVacancy = _context.Vacancy.Include(x => x.Departments).Where(x => x.Depart_ID == int.Parse(did) && timing.Contains(((int)x.Timing))).OrderByDescending(x=>x.Vacancy_ID).ToList();
                //FilteredVacancy.Where(x => timing.Contains(((int)x.Timing)));
                vm.vacancy = FilteredVacancy;
                return PartialView("JobListingPartialView", vm);
            };
            if (did != null)
            {
                var FilteredVacancy = _context.Vacancy.Include(x => x.Departments).Where(x => x.Depart_ID == int.Parse(did)).OrderByDescending(x => x.Vacancy_ID).ToList();
                if (FilteredVacancy.Count == 0)
                {
                    var result = _context.Department.Where(x => x.Department_ID == int.Parse(did)).FirstOrDefault();
                    string dname = result.Department_Name;
                    TempData["noData"] = "Sorry No Vacancy Available for "+dname+".";
                }
                vm.vacancy = FilteredVacancy;
            }else if (timing != null && timing.Count > 0)
            {
               var FilteredVacancy = _context.Vacancy.Include(x => x.Departments).Where(x => timing.Contains(((int)x.Timing))).OrderByDescending(x=>x.Vacancy_ID).ToList();
                if(FilteredVacancy.Count == 0)
                {
                    TempData["noData"] = "Sorry No Vacancy Available";
                }
                vm.vacancy = FilteredVacancy;
                
            }
            else
            {
                var vacancies = _context.Vacancy.Include(x => x.Departments).OrderByDescending(x => x.Vacancy_ID).ToList();
                vm.vacancy = vacancies;
            }
            return PartialView("JobListingPartialView",vm);
        }
        [HttpGet]
        public IActionResult Form(int id)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                var returnUrl = Url.Action("Form", "User", new { id=id});
                return RedirectToAction("Login","User",new { ReturnUrl = returnUrl});
            }
            var result = _context.Vacancy.Where(x => x.Vacancy_ID==id).FirstOrDefault();
            Applicant app = new Applicant();
            app.Applied_For = result.Vacancy_Title;
            return View(app);
        }
        [HttpPost]
        public IActionResult Form(Applicant app,IFormFile image)
        {
            string fileName = Path.GetFileName(image.FileName);
            string filePath = Path.Combine(_env.WebRootPath,"CVS",fileName);
            FileStream fs = new FileStream(filePath,FileMode.Create);
            image.CopyTo(fs);
            string uniqueNumber = $"A{ Guid.NewGuid().ToString("N").Substring(0,8).ToUpper() }";
            app.Applicant_Number = uniqueNumber;
            app.Applicant_Status = Applicant_Status.In_Proccess;
            app.Creation_Date = DateTime.Now;
            app.Applicant_CV = fileName;
           _context.Applicant.Add(app);
           _context.SaveChanges();
            TempData["success"] = "You Applied For this Job.";
            return RedirectToAction("Jobs");
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["returnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user,string returnUrl = null)
        {
            var result = _context.User.Where( x=> x.User_Email == user.User_Email && x.User_Password == user.User_Password).FirstOrDefault();
            if (result != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,result.User_Name)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user with the authentication cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index","User");
                }
            }
            return RedirectToAction("Jobs","User");
        }
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user,string returnUrl = null)
        {
          var duplicateData = _context.User.Where(x => x.User_Email == user.User_Email).FirstOrDefault();
            if(duplicateData != null)
            {
                return RedirectToAction("Register" , "User", new { ReturnUrl = returnUrl});
            }
            else
            {
                _context.User.Add(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Login", "User", new {ReturnUrl = returnUrl });   
        }
        public JsonResult searchEmail(string email = null)
        {
          var result = _context.User.Where(x => x.User_Email == email).FirstOrDefault();
            if(result != null)
            {
                var data = new {
                    text = "Email Already Exit",
                    color="red"
                };
                
                return new JsonResult(data);
            }
            else
            {
                var data = new
                {
                    text = "Email is good to go.",
                    color = "green"
                };

                return new JsonResult(data);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","User");
        }
    }
}
