using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CommerceApi
{
    public class TransactionReader
    {
        public static List<Transaction> ImportTransactions(string file)
        {
            List<string> lines = new List<string>();
            List<Transaction> transactionList = new List<Transaction>();

            try
            {
                lines = File.ReadAllLines(file).ToList();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
           
            foreach (string line in lines)
            {
                string[] cell = line.Split(',');
                Transaction transaction = new Transaction(
                    Guid.NewGuid(),
                    cell[0],
                    cell[1],
                    cell[2],
                    cell[3],
                    cell[4],
                    cell[5],
                    cell[6]);
                transactionList.Add(transaction);
            }

            // print to console for testing
            /*
            foreach (Transaction transaction in transactionList)
            {
                Console.WriteLine(transaction);
            }
            // Console.ReadLine();
            */ 

            return transactionList;
        }
    }
}
