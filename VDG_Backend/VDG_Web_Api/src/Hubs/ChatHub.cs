using Microsoft.AspNetCore.SignalR;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Hubs
{
	public class ChatHub : Hub
	{
		private readonly IClaimService _claimService;
		private readonly ITicketRepository _ticketRepository;
		public ChatHub(IClaimService claimService, ITicketRepository ticketRepository)
		{
			_claimService = claimService;
			_ticketRepository = ticketRepository;
		}

		public async Task SendMessageAsync(int userId, int ticketId, string message)
		{
			await Clients.User(_claimService.GetCurrentUserId().ToString()).SendAsync("ReceiveMessageAsync", userId, message);
			await _ticketRepository.SendMessageAsync(new()
			{
				Date = DateTime.Now,
				OwnerId = _claimService.GetCurrentUserId(),
				Text = message,
				TicketId = ticketId,
			});
		}
	}
}
