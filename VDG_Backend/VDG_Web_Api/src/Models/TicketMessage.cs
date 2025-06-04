using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

[Table("Ticket_Message")]
public partial class TicketMessage
{
    [Key]
    public int Id { get; set; }

    [Column("Ticket_Id")]
    public int? TicketId { get; set; }

    [Column(TypeName = "text")]
    public string? Text { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public int? UserId { get; set; }

    public DateOnly? Date { get; set; }

    [ForeignKey("TicketId")]
    [InverseProperty("TicketMessages")]
    public virtual Ticket? Ticket { get; set; }
}
