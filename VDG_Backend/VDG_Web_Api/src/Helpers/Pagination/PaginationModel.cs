namespace VDG_Web_Api.src.Helpers.Pagination
{
	public class PaginationModel<T>
	{
		public IEnumerable<T> Data { get; }
		public int TotalPages { get; set; }
		public int Page { get; set; }
		public int Total { get; set; }

		public PaginationModel(IEnumerable<T> data, int totalPages, int page, int total)
		{
			Data = data;
			TotalPages = totalPages;
			Page = page;
			Total = total;
		}
	}
}
