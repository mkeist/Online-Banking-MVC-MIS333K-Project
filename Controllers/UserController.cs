using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//TODO: Change this using statement to match your project
using Team_4_Project.DAL;
using Team_4_Project.Models;

//TODO: Change this namespace to match your project
namespace Team_4_Project.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _db;


        public UserController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signIn;
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        // GET: /User/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /User/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    //Add the rest of the custom user fields here
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    StreetAddress = model.StreetAddress,
                    CityAddress = model.CityAddress,
                    StateAddress = model.StateAddress,
                    ZipCodeAddress = model.ZipCodeAddress,
                    PhoneNumber = model.PhoneNumber,
                    Birthday = model.Birthday,
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: Add user to desired role. This example adds the user to the customer role
                    await _userManager.AddToRoleAsync(user, "Customer");

                    Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        // GET: /User/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards User lockout
            // To enable password failures to trigger User lockout, change to shouldLockout: true
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        //GET: User/Index
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();
            RegisterViewModel rvm = new RegisterViewModel();

            //get user info
            String id = User.Identity.Name;
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == id);

            //populate the view model
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            rvm.FirstName = user.FirstName;
            rvm.LastName = user.LastName;
            rvm.MiddleInitial = user.MiddleInitial;
            rvm.StreetAddress = user.StreetAddress;
            rvm.CityAddress = user.CityAddress;
            rvm.StateAddress = user.StateAddress;
            rvm.ZipCodeAddress = user.ZipCodeAddress;
            rvm.PhoneNumber = user.PhoneNumber;
            rvm.Email = user.Email;


            //send data to the view
            return View(rvm);
        }

        //GET: User/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            String UName = User.Identity.Name;
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == UName);
            if (id == null)
            {
                return View("Error", new String[] { "Please specify the account you wish to edit!" });
            }
            return View(user);
        }

        //POST: User/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AppUser appuser)
        {
            try
            {
                //var context = new Models.ApplicationDbContext();
                //AppUser DBUser = _db.Users.Find(appuser.Id);
                String UName = User.Identity.Name;
                AppUser user = _db.Users.FirstOrDefault(u => u.UserName == UName);
                //context.Entry(appuser).State = EntityState.Modified;
                //user.Email = appuser.Email;
                //user.UserName = appuser.UserName;
                if (User.IsInRole("Customer"))
                    {
                    user.FirstName = appuser.FirstName;
                    //appuser.FirstName = user.FirstName;
                    appuser.LastName = user.LastName;
                    user.MiddleInitial = appuser.MiddleInitial;
                    }
                

                user.StreetAddress = appuser.StreetAddress;
                user.CityAddress = appuser.CityAddress;
                user.StateAddress = appuser.StateAddress;
                user.ZipCodeAddress = appuser.ZipCodeAddress;
                user.PhoneNumber = appuser.PhoneNumber;
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
                if (id.ToString() != appuser.Id)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
        }

        //Logic for change password
        // GET: /User/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /User/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(userLoggedIn, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(userLoggedIn, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        //GET:/User/AccessDenied
        public ActionResult AccessDenied(String ReturnURL)
        {
            return View("Error", new string[] { "Access is denied" });
        }

        // POST: /User/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}