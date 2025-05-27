using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Doctor")]
public partial class Doctor
{
	[Key]
	[Column("Syndicate_Id")]
	[StringLength(16)]
	[Unicode(false)]
	public string SyndicateId { get; set; } = null!;

	[Column("User_Id")]
	public int? UserId { get; set; }

	[Column("Speciality_Id")]
	public int? SpecialityId { get; set; }

	[InverseProperty("Doctor")]
	public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

	[InverseProperty("Doctor")]
	public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

	[ForeignKey("SpecialityId")]
	[InverseProperty("Doctors")]
	public virtual Speciality? Speciality { get; set; }

	[InverseProperty("Doctor")]
	public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

	[ForeignKey("UserId")]
	public virtual User? User { get; set; }

	[InverseProperty("Doctor")]
	public virtual ICollection<VirtualClinic> VirtualClinics { get; set; } = new List<VirtualClinic>();
}
