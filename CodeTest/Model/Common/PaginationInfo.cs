using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTest.Model
{
    public class PaginationInfo
    {
		public int CurrentPageNumber { get; set; }
		public int PageSize { get; set; }

		public int Skip { get {
				return PageSize * (CurrentPageNumber - 1);
			} }

		public PaginationInfo()
		{
			CurrentPageNumber = 1;
			PageSize = 10;
		}
	}
}
