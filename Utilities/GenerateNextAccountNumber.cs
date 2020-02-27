using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team_4_Project.DAL;

namespace Team_4_Project.Utilities
{
    public class GenerateNextAccountNumber
    {
        public static Int32 GetNextAccountNumber(AppDbContext _context)
        {
            Int32 intMaxAccountNumber; //the current maximum account number
            Int32 intNextAccountNumber; //the account number for the next class

            if (_context.Account.Count() == 0) //there are no accounts in the database yet
            {
                intMaxAccountNumber = 999999999;//account numbers start at 1000000000
            }
            else
            {
                intMaxAccountNumber = _context.Account.Max(a => a.AccountNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextAccountNumber = intMaxAccountNumber + 1;

            //return the value
            return intNextAccountNumber;
        }
    }
}
