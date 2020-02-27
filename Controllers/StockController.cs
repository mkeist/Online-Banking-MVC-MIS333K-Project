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
    public class StockController : Controller
    {
        private readonly AppDbContext _context;

        public StockController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stocks.ToListAsync());
        }

        // GET: Stocks/Create
        //[Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            
            return View();
        }

        //[Authorize(Roles = "Manager")]
        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockID,StockName,TickerSymbol,CurrentPrice,StockFee")] Stock stock, int SelectedStockType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                Stock dbStock = _context.Stocks.FirstOrDefault(p => p.StockID == stock.StockID);

                
                return RedirectToAction(nameof(Index));
            }
            
            return View(stock);
        }

        //[Authorize(Roles = "Manager")]
        // GET: Stock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            
            return View(stock);
        }

        //[Authorize(Roles = "Manager")]
        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockID,StockName,TickerSymbol,CurrentPrice,StockFee")] Stock stock)
        {
            if (id != stock.StockID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Stock dbStock = _context.Stocks
                    .FirstOrDefault(p => p.StockID == stock.StockID);
                
                //update the scalar properties
                dbStock.CurrentPrice = stock.CurrentPrice;

                //save changes
                _context.Stocks.Update(dbStock);
                _context.SaveChanges();

                //return to course listing page
                return RedirectToAction(nameof(Index));
            }

            //this is the sad path - data model is not valid
            return View(stock);
        }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Stock stock = await _context.Stocks
                .FirstOrDefaultAsync(m => m.StockID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        //private IEnumerable<AccountType> GetAllStockType()
        //{
        //    List<AccountType> accounttypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().ToList();

        //    return accounttypes;
        //}
    }
    
}
