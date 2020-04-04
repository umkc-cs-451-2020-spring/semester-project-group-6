using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace CommerceApi.initial_setup {
    public class FillDB {
        // Create a list of transactions by passing in CustomerA.csv into TransactionReader 
        List<Transaction> transactions = TransactionReader.ImportTransactions("initial setup/transactions/CustomerA.csv");
        private SqlConnection conn = new SqlConnection("Server=localhost\\sqlexpress;Database=commerceDB;Trusted_Connection=True;");
        public void populateDatabase() {
            SqlConnection conn = null;

            try {
                // conn is a connection string that is used to connect to SQL Server 
                conn = new SqlConnection("Server=localhost\\sqlexpress;Database=commerceDB;Trusted_Connection=True;");

                foreach (Transaction entry in transactions) {

                    // command is used to call the stored procedure
                    var command = new SqlCommand("CREATE_TRANSACTION", conn) { CommandType = CommandType.StoredProcedure };

                    

                    // Add the function arguments as the parameters for the stored procedure in the database
                    command.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", entry.accountNumber));
                    command.Parameters.Add(new SqlParameter("@ACCOUNT_TYPE", entry.accountType));
                    command.Parameters.Add(new SqlParameter("@PROCESSING_DATE", entry.processDate));
                    command.Parameters.Add(new SqlParameter("@BALANCE", entry.balance));
                    command.Parameters.Add(new SqlParameter("@TRANSACTION_TYPE", entry.transactionType));
                    command.Parameters.Add(new SqlParameter("@TRANSACTION_AMOUNT", entry.amount));
                    command.Parameters.Add(new SqlParameter("@TRANSACTION_DESCRIPTION", entry.description));

                    // Establish the connection and execute our stored procedure
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    conn.Close();

                    // Was used for testing the triggers and notifications
                    // checkTriggers(entry);
                }
            }

            // If everything was successful, then we are done, and we can now close the connection
            finally {
                if (conn != null)
                    conn.Close();
            }
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
    }
}