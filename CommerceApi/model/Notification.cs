using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi {

    public class Notification {
        public Notification (
            string transactionID,
            string accountNumber,
            string notificationMessage
        )

        {
            this.transactionID = transactionID;
            this.accountNumber = accountNumber;
            this.notificationMessage = notificationMessage;
        }

        public Notification () {
            this.transactionID = "";
            this.accountNumber = "";
            this.notificationMessage = "";
        }

        [JsonProperty(PropertyName = "transactionID")]
        public string transactionID { get; set; }
        [JsonProperty(PropertyName = "accountNumber")]
        public string accountNumber { get; set; }
        [JsonProperty(PropertyName = "notificationMessage")]
        public string notificationMessage { get; set; }

        public override string ToString() {
            return
                " Transaction ID: " + transactionID +
                " Account Number: " + accountNumber +
                " Notification Message: " + notificationMessage;
        }
    }
}