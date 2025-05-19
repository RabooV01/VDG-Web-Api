using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Virtual_Clinic")]
public partial class VirtualClinic
{
	[Key]
	public int Id { get; set; }

	[Column("Doctor_Id")]
	[StringLength(16)]
	[Unicode(false)]
	public string? DoctorId { get; set; }

	[Column("Start_Work_Hours")]
	public TimeOnly? StartWorkHours { get; set; }

	[Column("End_Work_Hours")]
	public TimeOnly? EndWorkHours { get; set; }

	[StringLength(255)]
	[Unicode(false)]
	public string? Status { get; set; }

	[Column("Avg_Service")]
	public double? AvgService { get; set; }

	[Column("Ticket_Status")]
	[StringLength(255)]
	[Unicode(false)]
	public string? TicketStatus { get; set; }

	[Column("Preview_Const")]
	public double? PreviewConst { get; set; }

	[Column("Ticket_Const")]
	public double? TicketConst { get; set; }

	[ForeignKey("DoctorId")]
	[InverseProperty("VirtualClinics")]
	public virtual Doctor? Doctor { get; set; }

	[InverseProperty("Vritual")]
	public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
