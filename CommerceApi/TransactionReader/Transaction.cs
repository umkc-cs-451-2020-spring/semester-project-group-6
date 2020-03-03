using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi
{
    public class Transaction
    {
        public Transaction(Guid id, string accountNumber, string accountType, string processDate, string balance, string transactionType, string amount, string description)
        {
            this.id = id;
            this.accountNumber = accountNumber;
            this.accountType = accountType;
            this.processDate = processDate;
            this.balance = balance;
            this.transactionType = transactionType;
            this.amount = amount;
            this.description = description;
        }

        private Guid id { get; set; }
        private string accountNumber { get; set; }
        private string accountType { get; set; }
        private string processDate { get; set; }
        private string balance { get; set; }
        private string transactionType { get; set; }
        private string amount { get; set; }
        private string description { get; set; }

        // to test output
        public override string ToString()
        {
            return
                "ID: " + id +
                " Account Number: " + accountNumber +
                " Account Type: " + accountType +
                " Processing Date: " + processDate +
                " Balance: " + balance +
                " Transaction Type: " + transactionType +
                " Amount: " + amount +
                " Description: " + description;
        }



    }
}
