using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLLib
{
    
		public class ResultQuery
		{
			public int ID { get; set; }
			public int TotalRow { get; set; }
			public bool IsSuccess { get; set; }
			public string ErrorText { get; set; }
		}
	
}
