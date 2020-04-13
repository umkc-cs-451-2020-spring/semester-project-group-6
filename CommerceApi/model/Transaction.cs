using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi
{
    
    public class Transaction
    {
        public Transaction(
            string accountNumber, 
            string processDate, 
            string balance, 
            string transactionType, 
            string amount, 
            string description)

        {
            this.accountNumber = accountNumber;
            this.processDate = processDate;
            this.balance = balance;
            this.transactionType = transactionType;
            this.amount = amount;
            this.description = description;
            this.accountType = "Checking"; // Hard coded this for the time being
        }

        public Transaction() {
            this.accountNumber = "";
            this.processDate = "";
            this.balance = "";
            this.transactionType = "";
            this.amount = "";
            this.description = "";
            this.accountType = "Checking";
        }

        [JsonProperty(PropertyName = "accountNumber")]
        public string accountNumber { get; set; }
        [JsonProperty(PropertyName = "accountType")]
        public string accountType { get; set; }
        [JsonProperty(PropertyName = "processDate")]
        public string processDate { get; set; }
        [JsonProperty(PropertyName = "balance")]
        public string balance { get; set; }
        [JsonProperty(PropertyName = "transactionType")]
        public string transactionType { get; set; }
        [JsonProperty(PropertyName = "transactionAmount")]
        public string amount { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        // to test output
        public override string ToString()
        {
            return
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
