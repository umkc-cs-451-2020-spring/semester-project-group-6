using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao
{

    public interface ITransactionDao
    {
        List<Transaction> getAllTransactions();
        List<Transaction> getTransactionByAccountNumber(int accountNumber);
        Transaction insertTransaction(Transaction transaction);

    }


}
