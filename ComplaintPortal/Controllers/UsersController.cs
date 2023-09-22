using ComplaintPortal.Data;
using ComplaintPortal.Models;
using ComplaintPortal.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplaintPortal.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDbContext userDbContext;

        public UsersController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users =  await userDbContext.Users.ToListAsync();
            return View(users); 
        }
       
        [HttpGet]
        public IActionResult AddComplaint()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComplaint(AddComplaintViewModel addComplaintRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = addComplaintRequest.Name,
                Email = addComplaintRequest.Email,
                Complaint = addComplaintRequest.Complaint,
                Status = "Submmited"

            };
            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();
            return  RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(user != null)
            {
                var viewModel = new UpdateUserViewModel()
                {

                    Id = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    Complaint = user.Complaint,
                    Status = user.Status

                };
                return await Task.Run( () => View("View", viewModel));

            }



            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateUserViewModel model)
        {
            var user = await userDbContext.Users.FindAsync(model.Id);
            if(user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Complaint = model.Complaint;  
                user.Status = model.Status;
                
                await userDbContext.SaveChangesAsync();

                return RedirectToAction("Index");


            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUserViewModel model)
        {
            var user = await userDbContext.Users.FindAsync(model.Id);
            if(user != null)
            {
                userDbContext.Users.Remove(user);
                await userDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
