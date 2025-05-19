using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VDG_Web_Api.src.Models;

[Table("Rating")]
public partial class Rating
{
    [Key]
    public int Id { get; set; }

    [Column("User_Id")]
    public int? UserId { get; set; }

    [Column("Doctor_Id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? DoctorId { get; set; }

    [Column("Avg_Wait")]
    public double? AvgWait { get; set; }

    [Column("Avg_Service")]
    public double? AvgService { get; set; }

    public double? Act { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Ratings")]
    public virtual Doctor? Doctor { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Ratings")]
    public virtual User? User { get; set; }
}
