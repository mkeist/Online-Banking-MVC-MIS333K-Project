using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team_4_Project.DAL;
using Team_4_Project.Models;
using Team_4_Project.Models.ViewModels;

namespace Team_4_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //_context = context;
            //adding comment to test github
            //adding another comment
            List<Account> accounts = _context.Account.Where(r => r.User.UserName == User.Identity.Name).ToList();
            Int32 countofaccounts = accounts.Count();
            ViewBag.CountofAccounts = countofaccounts;

            List<Transaction> transactions = _context.Transaction.Where(o => o.Status == TransactionStatus.Pending).ToList();
            Int32 countofpending = transactions.Count();
            ViewBag.CountofTransactionPending = countofpending;

            List<Disputes> disputes = _context.Disputes.Where(o => o.DisputeStatus == DisputeStatus.Submitted).ToList();
            Int32 countofsubmitted = disputes.Count();
            ViewBag.CountofDisputesPending = countofsubmitted;
            return View();
        }
    }
}
