using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team_4_Project.DAL;
using Team_4_Project.Models;

namespace Team_4_Project.Controllers
{
    public class StockPortionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StockPortionController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: StockPortion
        public async Task<IActionResult> Index(Int32 accountID)
        {
            List<StockPortion> sps = _context.StockPortions
                .Include(sp => sp.Stock)
                .Where(s => s.Account.AccountID == accountID).ToList();
            return View(sps);
        }

        // GET: StockPortion/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a stock to buy!" });
            }
            Stock stock = _context.Stocks.FirstOrDefault(r => r.StockID == id);
            ViewBag.SelectedStk = "Stock Name:         " + stock.StockName;

            ViewBag.AllAccounts = GetAllAccounts();
            return View();

        }

        // POST: StockPortion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockPortionID,ShareNumber,CurrentPrice,StockPortfolio")] StockPortion stockportion, int SelectedAccount, int id)
        {
            Account account = _context.Account.Find(SelectedAccount);
            account.User = await _userManager.FindByNameAsync(User.Identity.Name);
            
            Stock stock = _context.Stocks.Find(id);
            Account sp = _context.Account
                    .FirstOrDefault(a => a.User == account.User && a.accountType == AccountType.Stock);
            if (sp.Status == Status.Inactive)
            {
                return View("Error", new String[] { "Account is inactive!" });
            }

            //Create transactions for from account (selected account) and to account (sp)
            Transaction fromAccounttrans = new Transaction();
            Transaction toPortfoliotrans = new Transaction();
            Transaction stockbuyfee = new Transaction();



            if (ModelState.IsValid)
            {
                if (sp != null)
                {
                    if ((account.Value - (stock.CurrentPrice*stockportion.ShareNumber))> 0)
                    {
                        stockportion.Stock = stock;
                        stockportion.Account = sp;
                        stockportion.CurrentPrice = stock.CurrentPrice;
                        
                        //assign all fee tranaction properties
                        stockbuyfee.Account = account;
                        stockbuyfee.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
                        stockbuyfee.TransactionType = TransacType.Fee;
                        stockbuyfee.Status = TransactionStatus.Approved;
                        stockbuyfee.TransactionDate = DateTime.Today;
                        stockbuyfee.Description = "Fee for purchase of " + stock.StockName;
                        stockbuyfee.TransactionAmount = 10m;

                        if (account.AccountID==sp.AccountID)
                        {
                            account.AvailableCash = account.AvailableCash - (stock.CurrentPrice * stockportion.ShareNumber);
                            sp.CashValueFees += 10m;
                        }
                        else
                        {
                            account.Value = account.Value - (stock.CurrentPrice * stockportion.ShareNumber) - 10m;
                        }

                        //update the transaction for selected account
                        fromAccounttrans.Account = account;
                        fromAccounttrans.TransactionNumber = (1 + Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context));
                        fromAccounttrans.TransactionType = TransacType.Withdrawal;
                        fromAccounttrans.Status = TransactionStatus.Approved;
                        fromAccounttrans.TransactionDate = DateTime.Today;
                        fromAccounttrans.Description = "Stock purchase - Account " + account.AccountNumber;
                        fromAccounttrans.TransactionAmount = (stock.CurrentPrice * stockportion.ShareNumber);
                        
                        _context.Add(stockportion);
                        _context.Add(stockbuyfee);
                        _context.Add(fromAccounttrans);

                        List<StockPortion> sps = _context.StockPortions.Include(st => st.Stock).Where(s => s.Account.AccountID == sp.AccountID).ToList();
                        sps.Add(stockportion);

                        int OrdinaryCount = 0;
                        int IFCount = 0;
                        int MFCount = 0;
                        foreach (var stk in sps)
                        {
                            if (stk.Stock.StockType == StockType.IndexFund)
                            {
                                IFCount = IFCount + 1;
                            }
                            else if (stk.Stock.StockType == StockType.Ordinary)
                            {
                                OrdinaryCount = OrdinaryCount + 1;
                            }
                            else if (stk.Stock.StockType == StockType.MutualFund)
                            {
                                MFCount = MFCount + 1;
                            }
                            
                        }
                        if (OrdinaryCount >= 2 && IFCount >= 1 && MFCount >= 1)
                        {
                            sp.Balanced = Balanced.Balanced;
                        }
                        else
                        {
                            sp.Balanced = Balanced.Unbalanced;
                        }
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Details", "StockPortfolio", new { id = sp.AccountID });
                    }
                    ViewBag.ErrorMessage = "You do not have enough of a balance in this account!";
                    ViewBag.SelectedStk = "Stock Name:         " + stock.StockName;
                    ViewBag.AllAccounts = GetAllAccounts();
                    return View(stockportion);

                }
                ViewBag.ErrorMessage = "Please make a stock portfolio first!";
                ViewBag.SelectedStk = "Stock Name:         " + stock.StockName;
                ViewBag.AllAccounts = GetAllAccounts();
                return View(stockportion);
            }

            ViewBag.SelectedStk = "Stock Name:         " + stock.StockName;
            ViewBag.AllAccounts = GetAllAccounts();
            return View(stockportion);
        }

        private SelectList GetAllAccounts()
        {
            //get a list of all accounts from the database
            List<Account> AllAccounts = _context.Account.Where(r => r.User.UserName == User.Identity.Name).ToList();

            //convert this to a select list
            SelectList accounts = new SelectList(AllAccounts, "AccountID", "AccountName");

            //return the select list
            return accounts;

        }

        // GET: StockPortion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StockPortion stockportion = _context.StockPortions
                .Include(p => p.Stock)
                .Include(od => od.Account)
                .FirstOrDefault(od => od.StockPortionID == id);

            if (stockportion == null)
            {
                return NotFound();
            }
            return View(stockportion);
        }

        // POST: StockPortion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockPortionID,ShareNumber,CurrentPrice,StockPortion")] StockPortion stockportion)
        {
            StockPortion dbSP = _context.StockPortions
                .Include(od => od.Stock)
                .Include(od => od.Account)
                .FirstOrDefault(o => o.StockPortionID == stockportion.StockPortionID);
            //transactions for selling stocks
            Transaction sellTrans = new Transaction();
            sellTrans.Account = dbSP.Account;
            sellTrans.TransactionAmount = dbSP.Stock.CurrentPrice * dbSP.ShareNumber;
            sellTrans.Status = TransactionStatus.Approved;
            sellTrans.TransactionDate = DateTime.Today;
            sellTrans.TransactionNumber = Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context);
            sellTrans.TransactionType = TransacType.Deposit;
            sellTrans.Description = "Stock Name " + dbSP.Stock.StockName + " Quantity Sold "  + dbSP.ShareNumber + " Initial Stock Price " + dbSP.CurrentPrice + " Current Stock Price " + dbSP.Stock.CurrentPrice + " Gains/Losses " + dbSP.Account.Gains;

            //transactions for selling stocks
            Transaction sellTransFee = new Transaction();
            sellTransFee.Account = dbSP.Account;
            sellTransFee.TransactionAmount = dbSP.Stock.StockFee;
            sellTransFee.Status = TransactionStatus.Approved;
            sellTransFee.TransactionDate = DateTime.Today;
            sellTransFee.TransactionNumber = (1 +Utilities.GenerateNextTransactionNumber.GetNextTransactionNumber(_context));
            sellTransFee.TransactionType = TransacType.Fee;
            sellTransFee.Description = "Fee for sale of " + dbSP.Stock.StockName;


            dbSP.ShareNumber -= stockportion.ShareNumber;
            
            List<StockPortion> UserStock = _context.StockPortions.Where(r => r.Account.User.UserName == User.Identity.Name).ToList();

            if (ModelState.IsValid && dbSP.ShareNumber>=0)
            {
                dbSP.Account.Gains = (dbSP.Stock.CurrentPrice - dbSP.CurrentPrice) * dbSP.ShareNumber;
                dbSP.Account.CashValueFees = dbSP.Account.CashValueFees + dbSP.Stock.StockFee;
                dbSP.Account.User = await _userManager.FindByNameAsync(User.Identity.Name);
                List<StockPortion> sps = _context.StockPortions.Include(st => st.Stock).Where(s => s.Account.AccountID == dbSP.Account.AccountID).ToList();

                if (dbSP.ShareNumber==0)
                {
                    sps.Remove(dbSP);
                }

                int OrdinaryCount = 0;
                int IFCount = 0;
                int MFCount = 0;
                foreach (var stk in sps)
                {
                    if (stk.Stock.StockType == StockType.IndexFund)
                    {
                        IFCount = IFCount + 1;
                    }
                    else if (stk.Stock.StockType == StockType.Ordinary)
                    {
                        OrdinaryCount = OrdinaryCount + 1;
                    }
                    else if (stk.Stock.StockType == StockType.MutualFund)
                    {
                        MFCount = MFCount + 1;
                    }

                }
                if (OrdinaryCount >= 2 && IFCount >= 1 && MFCount >= 1)
                {
                    dbSP.Account.Balanced = Balanced.Balanced;
                }
                else
                {
                    dbSP.Account.Balanced = Balanced.Unbalanced;
                }

                if (dbSP.ShareNumber>0)
                {
                    _context.Update(dbSP);
                }
                else
                {
                    _context.Update(dbSP);
                    _context.StockPortions.Remove(dbSP);
                }

                _context.Add(sellTrans);
                _context.Add(sellTransFee);
                
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "StockPortfolio", new { id = dbSP.Account.AccountID });
            }
            ViewBag.ErrorMessage = "You cannot sell more stocks than you have!";
            return View(dbSP);
        }

        // GET: StockPortion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockportion = await _context.StockPortions
                .Include(m=>m.Stock)
                .FirstOrDefaultAsync(m => m.StockPortionID == id);
            if (stockportion == null)
            {
                return NotFound();
            }

            Decimal PriceChange = stockportion.Stock.CurrentPrice - stockportion.CurrentPrice;
            ViewBag.PriceChange = "$" + PriceChange;
            return View(stockportion);
        }

        private SelectList GetAllStocks()
        {
            //get a list of all stocks from the database
            List<Stock> AllStocks = _context.Stocks.ToList();

            //convert this to a select list
            SelectList stocks = new SelectList(AllStocks, "StockID", "StockName");

            //return the select list
            return stocks;

        }


    }
}
