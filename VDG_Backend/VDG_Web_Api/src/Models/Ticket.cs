using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.Models;

[Table("Ticket")]
public partial class Ticket
{
	[Key]
	public int Id { get; set; }

	[Column("User_Id")]
	public int UserId { get; set; }

	[Column("Doctor_Id")]
	[StringLength(16)]
	[Unicode(false)]
	public int DoctorId { get; set; }

	[StringLength(16)]
	[Unicode(false)]
	public TicketStatus Status { get; set; } = TicketStatus.Pending;

	[Column("Close_Date")]
	public DateTime? CloseDate { get; set; }

	[ForeignKey("DoctorId")]
	[InverseProperty("Tickets")]
	public virtual Doctor Doctor { get; set; } = null!;

	[ForeignKey("UserId")]
	[InverseProperty("Tickets")]
	public virtual User User { get; set; } = null!;

	[InverseProperty("Ticket")]
	public virtual ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();

}
