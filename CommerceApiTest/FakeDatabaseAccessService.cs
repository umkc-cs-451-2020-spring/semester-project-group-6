using CommerceApi;
using CommerceApi.dao;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace CommerceApiTest
{


    public class FakeDatabaseAccessService : IValuesDao
    {

        List<Transaction> fakeTransactions = new List<Transaction>();


        public FakeDatabaseAccessService()
        {
            Transaction fakeTransaction01 = new Transaction("000001", "03/10/20", "3,000.00", "DR", "$1.00", "Test transaction 1");
            Transaction fakeTransaction02 = new Transaction("000001", "03/12/20", "3,002.00", "DR", "$2", "Test transaction 2");
            Transaction fakeTransaction03 = new Transaction("000002", "03/12/20", "3,002.00", "DR", "$2", "Test transaction 3");
            fakeTransactions.Add(fakeTransaction01);
            fakeTransactions.Add(fakeTransaction02);
        }




        public void checkTriggers(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void clearNotifications(int accountNumber)
        {
            throw new NotImplementedException();
        }

        public void createNotification(Transaction transaction, string triggerType, string triggerValue)
        {
            throw new NotImplementedException();
        }

        public void createTrigger(int accountNumber, string triggerType, string triggerValue)
        {
            throw new NotImplementedException();
        }

        public void removeTrigger(int accountNumber, string triggerType, string triggerValue)
        {
            throw new NotImplementedException();
        }


        public List<Transaction> getAllTransactions()
        {
            return fakeTransactions;
        }

        public List<Transaction> getTransactionByAccountNumber(int accountNumber)
        {
            List<Transaction> fakeTransactionsByAccNum = new List<Transaction>();

            // finds all accounts matching accountNumber and adds them to a list
            for (int i = 0; i < fakeTransactions.Count; i++)
            {
                if (fakeTransactions[i].accountNumber == accountNumber.ToString())
                {
                    fakeTransactionsByAccNum.Add(fakeTransactions[i]);
                }
            }

            return fakeTransactionsByAccNum;
        }

        public Transaction insertTransaction(Transaction fakeTransaction)
        {
            fakeTransactions.Add(fakeTransaction);
            return fakeTransaction;

        }
    }
    
}
