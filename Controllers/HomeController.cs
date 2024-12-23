using Hr_Vacancy_Managment.Data;
using Hr_Vacancy_Managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace Hr_Vacancy_Managment.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _web;

        public HomeController(ILogger<HomeController> logger,DataContext context, IWebHostEnvironment web)
        {
            _logger = logger;
            _context = context;
            _web = web;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Department Section Started
        public IActionResult Department()
        {
            var result = _context.Department.ToList().OrderByDescending(x => x.Department_ID);
            return View(result);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Department(Department department)
        {
                department.Created_At = DateTime.Now;
            department.Department_Status = 0;
                _context.Department.Add(department);
                _context.SaveChanges();
            
            return RedirectToAction("Department");
        }
        public IActionResult Edit(int id)
        {
           var result = _context.Department.Where(x => x.Department_ID == id).FirstOrDefault();
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            _context.Department.Update(department);
            _context.SaveChanges();
            return RedirectToAction("Department");
        }
        public IActionResult Delete(int id)
        {
            var result = _context.Department.Find(id);
            _context.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Department");
        }
        //Department Section Ended
        //Employees Section Start
        public IActionResult Employees()
        {
            var data = _context.Department.Where(x => x.Department_Status == Status.Active).ToList();
            ViewBag.departments = new  SelectList(data, "Department_ID", "Department_Name") ;
           var result = _context.Employees.ToList().OrderByDescending(x=>x.Employee_ID);
            return View(result);
        }
        [HttpPost]
        public IActionResult Employees(Employee emp)
        {
            string uniqueNumber = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            string uniqueId = $"E{uniqueNumber}";
            emp.Employee_Status = 0;
            emp.Employee_Number = uniqueId;
            emp.Created_At = DateTime.Now;
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return RedirectToAction("Employees");
        }
        public IActionResult Emp_Edit(int id)
        {
            var result = _context.Employees.Where(x => x.Employee_ID == id).FirstOrDefault();
            ViewBag.departs = new SelectList(_context.Department.Where(x=>x.Department_Status == Status.Active).ToList(),"Department_ID", "Department_Name",result.Depart_ID);
            return View(result);
        }
        [HttpPost]
        public IActionResult Emp_Edit(Employee emp) 
        {
            _context.Update(emp);
            _context.SaveChanges();
            return RedirectToAction("Employees");
        }
        public IActionResult Emp_Delete(int id)
        {
            return View();
        }
        //Employees Section End
        //Vacancies Section Started
        public IActionResult Vacancies()
        {
            var data = _context.Department.Where(x => x.Department_Status == Status.Active).ToList();
            ViewBag.departs = new SelectList(data, "Department_ID", "Department_Name");
            var result = _context.Vacancy.ToList().OrderByDescending(x => x.Vacancy_ID);
            return View(result);
        }
        [HttpPost]
        public IActionResult Vacancies(Vacancy vacancy) 
        {
            string uniqueNumber = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            string Vnum = $"V{uniqueNumber}";
            vacancy.Vacancy_Number = Vnum;
            vacancy.Date_Of_Creation = DateTime.Now;
            vacancy.Vacancy_Status = Vacancy_Status.Open;
            vacancy.Owned_By = "hassan";
            _context.Vacancy.Add(vacancy);
            _context.SaveChanges();
            return RedirectToAction("Vacancies");
        }
        public IActionResult Edit_Vacancy(int id) 
        {
            var data = _context.Department.Where(x => x.Department_Status == Status.Active).ToList();
            ViewBag.departs = new SelectList(data, "Department_ID", "Department_Name");
            var result = _context.Vacancy.Where(x => x.Vacancy_ID == id).FirstOrDefault();
            //if(result.Vacancy_Status == Vacancy_Status.Close)
            //{
            //    TempData["closedVac"] = "Vacancy is closed. You cannot Edit it.";
            //    return RedirectToAction("Vacancies");
            //}
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit_Vacancy(Vacancy vacancy,string prev_Status)
        {
          if(prev_Status == "Close" ) {
                TempData["closedVac"] = "Vacancy is closed. You Cannot Open Or Suspend it.";
                return RedirectToAction("Edit_Vacancy");
            }
            _context.Update(vacancy);
            _context.SaveChanges();
            return RedirectToAction("Vacancies");
        }
        //Vacancies Section End
        //Applicant Section Start
        public IActionResult Applicant()
        {
            ViewBag.vacancies = new SelectList(_context.Vacancy.Where(x=>x.Vacancy_Status == Vacancy_Status.Open).ToList(), "Vacancy_Number", "Vacancy_Title") ; 
            var result = _context.Applicant.ToList().OrderByDescending(x => x.Applicant_ID);
            return View(result);
        }
        [HttpPost]
        public IActionResult Applicant(Applicant applicant,IFormFile filecv)
        {
           string FileName =  Path.GetFileName(filecv.FileName);
            string FilePath = Path.Combine(_web.WebRootPath,"CVS",FileName);
            FileStream fs = new FileStream(FilePath,FileMode.Create);
            filecv.CopyTo(fs);
            string uniqueNumber = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            string ApplicantNumber = $"A{uniqueNumber}";
            if (applicant.Applied_For == null)
            { applicant.Applicant_Status = Applicant_Status.Not_in_proccess; }
            else
            {
                applicant.Applicant_Status = Applicant_Status.In_Proccess;
            };
            applicant.Creation_Date = DateTime.Now;
            applicant.Applicant_Number = ApplicantNumber;
            applicant.Applicant_CV = FileName;
            _context.Applicant.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("Applicant");
        }
        public IActionResult Applicant_Details(int id) 
        {
           var result = _context.Applicant.Where(x => x.Applicant_ID == id).FirstOrDefault();
            return View(result);
        }
        public IActionResult Edit_Applicant(int id)
        {
            var result = _context.Applicant.Where(x => x.Applicant_ID == id).FirstOrDefault();
            ViewBag.vacancies = new SelectList(_context.Vacancy.Where(x=>x.Vacancy_Status == Vacancy_Status.Open).ToList(), "Vacancy_Number", "Vacancy_Title",result.Applied_For);
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit_Applicant(Applicant applicant)
        {

            if(applicant.Applicant_Status == Applicant_Status.Hired && applicant.Applied_For != null)
            {
                var vacID = _context.Vacancy.Where(x => x.Vacancy_Number == applicant.Applied_For).FirstOrDefault();
                var date = DateTime.Now;
                _context.Applicant_Vacancy.Add(new Applicant_Vacancy{Applicant_ID = applicant.Applicant_ID,Vacancy_ID = vacID.Vacancy_ID,Applied_Date = date });
                _context.SaveChanges();
            }
            if (applicant.Applied_For == null && applicant.Applicant_Status == Applicant_Status.Hired) {
                TempData["errorApp"] = "Please select any vacancy then hire.";
                return RedirectToAction("Edit_Applicant");
            }
            else
            {
                TempData["SuccessUpdating"] = "Successfully Updated";
                _context.Applicant.Update(applicant);
                _context.SaveChanges();
            }

            return RedirectToAction("Applicant");
        }
        public IActionResult Delete_Applicant(int id)
        {
          var result = _context.Applicant.Find(id);
            _context.Applicant.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Applicant");
        }
        public IActionResult HiredApplicant() 
        {
            var result = _context.Applicant_Vacancy.Include(x=> x.vacancy).Include(x => x.applicant).ToList().OrderByDescending(x=> x.Applicant_Vacancy_ID);
            return View(result);
        }
        public JsonResult EmployeeNameWidNum(string empNum)
        {
           var result = _context.Employees.Where(x => x.Employee_Number == empNum).FirstOrDefault();
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult  Edit_Interview_Schedule(int id)
        {
           var result =_context.Applicant_Vacancy.Where(x => x.Applicant_Vacancy_ID == id).FirstOrDefault();
            var vacany =  _context.Vacancy.Where(x => x.Vacancy_ID == result.Vacancy_ID).FirstOrDefault();
            ViewBag.Interviewers = new SelectList(_context.Employees.Where(x => x.Depart_ID == vacany.Depart_ID && x.Employee_Status == Employee_Status.Active).ToList(), "Employee_Number", "Employee_Number");
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit_Interview_Schedule(Applicant_Vacancy app_vac)
        {
            if (app_vac.Scheduled_Interview_Date > DateTime.Now)
            {
                _context.Applicant_Vacancy.Update(app_vac);
                _context.SaveChanges();
            }
            else
            {
                TempData["errorDate"] = "Please Choose Upcoming Date.";
                return RedirectToAction("Edit_Interview_Schedule");
            }
           var count = _context.Applicant_Vacancy.Where(x => x.Vacancy_ID == app_vac.Vacancy_ID && x.App_Vac_Status == Applicant_Vacancy_Status.Selected).Count();
            var vacancy = _context.Vacancy.Where(x => x.Vacancy_ID == app_vac.Vacancy_ID).FirstOrDefault();
            if(count >= vacancy.Number_Of_Jobs_Under_Vacancy)
            {
                vacancy.Vacancy_Status = Vacancy_Status.Close;
                _context.Vacancy.Update(vacancy);
                _context.SaveChanges();
                TempData["vacancyClose"] = "Vacancy is closed.";
            }
            return RedirectToAction("HiredApplicant");
        }
        //Applicant Section End
        //User Section Start
        public IActionResult User()
        {
           var result = _context.User.ToList();
            return View(result);
        }
        //User Section End
        
    }
}
