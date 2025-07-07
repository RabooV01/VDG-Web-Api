using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Virtual_Clinic")]
public partial class VirtualClinic
{
	[Key]
	public int Id { get; set; }

	[Column("Doctor_Id")]
	public int DoctorId { get; set; }

	[StringLength(255)]
	[Unicode(false)]
	public string Location { get; set; } = string.Empty;

	[StringLength(255)]
	[Unicode(false)]
	[DefaultValue("Inactive")]
	public string Status { get; set; } = "Inactive";

	[Column("Avg_Service")]
	public int AvgService { get; set; }

	[Column("Preview_Cost")]
	public double PreviewCost { get; set; }

	[ForeignKey("DoctorId")]
	[InverseProperty("VirtualClinics")]
	public virtual Doctor Doctor { get; set; } = null!;

	[InverseProperty("Virtual")]
	public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

	[InverseProperty("Clinic")]
	public virtual ICollection<ClinicWorkTime> WorkTimes { get; set; } = new List<ClinicWorkTime>();
}
