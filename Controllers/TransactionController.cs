using System;
using System.Collections.Generic;
using System.Globalization;
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
using Team_4_Project.Models.ViewModels;

namespace Team_4_Project.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: Transaction
        public IActionResult Index(Int32 AccountID)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (User.IsInRole("Manager"))
            {
                transactions = _context.Transaction.ToList();
            }
            if (User.IsInRole("Customer") && AccountID == 0)//user is customer but didn't specify account
            {
                transactions = _context.Transaction.Where(r => r.Account.User.UserName == User.Identity.Name).ToList();
            }
            if (User.IsInRole("Customer") && AccountID != 0)
            {
                transactions = _context.Transaction.Where(r => r.Account.AccountID == AccountID).ToList();
                //transactions = transactions.Where(r => r.Account.AccountID == AccountID)).ToList();
            }
            return View(transactions);

        }

        public IActionResult AllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            if (User.IsInRole("Manager"))
            {
                transactions = _context.Transaction.ToList();
            }
            return View("Index",transactions);
        }

        public IActionResult PendingTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            if (User.IsInRole("Manager"))
            {
                transactions = _context.Transaction.ToList();
            }
            return View(transactions);
        }


        public ActionResult TransactionSearch(Int32 accountID)
        {
            // populate viewbag with list of transaction types
            ViewBag.AllTransactionTypes = GetAllTransactionTypes();

            // create new instance of searchviewmodel
            TransactionSearchViewModel tsvm = new TransactionSearchViewModel();
            //tsvm.Account.AccountID = accountID;
            tsvm.SelectedTransactionType = TransacType.AllTransactions;
            tsvm.SearchDateRange = DateRange.AllDates;
            tsvm.SearchAmountRange = AmountRange.AllAmounts;

            return View(tsvm);
        }

        public ActionResult DisplaySearchResults(TransactionSearchViewModel tsvm)
        {
            //establish query to select all for the account
            var query = from t in _context.Transaction
                        .Include(t => t.Account)
                        select t;
            if (User.IsInRole("Employee") || User.IsInRole("Manager"))
            {
                query = query;
            }
            if (User.IsInRole("Customer"))
            {
                query = query.Where(t => t.Account.User.UserName == User.Identity.Name);
            }
            if (tsvm.SearchDescription != null && tsvm.SearchDescription != "")
            {
                query = query.Where(t => t.Description.Contains(tsvm.SearchDescription));
            }

            if (tsvm.SelectedTransactionType != TransacType.AllTransactions)
            {
                query = query.Where(t => t.TransactionType == tsvm.SelectedTransactionType);
            }

            switch (tsvm.SearchAmountRange)
            {
                case AmountRange.AllAmounts:
                    query = query;
                    break;
                case AmountRange.UpTo100:
                    query = query.Where(t => t.TransactionAmount <= 100);
                    break;

                case AmountRange.OneTo200:
                    query = query.Where(t => t.TransactionAmount >= 100 && t.TransactionAmount <= 200);
                    break;

                case AmountRange.TwoTo300:
                    query = query.Where(t => t.TransactionAmount >= 200 && t.TransactionAmount <= 300);
                    break;

                case AmountRange.Over300:
                    query = query.Where(t => t.TransactionAmount >= 300);
                    break;

                case AmountRange.CustomRange:
                    // make sure custom range is entered if user has selected that option
                    try
                    {
                        // try to perform the query based on custom range
                        query = query.Where(t => t.TransactionAmount >= tsvm.SearchAmountLower && t.TransactionAmount <= tsvm.SearchAmountUpper);
                    }
                    catch (Exception ex) // happens when user has not entered a valid custom range 
                    {
                        // set the ViewBag's error message
                        ViewBag.ErrorMessage = ex.Message;

                        // send the user back to the view
                        return View("TransactionSearch", tsvm);
                    }
                    break;
            }
            var today = DateTime.Now;

            switch (tsvm.SearchDateRange)
            {
                case DateRange.AllDates:
                    query = query;
                    break;

                case DateRange.Last15Days:
                    var searchingdate15 = today.AddDays(-15);
                    query = query.Where(t => t.TransactionDate >= searchingdate15);
                    break;

                case DateRange.Last30Days:
                    var searchingdate30 = today.AddDays(-30);
                    query = query.Where(t => t.TransactionDate >= searchingdate30);
                    break;

                case DateRange.Last60Days:
                    var searchingdate60 = today.AddDays(-60);
                    query = query.Where(t => t.TransactionDate >= searchingdate60);
                    break;

                case DateRange.CustomRange:
                    // make sure custom range is entered if user has selected that option
                    try
                    {
                        // display an error message if there is a problem
                        if (tsvm.SearchDateLower > tsvm.SearchDateUpper || tsvm.SearchDateUpper == null || tsvm.SearchDateLower == null) // invalid input
                        {
                            // set the ViewBag's error message
                            ViewBag.ErrorMessage = "Please enter a valid date range.";

                            //send the user back to the index view to try again
                            ViewBag.AllTransactionTypes = GetAllTransactionTypes();
                            tsvm.SelectedTransactionType = TransacType.AllTransactions;
                            tsvm.SearchDateRange = DateRange.AllDates;
                            tsvm.SearchAmountRange = AmountRange.AllAmounts;
                            return View("TransactionSearch", tsvm);
                        }
                        // try to perform the query based on custom range
                        query = query.Where(t => t.TransactionDate >= tsvm.SearchDateLower && t.TransactionDate <= tsvm.SearchDateUpper);
                    }
                    catch (Exception ex) // happens when user has not entered custom range 
                    {
                        // set the ViewBag's error message
                        ViewBag.ErrorMessage = ex.Message;

                        // send the user back to the view
                        return View("TransactionSearch", tsvm);
                    }
                    break;
            }


            List<Transaction> SelectedTransactions = query.OrderByDescending(t => t.TransactionNumber).ThenBy(t => t.TransactionType).ThenBy(t => t.Description)
                .ThenBy(t => t.TransactionAmount).ThenBy(t => t.TransactionDate).ToList();

            //TODO: make the viewbag counts specific to the individual user 
            ViewBag.AllTransactionsCount = _context.Transaction.Count();
            ViewBag.SelectedTransactionsCount = SelectedTransactions.Count();

            //TODO: need to fix orderby descending
            return View("Index", SelectedTransactions.OrderByDescending(b => b.TransactionDate));
        }

        // GET: Transaction/Create
        //This action has been modified to include an account id so that we 
        //will know which account to associate the transaction detail with
        public IActionResult Withdraw(Int32 accountID)
        {
            //Transaction tran = new Transaction();
            //tran.Account = _context.Account.Find(accountID);
            ViewBag.GetAllUsersAccounts = GetAllUsersAccounts();
            //make sure you pass the newly created transaction to the view
            return View();
        }

        // POST: Transaction/Deposit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw([Bind("TransactionType,TransactionID,TransactionNumber,TransactionAmount,Fees,TransactionDate,Description,Comments")] Transaction transaction, int SelectedAccount)
        {
            //figure out what type of account they selected
            //if statements to find the portfolio, 
            //find the correct account
            Account acc = _context.Account.FirstOrDefault(a => a.AccountID == SelectedAccount);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (acc.Status == Status.Inactive)
            {
                return View("Error", new String[] { "Account is inactive!" });
            }
            if (transaction.TransactionAmount > acc.Value)
            {
                return View("Error", new String[] { "Can't withdraw more than balance!" });
            }



            ////determine if the account for deposit is an IRA
            //if (acc.accountType == AccountType.IRA)
            //{
            //    //check their age
            //    int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            //    int dob = int.Parse(user.Birthday.ToString("yyyyMMdd"));
            //    int age = (now - dob) / 10000;
            //    if (age > 70)
            //    {
            //        return View("Error", new String[] { "Ol than 70" });
            //    }

            //    if (acc.Value > 5000)
            //    {
            //        return View("Error", new String[] { "Already contributed for this year" });
            //    }

            //    //transaction.Account = acc;
            //    ////add the transaction to the list of transactions for this account
            //    //acc.Transactions.Add(transaction);

            //    ////set transaction number, status, value
            //    //transaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
            //    transaction.TransactionType = TransacType.Contribution;
            //    transaction.Description = "Contribution";
            //}

            transaction.Account = acc;
            //add the transaction to the list of transactions for this account
            acc.Transactions.Add(transaction);

            //set transaction number
            transaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
            transaction.Status = TransactionStatus.Approved;
            transaction.TransactionType = TransacType.Withdrawal;
            acc.Value = (acc.Value - transaction.TransactionAmount);


            //transaction.TransactionDate = DateTime.Today;


            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();

                //modify the redirect to action to take you to the transaction details page
                return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        //This action has been modified to include an account id so that we 
        //will know which account to associate the transaction detail with
        public IActionResult Deposit(Int32 accountID)
        {
            //Transaction tran = new Transaction();
            //tran.Account = _context.Account.Find(accountID);
            ViewBag.GetAllUsersAccounts = GetAllUsersAccounts();
            //make sure you pass the newly created transaction to the view
            return View();
        }

        // POST: Transaction/Deposit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit([Bind("TransactionType,TransactionID,TransactionNumber,TransactionAmount,Fees,TransactionDate,Description,Comments")] Transaction transaction, int SelectedAccount)
        {
            //figure out what type of account they selected
            //if statements to find the portfolio, 
            //find the correct account
            Account acc = _context.Account.FirstOrDefault(a => a.AccountID == SelectedAccount);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (acc.Status==Status.Inactive)
            {
                return View("Error", new String[] { "Account is inactive!" });
            }

            //StockPortfolio portforlio = _context.StockPortfolios.Find(SelectedAccount);
            if (acc.accountType == AccountType.Stock)
            {
                acc.AvailableCash += transaction.TransactionAmount;
            }
            //IRAAccount ira = _context.

            //determine if the account for deposit is an IRA
            if (acc.accountType == AccountType.IRA)
            {
                //check their age
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(user.Birthday.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                if (age > 70)
                {
                    return View("Error", new String[] { "Older than 70" });
                }

                if (acc.Value > 5000)
                {
                    return View("Error", new String[] { "Already contributed for this year" });
                }

                //transaction.Account = acc;
                ////add the transaction to the list of transactions for this account
                //acc.Transactions.Add(transaction);

                ////set transaction number, status, value
                //transaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                transaction.TransactionType = TransacType.Contribution;
                transaction.Description = "Contribution";
            }

            transaction.Account = acc;
            //add the transaction to the list of transactions for this account
            acc.Transactions.Add(transaction);

            //set transaction number
            transaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
            if (acc.accountType != AccountType.IRA)
            {
                transaction.TransactionType = TransacType.Deposit;
            }
                
            if (transaction.TransactionAmount >= 5000)
            {
                transaction.Status = TransactionStatus.Pending;
            }
            else
            {
                transaction.Status = TransactionStatus.Approved;
                //acc.Value = (acc.Value + transaction.TransactionAmount);
                if (acc.accountType != AccountType.Stock)
                {
                    acc.Value = (acc.Value + transaction.TransactionAmount);
                }
            }
            if (acc.accountType != AccountType.IRA)
            {
                transaction.Description = "Deposit";
            }
                
            //transaction.TransactionDate = DateTime.Today;
            

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();

                //modify the redirect to action to take you to the transaction details page
                return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });
            }
            return View(transaction);
        }

        public ActionResult Transfer(Int32 accountID)
        {
            //Transaction tran = new Transaction();
            //tran.Account = _context.Account.Find(accountID);
            ViewBag.GetTransferAccounts = GetTransferAccounts();
            TransferViewModel tvm = new TransferViewModel();
            //make sure you pass the newly created transaction to the view
            return View(tvm);
        }

        //GET: Transaction/TransferConfirmation
        public ActionResult TransferConfirmation([Bind("FromAccountID,ToAccountID,TransferAmount,TransferDate, TransferComments")] TransferViewModel tvm)
        {
            //put data in the properties of the viewmodel?
            //TransferViewModel newtvm = new TransferViewModel();

            var ToAccount = _context.Account.Find(tvm.ToAccountID);
            var FromAccount = _context.Account.Find(tvm.FromAccountID);
            ViewBag.ToAccountName = ToAccount.AccountName;
            ViewBag.FromAccountName = FromAccount.AccountName;
            //var TransferAmount = tvm.TransferAmount;
            //ViewBag.TransferAmount = TransferAmount;
            //var TransferDate = tvm.TransferDate;
            //ViewBag.TransferDate = TransferDate;
            //var TransferComments = tvm.TransferComments;
            //ViewBag.TransferComments = TransferComments;
            //newtvm.FromAccountID = tvm.FromAccountID;
            //newtvm.ToAccountID = tvm.ToAccountID;
            //newtvm.TransferAmount = tvm.TransferAmount;
            //return RedirectToAction("TransferConfirmed", new { newtvm });
            return View(tvm);
        }

        public ActionResult TransferConfirmed(TransferViewModel tvm)
        {
            //find the accounts for the transfer
            var ToAccount = _context.Account.Include(a =>a.User).FirstOrDefault(a => a.AccountID == tvm.ToAccountID);
            var FromAccount = _context.Account.Include(a => a.User).FirstOrDefault(a => a.AccountID == tvm.FromAccountID);

            if (FromAccount.Status == Status.Inactive)
            {
                return View("Error", new String[] { "Account is inactive!" });
            }

            //AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser user = ToAccount.User;

            //TODO: adjust transfers based on which accounts they are transfering from
            //stocks and IRA will have specific transfer/distro rules.

            //create two transactions for the from and to accounts
            Transaction fromTransaction = new Transaction();
            Transaction toTransaction = new Transaction();

            if (FromAccount.Value < 0)
            {
                //if the account value is less than 0
                return View("Error", new String[] { "Account has a negative balance" });
            }

            //subtract the entered amount from the balance of the fromAccount
            if (tvm.TransferAmount > (FromAccount.Value+ 50))
            {
                //if the transfer amount is greater than their value and a $50 loan, result in error
                return View("Error", new String[] { "Not enough funds to complete transfer with $50 loan" });
            }

            if (FromAccount.accountType == AccountType.IRA)
            {
                //distribution is qualified if older than 65
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(user.Birthday.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                if (age > 65)
                {
                    fromTransaction.Distribution = Distribution.Qualified;
                }

                else
                {
                    Transaction unqualdistro = new Transaction();
                    unqualdistro.Account = FromAccount;
                    unqualdistro.TransactionType = TransacType.Fee;
                    unqualdistro.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                    unqualdistro.Status = TransactionStatus.Approved;
                    unqualdistro.Distribution = Distribution.Unqualified;
                    unqualdistro.TransactionDate = tvm.TransferDate;
                    unqualdistro.TransactionAmount = -30;
                    unqualdistro.Description = "Service Fee for Unqualified Distribution";
                    _context.Add(unqualdistro);
                    FromAccount.Transactions.Add(unqualdistro);
                    _context.SaveChanges();


                    if (tvm.TransferAmount > 3000)
                    {
                        _context.Remove(unqualdistro);
                        FromAccount.Transactions.Add(unqualdistro);
                        _context.SaveChanges();
                        return View("Error", new String[] { "Unqualified distribution cannot be greater than $3000" });
                    }
                }
            }

            if ((tvm.TransferAmount > FromAccount.Value) && (tvm.TransferAmount - FromAccount.Value) <= 50)
            {
                //bank loans the user up to 50 -> the difference between the subtraction and 50
                var LoanAmount = 50 - (tvm.TransferAmount - FromAccount.Value);

                //begin the transfer from the account
                fromTransaction.Account = FromAccount;
                fromTransaction.TransactionType = TransacType.Transfer;
                fromTransaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                fromTransaction.Status = TransactionStatus.Approved;
                fromTransaction.TransactionDate = tvm.TransferDate;
                fromTransaction.TransactionAmount = tvm.TransferAmount;
                fromTransaction.Description = "Transfer to account " + ToAccount.AccountName;
                fromTransaction.Comments = tvm.TransferComments;

                //decrease the balance in the FromAccount based on the transfer
                FromAccount.Value = FromAccount.Value - tvm.TransferAmount;

                //complete the overdraft transaction
                Transaction OverdraftTransaction = new Transaction();
                OverdraftTransaction.Account = FromAccount;
                OverdraftTransaction.TransactionType = TransacType.Fee;
                OverdraftTransaction.TransactionNumber = (1+ Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context));
                OverdraftTransaction.Status = TransactionStatus.Approved;
                OverdraftTransaction.TransactionDate = tvm.TransferDate;
                OverdraftTransaction.TransactionAmount = -30;
                OverdraftTransaction.Description = "Overdraft Fee";
                OverdraftTransaction.Comments = "Transaction " + fromTransaction.TransactionNumber + " resulted in an overdraft";

                //decrease the balance in the FromAccount based on the overdraft fee
                FromAccount.Value = FromAccount.Value - 30;

                //complete transfer to other account
                toTransaction.Account = ToAccount;
                toTransaction.TransactionType = TransacType.Transfer;
                toTransaction.TransactionNumber = (2+Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context));
                toTransaction.Status = TransactionStatus.Approved;
                toTransaction.TransactionDate = tvm.TransferDate;
                toTransaction.TransactionAmount = tvm.TransferAmount;
                toTransaction.Description = "Transfer from account " + FromAccount.AccountName;
                toTransaction.Comments = tvm.TransferComments;

                //add to the balance of the ToAccount based on the transfer
                ToAccount.Value = ToAccount.Value + tvm.TransferAmount;

                if (ModelState.IsValid)
                {
                    _context.Add(fromTransaction);
                    FromAccount.Transactions.Add(fromTransaction);
                    FromAccount.Transactions.Add(OverdraftTransaction);
                    _context.Add(toTransaction);
                    ToAccount.Transactions.Add(toTransaction);
                    _context.Add(OverdraftTransaction);
                    _context.SaveChanges();

                    //return to transactions index to view new transactions
                    return RedirectToAction("Index");
                }
            }

            else
            {
                //begin the transfer from the account
                fromTransaction.Account = FromAccount;
                fromTransaction.TransactionType = TransacType.Transfer;
                fromTransaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                fromTransaction.Status = TransactionStatus.Approved;
                fromTransaction.TransactionDate = tvm.TransferDate;
                fromTransaction.TransactionAmount = tvm.TransferAmount;
                fromTransaction.Description = "Transfer to account " + ToAccount.AccountName;
                fromTransaction.Comments = tvm.TransferComments;

                //decrease the balance in the FromAccount based on the transfer
                FromAccount.Value = FromAccount.Value - tvm.TransferAmount;

                //complete transfer to other account
                toTransaction.Account = ToAccount;
                toTransaction.TransactionType = TransacType.Transfer;
                toTransaction.TransactionNumber = (1+Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context));
                toTransaction.Status = TransactionStatus.Approved;
                toTransaction.TransactionDate = tvm.TransferDate;
                toTransaction.TransactionAmount = tvm.TransferAmount;
                toTransaction.Description = "Transfer from account " + FromAccount.AccountName;
                toTransaction.Comments = tvm.TransferComments;

                //add to the balance of the ToAccount based on the transfer
                ToAccount.Value = ToAccount.Value + tvm.TransferAmount;


                //do this in the confirmation page?
                if (ModelState.IsValid)
                {
                    _context.Add(fromTransaction);
                    FromAccount.Transactions.Add(fromTransaction);
                    _context.Add(toTransaction);
                    ToAccount.Transactions.Add(toTransaction);
                    _context.SaveChanges();

                    //return to transactions index to view new transactions
                    return RedirectToAction("Index");
                }
            }
            return View("Transfer");
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO: hide option to dispute transaction if they have already had a dispute accepted
            List<Disputes> TransactionDisputes = _context.Disputes.Where(d => d.Transaction.TransactionID == id).ToList();
            var transaction = await _context.Transaction
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            transaction.Disputes = TransactionDisputes;
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = _context.Transaction
                .Include(a => a.Account)
                .FirstOrDefault(t => t.TransactionID == id);
                            
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionType,TransactionID,TransactionNumber,TransactionAmount,Fees,TransactionDate,Description,Comments")] Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return NotFound();
            }

            
            Transaction DBTran = _context.Transaction
                .Include(a => a.Account)
                .FirstOrDefault(t => t.TransactionID == transaction.TransactionID);
            Account DBAcc = _context.Account.FirstOrDefault(b => b.AccountID == DBTran.Account.AccountID);
            DBAcc.Value = DBAcc.Value + transaction.TransactionAmount;
            DBTran.Status = TransactionStatus.Approved;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(DBTran);
                    _context.Update(DBAcc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionID))
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
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = _context.Transaction
                .Include(t => t.Disputes)
                .FirstOrDefault(t => t.TransactionID == id);
            //Disputes deleteDisp = _context.Disputes.Where(d => d.Transaction.TransactionID == id)
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetAllUsersAccounts()
        {
            //get all of the user's accounts from the database
            List<Account> accountList = _context.Account.Where(r => r.User.UserName == User.Identity.Name).ToList();

            //convert the list into a SelectList by calling the SelectList constructor
            SelectList accountSelectList = new SelectList(accountList.OrderBy(a => a.AccountID), "AccountID", "AccountName");

            return accountSelectList;
        }
        private SelectList GetTransferAccounts()
        {
            //get all of the user's accounts from the database
            List<Account> accountList = _context.Account.Where(r => r.User.UserName == User.Identity.Name).ToList();

            //convert the list into a SelectList by calling the SelectList constructor
            SelectList accountSelectList = new SelectList(accountList.OrderBy(a => a.AccountID), "AccountID", "AccountName", "PartialAccountNumber", "Value");

            return accountSelectList;
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.TransactionID == id);
        }
        private SelectList GetAllTransactionTypes()
        {
            List<TransacType> typelist = Enum.GetValues(typeof(TransacType)).Cast<TransacType>().ToList();
            SelectList typeSelectList = new SelectList(typelist);
            return typeSelectList;
        }
        
    }
}
