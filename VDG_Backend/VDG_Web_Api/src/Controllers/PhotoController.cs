using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.FileHandler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhotoController : ControllerBase
	{
		private readonly IFileHandler _fileHandler;

		public PhotoController(IFileHandler fileHandler)
		{
			_fileHandler = fileHandler;
		}

		[HttpPost]
		public async Task<ActionResult<string>> Post(IFormFile file)
		{
			try
			{
				var fileName = await _fileHandler.SaveFile(file);
				return Ok(fileName);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{fileName}")]
		public async Task<ActionResult> Delete(string fileName)
		{
			try
			{
				await _fileHandler.DeleteFile(fileName);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
