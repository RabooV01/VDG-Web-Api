using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VDG_Web_Api.src.Models;

[Table("Post")]
public partial class Post
{
    [Key]
    public int Id { get; set; }

    [Column("Doctor_Id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? DoctorId { get; set; }

    [Column(TypeName = "text")]
    public string? Content { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Posts")]
    public virtual Doctor? Doctor { get; set; }
}
