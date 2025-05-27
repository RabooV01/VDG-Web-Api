using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Data;

public partial class VdgDbDemoContext : DbContext
{

    public VdgDbDemoContext(DbContextOptions<VdgDbDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketMessage> TicketMessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VirtualClinic> VirtualClinics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
<<<<<<< HEAD
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-RABOO;Initial Catalog=VDG_DB_Demo;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
=======
		=> optionsBuilder.UseSqlServer("Data Source=LAPTOP-RABOO;Initial Catalog=VDG_Migration;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");


        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.SyndicateId).HasName("PK__Doctor__B5BD6B27701103BA");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Doctors).HasConstraintName("Doctor_Speciality_FK");

<<<<<<< HEAD
            entity.HasOne(d => d.User).WithMany(p => p.Doctors).HasConstraintName("Doctor_User_FK");
        });
=======
			entity.HasOne(d => d.User).WithOne().HasConstraintName("Doctor_User_FK");
		});
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC07A0A03B3C");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC07FAD031E1");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Posts).HasConstraintName("Post_Doctor_FK");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rating__3214EC07B32B9B16");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Ratings).HasConstraintName("Rating_Doctor_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings).HasConstraintName("Rating_User_FK");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07765A5547");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations).HasConstraintName("Reservation_User_FK");

            entity.HasOne(d => d.Vritual).WithMany(p => p.Reservations).HasConstraintName("Reservation_Virtual_FK");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC074808F671");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC076F6CA0F8");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Tickets).HasConstraintName("Ticket_Doctor_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets).HasConstraintName("Ticket_User_FK");
        });

        modelBuilder.Entity<TicketMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket_M__3214EC079464AE2E");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMessages).HasConstraintName("Message_Ticket_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC071AAE95BF");

            entity.Property(e => e.Id).ValueGeneratedNever();

<<<<<<< HEAD
            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("User_Person_FK");
        });
=======
			entity.HasOne(d => d.Person).WithOne()
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("User_Person_FK");
		});
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f

        modelBuilder.Entity<VirtualClinic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Virtual___3214EC078277E8B9");

            entity.HasOne(d => d.Doctor).WithMany(p => p.VirtualClinics).HasConstraintName("Clinic_Doctor_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
