using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team_4_Project.DAL;
using Team_4_Project.Models;
using Team_4_Project.Models.ViewModels;

namespace Team_4_Project.Controllers
{
    public class PayeeController : Controller
    {
        private readonly AppDbContext _context;

        public PayeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Payee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payees.ToListAsync());
        }

        // GET: Payee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payee = await _context.Payees
                .FirstOrDefaultAsync(m => m.PayeeID == id);
            if (payee == null)
            {
                return NotFound();
            }

            return View(payee);
        }

        // GET: Payee/Create
        [Authorize (Roles="Customer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayeeID,Name,PayeeStreetAddress,PayeeCityAddress,PayeeStateAddress,PayeeZipCode,PayeePhoneNumber,PayeeType")] Payee payee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payee);
        }

        // GET: Payee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payee = await _context.Payees.FindAsync(id);
            if (payee == null)
            {
                return NotFound();
            }
            return View(payee);
        }

        // POST: Payee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PayeeID,Name,PayeeStreetAddress,PayeeCityAddress,PayeeStateAddress,PayeeZipCode,PayeePhoneNumber,PayeeType")] Payee payee)
        {
            if (id != payee.PayeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayeeExists(payee.PayeeID))
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
            return View(payee);
        }

        // GET: Payee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payee = await _context.Payees
                .FirstOrDefaultAsync(m => m.PayeeID == id);
            if (payee == null)
            {
                return NotFound();
            }

            return View(payee);
        }

        // POST: Payee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payee = await _context.Payees.FindAsync(id);
            _context.Payees.Remove(payee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayeeExists(int id)
        {
            return _context.Payees.Any(e => e.PayeeID == id);
        }

        //create a function for the payeeSearchViewModel
        public ActionResult PaymentInfo(int PaymentAmount, int FromAccountID, DateTime PaymentDate)
        {
            // populate viewbag with the payment info
            //ViewBag.PaymentInfo = GetPaymentAmount();     // still need to create this

            // create new instance of searchviewmodel
            PayeeSearchViewModel psvm = new PayeeSearchViewModel();
            //psvm.FromAccountID = model.FromAccountID;
            psvm.PaymentAmount = psvm.PaymentAmount;
            psvm.PaymentDate = psvm.PaymentDate;

            return View(psvm);

        }
        public ActionResult PayBill(Int32 accountID)
        {
            //Transaction tran = new Transaction();
            //tran.Account = _context.Account.Find(accountID);
            ViewBag.GetAllPayees = GetAllPayees2();
            ViewBag.GetAccounts = GetAllAccounts();
            PayeeSearchViewModel psvm = new PayeeSearchViewModel();
            //make sure you pass the newly created transaction to the view
            return View(psvm);
        }

        public ActionResult PayeeSearch(string PayeeID)
        {
            // populate viewbag with list of transaction types
            ViewBag.AllPayeeTypes = GetAllPayees();


            return View();
        }

        private SelectList GetAllPayees()
        {
            List<PayeeType> typelist = Enum.GetValues(typeof(PayeeType)).Cast<PayeeType>().ToList();
            SelectList typeSelectList = new SelectList(typelist);
            return typeSelectList;
        }

        private SelectList GetAllPayees2()
        {
            List<Payee> payeesList = _context.Payees.ToList();
            SelectList AllPayeeSelectList = new SelectList(payeesList, "PayeeID", "Name", "PayeeType", "PayeeType");
            //SelectList PayeeSelectList -new SelectList(_context.Payees, "PayeeID", "Name");
            return AllPayeeSelectList;
        }

        private SelectList GetAllAccounts()
        {
            //get all of the user's accounts from the database
            List<Account> accountList = _context.Account.Where(r => r.User.UserName == User.Identity.Name).ToList();
            //convert the list into a SelectList by calling the SelectList constructor
            SelectList accountSelectList = new SelectList(accountList.OrderBy(a => a.AccountID), "AccountID", "AccountName", "PartialAccountNumber", "Value");

            return accountSelectList;
        }

        public ActionResult PayBillConfirmation([Bind("FromAccountID,PaymentAmount,PaymentDate,PayeeID")] PayeeSearchViewModel psvm)
        {
            var FromAccount = _context.Account.Find(psvm.FromAccountID);
            var Payee = _context.Payees.Find(psvm.PayeeID);
            //ViewBag.ToAccountName = ToAccount.AccountName;
            ViewBag.FromAccountName = FromAccount.AccountName;
            var Value = psvm.PaymentAmount;
            ViewBag.PaymentAmount = Value;
            var PaymentDate = psvm.PaymentDate;
            ViewBag.PaymentDate = PaymentDate;
            ViewBag.FromPayee = Payee.Name;
            //var TransferComments = psvm.TransferComments;
            //ViewBag.TransferComments = TransferComments;
            return View(psvm);
        }

        public ActionResult PayBillConfirmed(PayeeSearchViewModel psvm)
        {
            //find the accounts for the transfer
            var Payee = _context.Payees.FirstOrDefault(a => a.PayeeID == psvm.PayeeID);
            var FromAccount = _context.Account.FirstOrDefault(a => a.AccountID == psvm.FromAccountID);

            //TODO: adjust transfers based on which accounts they are transfering from
            //stocks and IRA will have specific transfer/distro rules.

            //create two transactions for the from and to accounts
            Transaction PaymentTransaction = new Transaction();

            if (FromAccount.Value < 0)
            {
                //if the account value is less than 0
                return View("Error", new String[] { "Account has a negative balance" });
            }

            //subtract the entered amount from the balance of the fromAccount
            if (psvm.PaymentAmount > (FromAccount.Value + 50))
            {
                //if the transfer amount is greater than their value and a $50 loan, result in error
                return View("Error", new String[] { "Not enough funds to complete transfer with $50 loan" });
            }

            if ((psvm.PaymentAmount > FromAccount.Value) && (psvm.PaymentAmount - FromAccount.Value) <= 50)
            {
                //bank loans the user up to 50 -> the difference between the subtraction and 50
                var LoanAmount = 50 - (psvm.PaymentAmount - FromAccount.Value);

                //begin the transfer from the account
                PaymentTransaction.Account = FromAccount;
                PaymentTransaction.TransactionType = TransacType.Transfer;
                PaymentTransaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                PaymentTransaction.Status = TransactionStatus.Approved;
                PaymentTransaction.TransactionDate = psvm.PaymentDate;
                PaymentTransaction.TransactionAmount = psvm.PaymentAmount;
                PaymentTransaction.Description = "Payment to bill " + Payee.Name;
                //fromTransaction.Comments = tvm.TransferComments;

                //decrease the balance in the FromAccount based on the Payment amount
                FromAccount.Value = FromAccount.Value - psvm.PaymentAmount;

                //complete the overdraft transaction
                Transaction OverdraftTransaction = new Transaction();
                OverdraftTransaction.Account = FromAccount;
                OverdraftTransaction.TransactionType = TransacType.Fee;
                OverdraftTransaction.TransactionNumber = (1 + Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context));
                OverdraftTransaction.Status = TransactionStatus.Approved;
                OverdraftTransaction.TransactionDate = psvm.PaymentDate;
                OverdraftTransaction.TransactionAmount = -30;
                OverdraftTransaction.Description = "Overdraft Fee";
                OverdraftTransaction.Comments = "Transaction " + PaymentTransaction.TransactionNumber + " resulted in an overdraft";

                //decrease the balance in the FromAccount based on the overdraft fee
                FromAccount.Value = FromAccount.Value - 30;

                if (ModelState.IsValid)
                {
                    _context.Add(PaymentTransaction);
                    FromAccount.Transactions.Add(PaymentTransaction);
                    FromAccount.Transactions.Add(OverdraftTransaction);
                    _context.Add(PaymentTransaction);
                    //ToAccount.Transactions.Add(toTransaction);
                    _context.SaveChangesAsync();

                    //return to transactions index to view new transactions
                    return View("PayBillConfirmation");
                }
            }

            else
            {
                //begin the transfer from the account
                PaymentTransaction.Account = FromAccount;
                PaymentTransaction.TransactionType = TransacType.Withdrawal;
                PaymentTransaction.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                PaymentTransaction.Status = TransactionStatus.Approved;
                PaymentTransaction.TransactionDate = psvm.PaymentDate;
                PaymentTransaction.TransactionAmount = psvm.PaymentAmount;
                PaymentTransaction.Description = "Payment to bill " + Payee.Name;
                //fromTransaction.Comments = tvm.TransferComments;

                //decrease the balance in the FromAccount based on the transfer
                FromAccount.Value = FromAccount.Value - psvm.PaymentAmount;

                //do this in the confirmation page?
                if (ModelState.IsValid)
                {
                    _context.Add(PaymentTransaction);
                    FromAccount.Transactions.Add(PaymentTransaction);
                    _context.Add(PaymentTransaction);
                    _context.SaveChanges();

                    //return to transactions index to view new transactions
                    return View("PayBillConfirmation");
                }
            }
            return View("PayBill");
        }
    }
}
