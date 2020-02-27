using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Team_4_Project.DAL;
using Team_4_Project.Models;
using System.Net.Mail;
using System.Net;

namespace Team_4_Project.Controllers
{
    public class DisputesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DisputesController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }


        // GET: Disputes
        public IActionResult Index()
        {
            List<Disputes> disputes = new List<Disputes>();
            if (User.IsInRole("Manager"))
            {
                disputes = _context.Disputes.ToList();
            }
            else //user is customer
            {
                //TODO: add an Include statement to include transactions?
                //disputes = _context.Disputes.Where(r => r.User.UserName == User.Identity.Name).ToList();
            }
            return View(disputes);
        }

        // GET: Disputes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disputes = await _context.Disputes
                .FirstOrDefaultAsync(m => m.DisputesID == id);
            if (disputes == null)
            {
                return NotFound();
            }

            return View(disputes);
        }

        // GET: Disputes/Create
        public IActionResult Create(int transactionID)
        {
            Disputes dispute = new Disputes();
            dispute.Transaction = _context.Transaction.Find(transactionID);
            return View(dispute);
        }

        // POST: Disputes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisputesID,DisputeComment,DisputeStatus,CorrectAmount,RequestDelete,Transaction")] Disputes disputes)
        {
            //find the correct transaction
            Transaction trans = _context.Transaction.Find(disputes.Transaction.TransactionID);

            //does this transaction have a dispute?
            //List<Disputes> disputeList = _context.Disputes.Include(t => t.Transaction).FirstOrDefault(d => d.Transaction.Disputes.)
            Disputes existingDispute = _context.Disputes.Include(t => t.Transaction).FirstOrDefault(d => d.Transaction.TransactionID == trans.TransactionID);
            if (existingDispute != null && existingDispute.DisputeStatus ==DisputeStatus.Accepted)
            {
                return View("Error", new String[] { "This transactions has an accepted dispute" });
            }
            
            //set dispute status
            disputes.DisputeStatus = DisputeStatus.Submitted;
            disputes.Transaction = trans;
            //trans.Disputes.Add(disputes);
            //var numDisputes = trans.Disputes.Count();

            if (ModelState.IsValid)
            {
                _context.Add(disputes);
                await _context.SaveChangesAsync();
                //TODO: fix redirect
                return RedirectToAction("DisputeConfirmation", new { id = disputes.DisputesID });
                //return RedirectToAction("Index");
            }
            return View(disputes);
        }

        // GET: Disputes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Disputes DBdisputes =  _context.Disputes
                .Include(t => t.Transaction)
                .ThenInclude(a => a.Account)
                .ThenInclude(u => u.User)
                .FirstOrDefault(d => d.DisputesID == id);

            if (DBdisputes == null)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        if ()
            //        _context.Update(DBdisputes);
            //        await _context.SaveChangesAsync();
            //    }
            //}

            return View(DBdisputes);
        }

        // POST: Disputes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisputesID,DisputeComment,ManagerComment,ManagerAdjustAmount,DisputeStatus,CorrectAmount,RequestDelete,Transaction")] Disputes disputes)
        {
            if (id != disputes.DisputesID)
            {
                return NotFound();
            }

            Disputes DBdisp = _context.Disputes
                .Include(t => t.Transaction)
                .ThenInclude(a => a.Account)
                .FirstOrDefault(d => d.DisputesID == id);
            Transaction DBTrans = _context.Transaction
                .Include(a => a.Account)
                .FirstOrDefault(t => t.TransactionID == DBdisp.Transaction.TransactionID);
            Account DBacc = _context.Account.Include(u => u.User).FirstOrDefault(b => b.AccountID == DBTrans.Account.AccountID);

            if (disputes.DisputeStatus == DisputeStatus.Accepted)
            {
                DBTrans.Description = "Dispute " + disputes.DisputeStatus + "-" + DBTrans.Description;
                DBdisp.DisputeStatus = DisputeStatus.Accepted;

                if (DBdisp.RequestDelete == true)
                {
                    DBdisp.DisputeStatus = DisputeStatus.Accepted;
                    return RedirectToAction("Delete", "Transaction", new { id = DBTrans.TransactionID });
                }

                //update account based on changed transacations
                if (DBdisp.CorrectAmount > DBTrans.TransactionAmount)
                {
                    DBacc.Value = DBacc.Value + (DBdisp.CorrectAmount - DBTrans.TransactionAmount);
                }
                if (DBdisp.CorrectAmount < DBTrans.TransactionAmount)
                {
                    DBacc.Value = DBacc.Value - (DBTrans.TransactionAmount - DBdisp.CorrectAmount);
                }
                DBdisp.DisputeComment = disputes.DisputeComment;
                DBdisp.ManagerComment = disputes.ManagerComment;
                DBTrans.TransactionAmount = DBdisp.CorrectAmount;

            }
            if (disputes.DisputeStatus == DisputeStatus.Rejected)
            {
                //send email to Account.User.Email saying it was rejected with manager comments
                DBdisp.DisputeStatus = DisputeStatus.Rejected;
                DBTrans.Description = "Dispute " + disputes.DisputeStatus + "-" + DBTrans.Description;
                DBdisp.DisputeStatus = DisputeStatus.Rejected;
                DBdisp.DisputeComment = disputes.DisputeComment;
                //Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context)
                Utilities.EmailMessaging.SendEmail(DBacc.User.Email, "Dispute was rejected", DBdisp.DisputeComment);
            }

            if (disputes.DisputeStatus == DisputeStatus.Adjusted)
            {
                
                DBTrans.Description = "Dispute " + disputes.DisputeStatus + "-" + DBTrans.Description;
                DBdisp.DisputeStatus = DisputeStatus.Adjusted;

                //update account based on changed transactions
                if (disputes.ManagerAdjustAmount > DBTrans.TransactionAmount)
                {
                    DBacc.Value = DBacc.Value + (disputes.ManagerAdjustAmount - DBTrans.TransactionAmount);
                }
                if (disputes.ManagerAdjustAmount < DBTrans.TransactionAmount)
                {
                    DBacc.Value = DBacc.Value - (DBTrans.TransactionAmount - disputes.ManagerAdjustAmount);
                }
                DBdisp.DisputeComment = disputes.DisputeComment;
                DBTrans.TransactionAmount = disputes.ManagerAdjustAmount;

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(DBacc);
                    _context.Update(DBTrans);
                    _context.Update(DBdisp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisputesExists(disputes.DisputesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DisputeResolveConfirmation));
            }
            return View(disputes);
        }

        // GET: Disputes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disputes = await _context.Disputes
                .FirstOrDefaultAsync(m => m.DisputesID == id);
            if (disputes == null)
            {
                return NotFound();
            }

            return View(disputes);
        }

        // POST: Disputes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disputes = await _context.Disputes.FindAsync(id);
            _context.Disputes.Remove(disputes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisputesExists(int id)
        {
            return _context.Disputes.Any(e => e.DisputesID == id);
        }

        public IActionResult DisputeConfirmation(int id)
        {
            Disputes disputes = _context.Disputes.Find(id);
            return View(disputes);
        }

        public IActionResult DisputeResolveConfirmation()
        {
            return View();
        }

    }
}
