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
    public class StockPortfolioController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public StockPortfolioController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: StockPortfolio
        public IActionResult Index()
        {

            List<Account> stockportfolios = new List<Account>();
            if (User.IsInRole("Manager"))
            {
                stockportfolios = _context.Account.Where(a => a.accountType == AccountType.Stock).ToList();
            }

            //user gets shown only their orders 
            else
            {
                stockportfolios = _context.Account.Where(o => o.User.UserName == User.Identity.Name && o.accountType == AccountType.Stock).ToList();
                
            }
            return View(stockportfolios);
        }

        // GET: StockPortfolio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a stock portfolio to view!" });
            }

            Account stockportfolio = await _context.Account
                .Include(o => o.StockPortions).ThenInclude(o => o.Stock)
                .Include(o => o.User)
                .Where(o => o.accountType == AccountType.Stock)
                .FirstOrDefaultAsync(m => m.AccountID == id);

            if (stockportfolio == null)
            {
                return View("Error", new String[] { "Cannot find this order!" });
            }

            if (User.IsInRole("Manager") == false && stockportfolio.User.UserName != User.Identity.Name) //they are trying to see something that isn't theirs
            {
                return View("Error", new String[] { "Unauthorized: You are attempting to view another customer's stock portfolio!" });
            }

            
            var query = from t in _context.StockPortions            
                .Include(o => o.Stock)
                .Include(o=>o.Account).ThenInclude(o=>o.User)
                .Where(o => o.Account.User.UserName == stockportfolio.User.UserName)
                 select t;

            List<StockPortion> stockportion = query.ToList();

            Decimal stockportionval = 0;
            foreach (var stk in stockportion)
            {

                stockportionval += stk.Stock.CurrentPrice * stk.ShareNumber;
            }
            stockportfolio.CashValPortion= stockportfolio.Gains - stockportfolio.CashValueFees + stockportfolio.Bonuses + stockportfolio.AvailableCash;
            stockportfolio.Value = stockportfolio.CashValPortion + stockportionval;

            ViewBag.StockPortion = "$" + stockportionval;
            
            return View(stockportfolio);
        }

        public IActionResult EndPeriodTask()
        {
            List<Account> accounts = new List<Account>();
            accounts = _context.Account.Include(r => r.StockPortions).ThenInclude(s => s.Stock).Where(r => r.accountType == AccountType.Stock).ToList();
            
            foreach (var sp in accounts)
            {
                if (sp.Balanced==Balanced.Balanced)
                {
                    Decimal stockportionval = 0;
                    foreach (var stk in sp.StockPortions)
                    {
                        stockportionval += stk.Stock.CurrentPrice * stk.ShareNumber;
                    }
                    sp.CashValPortion = sp.Gains - sp.CashValueFees + sp.Bonuses + sp.AvailableCash;

                    sp.Bonuses = .1m * stockportionval;
                    sp.Value = sp.CashValPortion + sp.Bonuses +stockportionval;

                    _context.Update(sp);
                }
                
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
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
