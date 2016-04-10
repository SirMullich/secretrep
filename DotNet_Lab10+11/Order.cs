using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DotNet_Lab10_11
{
	class Order
	{
		[PhoneMaskAttribute("+X (XXX) XXX XXXX", ErrorMessage = "bad value")]
		public string customerPhone {get; set; }

		//[StreetMask(ErrorMessage = "wrong address")]
		public string customerStreet {get; set;}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	sealed public class PhoneMaskAttribute : ValidationAttribute
	{
		private String mask;

		public PhoneMaskAttribute(String mask)
		{
			this.mask = mask;
		}

		public override bool IsValid(object value)
		{
			string phoneNumber = (String)value;
			// Console.WriteLine(phoneNumber);
			if (phoneNumber == null)
			{
				throw new ArgumentException();
			}
			else
			{
				return GoodNumber(phoneNumber);
			}
		}

		internal bool GoodNumber(string number)
		{
			if (mask.Length != number.Length)
			{
				//Console.WriteLine("length {0} {1}", mask.Length, number.Length);
				return false;
			}

			for (int i = 0; i < number.Length; i++)
			{
				// Цифра
				if ( (!char.IsDigit(number[i])) && mask[i] == 'X')
				{
					//Console.WriteLine("number");
					return false;
				}

				// Все остальное
				if ( char.IsDigit(number[i]) && mask[i] != 'X' )
				{
					//Console.WriteLine("Other {0} {1}", number[i], mask[i]);
					return false;
				}
			}

			return true;
		}
	}

	sealed public class StreetMaskAttribute : ValidationAttribute
	{

		public override bool IsValid(object value)
		{
			string address = (String)value;
			// Console.WriteLine(phoneNumber);
			if (address == null)
			{
				throw new ArgumentException();
			}
			else
			{
				return GoodAddress(address);
			}
		}

		internal bool GoodAddress(string address) 
		{
			string[] splitAddress = address.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries);

			if (splitAddress.Count() != 2)
			{
				//Console.WriteLine("wrong length");
				return false;
			}

			for (int i = 0; i < splitAddress[1].Count(); i++)
			{
				if (!char.IsDigit(splitAddress[1][i])) 
				{
					//Console.WriteLine("second var is not a number!");
					return false;
				}
				
			}
			for (int i = 0; i < splitAddress[0].Count(); i++)
			{
				if (char.IsDigit(splitAddress[0][i]))
				{
					//Console.WriteLine("1st var has a number");
					return false;
				}
			}

			return true;
		}
	}
}
