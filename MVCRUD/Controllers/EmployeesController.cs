using Microsoft.AspNetCore.Mvc;
using MVCCRUD.Data;
using MVCRUD.Models;
using MVCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MVCCRUD3.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
           // var employees = await mvcDemoDbContext.Employees.Where(x => x.RecStatus == 'A').OrderBy(x => x.Username).ToListAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Employee  emp)
        {
            if (emp.Username == "Bipin" && emp.Password == "123")
            {
                // Authentication successful
                var employees = await mvcDemoDbContext.Employees.Where(x => x.Username == emp.Username && x.Password == emp.Password).ToListAsync();
                return RedirectToAction("Index", "Employees", employees);
            }

            ModelState.AddModelError("", "Invalid username or password.");

            TempData["ErrorMessage"] = "Invalid username or password.";
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.Where(x => x.RecStatus == 'A').OrderBy(x => x.Username).ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Username = addEmployeeRequest.Username,
                Password = addEmployeeRequest.Password,
                DOB = addEmployeeRequest.DOB,
                Email = addEmployeeRequest.Email,
                Address = addEmployeeRequest.Address,
                Phone = addEmployeeRequest.Phone,
                Java = addEmployeeRequest.Java,
                Python = addEmployeeRequest.Python,
                CPlusPlus = addEmployeeRequest.CPlusPlus,
                RecStatus = addEmployeeRequest.RecStatus,
                Gender = addEmployeeRequest.Gender,
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Username = employee.Username,
                    Password = employee.Password,
                    DOB = employee.DOB,
                    Email = employee.Email,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    Gender = employee.Gender,
                    Python = employee.Python,
                    Java = employee.Java,
                    RecStatus = employee.RecStatus,
                    CPlusPlus = employee.CPlusPlus
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Username = model.Username;
                employee.Password = model.Password;
                employee.DOB = model.DOB;
                employee.Email = model.Email;
                employee.Address = model.Address;
                employee.Phone = model.Phone;
                employee.Gender = model.Gender;
                employee.Python = model.Python;
                employee.Java = model.Java;
                employee.CPlusPlus = model.CPlusPlus;
                employee.RecStatus = model.RecStatus;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Employee model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.RecStatus = 'D';

                // for deleting a record use the below code. 
                // mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
