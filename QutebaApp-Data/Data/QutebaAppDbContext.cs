using Microsoft.EntityFrameworkCore;
using QutebaApp_Data.Models;

namespace QutebaApp_Data.Data
{
    public partial class QutebaAppDbContext : DbContext
    {
        public QutebaAppDbContext()
        {
        }

        public QutebaAppDbContext(DbContextOptions<QutebaAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Spending> Spendings { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryName)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(45);

                entity.Property(e => e.CategoryCreationTime)
                    .IsRequired()
                    .HasColumnName("category_creation_time");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("profiles");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.Property(e => e.Income).HasColumnName("income");

                entity.Property(e => e.PhotoUrl)
                    .HasColumnName("photo_url")
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(45);

                entity.Property(e => e.IncomeCreationTime)
                    .IsRequired()
                    .HasColumnName("income_creation_time");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Profiles)
                    .HasForeignKey<Profile>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.RoleName)
                    .HasName("role_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(45);

                entity.Property(e => e.RoleCreationTime)
                    .IsRequired()
                    .HasColumnName("role_creation_time");
            });

            modelBuilder.Entity<Spending>(entity =>
            {
                entity.ToTable("spendings");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_ID_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CategoryId).HasColumnName("category_ID");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.Property(e => e.SpendingCreationTime)
                    .IsRequired()
                    .HasColumnName("spending_creation_time");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Spendings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Spendings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_s_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_ID_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAccountWith)
                    .IsRequired()
                    .HasColumnName("created_account_with")
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("fullname")
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(45);

                entity.Property(e => e.RoleId).HasColumnName("role_ID");

                entity.Property(e => e.UserCreationTime)
                    .IsRequired()
                    .HasColumnName("user_creation_time");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
