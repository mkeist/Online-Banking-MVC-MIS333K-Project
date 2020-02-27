using Team_4_Project.Models;
using Team_4_Project.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Team_4_Project.Seeding
{
	public static class SeedAccounts
	{
		public static void SeedAllAccounts(IServiceProvider serviceProvider)
		{
			AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
			if (_db.Account.Count() == 21)
			{
				throw new NotSupportedException("The database already contains all 20 accounts!");
			}

			Int32 intAccountsAdded = 0;
			Int32 strAccountNumber = 0;
			List<Account> Accounts = new List<Account>();

			try
			{
				Account b1 = new Account()
				{
					AccountNumber = 1000000000,
					AccountName = "Shan's Stock",
					accountType = AccountType.Stock,
					Value = 0m,
				};
				b1.User = _db.Users.FirstOrDefault(g => g.UserName == "Dixon@aool.com");
				Accounts.Add(b1);
				b1.PartialAccountNumber = b1.AccountNumber.ToString();
				b1.PartialAccountNumber = "******"+ b1.PartialAccountNumber.Substring(b1.PartialAccountNumber.Length - 4);

				Account b2 = new Account()
				{
					AccountNumber = 1000000001,
					AccountName = "William's Savings",
					accountType = AccountType.Savings,
					Value = 40035.5m,
				};
				b2.User = _db.Users.FirstOrDefault(g => g.UserName == "willsheff@email.com");
				Accounts.Add(b2);
				b2.PartialAccountNumber = b2.AccountNumber.ToString();
				b2.PartialAccountNumber = "******"+ b2.PartialAccountNumber.Substring(b2.PartialAccountNumber.Length - 4);

				Account b3 = new Account()
				{
					AccountNumber = 1000000002,
					AccountName = "Gregory's Checking",
					accountType = AccountType.Checking,
					Value = 39779.49m,
				};
				b3.User = _db.Users.FirstOrDefault(g => g.UserName == "smartinmartin.Martin@aool.com");
				Accounts.Add(b3);
				b3.PartialAccountNumber = b3.AccountNumber.ToString();
				b3.PartialAccountNumber = "******"+ b3.PartialAccountNumber.Substring(b3.PartialAccountNumber.Length - 4);

				Account b4 = new Account()
				{
					AccountNumber = 1000000003,
					AccountName = "Allen's Checking",
					accountType = AccountType.Checking,
					Value = 47277.33m,
				};
				b4.User = _db.Users.FirstOrDefault(g => g.UserName == "avelasco@yaho.com");
				Accounts.Add(b4);
				b4.PartialAccountNumber = b4.AccountNumber.ToString();
				b4.PartialAccountNumber = "******"+ b4.PartialAccountNumber.Substring(b4.PartialAccountNumber.Length - 4);

				Account b5 = new Account()
				{
					AccountNumber = 1000000004,
					AccountName = "Reagan's Checking",
					accountType = AccountType.Checking,
					Value = 70812.15m,
				};
				b5.User = _db.Users.FirstOrDefault(g => g.UserName == "rwood@voyager.net");
				Accounts.Add(b5);
				b5.PartialAccountNumber = b5.AccountNumber.ToString();
				b5.PartialAccountNumber = "******"+ b5.PartialAccountNumber.Substring(b5.PartialAccountNumber.Length - 4);

				Account b6 = new Account()
				{
					AccountNumber = 1000000005,
					AccountName = "Kelly's Savings",
					accountType = AccountType.Savings,
					Value = 21901.97m,
				};
				b6.User = _db.Users.FirstOrDefault(g => g.UserName == "nelson.Kelly@aool.com");
				Accounts.Add(b6);
				b6.PartialAccountNumber = b6.AccountNumber.ToString();
				b6.PartialAccountNumber = "******"+ b6.PartialAccountNumber.Substring(b6.PartialAccountNumber.Length - 4);

				Account b7 = new Account()
				{
					AccountNumber = 1000000006,
					AccountName = "Eryn's Checking",
					accountType = AccountType.Checking,
					Value = 70480.99m,
				};
				b7.User = _db.Users.FirstOrDefault(g => g.UserName == "erynrice@aool.com");
				Accounts.Add(b7);
				b7.PartialAccountNumber = b7.AccountNumber.ToString();
				b7.PartialAccountNumber = "******"+ b7.PartialAccountNumber.Substring(b7.PartialAccountNumber.Length - 4);

				Account b8 = new Account()
				{
					AccountNumber = 1000000007,
					AccountName = "Jake's Savings",
					accountType = AccountType.Savings,
					Value = 7916.4m,
				};
				b8.User = _db.Users.FirstOrDefault(g => g.UserName == "westj@pioneer.net");
				Accounts.Add(b8);
				b8.PartialAccountNumber = b8.AccountNumber.ToString();
				b8.PartialAccountNumber = "******"+ b8.PartialAccountNumber.Substring(b8.PartialAccountNumber.Length - 4);

				Account b9 = new Account()
				{
					AccountNumber = 1000000008,
					AccountName = "Michelle's Stock",
					accountType = AccountType.Stock,
					Value = 0m,
				};
				b9.User = _db.Users.FirstOrDefault(g => g.UserName == "mb@aool.com");
				Accounts.Add(b9);
				b9.PartialAccountNumber = b9.AccountNumber.ToString();
				b9.PartialAccountNumber = "******"+ b9.PartialAccountNumber.Substring(b9.PartialAccountNumber.Length - 4);

				Account b10 = new Account()
				{
					AccountNumber = 1000000009,
					AccountName = "Jeffrey's Savings",
					accountType = AccountType.Savings,
					Value = 69576.83m,
				};
				b10.User = _db.Users.FirstOrDefault(g => g.UserName == "jeff@ggmail.com");
				Accounts.Add(b10);
				b10.PartialAccountNumber = b10.AccountNumber.ToString();
				b10.PartialAccountNumber = "******"+ b10.PartialAccountNumber.Substring(b10.PartialAccountNumber.Length - 4);

				Account b11 = new Account()
				{
					AccountNumber = 1000000010,
					AccountName = "Kelly's Stock",
					accountType = AccountType.Stock,
					Value = 0m,
				};
				b11.User = _db.Users.FirstOrDefault(g => g.UserName == "nelson.Kelly@aool.com");
				Accounts.Add(b11);
				b11.PartialAccountNumber = b11.AccountNumber.ToString();
				b11.PartialAccountNumber = "******"+ b11.PartialAccountNumber.Substring(b11.PartialAccountNumber.Length - 4);

				Account b12 = new Account()
				{
					AccountNumber = 1000000011,
					AccountName = "Eryn's Checking 2",
					accountType = AccountType.Checking,
					Value = 30279.33m,
				};
				b12.User = _db.Users.FirstOrDefault(g => g.UserName == "erynrice@aool.com");
				Accounts.Add(b12);
				b12.PartialAccountNumber = b12.AccountNumber.ToString();
				b12.PartialAccountNumber = "******"+ b12.PartialAccountNumber.Substring(b12.PartialAccountNumber.Length - 4);

				Account b13 = new Account()
				{
					AccountNumber = 1000000012,
					AccountName = "Jennifer's IRA",
					accountType = AccountType.IRA,
					Value = 5000m,
				};
				b13.User = _db.Users.FirstOrDefault(g => g.UserName == "mackcloud@pimpdaddy.com");
				Accounts.Add(b13);
				b13.PartialAccountNumber = b13.AccountNumber.ToString();
				b13.PartialAccountNumber = "******"+ b13.PartialAccountNumber.Substring(b13.PartialAccountNumber.Length - 4);

				Account b14 = new Account()
				{
					AccountNumber = 1000000013,
					AccountName = "Sarah's Savings",
					accountType = AccountType.Savings,
					Value = 11958.08m,
				};
				b14.User = _db.Users.FirstOrDefault(g => g.UserName == "ss34@ggmail.com");
				Accounts.Add(b14);
				b14.PartialAccountNumber = b14.AccountNumber.ToString();
				b14.PartialAccountNumber = "******"+ b14.PartialAccountNumber.Substring(b14.PartialAccountNumber.Length - 4);

				Account b15 = new Account()
				{
					AccountNumber = 1000000014,
					AccountName = "Jeremy's Savings",
					accountType = AccountType.Savings,
					Value = 72990.47m,
				};
				b15.User = _db.Users.FirstOrDefault(g => g.UserName == "tanner@ggmail.com");
				Accounts.Add(b15);
				b15.PartialAccountNumber = b15.AccountNumber.ToString();
				b15.PartialAccountNumber = "******"+ b15.PartialAccountNumber.Substring(b15.PartialAccountNumber.Length - 4);

				Account b16 = new Account()
				{
					AccountNumber = 1000000015,
					AccountName = "Elizabeth's Savings",
					accountType = AccountType.Savings,
					Value = 7417.2m,
				};
				b16.User = _db.Users.FirstOrDefault(g => g.UserName == "liz@ggmail.com");
				Accounts.Add(b16);
				b16.PartialAccountNumber = b16.AccountNumber.ToString();
				b16.PartialAccountNumber = "******"+ b16.PartialAccountNumber.Substring(b16.PartialAccountNumber.Length - 4);

				Account b17 = new Account()
				{
					AccountNumber = 1000000016,
					AccountName = "Allen's IRA",
					accountType = AccountType.IRA,
					Value = 5000m,
				};
				b17.User = _db.Users.FirstOrDefault(g => g.UserName == "ra@aoo.com");
				Accounts.Add(b17);
				b17.PartialAccountNumber = b17.AccountNumber.ToString();
				b17.PartialAccountNumber = "******"+ b17.PartialAccountNumber.Substring(b17.PartialAccountNumber.Length - 4);

				Account b18 = new Account()
				{
					AccountNumber = 1000000017,
					AccountName = "John's Stock",
					accountType = AccountType.Stock,
					Value = 0m,
				};
				b18.User = _db.Users.FirstOrDefault(g => g.UserName == "johnsmith187@aool.com");
				Accounts.Add(b18);
				b18.PartialAccountNumber = b18.AccountNumber.ToString();
				b18.PartialAccountNumber = "******"+ b18.PartialAccountNumber.Substring(b18.PartialAccountNumber.Length - 4);

				Account b19 = new Account()
				{
					AccountNumber = 1000000018,
					AccountName = "Clarence's Savings",
					accountType = AccountType.Savings,
					Value = 1642.82m,
				};
				b19.User = _db.Users.FirstOrDefault(g => g.UserName == "mclarence@aool.com");
				Accounts.Add(b19);
				b19.PartialAccountNumber = b19.AccountNumber.ToString();
				b19.PartialAccountNumber = "******"+ b19.PartialAccountNumber.Substring(b19.PartialAccountNumber.Length - 4);

				Account b20 = new Account()
				{
					AccountNumber = 1000000019,
					AccountName = "Sarah's Checking",
					accountType = AccountType.Checking,
					Value = 84421.45m,
				};
				b20.User = _db.Users.FirstOrDefault(g => g.UserName == "ss34@ggmail.com");
				Accounts.Add(b20);
				b20.PartialAccountNumber = b20.AccountNumber.ToString();
				b20.PartialAccountNumber = "******"+ b20.PartialAccountNumber.Substring(b20.PartialAccountNumber.Length - 4);

				try
				{
					foreach (Account accountToAdd in Accounts)
					{
						strAccountNumber = accountToAdd.AccountNumber;
						Account _dbAccount = _db.Account.FirstOrDefault(b => b.AccountNumber == accountToAdd.AccountNumber);
						if (_dbAccount == null) //this account doesn't exist
						{
							_db.Account.Add(accountToAdd);
							_db.SaveChanges();
							intAccountsAdded += 1;
						}
						else //Account exists - update values
						{
							_dbAccount.AccountNumber = accountToAdd.AccountNumber;
							_dbAccount.PartialAccountNumber = accountToAdd.PartialAccountNumber;
							_dbAccount.AccountName = accountToAdd.AccountName;
							_dbAccount.accountType = accountToAdd.accountType;
							_dbAccount.Value = accountToAdd.Value;
							_db.Update(_dbAccount);
							_db.SaveChanges();
							intAccountsAdded += 1;
						}
					}
				}
				catch (Exception ex)
				{
					String msg = "  Repositories added:" + intAccountsAdded + "; Error on " + strAccountNumber;
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
