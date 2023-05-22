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
            entity.HasKey(e => e.AbsenceId).HasName("PK__Absences__9BAC7E73EE52C46D");

            entity.Property(e => e.AbsenceId).HasColumnName("absence_id");
            entity.Property(e => e.AbsenceDate)
                .HasColumnType("date")
                .HasColumnName("absence_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Absences)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Absences__studen__15DA3E5D");

            entity.HasOne(d => d.Subject).WithMany(p => p.Absences)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Absences__subjec__16CE6296");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__FDF4798621B7E17C");

            entity.HasIndex(e => e.ProfId, "UQ__Classes__44D3854587360A82").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassYear).HasColumnName("class_year");
            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.SpecId).HasColumnName("spec_id");

            entity.HasOne(d => d.Prof).WithOne(p => p.Class)
                .HasForeignKey<Class>(d => d.ProfId)
                .HasConstraintName("FK__Classes__prof_id__01D345B0");

            entity.HasOne(d => d.Spec).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SpecId)
                .HasConstraintName("FK__Classes__spec_id__00DF2177");
        });

        modelBuilder.Entity<ClassSubject>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.SubjectId }).HasName("PK__ClassSub__E8F436E0BF3EB766");

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
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__3A8F732CC9382C1F");

            entity.Property(e => e.GradeId).HasColumnName("grade_id");
            entity.Property(e => e.GradeVal).HasColumnName("grade_val");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grades__student___0E391C95");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Grades__subject___0F2D40CE");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfId).HasName("PK__Professo__44D385441915D36E");

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
                .HasConstraintName("FK__Professor__email__7A3223E8");
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
            entity.HasKey(e => e.StudentId).HasName("PK__Students__2A33069A80B0C939");

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
                .HasConstraintName("FK__Students__class___09746778");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__Students__email__0880433F");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__5004F66064EA72FA");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.ProfId).HasColumnName("prof_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.Prof).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ProfId)
                .HasConstraintName("FK__Subjects__prof_i__7D0E9093");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Users__AB6E61655CC3CA92");

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
