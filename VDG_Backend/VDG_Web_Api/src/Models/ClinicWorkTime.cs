using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Clinic_WorkTime")]
public partial class ClinicWorkTime
{
	[Key]
	public int Id { get; set; }

	[Column("Clinic_Id")]
	public int ClinicId { get; set; }
    
    [ForeignKey("ClinicId")]
    [InverseProperty("WorkTimes")]
    public VirtualClinic? Clinic { get; set; }

    [Column("Start_WorkHours")]
	public TimeOnly? StartWorkHours { get; set; }

    [Column("End_WorkHours")]
	public TimeOnly? EndWorkHours { get; set; }
}
