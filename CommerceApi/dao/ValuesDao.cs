using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao
{

    public interface IValuesDao
    {
        List<Transaction> getAllTransactions();
        List<Transaction> getTransactionByAccountNumber(string accountNumber);
        List<Notification> getAllNotifications();
        List<Notification> getNotificationsByAccount(string accountNumber);
        List<Transaction> getTransactionByID(string transactionID);
        void insertTransaction(Transaction transaction);
        void checkTriggers(Transaction transaction);
        void createNotification(Transaction transaction, string triggerType, string triggerValue);
        void createTrigger(string accountNumber, string triggerType, string triggerValue);
        void removeTrigger(string accountNumber, string triggerType, string triggerValue);
        void clearNotifications(string accountNumber);
        void clearTriggers(string accountNumber);
        void updateTransactionID();
    }
}