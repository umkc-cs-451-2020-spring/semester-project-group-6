using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApi {
	public class Trigger {
		public Trigger (
			string accountNumber,
			string triggerType,
			string triggerValue
			) 

		{
			this.accountNumber = accountNumber;
			this.triggerType = triggerType;
			this.triggerValue = triggerValue;
		}
		public Trigger() {
			accountNumber = "";
			triggerType = "";
			triggerValue = "";
		}

		[JsonProperty(PropertyName = "accountNumber")]
		public string accountNumber;
		[JsonProperty(PropertyName = "triggerType")]
		public string triggerType;
		[JsonProperty(PropertyName = "triggerValue")]
		public string triggerValue;

		public override string ToString() {
			return
				" Account Number: " + accountNumber +
				" Trigger Type: " + triggerType +
				" Trigger Value: " + triggerValue;
		}
	}
}