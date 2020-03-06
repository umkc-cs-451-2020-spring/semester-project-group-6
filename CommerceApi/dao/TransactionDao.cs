using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao
{

    public interface ITransactionDao
    {
        List<Transaction> getAllTransactions();

        Transaction insertTransaction(Guid id, Transaction transaction);
       
    }


}
