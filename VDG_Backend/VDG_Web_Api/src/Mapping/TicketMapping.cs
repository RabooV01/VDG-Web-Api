using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
	public static class TicketMapping
	{
		public static Ticket ToEntity(this TicketDTO ticketDTO)
			=> new()
			{
				Id = ticketDTO.Id,
				UserId = ticketDTO.UserId,
				DoctorId = ticketDTO.DoctorId,
				CloseDate = ticketDTO.CloseDate,
				Status = ticketDTO.Status
			};

		public static TicketDTO ToDto(this Ticket ticket, DateTime firstMessageDate)
			=> new()
			{
				Id = ticket.Id,
				UserId = ticket.UserId,
				Status = ticket.Status,
				DoctorId = ticket.DoctorId,
				CloseDate = ticket.CloseDate,
				OpenDate = firstMessageDate
			};

		public static TicketMessage ToEntity(this TicketMessageDTO ticketMessageDTO)
			=> new()
			{
				Id = ticketMessageDTO.Id,
				TicketId = ticketMessageDTO.TicketId,
				Date = ticketMessageDTO.Date,
				OwnerId = ticketMessageDTO.OwnerId,
				Text = ticketMessageDTO.Text
			};

		public static TicketMessageDTO ToDto(this TicketMessage ticketMessage)
			=> new()
			{
				Id = ticketMessage.Id,
				TicketId = ticketMessage.TicketId,
				Date = ticketMessage.Date,
				OwnerId = ticketMessage.OwnerId,
				Text = ticketMessage.Text
			};

		public static UserTicketDTO ToUserTicketDto(this Ticket ticket, DateTime firstMessageDate)
			=> new()
			{
				Id = ticket.Id,
				DoctorId = ticket.DoctorId,
				Status = ticket.Status,
				UserId = ticket.UserId,
				CloseDate = ticket.CloseDate,
				DoctorName = $"{ticket.Doctor.User.Person.FirstName} {ticket.Doctor.User.Person.LastName}",
				OpenDate = firstMessageDate,
				DoctorSpeciality = ticket.Doctor.Speciality.Name
			};

		public static DoctorTicketDTO ToDoctorTicketDto(this Ticket ticket, DateTime firstMessageDate)
			=> new()
			{
				Id = ticket.Id,
				DoctorId = ticket.DoctorId,
				Status = ticket.Status,
				UserId = ticket.UserId,
				CloseDate = ticket.CloseDate,
				UserName = $"{ticket.User.Person.FirstName} {ticket.User.Person.LastName}",
				OpenDate = firstMessageDate
			};

		public static Ticket ToEntity(this AddTicketDTO addTicketDTO)
			=> new()
			{
				UserId = addTicketDTO.UserId,
				DoctorId = addTicketDTO.DoctorId,
				CloseDate = null
			};
	}
}
