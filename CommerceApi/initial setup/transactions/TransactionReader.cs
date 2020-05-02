using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


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
                string[] cell = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                Transaction transaction = new Transaction(
                    cell[0],
                    cell[1],
                    cell[2],
                    cell[3].Substring(cell[3].IndexOf("\"") + 1, cell[3].Length - 2),
                    cell[4],
                    cell[5],
                    cell[6],
                    cell[7],
                    cell[8]);
                transactionList.Add(transaction);
            }

            return transactionList;
        }
    }
}