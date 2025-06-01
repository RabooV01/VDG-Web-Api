using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Reservation")]
public partial class Reservation
{
	[Key]
	public int Id { get; set; }

	[Column("User_Id")]
	public int? UserId { get; set; }

	[Column("Vritual_Id")]
	public int? VritualId { get; set; }

	public DateTime ScheduledAt { get; set; }

	[StringLength(512)]
	public string? Text { get; set; }

	[StringLength(255)]
	[AllowedValues(["preview", "revision"])]
	[Unicode(false)]
	public string? Type { get; set; }

	[ForeignKey("UserId")]
	[InverseProperty("Reservations")]
	public virtual User? User { get; set; }

	[ForeignKey("VritualId")]
	[InverseProperty("Reservations")]
	public virtual VirtualClinic? Vritual { get; set; }
}
