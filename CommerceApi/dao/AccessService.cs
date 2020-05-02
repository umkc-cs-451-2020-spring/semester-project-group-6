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
        public List<Notification> notificationList = new List<Notification>();
        public List<Trigger> triggerList = new List<Trigger>();

        // This function returns all transactions, regardless of account number
        public List<Transaction> getAllTransactions() {
            transactionList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_ALL_TRANSACTIONS", conn) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    Transaction newTransaction = new Transaction();

                    newTransaction.transactionID = reader["ID"].ToString();
                    newTransaction.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newTransaction.accountType = reader["ACCOUNT_TYPE"].ToString();
                    newTransaction.processDate = reader["PROCESSING_DATE"].ToString();
                    newTransaction.balance = reader["BALANCE"].ToString();
                    newTransaction.transactionType = reader["TRANSACTION_TYPE"].ToString();
                    newTransaction.amount = reader["TRANSACTION_AMOUNT"].ToString();
                    newTransaction.description = reader["TRANSACTION_DESCRIPTION"].ToString();
                    newTransaction.time = reader["TRANSACTION_TIME"].ToString();
                    newTransaction.state = reader["TRANSACTION_STATE"].ToString();

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
        public List<Transaction> getTransactionByAccountNumber(string accountNumber) {
            transactionList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_SPECIFIC_TRANSACTION", conn) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber.ToString()));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    Transaction newTransaction = new Transaction();

                    newTransaction.transactionID = reader["ID"].ToString();
                    newTransaction.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newTransaction.accountType = reader["ACCOUNT_TYPE"].ToString();
                    newTransaction.processDate = reader["PROCESSING_DATE"].ToString();
                    newTransaction.balance = reader["BALANCE"].ToString();
                    newTransaction.transactionType = reader["TRANSACTION_TYPE"].ToString();
                    newTransaction.amount = reader["TRANSACTION_AMOUNT"].ToString();
                    newTransaction.description = reader["TRANSACTION_DESCRIPTION"].ToString();
                    newTransaction.time = reader["TRANSACTION_TIME"].ToString();
                    newTransaction.state = reader["TRANSACTION_STATE"].ToString();

                    transactionList.Add(newTransaction);
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }

            return transactionList;
        }

        public List<Notification> getAllNotifications() {
            notificationList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_ALL_NOTIFICATIONS", conn) { CommandType = CommandType.StoredProcedure };
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    Notification newNotification = new Notification();

                    newNotification.transactionID = reader["TRANSACTION_ID"].ToString();
                    newNotification.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newNotification.notificationMessage = reader["TRIGGER_MESSAGE"].ToString();

                    notificationList.Add(newNotification);
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }

            updateTransactionID();

            return notificationList;
        }

        public List<Notification> getNotificationsByAccount(string accountNumber) {
            notificationList.Clear();

            try {

                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_SPECIFIC_NOTIFICATION", conn) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber));

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read()) {
                    Notification newNotification = new Notification();

                    newNotification.transactionID = reader["TRANSACTION_ID"].ToString();
                    newNotification.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newNotification.notificationMessage = reader["TRIGGER_MESSAGE"].ToString();

                    notificationList.Add(newNotification);
                }

                conn.Close();

                updateTransactionID();
            }

            catch {
                conn.Close();
            }

            return notificationList;
        }

        public List<Transaction> getTransactionByID(string transactionID) {
            transactionList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_TRANSACTION_BY_ID", conn) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@TRANSACTION_ID", transactionID));

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read()) {
                    Transaction newTransaction = new Transaction();

                    newTransaction.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newTransaction.accountType = reader["ACCOUNT_TYPE"].ToString();
                    newTransaction.processDate = reader["PROCESSING_DATE"].ToString();
                    newTransaction.balance = reader["BALANCE"].ToString();
                    newTransaction.transactionType = reader["TRANSACTION_TYPE"].ToString();
                    newTransaction.amount = reader["TRANSACTION_AMOUNT"].ToString();
                    newTransaction.description = reader["TRANSACTION_DESCRIPTION"].ToString();
                    newTransaction.time = reader["TRANSACTION_TIME"].ToString();
                    newTransaction.state = reader["TRANSACTION_STATE"].ToString();

                    transactionList.Add(newTransaction);
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }

            return transactionList;
        }

        public List<Trigger> getAllTriggers() {
            triggerList.Clear();

            try {
                conn.Open();
                SqlCommand command = new SqlCommand("RETRIEVE_ALL_TRIGGERS", conn) { CommandType = CommandType.StoredProcedure };

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read()) {
                    Trigger newTrigger = new Trigger();

                    newTrigger.accountNumber = reader["ACCOUNT_NUMBER"].ToString();
                    newTrigger.triggerType = reader["TRIGGER_TYPE"].ToString();
                    newTrigger.triggerValue = reader["TRIGGER_VALUE"].ToString();

                    triggerList.Add(newTrigger);
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }

            return triggerList;
        }

        // This function inserts a transaction into the database
        public void insertTransaction(Transaction transaction) {
            try {
                var command = new SqlCommand("CREATE_TRANSACTION", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", transaction.accountNumber));
                command.Parameters.Add(new SqlParameter("@ACCOUNT_TYPE", transaction.accountType));
                command.Parameters.Add(new SqlParameter("@PROCESSING_DATE", transaction.processDate));
                command.Parameters.Add(new SqlParameter("@BALANCE", transaction.balance));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_TYPE", transaction.transactionType));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_AMOUNT", transaction.amount));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_DESCRIPTION", transaction.description));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_TIME", transaction.time));
                command.Parameters.Add(new SqlParameter("@TRANSACTION_STATE", transaction.state));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();

                checkTriggers(transaction);
            }

            catch {
                conn.Close();
            }

            conn.Close();
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

                    else if (triggerType.ToUpper() == "TIME") {
                        string[] timePieces = triggerValue.Split("-");
                        string strippedTime = transaction.time.Remove(transaction.time.IndexOf(":"), 1);
                        int time = Convert.ToInt32(strippedTime);
                        int triggerTimeStart = Convert.ToInt32(timePieces[0].Remove(timePieces[0].IndexOf(":"), 1));
                        int triggerTimeEnd = Convert.ToInt32(timePieces[1].Remove(timePieces[1].IndexOf(":"), 1));

                        if (time >= triggerTimeStart || time < triggerTimeEnd) {
                            conn.Close();
                            createNotification(transaction, triggerType, triggerValue);
                            conn.Open();
                        }
                    }

                    else if (triggerType.ToUpper() == "STATE") {
                        if (transaction.state != triggerValue) {
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

                else if (triggerType.ToUpper() == "TIME") {
                    string message = "Purchase made within " + triggerValue;
                    command.Parameters.Add(new SqlParameter("@TRIGGER_MESSAGE", message));
                }

                else if (triggerType.ToUpper() == "STATE") {
                    string message = "Out of state purchase made in " + transaction.state;
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

        public void createTrigger(string accountNumber, string triggerType, string triggerValue) {
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

        public void removeTrigger(string accountNumber, string triggerType, string triggerValue) {
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

        public void clearNotifications(string accountNumber) {
            try {
                var command = new SqlCommand("CLEAR_NOTIFICATIONS", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
                notificationList.Clear();
            }

            catch {
                conn.Close();
            }
        }

        public void clearTriggers(string accountNumber) {
            try {
                var command = new SqlCommand("CLEAR_TRIGGERS", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", accountNumber));

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                conn.Close();
            }

            catch {
                conn.Close();
            }
        }

        public void updateTransactionID() {
            try {
                SqlCommand command = new SqlCommand("UPDATE_TRANSACTION_ID", conn) { CommandType = CommandType.StoredProcedure };

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int index = 0;
                while(reader.Read()) {
                    notificationList[index].transactionID = reader["ID"].ToString();
                    index++;
                }

                conn.Close();
            }

            catch {
                conn.Close();
            }
        }
    }
}
