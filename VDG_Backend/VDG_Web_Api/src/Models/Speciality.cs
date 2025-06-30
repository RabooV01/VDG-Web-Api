using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Speciality")]
public partial class Speciality
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string name { get; set; } = string.Empty;

    [InverseProperty("Speciality")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
