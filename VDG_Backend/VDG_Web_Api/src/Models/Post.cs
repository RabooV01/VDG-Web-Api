using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Post")]
public partial class Post
{
	[Key]
	public int Id { get; set; }

	[Column("Doctor_Id")]
	[StringLength(16)]
	[Unicode(false)]
	public int DoctorId { get; set; }

	[Column(TypeName = "text")]
	public string Content { get; set; } = string.Empty;

	[ForeignKey("DoctorId")]
	[InverseProperty("Posts")]
	public virtual Doctor Doctor { get; set; } = null!;
}
