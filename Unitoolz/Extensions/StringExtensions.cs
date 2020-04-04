using UnityEngine;
using System.Text;
using System.Collections.Generic;


namespace Unitoolz.Extensions
{
	public static class stringExtensions 
	{
		public static string FormatFromDictionary(this string formatString, Dictionary<string, object> ValueDict) 
		{
			string newFormatString = formatString;
			foreach (var tuple in ValueDict)
			{
				newFormatString = newFormatString.Replace("{" + tuple.Key + "}", tuple.Value.ToString());            
			}
			return newFormatString;
		}
	}
}
