using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMessage(int Id)
        {
            try
            {
                ticketRepository.DeleteMessage(Id);
            }
            catch
            {

            }
        }


    }
}
