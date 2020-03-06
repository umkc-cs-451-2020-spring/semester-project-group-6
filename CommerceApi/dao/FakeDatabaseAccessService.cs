using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao
{
    public class FakeDatabaseAccessService : ITransactionDao
    {
        public List<Transaction> fakeTransactions = new List<Transaction>();
        public Transaction transaction01 = new Transaction(Guid.NewGuid(), "000001", "Checking", "03/10/20", "3,000.00", "DR", "$1.00", "Test transaction");
        public Transaction transaction02 = new Transaction(Guid.NewGuid(), "000002", "Savings", "03/02/20", "3,567.00", "DR", "$1.00", "Test transaction");

        public List<Transaction> getAllTransactions()
        {
            fakeTransactions.Add(transaction01);
            fakeTransactions.Add(transaction02);

            return fakeTransactions;
        }

        public Transaction insertTransaction(Guid id, Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}

