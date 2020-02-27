using Team_4_Project.Models;
using Team_4_Project.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Team_4_Project.Seeding
{
	public static class SeedCustomers
	{
		private static readonly object _userManager;

		public static async Task AddCustomer(IServiceProvider serviceProvider)
		{
			AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
			UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			RoleManager<IdentityRole> _rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			if (_db.Users.Count() == 50)
			{
				throw new NotSupportedException("The database already contains all 50 customers!");
			}

			Int32 intCustomersAdded = 0;
			String strEmailAddress = "Begin"; //helps to keep track of error on customers
			List<AppUser> Customers = new List<AppUser>();

			try
			{
				AppUser b1 = _db.Users.FirstOrDefault(u => u.UserName =="cbaker@freezing.co.uk");
				if (b1 == null)
				{
					b1= new AppUser();
					b1.UserName = "cbaker@freezing.co.uk";
					b1.Email = "cbaker@freezing.co.uk";
					b1.PasswordHash = "gazing";
					b1.FirstName = "Christopher";
					b1.LastName = "Baker";
					b1.MiddleInitial = "L";
					b1.StreetAddress = "1245 Lake Austin Blvd.";
					b1.CityAddress = "Austin";
					b1.StateAddress = "TX";
					b1.ZipCodeAddress = "78733";
					b1.PhoneNumber = "5125571146";
					b1.Birthday = new DateTime(1991, 2, 7);

					var result = await _userManager.CreateAsync(b1, "gazing");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b1 = _db.Users.FirstOrDefault(u => u.UserName == "cbaker@freezing.co.uk");
				};
				if (await _userManager.IsInRoleAsync(b1, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b1, "Customer");
				}
				_db.SaveChanges();

				AppUser b2 = _db.Users.FirstOrDefault(u => u.UserName =="mb@aool.com");
				if (b2 == null)
				{
					b2= new AppUser();
					b2.UserName = "mb@aool.com";
					b2.Email = "mb@aool.com";
					b2.PasswordHash = "banquet";
					b2.FirstName = "Michelle";
					b2.LastName = "Banks";
					b2.MiddleInitial = "";
					b2.StreetAddress = "1300 Tall Pine Lane";
					b2.CityAddress = "San Antonio";
					b2.StateAddress = "TX";
					b2.ZipCodeAddress = "78261";
					b2.PhoneNumber = "2102678873";
					b2.Birthday = new DateTime(1990, 6, 23);

					var result = await _userManager.CreateAsync(b2, "banquet");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b2 = _db.Users.FirstOrDefault(u => u.UserName == "mb@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b2, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b2, "Customer");
				}
				_db.SaveChanges();

				AppUser b3 = _db.Users.FirstOrDefault(u => u.UserName =="fd@aool.com");
				if (b3 == null)
				{
					b3= new AppUser();
					b3.UserName = "fd@aool.com";
					b3.Email = "fd@aool.com";
					b3.PasswordHash = "666666";
					b3.FirstName = "Franco";
					b3.LastName = "Broccolo";
					b3.MiddleInitial = "V";
					b3.StreetAddress = "62 Browning Rd";
					b3.CityAddress = "Houston";
					b3.StateAddress = "TX";
					b3.ZipCodeAddress = "77019";
					b3.PhoneNumber = "8175659699";
					b3.Birthday = new DateTime(1986, 5, 6);

					var result = await _userManager.CreateAsync(b3, "666666");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b3 = _db.Users.FirstOrDefault(u => u.UserName == "fd@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b3, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b3, "Customer");
				}
				_db.SaveChanges();

				AppUser b4 = _db.Users.FirstOrDefault(u => u.UserName =="wendy@ggmail.com");
				if (b4 == null)
				{
					b4= new AppUser();
					b4.UserName = "wendy@ggmail.com";
					b4.Email = "wendy@ggmail.com";
					b4.PasswordHash = "clover";
					b4.FirstName = "Wendy";
					b4.LastName = "Chang";
					b4.MiddleInitial = "L";
					b4.StreetAddress = "202 Bellmont Hall";
					b4.CityAddress = "Austin";
					b4.StateAddress = "TX";
					b4.ZipCodeAddress = "78713";
					b4.PhoneNumber = "5125943222";
					b4.Birthday = new DateTime(1964, 12, 21);

					var result = await _userManager.CreateAsync(b4, "clover");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b4 = _db.Users.FirstOrDefault(u => u.UserName == "wendy@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b4, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b4, "Customer");
				}
				_db.SaveChanges();

				AppUser b5 = _db.Users.FirstOrDefault(u => u.UserName =="limchou@yaho.com");
				if (b5 == null)
				{
					b5= new AppUser();
					b5.UserName = "limchou@yaho.com";
					b5.Email = "limchou@yaho.com";
					b5.PasswordHash = "austin";
					b5.FirstName = "Lim";
					b5.LastName = "Chou";
					b5.MiddleInitial = "";
					b5.StreetAddress = "1600 Teresa Lane";
					b5.CityAddress = "San Antonio";
					b5.StateAddress = "TX";
					b5.ZipCodeAddress = "78266";
					b5.PhoneNumber = "2107724599";
					b5.Birthday = new DateTime(1950, 6, 14);

					var result = await _userManager.CreateAsync(b5, "austin");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b5 = _db.Users.FirstOrDefault(u => u.UserName == "limchou@yaho.com");
				};
				if (await _userManager.IsInRoleAsync(b5, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b5, "Customer");
				}
				_db.SaveChanges();

				AppUser b6 = _db.Users.FirstOrDefault(u => u.UserName =="Dixon@aool.com");
				if (b6 == null)
				{
					b6= new AppUser();
					b6.UserName = "Dixon@aool.com";
					b6.Email = "Dixon@aool.com";
					b6.PasswordHash = "mailbox";
					b6.FirstName = "Shan";
					b6.LastName = "Dixon";
					b6.MiddleInitial = "D";
					b6.StreetAddress = "234 Holston Circle";
					b6.CityAddress = "Dallas";
					b6.StateAddress = "TX";
					b6.ZipCodeAddress = "75208";
					b6.PhoneNumber = "2142643255";
					b6.Birthday = new DateTime(1930, 5, 9);

					var result = await _userManager.CreateAsync(b6, "mailbox");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b6 = _db.Users.FirstOrDefault(u => u.UserName == "Dixon@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b6, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b6, "Customer");
				}
				_db.SaveChanges();

				AppUser b7 = _db.Users.FirstOrDefault(u => u.UserName =="louann@ggmail.com");
				if (b7 == null)
				{
					b7= new AppUser();
					b7.UserName = "louann@ggmail.com";
					b7.Email = "louann@ggmail.com";
					b7.PasswordHash = "aggies";
					b7.FirstName = "Lou Ann";
					b7.LastName = "Feeley";
					b7.MiddleInitial = "K";
					b7.StreetAddress = "600 S 8th Street W";
					b7.CityAddress = "Houston";
					b7.StateAddress = "TX";
					b7.ZipCodeAddress = "77010";
					b7.PhoneNumber = "8172556749";
					b7.Birthday = new DateTime(1930, 2, 24);

					var result = await _userManager.CreateAsync(b7, "aggies");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b7 = _db.Users.FirstOrDefault(u => u.UserName == "louann@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b7, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b7, "Customer");
				}
				_db.SaveChanges();

				AppUser b8 = _db.Users.FirstOrDefault(u => u.UserName =="tfreeley@minntonka.ci.state.mn.us");
				if (b8 == null)
				{
					b8= new AppUser();
					b8.UserName = "tfreeley@minntonka.ci.state.mn.us";
					b8.Email = "tfreeley@minntonka.ci.state.mn.us";
					b8.PasswordHash = "raiders";
					b8.FirstName = "Tesa";
					b8.LastName = "Freeley";
					b8.MiddleInitial = "P";
					b8.StreetAddress = "4448 Fairview Ave.";
					b8.CityAddress = "Houston";
					b8.StateAddress = "TX";
					b8.ZipCodeAddress = "77009";
					b8.PhoneNumber = "8173255687";
					b8.Birthday = new DateTime(1935, 9, 1);

					var result = await _userManager.CreateAsync(b8, "raiders");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b8 = _db.Users.FirstOrDefault(u => u.UserName == "tfreeley@minntonka.ci.state.mn.us");
				};
				if (await _userManager.IsInRoleAsync(b8, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b8, "Customer");
				}
				_db.SaveChanges();

				AppUser b9 = _db.Users.FirstOrDefault(u => u.UserName =="mgar@aool.com");
				if (b9 == null)
				{
					b9= new AppUser();
					b9.UserName = "mgar@aool.com";
					b9.Email = "mgar@aool.com";
					b9.PasswordHash = "mustangs";
					b9.FirstName = "Margaret";
					b9.LastName = "Garcia";
					b9.MiddleInitial = "L";
					b9.StreetAddress = "594 Longview";
					b9.CityAddress = "Houston";
					b9.StateAddress = "TX";
					b9.ZipCodeAddress = "77003";
					b9.PhoneNumber = "8176593544";
					b9.Birthday = new DateTime(1990, 7, 3);

					var result = await _userManager.CreateAsync(b9, "mustangs");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b9 = _db.Users.FirstOrDefault(u => u.UserName == "mgar@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b9, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b9, "Customer");
				}
				_db.SaveChanges();

				AppUser b10 = _db.Users.FirstOrDefault(u => u.UserName =="chaley@thug.com");
				if (b10 == null)
				{
					b10= new AppUser();
					b10.UserName = "chaley@thug.com";
					b10.Email = "chaley@thug.com";
					b10.PasswordHash = "region";
					b10.FirstName = "Charles";
					b10.LastName = "Haley";
					b10.MiddleInitial = "E";
					b10.StreetAddress = "One Cowboy Pkwy";
					b10.CityAddress = "Dallas";
					b10.StateAddress = "TX";
					b10.ZipCodeAddress = "75261";
					b10.PhoneNumber = "2148475583";
					b10.Birthday = new DateTime(1985, 9, 17);

					var result = await _userManager.CreateAsync(b10, "region");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b10 = _db.Users.FirstOrDefault(u => u.UserName == "chaley@thug.com");
				};
				if (await _userManager.IsInRoleAsync(b10, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b10, "Customer");
				}
				_db.SaveChanges();

				AppUser b11 = _db.Users.FirstOrDefault(u => u.UserName =="jeff@ggmail.com");
				if (b11 == null)
				{
					b11= new AppUser();
					b11.UserName = "jeff@ggmail.com";
					b11.Email = "jeff@ggmail.com";
					b11.PasswordHash = "hungry";
					b11.FirstName = "Jeffrey";
					b11.LastName = "Hampton";
					b11.MiddleInitial = "T";
					b11.StreetAddress = "337 38th St.";
					b11.CityAddress = "Austin";
					b11.StateAddress = "TX";
					b11.ZipCodeAddress = "78705";
					b11.PhoneNumber = "5126978613";
					b11.Birthday = new DateTime(1995, 1, 23);

					var result = await _userManager.CreateAsync(b11, "hungry");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b11 = _db.Users.FirstOrDefault(u => u.UserName == "jeff@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b11, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b11, "Customer");
				}
				_db.SaveChanges();

				AppUser b12 = _db.Users.FirstOrDefault(u => u.UserName =="wjhearniii@umch.edu");
				if (b12 == null)
				{
					b12= new AppUser();
					b12.UserName = "wjhearniii@umch.edu";
					b12.Email = "wjhearniii@umch.edu";
					b12.PasswordHash = "logicon";
					b12.FirstName = "John";
					b12.LastName = "Hearn";
					b12.MiddleInitial = "B";
					b12.StreetAddress = "4225 North First";
					b12.CityAddress = "Dallas";
					b12.StateAddress = "TX";
					b12.ZipCodeAddress = "75237";
					b12.PhoneNumber = "2148965621";
					b12.Birthday = new DateTime(1994, 1, 8);

					var result = await _userManager.CreateAsync(b12, "logicon");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b12 = _db.Users.FirstOrDefault(u => u.UserName == "wjhearniii@umch.edu");
				};
				if (await _userManager.IsInRoleAsync(b12, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b12, "Customer");
				}
				_db.SaveChanges();

				AppUser b13 = _db.Users.FirstOrDefault(u => u.UserName =="hicks43@ggmail.com");
				if (b13 == null)
				{
					b13= new AppUser();
					b13.UserName = "hicks43@ggmail.com";
					b13.Email = "hicks43@ggmail.com";
					b13.PasswordHash = "doofus";
					b13.FirstName = "Anthony";
					b13.LastName = "Hicks";
					b13.MiddleInitial = "J";
					b13.StreetAddress = "32 NE Garden Ln., Ste 910";
					b13.CityAddress = "San Antonio";
					b13.StateAddress = "TX";
					b13.ZipCodeAddress = "78239";
					b13.PhoneNumber = "2105788965";
					b13.Birthday = new DateTime(1990, 10, 6);

					var result = await _userManager.CreateAsync(b13, "doofus");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b13 = _db.Users.FirstOrDefault(u => u.UserName == "hicks43@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b13, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b13, "Customer");
				}
				_db.SaveChanges();

				AppUser b14 = _db.Users.FirstOrDefault(u => u.UserName =="bradsingram@mall.utexas.edu");
				if (b14 == null)
				{
					b14= new AppUser();
					b14.UserName = "bradsingram@mall.utexas.edu";
					b14.Email = "bradsingram@mall.utexas.edu";
					b14.PasswordHash = "mother";
					b14.FirstName = "Brad";
					b14.LastName = "Ingram";
					b14.MiddleInitial = "S";
					b14.StreetAddress = "6548 La Posada Ct.";
					b14.CityAddress = "Austin";
					b14.StateAddress = "TX";
					b14.ZipCodeAddress = "78736";
					b14.PhoneNumber = "5124678821";
					b14.Birthday = new DateTime(1984, 4, 12);

					var result = await _userManager.CreateAsync(b14, "mother");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b14 = _db.Users.FirstOrDefault(u => u.UserName == "bradsingram@mall.utexas.edu");
				};
				if (await _userManager.IsInRoleAsync(b14, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b14, "Customer");
				}
				_db.SaveChanges();

				AppUser b15 = _db.Users.FirstOrDefault(u => u.UserName =="mother.Ingram@aool.com");
				if (b15 == null)
				{
					b15= new AppUser();
					b15.UserName = "mother.Ingram@aool.com";
					b15.Email = "mother.Ingram@aool.com";
					b15.PasswordHash = "whimsical";
					b15.FirstName = "Todd";
					b15.LastName = "Jacobs";
					b15.MiddleInitial = "L";
					b15.StreetAddress = "4564 Elm St.";
					b15.CityAddress = "Austin";
					b15.StateAddress = "TX";
					b15.ZipCodeAddress = "78731";
					b15.PhoneNumber = "5124653365";
					b15.Birthday = new DateTime(1983, 4, 4);

					var result = await _userManager.CreateAsync(b15, "whimsical");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b15 = _db.Users.FirstOrDefault(u => u.UserName == "mother.Ingram@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b15, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b15, "Customer");
				}
				_db.SaveChanges();

				AppUser b16 = _db.Users.FirstOrDefault(u => u.UserName =="victoria@aool.com");
				if (b16 == null)
				{
					b16= new AppUser();
					b16.UserName = "victoria@aool.com";
					b16.Email = "victoria@aool.com";
					b16.PasswordHash = "nothing";
					b16.FirstName = "Victoria";
					b16.LastName = "Lawrence";
					b16.MiddleInitial = "M";
					b16.StreetAddress = "6639 Butterfly Ln.";
					b16.CityAddress = "Austin";
					b16.StateAddress = "TX";
					b16.ZipCodeAddress = "78761";
					b16.PhoneNumber = "5129457399";
					b16.Birthday = new DateTime(1961, 2, 3);

					var result = await _userManager.CreateAsync(b16, "nothing");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b16 = _db.Users.FirstOrDefault(u => u.UserName == "victoria@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b16, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b16, "Customer");
				}
				_db.SaveChanges();

				AppUser b17 = _db.Users.FirstOrDefault(u => u.UserName =="lineback@flush.net");
				if (b17 == null)
				{
					b17= new AppUser();
					b17.UserName = "lineback@flush.net";
					b17.Email = "lineback@flush.net";
					b17.PasswordHash = "GoodFellow";
					b17.FirstName = "Erik";
					b17.LastName = "Lineback";
					b17.MiddleInitial = "W";
					b17.StreetAddress = "1300 Netherland St";
					b17.CityAddress = "San Antonio";
					b17.StateAddress = "TX";
					b17.ZipCodeAddress = "78293";
					b17.PhoneNumber = "2102449976";
					b17.Birthday = new DateTime(1946, 9, 3);

					var result = await _userManager.CreateAsync(b17, "GoodFellow");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b17 = _db.Users.FirstOrDefault(u => u.UserName == "lineback@flush.net");
				};
				if (await _userManager.IsInRoleAsync(b17, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b17, "Customer");
				}
				_db.SaveChanges();

				AppUser b18 = _db.Users.FirstOrDefault(u => u.UserName =="elowe@netscrape.net");
				if (b18 == null)
				{
					b18= new AppUser();
					b18.UserName = "elowe@netscrape.net";
					b18.Email = "elowe@netscrape.net";
					b18.PasswordHash = "impede";
					b18.FirstName = "Ernest";
					b18.LastName = "Lowe";
					b18.MiddleInitial = "S";
					b18.StreetAddress = "3201 Pine Drive";
					b18.CityAddress = "San Antonio";
					b18.StateAddress = "TX";
					b18.ZipCodeAddress = "78279";
					b18.PhoneNumber = "2105344627";
					b18.Birthday = new DateTime(1992, 2, 7);

					var result = await _userManager.CreateAsync(b18, "impede");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b18 = _db.Users.FirstOrDefault(u => u.UserName == "elowe@netscrape.net");
				};
				if (await _userManager.IsInRoleAsync(b18, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b18, "Customer");
				}
				_db.SaveChanges();

				AppUser b19 = _db.Users.FirstOrDefault(u => u.UserName =="luce_chuck@ggmail.com");
				if (b19 == null)
				{
					b19= new AppUser();
					b19.UserName = "luce_chuck@ggmail.com";
					b19.Email = "luce_chuck@ggmail.com";
					b19.PasswordHash = "LuceyDucey";
					b19.FirstName = "Chuck";
					b19.LastName = "Luce";
					b19.MiddleInitial = "B";
					b19.StreetAddress = "2345 Rolling Clouds";
					b19.CityAddress = "San Antonio";
					b19.StateAddress = "TX";
					b19.ZipCodeAddress = "78268";
					b19.PhoneNumber = "2106983548";
					b19.Birthday = new DateTime(1942, 10, 25);

					var result = await _userManager.CreateAsync(b19, "LuceyDucey");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b19 = _db.Users.FirstOrDefault(u => u.UserName == "luce_chuck@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b19, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b19, "Customer");
				}
				_db.SaveChanges();

				AppUser b20 = _db.Users.FirstOrDefault(u => u.UserName =="mackcloud@pimpdaddy.com");
				if (b20 == null)
				{
					b20= new AppUser();
					b20.UserName = "mackcloud@pimpdaddy.com";
					b20.Email = "mackcloud@pimpdaddy.com";
					b20.PasswordHash = "cloudyday";
					b20.FirstName = "Jennifer";
					b20.LastName = "MacLeod";
					b20.MiddleInitial = "D";
					b20.StreetAddress = "2504 Far West Blvd.";
					b20.CityAddress = "Austin";
					b20.StateAddress = "TX";
					b20.ZipCodeAddress = "78731";
					b20.PhoneNumber = "5124748138";
					b20.Birthday = new DateTime(1965, 8, 6);

					var result = await _userManager.CreateAsync(b20, "cloudyday");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b20 = _db.Users.FirstOrDefault(u => u.UserName == "mackcloud@pimpdaddy.com");
				};
				if (await _userManager.IsInRoleAsync(b20, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b20, "Customer");
				}
				_db.SaveChanges();

				AppUser b21 = _db.Users.FirstOrDefault(u => u.UserName =="liz@ggmail.com");
				if (b21 == null)
				{
					b21= new AppUser();
					b21.UserName = "liz@ggmail.com";
					b21.Email = "liz@ggmail.com";
					b21.PasswordHash = "emarkbark";
					b21.FirstName = "Elizabeth";
					b21.LastName = "Markham";
					b21.MiddleInitial = "P";
					b21.StreetAddress = "7861 Chevy Chase";
					b21.CityAddress = "Austin";
					b21.StateAddress = "TX";
					b21.ZipCodeAddress = "78732";
					b21.PhoneNumber = "5124579845";
					b21.Birthday = new DateTime(1959, 4, 13);

					var result = await _userManager.CreateAsync(b21, "emarkbark");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b21 = _db.Users.FirstOrDefault(u => u.UserName == "liz@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b21, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b21, "Customer");
				}
				_db.SaveChanges();

				AppUser b22 = _db.Users.FirstOrDefault(u => u.UserName =="mclarence@aool.com");
				if (b22 == null)
				{
					b22= new AppUser();
					b22.UserName = "mclarence@aool.com";
					b22.Email = "mclarence@aool.com";
					b22.PasswordHash = "smartinmartin";
					b22.FirstName = "Clarence";
					b22.LastName = "Martin";
					b22.MiddleInitial = "A";
					b22.StreetAddress = "87 Alcedo St.";
					b22.CityAddress = "Houston";
					b22.StateAddress = "TX";
					b22.ZipCodeAddress = "77045";
					b22.PhoneNumber = "8174955201";
					b22.Birthday = new DateTime(1990, 1, 6);

					var result = await _userManager.CreateAsync(b22, "smartinmartin");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b22 = _db.Users.FirstOrDefault(u => u.UserName == "mclarence@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b22, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b22, "Customer");
				}
				_db.SaveChanges();

				AppUser b23 = _db.Users.FirstOrDefault(u => u.UserName =="smartinmartin.Martin@aool.com");
				if (b23 == null)
				{
					b23= new AppUser();
					b23.UserName = "smartinmartin.Martin@aool.com";
					b23.Email = "smartinmartin.Martin@aool.com";
					b23.PasswordHash = "looter";
					b23.FirstName = "Gregory";
					b23.LastName = "Martinez";
					b23.MiddleInitial = "R";
					b23.StreetAddress = "8295 Sunset Blvd.";
					b23.CityAddress = "Houston";
					b23.StateAddress = "TX";
					b23.ZipCodeAddress = "77030";
					b23.PhoneNumber = "8178746718";
					b23.Birthday = new DateTime(1987, 10, 9);

					var result = await _userManager.CreateAsync(b23, "looter");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b23 = _db.Users.FirstOrDefault(u => u.UserName == "smartinmartin.Martin@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b23, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b23, "Customer");
				}
				_db.SaveChanges();

				AppUser b24 = _db.Users.FirstOrDefault(u => u.UserName =="cmiller@mapster.com");
				if (b24 == null)
				{
					b24= new AppUser();
					b24.UserName = "cmiller@mapster.com";
					b24.Email = "cmiller@mapster.com";
					b24.PasswordHash = "chucky33";
					b24.FirstName = "Charles";
					b24.LastName = "Miller";
					b24.MiddleInitial = "R";
					b24.StreetAddress = "8962 Main St.";
					b24.CityAddress = "Houston";
					b24.StateAddress = "TX";
					b24.ZipCodeAddress = "77031";
					b24.PhoneNumber = "8177458615";
					b24.Birthday = new DateTime(1984, 7, 21);

					var result = await _userManager.CreateAsync(b24, "chucky33");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b24 = _db.Users.FirstOrDefault(u => u.UserName == "cmiller@mapster.com");
				};
				if (await _userManager.IsInRoleAsync(b24, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b24, "Customer");
				}
				_db.SaveChanges();

				AppUser b25 = _db.Users.FirstOrDefault(u => u.UserName =="nelson.Kelly@aool.com");
				if (b25 == null)
				{
					b25= new AppUser();
					b25.UserName = "nelson.Kelly@aool.com";
					b25.Email = "nelson.Kelly@aool.com";
					b25.PasswordHash = "orange";
					b25.FirstName = "Kelly";
					b25.LastName = "Nelson";
					b25.MiddleInitial = "T";
					b25.StreetAddress = "2601 Red River";
					b25.CityAddress = "Austin";
					b25.StateAddress = "TX";
					b25.ZipCodeAddress = "78703";
					b25.PhoneNumber = "5122926966";
					b25.Birthday = new DateTime(1956, 7, 4);

					var result = await _userManager.CreateAsync(b25, "orange");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b25 = _db.Users.FirstOrDefault(u => u.UserName == "nelson.Kelly@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b25, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b25, "Customer");
				}
				_db.SaveChanges();

				AppUser b26 = _db.Users.FirstOrDefault(u => u.UserName =="jojoe@ggmail.com");
				if (b26 == null)
				{
					b26= new AppUser();
					b26.UserName = "jojoe@ggmail.com";
					b26.Email = "jojoe@ggmail.com";
					b26.PasswordHash = "victorious";
					b26.FirstName = "Joe";
					b26.LastName = "Nguyen";
					b26.MiddleInitial = "C";
					b26.StreetAddress = "1249 4th SW St.";
					b26.CityAddress = "Dallas";
					b26.StateAddress = "TX";
					b26.ZipCodeAddress = "75238";
					b26.PhoneNumber = "2143125897";
					b26.Birthday = new DateTime(1963, 1, 29);

					var result = await _userManager.CreateAsync(b26, "victorious");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b26 = _db.Users.FirstOrDefault(u => u.UserName == "jojoe@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b26, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b26, "Customer");
				}
				_db.SaveChanges();

				AppUser b27 = _db.Users.FirstOrDefault(u => u.UserName =="orielly@foxnets.com");
				if (b27 == null)
				{
					b27= new AppUser();
					b27.UserName = "orielly@foxnets.com";
					b27.Email = "orielly@foxnets.com";
					b27.PasswordHash = "billyboy";
					b27.FirstName = "Bill";
					b27.LastName = "O'Reilly";
					b27.MiddleInitial = "T";
					b27.StreetAddress = "8800 Gringo Drive";
					b27.CityAddress = "San Antonio";
					b27.StateAddress = "TX";
					b27.ZipCodeAddress = "78260";
					b27.PhoneNumber = "2103450925";
					b27.Birthday = new DateTime(1983, 1, 7);

					var result = await _userManager.CreateAsync(b27, "billyboy");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b27 = _db.Users.FirstOrDefault(u => u.UserName == "orielly@foxnets.com");
				};
				if (await _userManager.IsInRoleAsync(b27, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b27, "Customer");
				}
				_db.SaveChanges();

				AppUser b28 = _db.Users.FirstOrDefault(u => u.UserName =="or@aool.com");
				if (b28 == null)
				{
					b28= new AppUser();
					b28.UserName = "or@aool.com";
					b28.Email = "or@aool.com";
					b28.PasswordHash = "radicalone";
					b28.FirstName = "Anka";
					b28.LastName = "Radkovich";
					b28.MiddleInitial = "L";
					b28.StreetAddress = "1300 Elliott Pl";
					b28.CityAddress = "Dallas";
					b28.StateAddress = "TX";
					b28.ZipCodeAddress = "75260";
					b28.PhoneNumber = "2142345566";
					b28.Birthday = new DateTime(1980, 3, 31);

					var result = await _userManager.CreateAsync(b28, "radicalone");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b28 = _db.Users.FirstOrDefault(u => u.UserName == "or@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b28, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b28, "Customer");
				}
				_db.SaveChanges();

				AppUser b29 = _db.Users.FirstOrDefault(u => u.UserName =="megrhodes@freezing.co.uk");
				if (b29 == null)
				{
					b29= new AppUser();
					b29.UserName = "megrhodes@freezing.co.uk";
					b29.Email = "megrhodes@freezing.co.uk";
					b29.PasswordHash = "gohorns";
					b29.FirstName = "Megan";
					b29.LastName = "Rhodes";
					b29.MiddleInitial = "C";
					b29.StreetAddress = "4587 Enfield Rd.";
					b29.CityAddress = "Austin";
					b29.StateAddress = "TX";
					b29.ZipCodeAddress = "78707";
					b29.PhoneNumber = "5123744746";
					b29.Birthday = new DateTime(1944, 8, 12);

					var result = await _userManager.CreateAsync(b29, "gohorns");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b29 = _db.Users.FirstOrDefault(u => u.UserName == "megrhodes@freezing.co.uk");
				};
				if (await _userManager.IsInRoleAsync(b29, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b29, "Customer");
				}
				_db.SaveChanges();

				AppUser b30 = _db.Users.FirstOrDefault(u => u.UserName =="erynrice@aool.com");
				if (b30 == null)
				{
					b30= new AppUser();
					b30.UserName = "erynrice@aool.com";
					b30.Email = "erynrice@aool.com";
					b30.PasswordHash = "iloveme";
					b30.FirstName = "Eryn";
					b30.LastName = "Rice";
					b30.MiddleInitial = "M";
					b30.StreetAddress = "3405 Rio Grande";
					b30.CityAddress = "Austin";
					b30.StateAddress = "TX";
					b30.ZipCodeAddress = "78705";
					b30.PhoneNumber = "5123876657";
					b30.Birthday = new DateTime(1934, 8, 2);

					var result = await _userManager.CreateAsync(b30, "iloveme");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b30 = _db.Users.FirstOrDefault(u => u.UserName == "erynrice@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b30, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b30, "Customer");
				}
				_db.SaveChanges();

				AppUser b31 = _db.Users.FirstOrDefault(u => u.UserName =="jorge@hootmail.com");
				if (b31 == null)
				{
					b31= new AppUser();
					b31.UserName = "jorge@hootmail.com";
					b31.Email = "jorge@hootmail.com";
					b31.PasswordHash = "greedy";
					b31.FirstName = "Jorge";
					b31.LastName = "Rodriguez";
					b31.MiddleInitial = "";
					b31.StreetAddress = "6788 Cotter Street";
					b31.CityAddress = "Houston";
					b31.StateAddress = "TX";
					b31.ZipCodeAddress = "77057";
					b31.PhoneNumber = "8178904374";
					b31.Birthday = new DateTime(1989, 8, 11);

					var result = await _userManager.CreateAsync(b31, "greedy");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b31 = _db.Users.FirstOrDefault(u => u.UserName == "jorge@hootmail.com");
				};
				if (await _userManager.IsInRoleAsync(b31, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b31, "Customer");
				}
				_db.SaveChanges();

				AppUser b32 = _db.Users.FirstOrDefault(u => u.UserName =="ra@aoo.com");
				if (b32 == null)
				{
					b32= new AppUser();
					b32.UserName = "ra@aoo.com";
					b32.Email = "ra@aoo.com";
					b32.PasswordHash = "familiar";
					b32.FirstName = "Allen";
					b32.LastName = "Rogers";
					b32.MiddleInitial = "B";
					b32.StreetAddress = "4965 Oak Hill";
					b32.CityAddress = "Austin";
					b32.StateAddress = "TX";
					b32.ZipCodeAddress = "78732";
					b32.PhoneNumber = "5128752943";
					b32.Birthday = new DateTime(1967, 8, 27);

					var result = await _userManager.CreateAsync(b32, "familiar");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b32 = _db.Users.FirstOrDefault(u => u.UserName == "ra@aoo.com");
				};
				if (await _userManager.IsInRoleAsync(b32, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b32, "Customer");
				}
				_db.SaveChanges();

				AppUser b33 = _db.Users.FirstOrDefault(u => u.UserName =="st-jean@home.com");
				if (b33 == null)
				{
					b33= new AppUser();
					b33.UserName = "st-jean@home.com";
					b33.Email = "st-jean@home.com";
					b33.PasswordHash = "historical";
					b33.FirstName = "Olivier";
					b33.LastName = "Saint-Jean";
					b33.MiddleInitial = "M";
					b33.StreetAddress = "255 Toncray Dr.";
					b33.CityAddress = "San Antonio";
					b33.StateAddress = "TX";
					b33.ZipCodeAddress = "78292";
					b33.PhoneNumber = "2104145678";
					b33.Birthday = new DateTime(1950, 7, 8);

					var result = await _userManager.CreateAsync(b33, "historical");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b33 = _db.Users.FirstOrDefault(u => u.UserName == "st-jean@home.com");
				};
				if (await _userManager.IsInRoleAsync(b33, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b33, "Customer");
				}
				_db.SaveChanges();

				AppUser b34 = _db.Users.FirstOrDefault(u => u.UserName =="ss34@ggmail.com");
				if (b34 == null)
				{
					b34= new AppUser();
					b34.UserName = "ss34@ggmail.com";
					b34.Email = "ss34@ggmail.com";
					b34.PasswordHash = "guiltless";
					b34.FirstName = "Sarah";
					b34.LastName = "Saunders";
					b34.MiddleInitial = "J";
					b34.StreetAddress = "332 Avenue C";
					b34.CityAddress = "Austin";
					b34.StateAddress = "TX";
					b34.ZipCodeAddress = "78705";
					b34.PhoneNumber = "5123497810";
					b34.Birthday = new DateTime(1977, 10, 29);

					var result = await _userManager.CreateAsync(b34, "guiltless");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b34 = _db.Users.FirstOrDefault(u => u.UserName == "ss34@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b34, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b34, "Customer");
				}
				_db.SaveChanges();

				AppUser b35 = _db.Users.FirstOrDefault(u => u.UserName =="willsheff@email.com");
				if (b35 == null)
				{
					b35= new AppUser();
					b35.UserName = "willsheff@email.com";
					b35.Email = "willsheff@email.com";
					b35.PasswordHash = "frequent";
					b35.FirstName = "William";
					b35.LastName = "Sewell";
					b35.MiddleInitial = "T";
					b35.StreetAddress = "2365 51st St.";
					b35.CityAddress = "Austin";
					b35.StateAddress = "TX";
					b35.ZipCodeAddress = "78709";
					b35.PhoneNumber = "5124510084";
					b35.Birthday = new DateTime(1941, 4, 21);

					var result = await _userManager.CreateAsync(b35, "frequent");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b35 = _db.Users.FirstOrDefault(u => u.UserName == "willsheff@email.com");
				};
				if (await _userManager.IsInRoleAsync(b35, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b35, "Customer");
				}
				_db.SaveChanges();

				AppUser b36 = _db.Users.FirstOrDefault(u => u.UserName =="sheff44@ggmail.com");
				if (b36 == null)
				{
					b36= new AppUser();
					b36.UserName = "sheff44@ggmail.com";
					b36.Email = "sheff44@ggmail.com";
					b36.PasswordHash = "history";
					b36.FirstName = "Martin";
					b36.LastName = "Sheffield";
					b36.MiddleInitial = "J";
					b36.StreetAddress = "3886 Avenue A";
					b36.CityAddress = "Austin";
					b36.StateAddress = "TX";
					b36.ZipCodeAddress = "78705";
					b36.PhoneNumber = "5125479167";
					b36.Birthday = new DateTime(1937, 11, 10);

					var result = await _userManager.CreateAsync(b36, "history");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b36 = _db.Users.FirstOrDefault(u => u.UserName == "sheff44@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b36, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b36, "Customer");
				}
				_db.SaveChanges();

				AppUser b37 = _db.Users.FirstOrDefault(u => u.UserName =="johnsmith187@aool.com");
				if (b37 == null)
				{
					b37= new AppUser();
					b37.UserName = "johnsmith187@aool.com";
					b37.Email = "johnsmith187@aool.com";
					b37.PasswordHash = "squirrel";
					b37.FirstName = "John";
					b37.LastName = "Smith";
					b37.MiddleInitial = "A";
					b37.StreetAddress = "23 Hidden Forge Dr.";
					b37.CityAddress = "San Antonio";
					b37.StateAddress = "TX";
					b37.ZipCodeAddress = "78280";
					b37.PhoneNumber = "2108321888";
					b37.Birthday = new DateTime(1954, 10, 26);

					var result = await _userManager.CreateAsync(b37, "squirrel");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b37 = _db.Users.FirstOrDefault(u => u.UserName == "johnsmith187@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b37, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b37, "Customer");
				}
				_db.SaveChanges();

				AppUser b38 = _db.Users.FirstOrDefault(u => u.UserName =="dustroud@mail.com");
				if (b38 == null)
				{
					b38= new AppUser();
					b38.UserName = "dustroud@mail.com";
					b38.Email = "dustroud@mail.com";
					b38.PasswordHash = "snakes";
					b38.FirstName = "Dustin";
					b38.LastName = "Stroud";
					b38.MiddleInitial = "P";
					b38.StreetAddress = "1212 Rita Rd";
					b38.CityAddress = "Dallas";
					b38.StateAddress = "TX";
					b38.ZipCodeAddress = "75221";
					b38.PhoneNumber = "2142346667";
					b38.Birthday = new DateTime(1932, 9, 1);

					var result = await _userManager.CreateAsync(b38, "snakes");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b38 = _db.Users.FirstOrDefault(u => u.UserName == "dustroud@mail.com");
				};
				if (await _userManager.IsInRoleAsync(b38, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b38, "Customer");
				}
				_db.SaveChanges();

				AppUser b39 = _db.Users.FirstOrDefault(u => u.UserName =="ericstuart@aool.com");
				if (b39 == null)
				{
					b39= new AppUser();
					b39.UserName = "ericstuart@aool.com";
					b39.Email = "ericstuart@aool.com";
					b39.PasswordHash = "landus";
					b39.FirstName = "Eric";
					b39.LastName = "Stuart";
					b39.MiddleInitial = "D";
					b39.StreetAddress = "5576 Toro Ring";
					b39.CityAddress = "Austin";
					b39.StateAddress = "TX";
					b39.ZipCodeAddress = "78746";
					b39.PhoneNumber = "5128178335";
					b39.Birthday = new DateTime(1930, 12, 28);

					var result = await _userManager.CreateAsync(b39, "landus");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b39 = _db.Users.FirstOrDefault(u => u.UserName == "ericstuart@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b39, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b39, "Customer");
				}
				_db.SaveChanges();

				AppUser b40 = _db.Users.FirstOrDefault(u => u.UserName =="peterstump@hootmail.com");
				if (b40 == null)
				{
					b40= new AppUser();
					b40.UserName = "peterstump@hootmail.com";
					b40.Email = "peterstump@hootmail.com";
					b40.PasswordHash = "rhythm";
					b40.FirstName = "Peter";
					b40.LastName = "Stump";
					b40.MiddleInitial = "L";
					b40.StreetAddress = "1300 Kellen Circle";
					b40.CityAddress = "Houston";
					b40.StateAddress = "TX";
					b40.ZipCodeAddress = "77018";
					b40.PhoneNumber = "8174560903";
					b40.Birthday = new DateTime(1989, 8, 13);

					var result = await _userManager.CreateAsync(b40, "rhythm");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b40 = _db.Users.FirstOrDefault(u => u.UserName == "peterstump@hootmail.com");
				};
				if (await _userManager.IsInRoleAsync(b40, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b40, "Customer");
				}
				_db.SaveChanges();

				AppUser b41 = _db.Users.FirstOrDefault(u => u.UserName =="tanner@ggmail.com");
				if (b41 == null)
				{
					b41= new AppUser();
					b41.UserName = "tanner@ggmail.com";
					b41.Email = "tanner@ggmail.com";
					b41.PasswordHash = "kindly";
					b41.FirstName = "Jeremy";
					b41.LastName = "Tanner";
					b41.MiddleInitial = "S";
					b41.StreetAddress = "4347 Almstead";
					b41.CityAddress = "Houston";
					b41.StateAddress = "TX";
					b41.ZipCodeAddress = "77044";
					b41.PhoneNumber = "8174590929";
					b41.Birthday = new DateTime(1982, 5, 21);

					var result = await _userManager.CreateAsync(b41, "kindly");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b41 = _db.Users.FirstOrDefault(u => u.UserName == "tanner@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b41, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b41, "Customer");
				}
				_db.SaveChanges();

				AppUser b42 = _db.Users.FirstOrDefault(u => u.UserName =="taylordjay@aool.com");
				if (b42 == null)
				{
					b42= new AppUser();
					b42.UserName = "taylordjay@aool.com";
					b42.Email = "taylordjay@aool.com";
					b42.PasswordHash = "instrument";
					b42.FirstName = "Allison";
					b42.LastName = "Taylor";
					b42.MiddleInitial = "R";
					b42.StreetAddress = "467 Nueces St.";
					b42.CityAddress = "Austin";
					b42.StateAddress = "TX";
					b42.ZipCodeAddress = "78705";
					b42.PhoneNumber = "5124748452";
					b42.Birthday = new DateTime(1960, 1, 8);

					var result = await _userManager.CreateAsync(b42, "instrument");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b42 = _db.Users.FirstOrDefault(u => u.UserName == "taylordjay@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b42, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b42, "Customer");
				}
				_db.SaveChanges();

				AppUser b43 = _db.Users.FirstOrDefault(u => u.UserName =="TayTaylor@aool.com");
				if (b43 == null)
				{
					b43= new AppUser();
					b43.UserName = "TayTaylor@aool.com";
					b43.Email = "TayTaylor@aool.com";
					b43.PasswordHash = "arched";
					b43.FirstName = "Rachel";
					b43.LastName = "Taylor";
					b43.MiddleInitial = "K";
					b43.StreetAddress = "345 Longview Dr.";
					b43.CityAddress = "Austin";
					b43.StateAddress = "TX";
					b43.ZipCodeAddress = "78705";
					b43.PhoneNumber = "5124512631";
					b43.Birthday = new DateTime(1975, 7, 27);

					var result = await _userManager.CreateAsync(b43, "arched");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b43 = _db.Users.FirstOrDefault(u => u.UserName == "TayTaylor@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b43, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b43, "Customer");
				}
				_db.SaveChanges();

				AppUser b44 = _db.Users.FirstOrDefault(u => u.UserName =="teefrank@hootmail.com");
				if (b44 == null)
				{
					b44= new AppUser();
					b44.UserName = "teefrank@hootmail.com";
					b44.Email = "teefrank@hootmail.com";
					b44.PasswordHash = "median";
					b44.FirstName = "Frank";
					b44.LastName = "Tee";
					b44.MiddleInitial = "J";
					b44.StreetAddress = "5590 Lavell Dr";
					b44.CityAddress = "Houston";
					b44.StateAddress = "TX";
					b44.ZipCodeAddress = "77004";
					b44.PhoneNumber = "8178765543";
					b44.Birthday = new DateTime(1968, 4, 6);

					var result = await _userManager.CreateAsync(b44, "median");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b44 = _db.Users.FirstOrDefault(u => u.UserName == "teefrank@hootmail.com");
				};
				if (await _userManager.IsInRoleAsync(b44, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b44, "Customer");
				}
				_db.SaveChanges();

				AppUser b45 = _db.Users.FirstOrDefault(u => u.UserName =="tuck33@ggmail.com");
				if (b45 == null)
				{
					b45= new AppUser();
					b45.UserName = "tuck33@ggmail.com";
					b45.Email = "tuck33@ggmail.com";
					b45.PasswordHash = "approval";
					b45.FirstName = "Clent";
					b45.LastName = "Tucker";
					b45.MiddleInitial = "J";
					b45.StreetAddress = "312 Main St.";
					b45.CityAddress = "Dallas";
					b45.StateAddress = "TX";
					b45.ZipCodeAddress = "75315";
					b45.PhoneNumber = "2148471154";
					b45.Birthday = new DateTime(1978, 5, 19);

					var result = await _userManager.CreateAsync(b45, "approval");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b45 = _db.Users.FirstOrDefault(u => u.UserName == "tuck33@ggmail.com");
				};
				if (await _userManager.IsInRoleAsync(b45, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b45, "Customer");
				}
				_db.SaveChanges();

				AppUser b46 = _db.Users.FirstOrDefault(u => u.UserName =="avelasco@yaho.com");
				if (b46 == null)
				{
					b46= new AppUser();
					b46.UserName = "avelasco@yaho.com";
					b46.Email = "avelasco@yaho.com";
					b46.PasswordHash = "decorate";
					b46.FirstName = "Allen";
					b46.LastName = "Velasco";
					b46.MiddleInitial = "G";
					b46.StreetAddress = "679 W. 4th";
					b46.CityAddress = "Dallas";
					b46.StateAddress = "TX";
					b46.ZipCodeAddress = "75207";
					b46.PhoneNumber = "2143985638";
					b46.Birthday = new DateTime(1963, 10, 6);

					var result = await _userManager.CreateAsync(b46, "decorate");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b46 = _db.Users.FirstOrDefault(u => u.UserName == "avelasco@yaho.com");
				};
				if (await _userManager.IsInRoleAsync(b46, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b46, "Customer");
				}
				_db.SaveChanges();

				AppUser b47 = _db.Users.FirstOrDefault(u => u.UserName =="westj@pioneer.net");
				if (b47 == null)
				{
					b47= new AppUser();
					b47.UserName = "westj@pioneer.net";
					b47.Email = "westj@pioneer.net";
					b47.PasswordHash = "grover";
					b47.FirstName = "Jake";
					b47.LastName = "West";
					b47.MiddleInitial = "T";
					b47.StreetAddress = "RR 3287";
					b47.CityAddress = "Dallas";
					b47.StateAddress = "TX";
					b47.ZipCodeAddress = "75323";
					b47.PhoneNumber = "2148475244";
					b47.Birthday = new DateTime(1993, 10, 14);

					var result = await _userManager.CreateAsync(b47, "grover");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b47 = _db.Users.FirstOrDefault(u => u.UserName == "westj@pioneer.net");
				};
				if (await _userManager.IsInRoleAsync(b47, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b47, "Customer");
				}
				_db.SaveChanges();

				AppUser b48 = _db.Users.FirstOrDefault(u => u.UserName =="louielouie@aool.com");
				if (b48 == null)
				{
					b48= new AppUser();
					b48.UserName = "louielouie@aool.com";
					b48.Email = "louielouie@aool.com";
					b48.PasswordHash = "sturdy";
					b48.FirstName = "Louis";
					b48.LastName = "Winthorpe";
					b48.MiddleInitial = "L";
					b48.StreetAddress = "2500 Padre Blvd";
					b48.CityAddress = "Dallas";
					b48.StateAddress = "TX";
					b48.ZipCodeAddress = "75220";
					b48.PhoneNumber = "2145650098";
					b48.Birthday = new DateTime(1952, 5, 31);

					var result = await _userManager.CreateAsync(b48, "sturdy");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b48 = _db.Users.FirstOrDefault(u => u.UserName == "louielouie@aool.com");
				};
				if (await _userManager.IsInRoleAsync(b48, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b48, "Customer");
				}
				_db.SaveChanges();

				AppUser b49 = _db.Users.FirstOrDefault(u => u.UserName =="rwood@voyager.net");
				if (b49 == null)
				{
					b49= new AppUser();
					b49.UserName = "rwood@voyager.net";
					b49.Email = "rwood@voyager.net";
					b49.PasswordHash = "decorous";
					b49.FirstName = "Reagan";
					b49.LastName = "Wood";
					b49.MiddleInitial = "B";
					b49.StreetAddress = "447 Westlake Dr.";
					b49.CityAddress = "Austin";
					b49.StateAddress = "TX";
					b49.ZipCodeAddress = "78746";
					b49.PhoneNumber = "5124545242";
					b49.Birthday = new DateTime(1992, 4, 24);

					var result = await _userManager.CreateAsync(b49, "decorous");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b49 = _db.Users.FirstOrDefault(u => u.UserName == "rwood@voyager.net");
				};
				if (await _userManager.IsInRoleAsync(b49, "Customer") == false)
				{
					await _userManager.AddToRoleAsync(b49, "Customer");
				}
				_db.SaveChanges();

			}
			catch (Exception e)
			{
				throw new InvalidOperationException(e.Message);
			}
		}
	}
}
