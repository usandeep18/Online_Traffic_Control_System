using ComplaintPortal.Data;
using ComplaintPortal.Models;
using ComplaintPortal.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplaintPortal.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserDbContext userDbContext;
        public EmployeesController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
       
        [HttpGet]
        public async Task<IActionResult> EmployeeListIndex()
        {
            var employees = await userDbContext.Employees.ToListAsync();
            // return view if the list contains valid employee i.e check email and password in list
            return View(employees);
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> AddEmployee(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Password = addEmployeeRequest.Password
            };
            await userDbContext.Employees.AddAsync(employee);  
            await userDbContext.SaveChangesAsync();
            return RedirectToAction("ViewEmployeeLogin");
              
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            // retrieves the single employee
            var employee = await userDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {

                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await userDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;              
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await userDbContext.SaveChangesAsync();

                return RedirectToAction("EmployeeListIndex");
            }
            return RedirectToAction("EmployeeListIndex"); // change this to send it to error page 
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await userDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                userDbContext.Employees.Remove(employee);
                await userDbContext.SaveChangesAsync();

                return RedirectToAction("EmployeeListIndex");
            }
            return RedirectToAction("EmployeeListIndex");
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployeeLogin()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ViewEmployeeLogin(string email, string password)
        {
            var employee = await userDbContext.Employees.
                FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

            if (employee != null)
            {
                return RedirectToAction("EmployeeListIndex");
            }
            else
                return RedirectToAction("ViewEmployeeLogin");

        }
    }
}
