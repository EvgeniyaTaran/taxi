using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Payment: IEntity
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		
		public PaymentDetails Details { get; set; }

		public Payment()
		{
			Details = new PaymentDetails();
		}
	}

	[ComplexType]
	public class PaymentDetails
	{
		public String ReceiverName { get; set; }
	}
}
