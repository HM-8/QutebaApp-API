using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QutebaApp_Data.Models;

namespace QutebaApp_Data.Data
{
    public partial class quteba_appContext : DbContext
    {
        public IConfiguration _configuration { get; }

        public quteba_appContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public quteba_appContext(DbContextOptions<quteba_appContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Spending> Spendings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PRIMARY");

                entity.ToTable("accounts");

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_ID_idx");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAccountWith)
                    .IsRequired()
                    .HasColumnName("created_account_with")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime).HasColumnName("creation_time");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnName("display_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmailVerified)
                    .HasColumnName("email_verified")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LastRefreshTime).HasColumnName("last_refresh_time");

                entity.Property(e => e.LastSigninTime).HasColumnName("last_signin_time");

                entity.Property(e => e.RoleId).HasColumnName("role_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_ID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationTime).HasColumnName("creation_time");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.UserUid)
                    .HasName("PRIMARY");

                entity.ToTable("profiles");

                entity.Property(e => e.UserUid)
                    .HasColumnName("user_UID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl)
                    .HasColumnName("photo_url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.SalaryCreationTime).HasColumnName("salary_creation_time");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Profile)
                    .HasForeignKey<Profile>(d => d.UserUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_UID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Spending>(entity =>
            {
                entity.ToTable("spendings");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_ID_idx");

                entity.HasIndex(e => e.UserUid)
                    .HasName("user_ID_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CategoryId).HasColumnName("category_ID");

                entity.Property(e => e.CreationTime).HasColumnName("creation_time");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.UserUid)
                    .IsRequired()
                    .HasColumnName("user_UID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Spendings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Spendings)
                    .HasForeignKey(d => d.UserUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
