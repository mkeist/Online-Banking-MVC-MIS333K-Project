using Team_4_Project.Models;
using Team_4_Project.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Team_4_Project.Seeding
{
	public static class SeedStocks
	{
		public static void SeedAllStocks(IServiceProvider serviceProvider)
		{
			AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
			if (_db.Stocks.Count() == 20)
			{
				throw new NotSupportedException("The database already contains all 20 Stocks!");
			}

			Int32 intStocksAdded = 0;
			String strEmailAddress = "Begin"; //helps to keep track of error on Stocks
			List<Stock> Stocks = new List<Stock>();

			try
			{
				Stock b1 = new Stock()
				{
					TickerSymbol = "GOOG",
					StockType = StockType.Ordinary,
					StockName = "Alphabet Inc.",
					CurrentPrice = 1315.2m,
					StockFee = 25,
				};
				Stocks.Add(b1);

				Stock b2 = new Stock()
				{
					TickerSymbol = "AAPL",
					StockType = StockType.Ordinary,
					StockName = "Apple Inc.",
					CurrentPrice = 266.095m,
					StockFee = 40,
				};
				Stocks.Add(b2);

				Stock b3 = new Stock()
				{
					TickerSymbol = "AMZN",
					StockType = StockType.Ordinary,
					StockName = "Amazon.com Inc.",
					CurrentPrice = 1755.03m,
					StockFee = 15,
				};
				Stocks.Add(b3);

				Stock b4 = new Stock()
				{
					TickerSymbol = "LUV",
					StockType = StockType.Ordinary,
					StockName = "Southwest Airlines",
					CurrentPrice = 57.695m,
					StockFee = 35,
				};
				Stocks.Add(b4);

				Stock b5 = new Stock()
				{
					TickerSymbol = "TXN",
					StockType = StockType.Ordinary,
					StockName = "Texas Instruments",
					CurrentPrice = 117.89m,
					StockFee = 15,
				};
				Stocks.Add(b5);

				Stock b6 = new Stock()
				{
					TickerSymbol = "HSY",
					StockType = StockType.Ordinary,
					StockName = "The Hershey Company",
					CurrentPrice = 146.42m,
					StockFee = 25,
				};
				Stocks.Add(b6);

				Stock b7 = new Stock()
				{
					TickerSymbol = "V",
					StockType = StockType.Ordinary,
					StockName = "Visa Inc.",
					CurrentPrice = 181.92m,
					StockFee = 10,
				};
				Stocks.Add(b7);

				Stock b8 = new Stock()
				{
					TickerSymbol = "NKE",
					StockType = StockType.Ordinary,
					StockName = "Nike",
					CurrentPrice = 93.44m,
					StockFee = 30,
				};
				Stocks.Add(b8);

				Stock b9 = new Stock()
				{
					TickerSymbol = "VWO",
					StockType = StockType.ETF,
					StockName = "Vanguard Emerging Markets ETF",
					CurrentPrice = 42.404m,
					StockFee = 20,
				};
				Stocks.Add(b9);

				Stock b10 = new Stock()
				{
					TickerSymbol = "F",
					StockType = StockType.Ordinary,
					StockName = "Ford Motor Company",
					CurrentPrice = 8.925m,
					StockFee = 10,
				};
				Stocks.Add(b10);

				Stock b11 = new Stock()
				{
					TickerSymbol = "BAC",
					StockType = StockType.Ordinary,
					StockName = "Bank of America Corporation",
					CurrentPrice = 32.935m,
					StockFee = 10,
				};
				Stocks.Add(b11);

				Stock b12 = new Stock()
				{
					TickerSymbol = "VNQ",
					StockType = StockType.ETF,
					StockName = "Vanguard REIT ETF",
					CurrentPrice = 93.22m,
					StockFee = 15,
				};
				Stocks.Add(b12);

				Stock b13 = new Stock()
				{
					TickerSymbol = "KMX",
					StockType = StockType.Ordinary,
					StockName = "CarMax, Inc.",
					CurrentPrice = 99.94m,
					StockFee = 15,
				};
				Stocks.Add(b13);

				Stock b14 = new Stock()
				{
					TickerSymbol = "DIA",
					StockType = StockType.IndexFund,
					StockName = "Dow Jones Industrial Average Index Fund",
					CurrentPrice = 279.27m,
					StockFee = 25,
				};
				Stocks.Add(b14);

				Stock b15 = new Stock()
				{
					TickerSymbol = "SPY",
					StockType = StockType.IndexFund,
					StockName = "S&P 500 Index Fund",
					CurrentPrice = 311.95m,
					StockFee = 25,
				};
				Stocks.Add(b15);

				Stock b16 = new Stock()
				{
					TickerSymbol = "BEN",
					StockType = StockType.Ordinary,
					StockName = "Franklin Resources, Inc.",
					CurrentPrice = 27.84m,
					StockFee = 25,
				};
				Stocks.Add(b16);

				Stock b17 = new Stock()
				{
					TickerSymbol = "PGSCX",
					StockType = StockType.MutualFund,
					StockName = "Pacific Advisors Small Cap Value Fund",
					CurrentPrice = 15.95m,
					StockFee = 15,
				};
				Stocks.Add(b17);

				Stock b18 = new Stock()
				{
					TickerSymbol = "DIS",
					StockType = StockType.Ordinary,
					StockName = "Disney",
					CurrentPrice = 148.98m,
					StockFee = 20,
				};
				Stocks.Add(b18);

				Stock b19 = new Stock()
				{
					TickerSymbol = "USAWX",
					StockType = StockType.MutualFund,
					StockName = "USAA World Growth Fund",
					CurrentPrice = 34.49m,
					StockFee = 15,
				};
				Stocks.Add(b19);

				Stock b20 = new Stock()
				{
					TickerSymbol = "CGLOX",
					StockType = StockType.MutualFund,
					StockName = "Capital Group Global Equity Fund",
					CurrentPrice = 16.72m,
					StockFee = 25,
				};
				Stocks.Add(b20);

				try
				{
					foreach (Stock stockToAdd in Stocks)
					{
						//strEmailAddress = stockToAdd.EmailAddress;
						Stock _dbStock = _db.Stocks.FirstOrDefault(b => b.StockName == stockToAdd.StockName);
						if (_dbStock == null) //this stock doesn't exist
						{
							_db.Stocks.Add(stockToAdd);
							_db.SaveChanges();
							intStocksAdded += 1;
						}
						else //Stock exists - update values
						{
							_dbStock.TickerSymbol = stockToAdd.TickerSymbol;
							_dbStock.StockType = stockToAdd.StockType;
							_dbStock.StockName = stockToAdd.StockName;
							_dbStock.CurrentPrice = stockToAdd.CurrentPrice;
							_dbStock.StockFee = stockToAdd.StockFee;
							_db.Update(_dbStock);
							_db.SaveChanges();
							intStocksAdded += 1;
						}
					}
				}
				catch (Exception ex)
				{
					String msg = "  Repositories added:" + intStocksAdded + "; Error on " + strEmailAddress;
					throw new InvalidOperationException(ex.Message + msg);
				}
			}
			catch (Exception e)
			{
				throw new InvalidOperationException(e.Message);
			}
		}
	}
}
