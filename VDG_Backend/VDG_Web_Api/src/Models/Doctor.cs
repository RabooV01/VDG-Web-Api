using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.Models;

[Table("Doctor")]
public partial class Doctor
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("User_Id")]
    public int UserId { get; set; }

    [Column("Description")]
    [DefaultValue("")]
    [Unicode(false)]
    [StringLength(1024)]
    public string Description { get; set; } = string.Empty;

    [Column("Speciality_Id")]
    public int SpecialityId { get; set; }

    [Column("Syndicate_Id")]
    [StringLength(16)]
    [Unicode(false)]
    public string SyndicateId { get; set; } = null!;

    [Column("Ticket_Status")]
    [StringLength(255)]
    [Unicode(false)]
    public TicketOptions TicketOption { get; set; } = TicketOptions.Any;

    [Column("Ticket_Cost")]
    public double TicketCost { get; set; } = 0;

    [InverseProperty("Doctor")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    [InverseProperty("Doctor")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [ForeignKey("SpecialityId")]
    [InverseProperty("Doctors")]
    public virtual Speciality Speciality { get; set; } = null!;

    [InverseProperty("Doctor")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;


    [InverseProperty("Doctor")]
    public virtual ICollection<VirtualClinic> VirtualClinics { get; set; } = new List<VirtualClinic>();
}
