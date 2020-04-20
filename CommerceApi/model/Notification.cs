using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi {

    public class Notification {
        public Notification (
            string accountNumber,
            string notificationMessage
        )

        {
            this.accountNumber = accountNumber;
            this.notificationMessage = notificationMessage;
        }

        public Notification () {
            this.accountNumber = "";
            this.notificationMessage = "";
        }

        [JsonProperty(PropertyName = "accountNumber")]
        public string accountNumber { get; set; }
        [JsonProperty(PropertyName = "notificationMessage")]
        public string notificationMessage { get; set; }

        public override string ToString() {
            return
                " Account Number: " + accountNumber +
                " Notification Message: " + notificationMessage;
        }
    }
}