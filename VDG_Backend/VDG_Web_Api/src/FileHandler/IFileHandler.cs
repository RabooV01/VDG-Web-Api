namespace VDG_Web_Api.src.FileHandler
{
	public interface IFileHandler
	{
		public Task<string> SaveFile(IFormFile file);
		public Task DeleteFile(string fileName);
	}
}
