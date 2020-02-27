using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Team_4_Project.DAL;
using Team_4_Project.Models;

namespace Team_4_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public AccountController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: Account
        //get all the user's accounts from the database 
        public IActionResult Index()
        {
            List<Account> accounts = new List<Account>();

            //accounts = _context.Account.Include(r=>r.StockPortions).ThenInclude(s => s.Stock).Where(r => r.User.UserName == User.Identity.Name).ToList();
            //Account sp = accounts.FirstOrDefault(r => r.accountType == AccountType.Stock);

            //Decimal stockportionval = 0;
            //foreach (var stk in sp.StockPortions)
            //{
            //    stockportionval += stk.Stock.CurrentPrice * stk.ShareNumber;
            //}
            //sp.CashValPortion = sp.Gains - sp.CashValueFees + sp.Bonuses + sp.AvailableCash;
            //sp.Value = sp.CashValPortion + stockportionval;

            //ViewBag.Value = "$" + sp.Value;

            if (User.IsInRole("Manager"))
            {
                //TODO: add an Include statement to include transactions?
                accounts = _context.Account.ToList();
            }
            else //user is customer
            {
                //TODO: add an Include statement to include transactions?
                accounts = _context.Account.Where(r => r.User.UserName == User.Identity.Name).ToList();
            }
            return View(accounts);
        }

        //GET: Confirmation
        public IActionResult Confirmation()
        {
            return View();
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO: add an Include statement to include transactions?
            Account accounts = await _context.Account
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.AccountID == id);

            if (accounts == null)
            {
                return View("Error", new String[] { "Cannot find this account!" });
            }

            if (User.IsInRole("Manager") == false && accounts.User.UserName != User.Identity.Name) //they are trying to see something that isn't theirs
            {
                return View("Error", new String[] { "Unauthorized: You are attempting to view another customer's account!" });
            }

            return View(accounts);
        }

        // GET: Account/Create
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountID,AccountNumber,AccountName,Status,accountType,User")] Account account)
        {
            // get user that is logged in
            account.User = await _userManager.FindByNameAsync(User.Identity.Name);

            //set account number
            account.AccountNumber = Utilities.GenerateNextAccountNumber.GetNextAccountNumber(_context);

            // set partial account number
            account.PartialAccountNumber = account.AccountNumber.ToString();
            account.PartialAccountNumber = "******" + account.PartialAccountNumber.Substring(account.PartialAccountNumber.Length - 4);

            //determine how many ira/stock accounts this person has
            var query = from a in _context.Account
                        select a;
            var IRACount = query.Where(a => a.User == account.User && a.accountType == AccountType.IRA).ToList().Count();
            var StockCount = query.Where(a => a.User == account.User && a.accountType == AccountType.Stock).ToList().Count();

            //case statements to set account name based on type of account
            if (account.accountType == AccountType.Checking)
            {
                if (account.AccountName is null)
                {
                    account.AccountName = "Longhorn Checking";
                }
                //need to redirect to InitialDeposit view
                if (ModelState.IsValid)
                {
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("InitialDeposit", new { id = account.AccountID });
                }
            }
            if (account.accountType == AccountType.Savings)
            {
                if (account.AccountName is null)
                {
                    account.AccountName = "Longhorn Savings";
                }
                //need to redirect to InitialDeposit view
                if (ModelState.IsValid)
                {
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("InitialDeposit", new { id = account.AccountID });
                }
            }
            if (account.accountType == AccountType.IRA)
            {
                if (account.AccountName is null)
                {
                    account.AccountName = "Longhorn IRA";
                }
                //need to redirect to IRA controller views
                if (ModelState.IsValid && IRACount < 1)
                {
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("InitialDeposit", new { id = account.AccountID });
                }
                else
                {
                    return View("Error", new String[] { "You already have an IRA account!" });
                }

            }
            if (account.accountType == AccountType.Stock)
            {
                account.User = await _userManager.FindByNameAsync(User.Identity.Name);
                if (account.AccountName is null)
                {
                    account.AccountName = "Longhorn Stock Portfolio";
                }
                if (ModelState.IsValid && StockCount < 1)
                {

                    account.accountType = AccountType.Stock;
                    account.Gains = 0m;
                    account.Bonuses = 0m;
                    account.Balanced = Balanced.Unbalanced;
                    account.Status = Status.Active;
                    account.Value = 0m;
                    account.AvailableCash = 0m;
                    account.CashValPortion = 0;

                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("InitialDeposit", new { id = account.AccountID });

                }
                else
                {
                    return View("Error", new String[] { "You already have an Stock portfolio!" });
                }
                //need to redirect to stock controller views
            }
            return View(account);

        }
        //GET: Account/InitialDeposit/5
        public IActionResult InitialDeposit(int? id)
        {
            Account account = _context.Account.Find(id);
            if (account == null)
            {
                return View("Error", new String[] { "Cannot find the account for initial deposit!" });
            }
            return View(account);
        }

        //POST: Account/InitialDeposit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InitialDeposit(int id, [Bind("AccountID,AccountNumber,AccountName,Status, accountType, Value")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            //find the correct account to assign the initial deposit
            Account DBAcc = await _context.Account
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.AccountID == id);

            //create a transaction for the initial deposit
            Transaction transaction = new Transaction();
            transaction.Account = DBAcc;
            transaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
            transaction.TransactionType = TransacType.Deposit;
            if (account.Value >= 5000)
            {
                transaction.Status = TransactionStatus.Pending;
            }
            if (account.Value <= 0)
            {
                ViewBag.ErrorMessage = "Initial deposit must be greater than $0.";
                return View("InitialDeposit", account);
            }
            else
            {
                //set the value for the account in the database equal to what the user entered
                transaction.Status = TransactionStatus.Approved;
                if (DBAcc.accountType == AccountType.Stock)
                {
                    DBAcc.AvailableCash = account.Value;
                }
                else
                {
                    DBAcc.Value = account.Value;
                }
            }
            transaction.TransactionAmount = account.Value;
            transaction.Description = "Initial Deposit";
            transaction.TransactionDate = DateTime.Today;
            DBAcc.Transactions.Add(transaction);

            if (ModelState.IsValid)
            {
                _context.Update(DBAcc);
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                //redirect to Account/Index
                //return RedirectToAction("Details", "Orders", new { id = dbOD.Order.OrderID });
                return RedirectToAction(nameof(Confirmation));
            }
            return View(DBAcc);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify the account you wish to edit!" });
            }

            //TODO: add an Include statement to include transactions?
            Account account = await _context.Account
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (account == null)
            {
                return View("Error", new String[] { "Cannot find the account you wish to edit!" });
            }
            if (User.IsInRole("Manager") == false && account.User.UserName != User.Identity.Name)
            {
                return View("Error", new String[] { "Unauthorized: You are attempting to edit another user's account!" });
            }
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountID,AccountNumber,AccountName,Status")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Account DBAcc = _context.Account.Find(account.AccountID);
                    if(User.Identity.IsAuthenticated && User.IsInRole("Manager"))
                    {
                        if (account.Status == Status.Active)
                        {
                            DBAcc.Status = Status.Active;
                        }
                        if (account.Status == Status.Inactive)
                        {
                            DBAcc.Status = Status.Inactive;
                        }
                    }
                    DBAcc.AccountName = account.AccountName;
                    _context.Update(DBAcc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountID))
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
            return View(account);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.AccountID == id);
        }
    }
}
