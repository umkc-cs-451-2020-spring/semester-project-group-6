using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao
{

    public interface IValuesDao
    {
        List<Transaction> getAllTransactions();
        List<Transaction> getTransactionByAccountNumber(int accountNumber);
        Transaction insertTransaction(Transaction transaction);
        void checkTriggers(Transaction transaction);
        void createNotification(Transaction transaction, string triggerType, string triggerValue);
        void createTrigger(int accountNumber, string triggerType, string triggerValue);
        void removeTrigger(int accountNumber, string triggerType, string triggerValue);
        void clearNotifications(int accountNumber);
    }
}