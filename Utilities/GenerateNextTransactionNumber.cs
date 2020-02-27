using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team_4_Project.DAL;
namespace Team_4_Project.Utilities
{
    public class GenerateNextTransactionNumber
    {
        public static Int32 GetNextTransactionNumber(AppDbContext _context)
        {
            Int32 intMaxTransactionNumber; //the current maximum transaction number
            Int32 intNextTransactionNumber; //the transaction number for the next class

            if (_context.Transaction.Count() == 0) //there are no transactions in the database yet
            {
                intMaxTransactionNumber = 9999;//transaction numbers start at 10000
            }
            else
            {
                intMaxTransactionNumber = _context.Transaction.Max(a => a.TransactionNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextTransactionNumber = intMaxTransactionNumber + 1;

            //return the value
            return intNextTransactionNumber;
        }
    }
}
