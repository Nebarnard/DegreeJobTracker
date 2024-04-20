using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DegreeJobTracker.Models
{
    public partial class DegreeJobTrackerContext : DbContext
    {
        public DegreeJobTrackerContext()
        {
        }

        public DegreeJobTrackerContext(DbContextOptions<DegreeJobTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Degree> Degrees { get; set; } = null!;
        public virtual DbSet<DegreeJobPerson> DegreeJobPeople { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<UserCredential> UserCredentials { get; set; } = null!;


        // Determine if a primary key value exists
        public bool DoesPrimaryKeyExist<TEntity>(object primaryKeyValue) where TEntity : class {
            var entityType = typeof(TEntity);
            var entity = this.Model.FindEntityType(entityType);

            if (entity == null) {
                throw new InvalidOperationException($"Entity type '{entityType.Name}' not found in the model.");
            }

            var primaryKey = entity.FindPrimaryKey();

            if (primaryKey == null) {
                throw new InvalidOperationException($"Entity type '{entityType.Name}' does not have a primary key defined.");
            }

            var primaryKeyPropertyName = primaryKey.Properties.First().Name;

            // Construct query to check if primary key value exists
            var query = this.Set<TEntity>().Where(entity => EF.Property<object>(entity, primaryKeyPropertyName).Equals(primaryKeyValue));

            // Execute the query and check if any entity matches the primary key value
            return query.Any();
        } // end method


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Degree>(entity =>
            {
                entity.ToTable("degree");

                entity.HasIndex(e => e.DegreeId, "UQ__degree__A1AFAEBA9ED2AE07")
                    .IsUnique();

                entity.Property(e => e.DegreeId).HasColumnName("degree_id");

                entity.Property(e => e.Concentration)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("concentration");

                entity.Property(e => e.Major)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("major");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Program)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("program");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.YearAwarded).HasColumnName("year_awarded");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Degrees)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_degree_person");
            });

            modelBuilder.Entity<DegreeJobPerson>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.JobId, e.DegreeId })
                    .HasName("pk_degree_job");

                entity.ToTable("degree_job_person");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.DegreeId).HasColumnName("degree_id");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.DegreeJobPeople)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_degree_job_person_degree");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.DegreeJobPeople)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_degree_job_person_job");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DegreeJobPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_degree_job_person_person");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job");

                entity.HasIndex(e => e.JobId, "UQ__job__6E32B6A4378CCD66")
                    .IsUnique();

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("business_name");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("job_title");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_job_person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.HasIndex(e => e.PersonId, "UQ__person__543848DEE076311E")
                    .IsUnique();

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("pk_user_credential");

                entity.ToTable("user_credential");

                entity.HasIndex(e => e.Username, "UQ__user_cre__F3DBC5721D8F5B7B")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
