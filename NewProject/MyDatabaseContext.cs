using System;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;

#nullable disable

namespace NewProject
{
    public partial class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext()
        {

        }
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
            //при изменении бд 
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Listener> Listeners { get; set; }
        

        internal object GetCollection<T>()
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-C8HRCMG8G6A\\SQLEXPRESS;Database=MyDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Performer>(entity =>
            {
                entity.ToTable("Performer");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                

                entity.HasIndex(p => p.NickName).IsUnique(true);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.PerformerId)
                    .HasConstraintName("SongsPerformer");

                entity.HasOne(d => d.Listener)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.ListenerId);
            });

            modelBuilder.Entity<Listener>(entity =>
            {
                entity.ToTable("Listener");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
