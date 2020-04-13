using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi.dao {
    public class AccessService : IValuesDao {
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

                checkTriggers(transaction);
            }

            catch {
                conn.Close();
                return transaction;
            }

            conn.Close();
            return transaction;
        }

        public void checkTriggers(Transaction transaction) {
            try {
                var command = new SqlCommand("RETRIEVE_SPECIFIC_TRIGGER", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", transaction.accountNumber));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    string triggerType = reader["TRIGGER_TYPE"].ToString();
                    string triggerValue = reader["TRIGGER_VALUE"].ToString();

                    if (triggerType.ToUpper() == "AMOUNT") {
                        string strippedAmount = transaction.amount.Remove(0, 1);
                        double amount = Convert.ToDouble(strippedAmount);
                        string strippedValue = triggerValue.Remove(0, 1);
                        if (amount > Convert.ToDouble(strippedValue)) {
                            conn.Close();
                            createNotification(transaction, triggerType, triggerValue);
                            conn.Open();
                        }
                    }
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }
        }

        public void createNotification(Transaction transaction, string triggerType, string triggerValue) {
            try {
                var command = new SqlCommand("CREATE_NOTIFICATION", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", transaction.accountNumber));

                if (triggerType.ToUpper() == "AMOUNT") {
                    string message = "Amount exceeds " + triggerValue;
                    command.Parameters.Add(new SqlParameter("@TRIGGER_MESSAGE", message));
                }

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
            }

            catch {
                conn.Close();
            }
        }

        public void createTrigger(int accountNumber, string triggerType, string triggerValue) {
            try {
                var command = new SqlCommand("CREATE_TRIGGER", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber));
                command.Parameters.Add(new SqlParameter("@TRIGGER_TYPE", triggerType));
                command.Parameters.Add(new SqlParameter("@TRIGGER_VALUE", triggerValue));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
            }

            catch {
                conn.Close();
            }
        }

        public void removeTrigger(int accountNumber, string triggerType, string triggerValue) {
            try {
                var command = new SqlCommand("REMOVE_TRIGGER", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber));
                command.Parameters.Add(new SqlParameter("@TRIGGER_TYPE", triggerType));
                command.Parameters.Add(new SqlParameter("@TRIGGER_VALUE", triggerValue));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
            }

            catch {
                conn.Close();
            }
        }

        public void clearNotifications(int accountNumber) {
            try {
                var command = new SqlCommand("CLEAR_NOTIFICATIONS", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
            }

            catch {
                conn.Close();
            }
        }
    }
}
