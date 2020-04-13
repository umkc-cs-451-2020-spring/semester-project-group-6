using CommerceApi;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CommerceApiTest
{
    public class FakeDBAccessServiceTests
    {

        FakeDatabaseAccessService fakeDatabaseAccessService = new FakeDatabaseAccessService();


        // lists and objects for expected values
        List<Transaction> actualTransactions = new List<Transaction>();

        List<Transaction> expectedTransactions = new List<Transaction>();
        Transaction expectedTransaction01 = new Transaction("000001", "03/10/20", "3,000.00", "DR", "$1.00", "Test transaction 1");
        Transaction expectedTransaction02 = new Transaction("000001", "03/12/20", "3,002.00", "DR", "$2", "Test transaction 2");

        List<Transaction> expectedTransactionsByAccNum = new List<Transaction>();
        Transaction expectedTransactionByAccNum01 = new Transaction("000002", "03/12/20", "3,002.00", "DR", "$2", "Test transaction 3");



        public FakeDBAccessServiceTests()
        {
            actualTransactions = fakeDatabaseAccessService.getAllTransactions();
            expectedTransactions.Add(expectedTransaction01);
            expectedTransactions.Add(expectedTransaction02);

            expectedTransactionsByAccNum.Add(expectedTransactionByAccNum01);
        }



        [Fact]
        public void testGetAllTransactions()
        {
            // Xunit cannot perform deep comparison on two objects, 
            // in the loop it compares each expected object's values against each actual object's values
            for (int i = 0; i < actualTransactions.Count; i++)
            {
                Assert.Equal(expectedTransactions[i].accountNumber, actualTransactions[i].accountNumber);
                Assert.Equal(expectedTransactions[i].balance, actualTransactions[i].balance);
                Assert.Equal(expectedTransactions[i].amount, actualTransactions[i].amount);
                Assert.Equal(expectedTransactions[i].processDate, actualTransactions[i].processDate);
                Assert.Equal(expectedTransactions[i].description, actualTransactions[i].description);
                Assert.Equal(expectedTransactions[i].accountType, actualTransactions[i].accountType);
            }
        }


        [Fact]
        public void testInsertTransaction()
        {
            // add expected transaction to local list
            Transaction expectedInsertTransaction = new Transaction("000001", "03/15/20", "4,000.00", "DR", "$1000.00", "Test insert transaction");
            expectedTransactions.Add(expectedInsertTransaction);

            // call access service
            Transaction actualInsertTransaction = fakeDatabaseAccessService.insertTransaction(expectedInsertTransaction);

            // compare return type
            Assert.Equal(expectedInsertTransaction.accountNumber, actualInsertTransaction.accountNumber);
            Assert.Equal(expectedInsertTransaction.balance, actualInsertTransaction.balance);
            Assert.Equal(expectedInsertTransaction.amount, actualInsertTransaction.amount);
            Assert.Equal(expectedInsertTransaction.processDate, actualInsertTransaction.processDate);
            Assert.Equal(expectedInsertTransaction.description, actualInsertTransaction.description);
            Assert.Equal(expectedInsertTransaction.accountType, actualInsertTransaction.accountType);

            
            // get all transactions again (now including the added one)
            testGetAllTransactions();
        }


        [Fact]
        public void testGetTransactionByAccountNumber()
        {
            // call access service
            List<Transaction> actualTransactionsByAccNum = fakeDatabaseAccessService.getTransactionByAccountNumber(000002);


            // compare return type
            for (int i = 0; i < actualTransactionsByAccNum.Count; i++)
            {
                Assert.Equal(expectedTransactionsByAccNum[i].accountNumber, actualTransactionsByAccNum[i].accountNumber);
                Assert.Equal(expectedTransactionsByAccNum[i].balance, actualTransactionsByAccNum[i].balance);
                Assert.Equal(expectedTransactionsByAccNum[i].amount, actualTransactionsByAccNum[i].amount);
                Assert.Equal(expectedTransactionsByAccNum[i].processDate, actualTransactionsByAccNum[i].processDate);
                Assert.Equal(expectedTransactionsByAccNum[i].description, actualTransactionsByAccNum[i].description);
                Assert.Equal(expectedTransactionsByAccNum[i].accountType, actualTransactionsByAccNum[i].accountType);
            }


        }

    }









}
