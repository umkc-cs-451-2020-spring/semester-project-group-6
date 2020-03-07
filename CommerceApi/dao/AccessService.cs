using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao {
    public class AccessService : ITransactionDao {
        // Access service will have a connection string all the time, no need to retype this for every function
        private SqlConnection conn = new SqlConnection("Server=localhost\\sqlexpress;Database=commerceDB;Trusted_Connection=True;");
        public List<Transaction> transactionList = new List<Transaction>();

        // This function returns all transactions, regardless of account number
        public List<Transaction> getAllTransactions() {
            transactionList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_ALL_TRANSACTIONS", conn) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    Transaction newTransaction = new Transaction();

                    newTransaction.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newTransaction.accountType = reader["ACCOUNT_TYPE"].ToString();
                    newTransaction.processDate = reader["PROCESSING_DATE"].ToString();
                    newTransaction.balance = reader["BALANCE"].ToString();
                    newTransaction.transactionType = reader["TRANSACTION_TYPE"].ToString();
                    newTransaction.amount = reader["TRANSACTION_AMOUNT"].ToString();
                    newTransaction.description = reader["TRANSACTION_DESCRIPTION"].ToString();

                    transactionList.Add(newTransaction);
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }

            return transactionList;
        }

        // This function returns transaction with a specific account number
        // TEAM: should we combine these two functions and use a variable to differentiate between all transactions and a specific one?
        public List<Transaction> getTransactionByAccountNumber(int accountNumber) {
            transactionList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_SPECIFIC_TRANSACTION", conn) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber.ToString()));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    Transaction newTransaction = new Transaction();

                    newTransaction.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newTransaction.accountType = reader["ACCOUNT_TYPE"].ToString();
                    newTransaction.processDate = reader["PROCESSING_DATE"].ToString();
                    newTransaction.balance = reader["BALANCE"].ToString();
                    newTransaction.transactionType = reader["TRANSACTION_TYPE"].ToString();
                    newTransaction.amount = reader["TRANSACTION_AMOUNT"].ToString();
                    newTransaction.description = reader["TRANSACTION_DESCRIPTION"].ToString();

                    transactionList.Add(newTransaction);
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }

            return transactionList;
        }

        // This function inserts a transaction into the database
        public Transaction insertTransaction(Transaction transaction) {
            try {
                var command = new SqlCommand("CREATE_TRANSACTION", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", transaction.accountNumber));
                command.Parameters.Add(new SqlParameter("@ACCOUNT_TYPE", transaction.accountType));
                command.Parameters.Add(new SqlParameter("@PROCESSING_DATE", transaction.processDate));
                command.Parameters.Add(new SqlParameter("@BALANCE", transaction.balance));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_TYPE", transaction.transactionType));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_AMOUNT", transaction.amount));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_DESCRIPTION", transaction.description));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
            }

            catch {
                conn.Close();
                return transaction;
            }

            conn.Close();
            return transaction;
        }
    }
}
