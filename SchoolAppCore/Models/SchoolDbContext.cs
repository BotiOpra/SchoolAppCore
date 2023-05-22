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

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassSubject> ClassSubjects { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<HeadTeacher> HeadTeachers { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:192.168.0.103,49172;Initial Catalog=SchoolDB;User ID=Boti2;Password=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(e => e.AbsenceId).HasName("PK__Absences__9BAC7E737EA6D142");

            entity.Property(e => e.AbsenceId).HasColumnName("absence_id");
            entity.Property(e => e.AbsenceDate)
                .HasColumnType("date")
                .HasColumnName("absence_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Absences)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Absences__studen__3B0BC30C");

            entity.HasOne(d => d.Subject).WithMany(p => p.Absences)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Absences__subjec__3BFFE745");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__FDF47986C86CD195");

            entity.HasIndex(e => e.ProfId, "UQ__Classes__44D38545A17050BD").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassYear).HasColumnName("class_year");
            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.SpecId).HasColumnName("spec_id");

            entity.HasOne(d => d.Prof).WithOne(p => p.Class)
                .HasForeignKey<Class>(d => d.ProfId)
                .HasConstraintName("FK__Classes__prof_id__27F8EE98");

            entity.HasOne(d => d.Spec).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SpecId)
                .HasConstraintName("FK__Classes__spec_id__2704CA5F");
        });

        modelBuilder.Entity<ClassSubject>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.SubjectId }).HasName("PK__ClassSub__E8F436E0CA63B352");

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

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__3A8F732CC3919305");

            entity.Property(e => e.GradeId).HasColumnName("grade_id");
            entity.Property(e => e.GradeVal).HasColumnName("grade_val");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grades__student___373B3228");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Grades__subject___382F5661");
        });

        modelBuilder.Entity<HeadTeacher>(entity =>
        {
            entity.HasKey(e => e.HeadTeacherId).HasName("PK__HeadTeac__A46CA1A3D7AEE6DA");

            entity.ToTable("HeadTeacher");

            entity.Property(e => e.HeadTeacherId).HasColumnName("headTeacher_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.HasOne(d => d.Class).WithMany(p => p.HeadTeachers)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__HeadTeach__class__336AA144");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.HeadTeachers)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__HeadTeach__email__32767D0B");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfId).HasName("PK__Professo__44D38544CCBE0D9E");

            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
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

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Professor__email__2057CCD0");
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
            entity.HasKey(e => e.StudentId).HasName("PK__Students__2A33069ACDF55556");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
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

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__class___2F9A1060");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__Students__email__2EA5EC27");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__5004F660F1B7960F");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.Prof).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ProfId)
                .HasConstraintName("FK__Subjects__prof_i__2334397B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Users__AB6E6165DB8427BD");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
