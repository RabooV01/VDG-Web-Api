
namespace VDG_Web_Api.src.FileHandler
{
	public class FileHandler : IFileHandler
	{
		private readonly IWebHostEnvironment _env;

		public FileHandler(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task<string> SaveFile(IFormFile file)
		{
			try
			{
				if (file == null || file.Length == 0)
				{
					throw new ArgumentNullException(nameof(file), "File required.");
				}

				var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");

				if (!Directory.Exists(uploadsPath))
					Directory.CreateDirectory(uploadsPath);

				var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
				var filePath = Path.Combine(uploadsPath, uniqueFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var fileUrl = $"/uploads/{uniqueFileName}";

				return fileUrl;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteFile(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentNullException(nameof(fileName), "File name is required.");
			}

			var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");

			var filePath = Path.Combine(uploadsPath, fileName);

			if (!File.Exists(filePath))
			{
				throw new ArgumentException("file is not exists");
			}

			await Task.Run(() => File.Delete(filePath));
		}
	}
}
