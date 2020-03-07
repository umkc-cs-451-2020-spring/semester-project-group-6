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
                }
            }

            // If everything was successful, then we are done, and we can now close the connection
            finally {
                if (conn != null)
                    conn.Close();
            }
        }
    }
}