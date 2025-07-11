using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Data;

public partial class VdgDbDemoContext : DbContext
{
	private readonly IConfiguration _config;

	public VdgDbDemoContext(DbContextOptions<VdgDbDemoContext> options, IConfiguration config)
		: base(options)
	{
		_config = config;
	}

	public virtual DbSet<Doctor> Doctors { get; set; } = null!;

	public virtual DbSet<Person> People { get; set; } = null!;

	public virtual DbSet<Post> Posts { get; set; } = null!;

	public virtual DbSet<Rating> Ratings { get; set; } = null!;

	public virtual DbSet<Reservation> Reservations { get; set; } = null!;

	public virtual DbSet<Speciality> Specialities { get; set; } = null!;

	public virtual DbSet<Ticket> Tickets { get; set; } = null!;

	public virtual DbSet<TicketMessage> TicketMessages { get; set; } = null!;

	public virtual DbSet<User> Users { get; set; } = null!;

	public virtual DbSet<VirtualClinic> VirtualClinics { get; set; } = null!;

	public virtual DbSet<ClinicWorkTime> ClinicWorkTimes { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer(_config.GetConnectionString("Default"));

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");


		modelBuilder.Entity<Doctor>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Doctor__B5BD6B27701103BA");

			entity.HasOne(d => d.Speciality).WithMany(p => p.Doctors).HasForeignKey(x => x.SpecialityId);

			entity.HasOne(d => d.User).WithOne().HasConstraintName("Doctor_User_FK");
		});

		modelBuilder.Entity<Person>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Person__3214EC07A0A03B3C");
		});

		modelBuilder.Entity<Post>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Post__3214EC07FAD031E1");

			entity.HasOne(d => d.Doctor).WithMany(p => p.Posts)
				.HasForeignKey(x => x.DoctorId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Rating>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Rating__3214EC07B32B9B16");

			entity.HasOne(d => d.Doctor).WithMany(p => p.Ratings)
				.HasForeignKey(x => x.DoctorId)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasOne(d => d.User).WithMany(p => p.Ratings)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.NoAction);
		});

		modelBuilder.Entity<Reservation>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07765A5547");

			entity.HasOne(d => d.User).WithMany(p => p.Reservations).HasForeignKey(x => x.UserId);

			entity.HasOne(d => d.Virtual).WithMany(p => p.Reservations).HasForeignKey(x => x.VirtualId);

			entity.Property(p => p.Type)
				.HasConversion(
				v => v.ToString(),                                          // Convert enum to string
				v => (BookingTypes)Enum.Parse(typeof(BookingTypes), v));    // Convert string back to enum
		});

		modelBuilder.Entity<Speciality>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC074808F671");
		});

		modelBuilder.Entity<Ticket>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC076F6CA0F8");

			entity.HasOne(d => d.Doctor).WithMany(p => p.Tickets)
				.HasForeignKey(x => x.DoctorId)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasOne(d => d.User).WithMany(p => p.Tickets)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.NoAction);
		});

		modelBuilder.Entity<TicketMessage>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Ticket_M__3214EC079464AE2E");

			entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMessages).HasForeignKey(x => x.TicketId);
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__User__3214EC071AAE95BF");

			entity.Property(e => e.Id);

			entity.HasOne(d => d.Person).WithOne()
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("User_Person_FK");
		});

		modelBuilder.Entity<VirtualClinic>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Virtual___3214EC078277E8B9");

			entity.HasOne(d => d.Doctor).WithMany(p => p.VirtualClinics)
			.HasForeignKey(x => x.DoctorId)
			.OnDelete(DeleteBehavior.NoAction);
		});

		modelBuilder.Entity<ClinicWorkTime>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__WorkTime");

			entity.HasOne(d => d.Clinic).WithMany(p => p.WorkTimes)
				.HasForeignKey(x => x.ClinicId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
