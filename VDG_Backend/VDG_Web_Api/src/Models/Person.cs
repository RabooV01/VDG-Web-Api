using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Person")]
public partial class Person
{
	[Key]
	public int Id { get; set; }

	[Column("First_Name")]
	[StringLength(64)]
	public string FirstName { get; set; } = string.Empty;

	[Column("Last_Name")]
	[StringLength(64)]
	public string LastName { get; set; } = string.Empty;

	public DateOnly? Birthdate { get; set; } = null!;

	[StringLength(12)]
	public string? Gender { get; set; } 

	[Column("Personal_Id")]
	[StringLength(32)]
	public string? PersonalId { get; set; }

	[StringLength(16)]
	[Unicode(false)]
	public string? Phone { get; set; }
}
