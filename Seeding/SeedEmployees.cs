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
	public static class SeedEmployees
	{
		private static readonly object _userManager;

		public static async Task AddEmployee(IServiceProvider serviceProvider)
		{
			AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
			UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			RoleManager<IdentityRole> _rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			if (_db.Users.Count() == 70)
			{
				throw new NotSupportedException("The database already contains all 20 employees!");
			}

			Int32 intCustomersAdded = 0;
			String strEmailAddress = "Begin"; //helps to keep track of error on customers
			List<AppUser> Employees = new List<AppUser>();

			try
			{
				AppUser b1 = _db.Users.FirstOrDefault(u => u.UserName =="t.jacobs@longhornbank.neet");
				if (b1 == null)
				{
					var EmployeeType = "Employee";
					b1= new AppUser();
					b1.UserName = "t.jacobs@longhornbank.neet";
					b1.Email = "t.jacobs@longhornbank.neet";
					b1.PasswordHash = "society";
					b1.FirstName = "Todd";
					b1.LastName = "Jacobs";
					b1.MiddleInitial = "L";
					b1.StreetAddress = "4564 Elm St.";
					b1.CityAddress = "Houston";
					b1.StateAddress = "TX";
					b1.ZipCodeAddress = "77003";
					b1.PhoneNumber = "8176593544";

					var result = await _userManager.CreateAsync(b1, "society");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b1 = _db.Users.FirstOrDefault(u => u.UserName == "t.jacobs@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b1, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b1, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b1, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b1, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b2 = _db.Users.FirstOrDefault(u => u.UserName =="e.rice@longhornbank.neet");
				if (b2 == null)
				{
					var EmployeeType = "Employee";
					b2= new AppUser();
					b2.UserName = "e.rice@longhornbank.neet";
					b2.Email = "e.rice@longhornbank.neet";
					b2.PasswordHash = "ricearoni";
					b2.FirstName = "Eryn";
					b2.LastName = "Rice";
					b2.MiddleInitial = "M";
					b2.StreetAddress = "3405 Rio Grande";
					b2.CityAddress = "Dallas";
					b2.StateAddress = "TX";
					b2.ZipCodeAddress = "75261";
					b2.PhoneNumber = "2148475583";

					var result = await _userManager.CreateAsync(b2, "ricearoni");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b2 = _db.Users.FirstOrDefault(u => u.UserName == "e.rice@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b2, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b2, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b2, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b2, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b3 = _db.Users.FirstOrDefault(u => u.UserName =="b.ingram@longhornbank.neet");
				if (b3 == null)
				{
					var EmployeeType = "Employee";
					b3= new AppUser();
					b3.UserName = "b.ingram@longhornbank.neet";
					b3.Email = "b.ingram@longhornbank.neet";
					b3.PasswordHash = "ingram45";
					b3.FirstName = "Brad";
					b3.LastName = "Ingram";
					b3.MiddleInitial = "S";
					b3.StreetAddress = "6548 La Posada Ct.";
					b3.CityAddress = "Austin";
					b3.StateAddress = "TX";
					b3.ZipCodeAddress = "78705";
					b3.PhoneNumber = "5126978613";

					var result = await _userManager.CreateAsync(b3, "ingram45");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b3 = _db.Users.FirstOrDefault(u => u.UserName == "b.ingram@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b3, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b3, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b3, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b3, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b4 = _db.Users.FirstOrDefault(u => u.UserName =="a.taylor@longhornbank.neet");
				if (b4 == null)
				{
					var EmployeeType = "Manager";
					b4= new AppUser();
					b4.UserName = "a.taylor@longhornbank.neet";
					b4.Email = "a.taylor@longhornbank.neet";
					b4.PasswordHash = "nostalgic";
					b4.FirstName = "Allison";
					b4.LastName = "Taylor";
					b4.MiddleInitial = "R";
					b4.StreetAddress = "467 Nueces St.";
					b4.CityAddress = "Dallas";
					b4.StateAddress = "TX";
					b4.ZipCodeAddress = "75237";
					b4.PhoneNumber = "2148965621";

					var result = await _userManager.CreateAsync(b4, "nostalgic");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b4 = _db.Users.FirstOrDefault(u => u.UserName == "a.taylor@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b4, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b4, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b4, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b4, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b5 = _db.Users.FirstOrDefault(u => u.UserName =="g.martinez@longhornbank.neet");
				if (b5 == null)
				{
					var EmployeeType = "Employee";
					b5= new AppUser();
					b5.UserName = "g.martinez@longhornbank.neet";
					b5.Email = "g.martinez@longhornbank.neet";
					b5.PasswordHash = "fungus";
					b5.FirstName = "Gregory";
					b5.LastName = "Martinez";
					b5.MiddleInitial = "R";
					b5.StreetAddress = "8295 Sunset Blvd.";
					b5.CityAddress = "San Antonio";
					b5.StateAddress = "TX";
					b5.ZipCodeAddress = "78239";
					b5.PhoneNumber = "2105788965";

					var result = await _userManager.CreateAsync(b5, "fungus");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b5 = _db.Users.FirstOrDefault(u => u.UserName == "g.martinez@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b5, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b5, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b5, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b5, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b6 = _db.Users.FirstOrDefault(u => u.UserName =="m.sheffield@longhornbank.neet");
				if (b6 == null)
				{
					var EmployeeType = "Manager";
					b6= new AppUser();
					b6.UserName = "m.sheffield@longhornbank.neet";
					b6.Email = "m.sheffield@longhornbank.neet";
					b6.PasswordHash = "longhorns";
					b6.FirstName = "Martin";
					b6.LastName = "Sheffield";
					b6.MiddleInitial = "J";
					b6.StreetAddress = "3886 Avenue A";
					b6.CityAddress = "Austin";
					b6.StateAddress = "TX";
					b6.ZipCodeAddress = "78736";
					b6.PhoneNumber = "5124678821";

					var result = await _userManager.CreateAsync(b6, "longhorns");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b6 = _db.Users.FirstOrDefault(u => u.UserName == "m.sheffield@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b6, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b6, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b6, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b6, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b7 = _db.Users.FirstOrDefault(u => u.UserName =="j.macleod@longhornbank.neet");
				if (b7 == null)
				{
					var EmployeeType = "Manager";
					b7= new AppUser();
					b7.UserName = "j.macleod@longhornbank.neet";
					b7.Email = "j.macleod@longhornbank.neet";
					b7.PasswordHash = "smitty";
					b7.FirstName = "Jennifer";
					b7.LastName = "MacLeod";
					b7.MiddleInitial = "D";
					b7.StreetAddress = "2504 Far West Blvd.";
					b7.CityAddress = "Austin";
					b7.StateAddress = "TX";
					b7.ZipCodeAddress = "78731";
					b7.PhoneNumber = "5124653365";

					var result = await _userManager.CreateAsync(b7, "smitty");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b7 = _db.Users.FirstOrDefault(u => u.UserName == "j.macleod@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b7, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b7, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b7, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b7, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b8 = _db.Users.FirstOrDefault(u => u.UserName =="j.tanner@longhornbank.neet");
				if (b8 == null)
				{
					var EmployeeType = "Employee";
					b8= new AppUser();
					b8.UserName = "j.tanner@longhornbank.neet";
					b8.Email = "j.tanner@longhornbank.neet";
					b8.PasswordHash = "tanman";
					b8.FirstName = "Jeremy";
					b8.LastName = "Tanner";
					b8.MiddleInitial = "S";
					b8.StreetAddress = "4347 Almstead";
					b8.CityAddress = "Austin";
					b8.StateAddress = "TX";
					b8.ZipCodeAddress = "78761";
					b8.PhoneNumber = "5129457399";

					var result = await _userManager.CreateAsync(b8, "tanman");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b8 = _db.Users.FirstOrDefault(u => u.UserName == "j.tanner@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b8, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b8, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b8, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b8, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b9 = _db.Users.FirstOrDefault(u => u.UserName =="m.rhodes@longhornbank.neet");
				if (b9 == null)
				{
					var EmployeeType = "Manager";
					b9= new AppUser();
					b9.UserName = "m.rhodes@longhornbank.neet";
					b9.Email = "m.rhodes@longhornbank.neet";
					b9.PasswordHash = "countryrhodes";
					b9.FirstName = "Megan";
					b9.LastName = "Rhodes";
					b9.MiddleInitial = "C";
					b9.StreetAddress = "4587 Enfield Rd.";
					b9.CityAddress = "San Antonio";
					b9.StateAddress = "TX";
					b9.ZipCodeAddress = "78293";
					b9.PhoneNumber = "2102449976";

					var result = await _userManager.CreateAsync(b9, "countryrhodes");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b9 = _db.Users.FirstOrDefault(u => u.UserName == "m.rhodes@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b9, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b9, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b9, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b9, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b10 = _db.Users.FirstOrDefault(u => u.UserName =="e.stuart@longhornbank.neet");
				if (b10 == null)
				{
					var EmployeeType = "Manager";
					b10= new AppUser();
					b10.UserName = "e.stuart@longhornbank.neet";
					b10.Email = "e.stuart@longhornbank.neet";
					b10.PasswordHash = "stewboy";
					b10.FirstName = "Eric";
					b10.LastName = "Stuart";
					b10.MiddleInitial = "F";
					b10.StreetAddress = "5576 Toro Ring";
					b10.CityAddress = "San Antonio";
					b10.StateAddress = "TX";
					b10.ZipCodeAddress = "78279";
					b10.PhoneNumber = "2105344627";

					var result = await _userManager.CreateAsync(b10, "stewboy");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b10 = _db.Users.FirstOrDefault(u => u.UserName == "e.stuart@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b10, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b10, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b10, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b10, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b11 = _db.Users.FirstOrDefault(u => u.UserName =="l.chung@longhornbank.neet");
				if (b11 == null)
				{
					var EmployeeType = "Employee";
					b11= new AppUser();
					b11.UserName = "l.chung@longhornbank.neet";
					b11.Email = "l.chung@longhornbank.neet";
					b11.PasswordHash = "lisssa";
					b11.FirstName = "Lisa";
					b11.LastName = "Chung";
					b11.MiddleInitial = "N";
					b11.StreetAddress = "234 RR 12";
					b11.CityAddress = "San Antonio";
					b11.StateAddress = "TX";
					b11.ZipCodeAddress = "78268";
					b11.PhoneNumber = "2106983548";

					var result = await _userManager.CreateAsync(b11, "lisssa");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b11 = _db.Users.FirstOrDefault(u => u.UserName == "l.chung@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b11, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b11, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b11, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b11, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b12 = _db.Users.FirstOrDefault(u => u.UserName =="l.swanson@longhornbank.neet");
				if (b12 == null)
				{
					var EmployeeType = "Manager";
					b12= new AppUser();
					b12.UserName = "l.swanson@longhornbank.neet";
					b12.Email = "l.swanson@longhornbank.neet";
					b12.PasswordHash = "swansong";
					b12.FirstName = "Leon";
					b12.LastName = "Swanson";
					b12.MiddleInitial = "";
					b12.StreetAddress = "245 River Rd";
					b12.CityAddress = "Austin";
					b12.StateAddress = "TX";
					b12.ZipCodeAddress = "78731";
					b12.PhoneNumber = "5124748138";

					var result = await _userManager.CreateAsync(b12, "swansong");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b12 = _db.Users.FirstOrDefault(u => u.UserName == "l.swanson@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b12, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b12, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b12, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b12, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b13 = _db.Users.FirstOrDefault(u => u.UserName =="w.loter@longhornbank.neet");
				if (b13 == null)
				{
					var EmployeeType = "Employee";
					b13= new AppUser();
					b13.UserName = "w.loter@longhornbank.neet";
					b13.Email = "w.loter@longhornbank.neet";
					b13.PasswordHash = "lottery";
					b13.FirstName = "Wanda";
					b13.LastName = "Loter";
					b13.MiddleInitial = "K";
					b13.StreetAddress = "3453 RR 3235";
					b13.CityAddress = "Austin";
					b13.StateAddress = "TX";
					b13.ZipCodeAddress = "78732";
					b13.PhoneNumber = "5124579845";

					var result = await _userManager.CreateAsync(b13, "lottery");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b13 = _db.Users.FirstOrDefault(u => u.UserName == "w.loter@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b13, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b13, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b13, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b13, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b14 = _db.Users.FirstOrDefault(u => u.UserName =="j.white@longhornbank.neet");
				if (b14 == null)
				{
					var EmployeeType = "Manager";
					b14= new AppUser();
					b14.UserName = "j.white@longhornbank.neet";
					b14.Email = "j.white@longhornbank.neet";
					b14.PasswordHash = "evanescent";
					b14.FirstName = "Jason";
					b14.LastName = "White";
					b14.MiddleInitial = "M";
					b14.StreetAddress = "12 Valley View";
					b14.CityAddress = "Houston";
					b14.StateAddress = "TX";
					b14.ZipCodeAddress = "77045";
					b14.PhoneNumber = "8174955201";

					var result = await _userManager.CreateAsync(b14, "evanescent");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b14 = _db.Users.FirstOrDefault(u => u.UserName == "j.white@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b14, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b14, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b14, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b14, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b15 = _db.Users.FirstOrDefault(u => u.UserName =="w.montgomery@longhornbank.neet");
				if (b15 == null)
				{
					var EmployeeType = "Manager";
					b15= new AppUser();
					b15.UserName = "w.montgomery@longhornbank.neet";
					b15.Email = "w.montgomery@longhornbank.neet";
					b15.PasswordHash = "monty3";
					b15.FirstName = "Wilda";
					b15.LastName = "Montgomery";
					b15.MiddleInitial = "K";
					b15.StreetAddress = "210 Blanco Dr";
					b15.CityAddress = "Houston";
					b15.StateAddress = "TX";
					b15.ZipCodeAddress = "77030";
					b15.PhoneNumber = "8178746718";

					var result = await _userManager.CreateAsync(b15, "monty3");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b15 = _db.Users.FirstOrDefault(u => u.UserName == "w.montgomery@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b15, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b15, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b15, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b15, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b16 = _db.Users.FirstOrDefault(u => u.UserName =="h.morales@longhornbank.neet");
				if (b16 == null)
				{
					var EmployeeType = "Employee";
					b16= new AppUser();
					b16.UserName = "h.morales@longhornbank.neet";
					b16.Email = "h.morales@longhornbank.neet";
					b16.PasswordHash = "hecktour";
					b16.FirstName = "Hector";
					b16.LastName = "Morales";
					b16.MiddleInitial = "N";
					b16.StreetAddress = "4501 RR 140";
					b16.CityAddress = "Houston";
					b16.StateAddress = "TX";
					b16.ZipCodeAddress = "77031";
					b16.PhoneNumber = "8177458615";

					var result = await _userManager.CreateAsync(b16, "hecktour");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b16 = _db.Users.FirstOrDefault(u => u.UserName == "h.morales@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b16, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b16, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b16, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b16, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b17 = _db.Users.FirstOrDefault(u => u.UserName =="m.rankin@longhornbank.neet");
				if (b17 == null)
				{
					var EmployeeType = "Employee";
					b17= new AppUser();
					b17.UserName = "m.rankin@longhornbank.neet";
					b17.Email = "m.rankin@longhornbank.neet";
					b17.PasswordHash = "rankmary";
					b17.FirstName = "Mary";
					b17.LastName = "Rankin";
					b17.MiddleInitial = "T";
					b17.StreetAddress = "340 Second St";
					b17.CityAddress = "Austin";
					b17.StateAddress = "TX";
					b17.ZipCodeAddress = "78703";
					b17.PhoneNumber = "5122926966";

					var result = await _userManager.CreateAsync(b17, "rankmary");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b17 = _db.Users.FirstOrDefault(u => u.UserName == "m.rankin@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b17, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b17, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b17, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b17, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b18 = _db.Users.FirstOrDefault(u => u.UserName =="l.walker@longhornbank.neet");
				if (b18 == null)
				{
					var EmployeeType = "Manager";
					b18= new AppUser();
					b18.UserName = "l.walker@longhornbank.neet";
					b18.Email = "l.walker@longhornbank.neet";
					b18.PasswordHash = "walkamile";
					b18.FirstName = "Larry";
					b18.LastName = "Walker";
					b18.MiddleInitial = "G";
					b18.StreetAddress = "9 Bison Circle";
					b18.CityAddress = "Dallas";
					b18.StateAddress = "TX";
					b18.ZipCodeAddress = "75238";
					b18.PhoneNumber = "2143125897";

					var result = await _userManager.CreateAsync(b18, "walkamile");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b18 = _db.Users.FirstOrDefault(u => u.UserName == "l.walker@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b18, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b18, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b18, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b18, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b19 = _db.Users.FirstOrDefault(u => u.UserName =="g.chang@longhornbank.neet");
				if (b19 == null)
				{
					var EmployeeType = "Manager";
					b19= new AppUser();
					b19.UserName = "g.chang@longhornbank.neet";
					b19.Email = "g.chang@longhornbank.neet";
					b19.PasswordHash = "changalang";
					b19.FirstName = "George";
					b19.LastName = "Chang";
					b19.MiddleInitial = "M";
					b19.StreetAddress = "9003 Joshua St";
					b19.CityAddress = "San Antonio";
					b19.StateAddress = "TX";
					b19.ZipCodeAddress = "78260";
					b19.PhoneNumber = "2103450925";

					var result = await _userManager.CreateAsync(b19, "changalang");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b19 = _db.Users.FirstOrDefault(u => u.UserName == "g.chang@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b19, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b19, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b19, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b19, "Manager");
						}
					};
				};
				_db.SaveChanges();

				AppUser b20 = _db.Users.FirstOrDefault(u => u.UserName =="g.gonzalez@longhornbank.neet");
				if (b20 == null)
				{
					var EmployeeType = "Employee";
					b20= new AppUser();
					b20.UserName = "g.gonzalez@longhornbank.neet";
					b20.Email = "g.gonzalez@longhornbank.neet";
					b20.PasswordHash = "offbeat";
					b20.FirstName = "Gwen";
					b20.LastName = "Gonzalez";
					b20.MiddleInitial = "J";
					b20.StreetAddress = "103 Manor Rd";
					b20.CityAddress = "Dallas";
					b20.StateAddress = "TX";
					b20.ZipCodeAddress = "75260";
					b20.PhoneNumber = "2142345566";

					var result = await _userManager.CreateAsync(b20, "offbeat");
					if (result.Succeeded == false)
					{
						throw new Exception("This user can't be added -"+ result.ToString());
					}
					_db.SaveChanges();
					b20 = _db.Users.FirstOrDefault(u => u.UserName == "g.gonzalez@longhornbank.neet");
					if (EmployeeType == "Employee")
					{
						if (await _userManager.IsInRoleAsync(b20, "Employee") == false)
						{
							await _userManager.AddToRoleAsync(b20, "Employee");
						}
					};
					if (EmployeeType == "Manager")
					{
						if (await _userManager.IsInRoleAsync(b20, "Manager") == false)
						{
							await _userManager.AddToRoleAsync(b20, "Manager");
						}
					};
				};
				_db.SaveChanges();

			}
			catch (Exception e)
			{
				throw new InvalidOperationException(e.Message);
			}
		}
	}
}
