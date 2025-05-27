using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("User")]
public partial class User
{
	[Key]
	public int Id { get; set; }

	[Column("Person_Id")]
	public int? PersonId { get; set; }

	[StringLength(128)]
	[Unicode(false)]
	public string Email { get; set; } = null!;

	[Column("Password_Hash")]
	[StringLength(128)]
	[Unicode(false)]
	public string PasswordHash { get; set; } = null!;

	[StringLength(32)]
	[Unicode(false)]
	public string? Role { get; set; }

	[ForeignKey("PersonId")]
	public virtual Person? Person { get; set; }

	[InverseProperty("User")]
	public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

	[InverseProperty("User")]
	public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

	[InverseProperty("User")]
	public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
