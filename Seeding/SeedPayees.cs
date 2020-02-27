using Team_4_Project.Models;
using Team_4_Project.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Team_4_Project.Seeding
{
    public static class SeedPayees
    {
        public static void SeedAllPayees(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            {
                if (_db.Payees.Count() == 6)
                {
                    throw new NotSupportedException("The database already contains all 6 payees!");
                }

                Int32 intPayeesAdded = 0;
                String strPayeeName = "Begin"; //helps to keep track of error on Payees
                List<Payee> Payees = new List<Payee>();

                try
                {
                    Payee b1 = new Payee()
                    {
                        Name = "City of Austin Water",
                        PayeeType = PayeeType.Utilities,
                        PayeeStreetAddress = "113 South Congress Ave.",
                        PayeeCityAddress = "Austin",
                        PayeeStateAddress = "TX",
                        PayeeZipCode = "78710",
                        PayeePhoneNumber = "5126645558",
                    };
                    Payees.Add(b1);

                    Payee b2 = new Payee()
                    {
                        Name = "Reliant Energy",
                        PayeeType = PayeeType.Utilities,
                        PayeeStreetAddress = "3500 E. Interstate 10",
                        PayeeCityAddress = "Houston",
                        PayeeStateAddress = "TX",
                        PayeeZipCode = "77099",
                        PayeePhoneNumber = "7135546697",
                    };
                    Payees.Add(b2);

                    Payee b3 = new Payee()
                    {
                        Name = "Lee Properties",
                        PayeeType = PayeeType.Rent,
                        PayeeStreetAddress = "2500 Salado",
                        PayeeCityAddress = "Austin",
                        PayeeStateAddress = "TX",
                        PayeeZipCode = "78705",
                        PayeePhoneNumber = "5124453312",
                    };
                    Payees.Add(b3);

                    Payee b4 = new Payee()
                    {
                        Name = "Capital One",
                        PayeeType = PayeeType.CreditCard,
                        PayeeStreetAddress = "1299 Fargo Blvd.",
                        PayeeCityAddress = "Cheyenne",
                        PayeeStateAddress = "WY",
                        PayeeZipCode = "82001",
                        PayeePhoneNumber = "5302215542",
                    };
                    Payees.Add(b4);

                    Payee b5 = new Payee()
                    {
                        Name = "Vanguard Title",
                        PayeeType = PayeeType.Mortgage,
                        PayeeStreetAddress = "10976 Interstate 35 South",
                        PayeeCityAddress = "Austin",
                        PayeeStateAddress = "TX",
                        PayeeZipCode = "78745",
                        PayeePhoneNumber = "5128654951",
                    };
                    Payees.Add(b5);

                    Payee b6 = new Payee()
                    {
                        Name = "Lawn Care of Texas",
                        PayeeType = PayeeType.Other,
                        PayeeStreetAddress = "4473 W. 3rd Street",
                        PayeeCityAddress = "Austin",
                        PayeeStateAddress = "TX",
                        PayeeZipCode = "78712",
                        PayeePhoneNumber = "5123365247",
                    };
                    Payees.Add(b6);

                    try
                    {
                        foreach (Payee payeeToAdd in Payees)
                        {
                            strPayeeName = payeeToAdd.Name;
                            Payee _dbPayee = _db.Payees.FirstOrDefault(b => b.Name == payeeToAdd.Name);
                            if (_dbPayee == null) //this Payee doesn't exist
                            {
                                _db.Payees.Add(payeeToAdd);
                                _db.SaveChanges();
                                intPayeesAdded += 1;
                            }
                            else //Payee exists - update values
                            {
                                _dbPayee.Name = payeeToAdd.Name;
                                _dbPayee.PayeeType = payeeToAdd.PayeeType;
                                _dbPayee.PayeeStreetAddress = payeeToAdd.PayeeStreetAddress;
                                _dbPayee.PayeeCityAddress = payeeToAdd.PayeeCityAddress;
                                _dbPayee.PayeeStateAddress = payeeToAdd.PayeeStateAddress;
                                _dbPayee.PayeeZipCode = payeeToAdd.PayeeZipCode;
                                _dbPayee.PayeePhoneNumber = payeeToAdd.PayeePhoneNumber;
                                _db.Update(_dbPayee);
                                _db.SaveChanges();
                                intPayeesAdded += 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        String msg = "  Repositories added:" + intPayeesAdded + "; Error on " + strPayeeName;
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
}

