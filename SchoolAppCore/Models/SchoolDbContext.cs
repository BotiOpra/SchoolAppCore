using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppCore.Models;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassSubject> ClassSubjects { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:192.168.0.103,49172;Initial Catalog=SchoolDB;User ID=Boti2;Password=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__FDF479867D7AB9C4");

            entity.HasIndex(e => e.ProfId, "UQ__Classes__44D38545DDBD6F54").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassYear).HasColumnName("class_year");
            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.SpecId).HasColumnName("spec_id");

            entity.HasOne(d => d.Prof).WithOne(p => p.Class)
                .HasForeignKey<Class>(d => d.ProfId)
                .HasConstraintName("FK__Classes__prof_id__37703C52");

            entity.HasOne(d => d.Spec).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SpecId)
                .HasConstraintName("FK__Classes__spec_id__367C1819");
        });

        modelBuilder.Entity<ClassSubject>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.SubjectId }).HasName("PK__ClassSub__E8F436E0ADEAD827");

            entity.ToTable("ClassSubject");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.HasSemesterPaper).HasColumnName("has_semester_paper");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_ClassSubject_Class");

            entity.HasOne(d => d.Subject).WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_ClassSubject_Subject");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfId).HasName("PK__Professo__44D38544E26EC8DD");

            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecId).HasName("PK__Speciali__F670C5676C0CEF2E");

            entity.ToTable("Specialization");

            entity.Property(e => e.SpecId).HasColumnName("spec_id");
            entity.Property(e => e.SpecName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("spec_name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__2A33069A9EA7A431");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__class___3A4CA8FD");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__5004F660FCFB211D");

            entity.ToTable("Subject");

            entity.HasIndex(e => e.ProfId, "UQ__Subject__44D385451EB87845").IsUnique();

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.Prof).WithOne(p => p.Subject)
                .HasForeignKey<Subject>(d => d.ProfId)
                .HasConstraintName("FK__Subject__prof_id__32AB8735");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
