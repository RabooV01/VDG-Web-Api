using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VDG_Web_Api.src.Models;

[Table("Ticket")]
public partial class Ticket
{
    [Key]
    public int Id { get; set; }

    [Column("User_Id")]
    public int? UserId { get; set; }

    [Column("Doctor_Id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? DoctorId { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("Close_Date")]
    public DateOnly? CloseDate { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Tickets")]
    public virtual Doctor? Doctor { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();

    [ForeignKey("UserId")]
    [InverseProperty("Tickets")]
    public virtual User? User { get; set; }
}
