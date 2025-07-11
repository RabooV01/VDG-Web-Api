using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TicketController : ControllerBase
	{
		private readonly ITicketService _ticketRepository;

		public TicketController(ITicketService ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}


	}
}
