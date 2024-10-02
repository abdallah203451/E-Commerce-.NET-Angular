using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Products
{
	public class PagedResult<T>
	{
		public List<T> Items { get; set; }
		public int TotalItems { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

		public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
	}
}
