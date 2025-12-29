// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;

// namespace DatabaseFirst.EFCore;

// public partial class Math28DBContext : DbContext
// {
//     public Math28DBContext()
//     {
//     }

//     public Math28DBContext(DbContextOptions<Math28DBContext> options)
//         : base(options)
//     {
//     }

//     public virtual DbSet<GeneratedPuzzel> GeneratedPuzzels { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Phetolo.Games.Maths28;Trusted_Connection=True;TrustServerCertificate=True;");

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<GeneratedPuzzel>(entity =>
//         {
//             entity.HasKey(e => e.RowId);

//             entity.ToTable("GeneratedPuzzel");

//             entity.Property(e => e.Created)
//                 .HasDefaultValueSql("(getdate())")
//                 .HasColumnType("datetime");
//             entity.Property(e => e.Puzzel)
//                 .HasMaxLength(50)
//                 .IsUnicode(false);
//             entity.Property(e => e.SuggestedAnswer)
//                 .HasMaxLength(50)
//                 .IsUnicode(false);
//         });

//         OnModelCreatingPartial(modelBuilder);
//     }

//     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
// }
