using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuApp.Domain.Entities;

namespace TuApp.Infrastructure.Context
{
    public partial class TicketeraDbContext : DbContext
    {
        public TicketeraDbContext()
        {
        }

        public TicketeraDbContext(DbContextOptions<TicketeraDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Response> Responses { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=.\\SQLExpress2017;Database=TicketeraBD;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(e => e.ResponseId).HasName("PK__response__EBECD8965AFD845B");

                entity.ToTable("responses");

                entity.Property(e => e.ResponseId)
                    .ValueGeneratedNever()
                    .HasColumnName("response_id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .HasColumnName("message");
                entity.Property(e => e.ResponderId).HasColumnName("responder_id");
                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.HasOne(d => d.Responder).WithMany(p => p.Responses)
                    .HasForeignKey(d => d.ResponderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__responses__respo__4AB81AF0");

                entity.HasOne(d => d.Ticket).WithMany(p => p.Responses)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__responses__ticke__49C3F6B7");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__roles__760965CCF48C18E5");

                entity.ToTable("roles");

                entity.HasIndex(e => e.RoleName, "UQ__roles__783254B179E39ED5").IsUnique();

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");
                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketId).HasName("PK__tickets__D596F96B46EDE3AC");

                entity.ToTable("tickets");

                entity.Property(e => e.TicketId)
                    .ValueGeneratedNever()
                    .HasColumnName("ticket_id");
                entity.Property(e => e.ClosedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("closed_at");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tickets__user_id__45F365D3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F4D627E05");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "UQ__users__AB6E616416E91523").IsUnique();

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC572B8D9839F").IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password_hash");
                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK__user_rol__6EDEA153000CCBB0");

                entity.ToTable("user_roles");

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.AssignedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("assigned_at");

                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__user_role__role___412EB0B6");

                entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__user_role__user___403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
