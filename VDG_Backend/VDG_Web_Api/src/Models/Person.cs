using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VDG_Web_Api.src.Models;

[Table("Person")]
public partial class Person
{
    [Key]
    public int Id { get; set; }

    [Column("First_Name")]
    [StringLength(64)]
    public string FirstName { get; set; } = null!;

    [Column("Last_Name")]
    [StringLength(64)]
    public string LastName { get; set; } = null!;

    public DateOnly? Birthdate { get; set; }

    [StringLength(12)]
    public string? Gender { get; set; }

    [Column("Personal_Id")]
    [StringLength(32)]
    public string? PersonalId { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
