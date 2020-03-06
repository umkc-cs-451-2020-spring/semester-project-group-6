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
            Guid id, 
            string accountNumber, 
            string accountType, 
            string processDate, 
            string balance, 
            string transactionType, 
            string amount, 
            string description)

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
        [JsonProperty(PropertyName = "id")]
        private Guid id { get; set; }
        [JsonProperty(PropertyName = "accountNumber")]
        private string accountNumber { get; set; }
        [JsonProperty(PropertyName = "accountType")]
        private string accountType { get; set; }
        [JsonProperty(PropertyName = "processDate")]
        private string processDate { get; set; }
        [JsonProperty(PropertyName = "balance")]
        private string balance { get; set; }
        [JsonProperty(PropertyName = "transactionType")]
        private string transactionType { get; set; }
        [JsonProperty(PropertyName = "transactionAmount")]
        private string amount { get; set; }
        [JsonProperty(PropertyName = "description")]
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
