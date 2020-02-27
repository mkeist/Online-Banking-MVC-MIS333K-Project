using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//Change these using statements to match your project
using Team_4_Project.DAL;
using Team_4_Project.Models;

//Change this namespace to match your project
namespace Team_4_Project.Controllers
{
    //Uncomment this line once you have roles working correctly
    //uncommented to restrict access to editing roles so that only admins can do it
    [Authorize(Roles = "Manager")]
    public class RoleAdminController : Controller
    {
        private AppDbContext _db;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private PasswordValidator<AppUser> _passwordValidator;


        public RoleAdminController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signIn)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signIn;
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }


        // GET: /RoleAdmin/
        public async Task<ActionResult> Index()
        {
            List<RoleEditModel> roles = new List<RoleEditModel>();
            
            foreach (IdentityRole role in _roleManager.Roles)
            {
                List<AppUser> members = new List<AppUser>();
                List<AppUser> nonMembers = new List<AppUser>();
                foreach (AppUser user in _userManager.Users)
                {
                    var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                    list.Add(user);
                }
                RoleEditModel re = new RoleEditModel();
                re.Role = role;
                re.Members = members;
                re.NonMembers = nonMembers;
                roles.Add(re);
            }
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            //if code gets this far, we need to show an error
            return View(name);
        }

        public async Task<ActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel { Role = role, Members = members, NonMembers = nonMembers });
        }


        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Role Not Found" });
        }

        //GET: list of all users that we iterate though
        private SelectList GetAllUsers()
        {
            //get all of the user's accounts from the database
            List<AppUser> userList = _db.Users.Where(r => r.Id == User.Identity.Name).ToList();

            //convert the list into a SelectList by calling the SelectList constructor
            SelectList userSelectList = new SelectList(userList.OrderBy(a => a.Id), "Id", "NormalizedEmail");

            return userSelectList;
        }

        public IActionResult ManageAllCustomers()
        {
            //Transaction tran = new Transaction();
            //tran.Account = _context.Account.Find(accountID);
            List<AppUser> allusers = new List<AppUser>();
            allusers = _db.Users.ToList();
           // var users = allusers.Where(r => r.Id == User.Identity.Name).ToList();
            //ViewBag.GetAllUsers = GetAllUsers();
            //make sure you pass the newly created transaction to the view
            //List<AppUser> users = new List<AppUser>();
                
            //users = _db.Users.ToList();
                
            return View(allusers);
            
        }



        //GET: list of all users that we iterate though
        private SelectList GetAllEmployees()
        {
            //get all of the user's accounts from the database
            List<AppUser> userList = _db.Users.Where(r => r.Id == User.Identity.Name).ToList();

            //convert the list into a SelectList by calling the SelectList constructor
            SelectList userSelectList = new SelectList(userList.OrderBy(a => a.Id), "Id", "NormalizedEmail");

            return userSelectList;
        }

        public async Task<IActionResult> ManageAllEmployees()
        {
            //Transaction tran = new Transaction();
            //tran.Account = _context.Account.Find(accountID);
            //List<AppUser> allemp = new List<AppUser>();       --used to use
            //allemp = _db.Users.ToList();      --used to use

            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Employee"))
                {
                    users.Add(user);
                }
            }
            return View(users);

            //allemployees = _db.Users.Include(r => r.).ToList();
            //allemployees = _db.Users.Where(r => r.Id).ToList();

            //return View(allemp);      --used to use

        }



        //GET: RoleAdmin/Edit
        public IActionResult EditUser(string Email)
        {
            //AppUser user = _db.Users.FirstOrDefault(u => u.UserName == UName);
            if (Email == null)
            {
                return View("Error", new String[] { "Please specify the account you wish to edit!" });
            }

            //AppUser user = _db.Users.Find(Email);
            AppUser user = _db.Users.FirstOrDefault(u => u.Email == Email);

            return View(user);
        }

        //POST: RoleAdmin/Edit
        [HttpPost]
        public async Task<IActionResult> EditUser(string Email, AppUser appuser)
        {
            try
            {
                //var context = new Models.ApplicationDbContext();
                var user1 = await _userManager.FindByIdAsync(appuser.Id);
                //var idd = await _userManager.GetUserId(user);
                AppUser DBUser = _db.Users.Find(appuser.Id);

                var userId = _db.Users
                    .Where(m => m.Id == appuser.Id)
                    .Select(m => m.Email)
                    .SingleOrDefault();
                var ids = appuser.Id;

                AppUser id = _db.Users.FirstOrDefault(i => i.Email == Email);
                //AppUser ide = _db.Users.Include(a => a.Id);

                String UName = Email;
                AppUser user = _db.Users.FirstOrDefault(u => u.UserName == UName);
                //context.Entry(appuser).State = EntityState.Modified;
                //user.Email = appuser.Email;
                //user.UserName = appuser.UserName;
                user.FirstName = appuser.FirstName;
                //appuser.FirstName = user.FirstName;
                user.LastName = appuser.LastName;
                user.MiddleInitial = appuser.MiddleInitial;
                user.StreetAddress = appuser.StreetAddress;
                user.CityAddress = appuser.CityAddress;
                user.StateAddress = appuser.StateAddress;
                user.ZipCodeAddress = appuser.ZipCodeAddress;
                user.PhoneNumber = appuser.PhoneNumber;
                //user.PasswordHash = appuser.PasswordHash;
                _db.Update(user);
                //await _db.SaveChangesAsync();
                //user.PasswordHash = user.PasswordHash;
                _db.SaveChanges();
                await _db.SaveChangesAsync();
            //var user = context.Users.Where(u => u.Id == id.ToString()).FirstOrDefault();
            //return View("Index");
        }

            catch (DbUpdateConcurrencyException)
            {
                if (Email != appuser.Email)
                {
                    return NotFound();
    }
                else
                {
                    throw;
                }
            }
            //return View("ManageAllCustomers");
            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(ManageAllCustomers));

        }


        //Logic for change password
        // GET: /User/ChangePassword
        public ActionResult ChangePassword(string Email)
        {
            AppUser user = _db.Users.FirstOrDefault(u => u.Email == Email);
            ChangePasswordViewModel1 cpvm = new ChangePasswordViewModel1();
            cpvm.Email = Email;
            return View(cpvm);
        }

        //
        // POST: /User/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel1 model, string Email)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            String UName = Email;
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == UName);
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
            //var result = await _userManager.ChangePasswordAsync(user, model.NewPassword, model.NewPassword);
            if (passwordChangeResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            AddErrors(passwordChangeResult);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        { 
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        

   }
}