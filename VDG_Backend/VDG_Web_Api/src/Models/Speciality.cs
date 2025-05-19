using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VDG_Web_Api.src.Models;

[Table("Speciality")]
public partial class Speciality
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string? Specialty { get; set; }

    [InverseProperty("Speciality")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
