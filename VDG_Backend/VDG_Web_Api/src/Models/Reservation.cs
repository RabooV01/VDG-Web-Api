using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDG_Web_Api.src.Models;

public enum BookingTypes { Preview, Revision }

[Table("Reservation")]
public partial class Reservation
{
    [Key]
    public int Id { get; set; }

    [Column("User_Id")]
    public int? UserId { get; set; }

    [Column("Virtual_Id")]
    public int? VirtualId { get; set; }

    public DateTime ScheduledAt { get; set; }

    [StringLength(512)]
    public string Text { get; set; } = string.Empty;

    public BookingTypes Type { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reservations")]
    public virtual User? User { get; set; }

    [ForeignKey("VirtualId")]
    [InverseProperty("Reservations")]
    public virtual VirtualClinic? Virtual { get; set; }
}
