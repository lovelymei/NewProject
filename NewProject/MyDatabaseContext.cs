using System;
using APIServer.Models;
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
        public virtual DbSet<Album> Albums { get; set; }


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

            });

            modelBuilder.Entity<Listener>(entity =>
            {
                entity.ToTable("Listener");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.HasOne(e => e.Album)
                   .WithMany(e => e.Songs)
                   .HasForeignKey(e => e.AlbumId)
                   .HasConstraintName("Album/Songs");

                entity.HasOne(e => e.Performer)
                   .WithMany(e => e.Songs)
                   .HasForeignKey(e => e.AccountId)
                   .HasConstraintName("Performer/Songs");

                entity.HasMany(d => d.Listeners)
                    .WithMany(d => d.Songs)
                    .UsingEntity(j => j.ToTable("ListenerSongLibrary"));

            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.HasOne(e => e.Performer)
                    .WithMany(e => e.Albums)
                    .HasForeignKey(e => e.AccountId)
                    .HasConstraintName("Performer/Albums");

                entity.HasMany(d => d.Listeners)
                   .WithMany(d => d.Albums)
                   .UsingEntity(j => j.ToTable("ListenerAlbumLibrary"));
            });


            //modelBuilder.Entity<Performer>(entity =>
            //{
            //    entity.ToTable("Performer");

            //    entity.Property(e => e.NickName)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);



            //    entity.HasIndex(p => p.NickName).IsUnique(true);
            //});

            //modelBuilder.Entity<Song>(entity =>
            //{
            //    entity.ToTable("Song");

            //    entity.Property(e => e.Title)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.DurationMs)
            //        .IsRequired();

            //    entity.HasOne(d => d.Performer)
            //        .WithMany(p => p.Songs)
            //        .HasForeignKey(d => d.PerformerId)
            //        .HasConstraintName("SongsPerformer");

            //    entity.HasOne(d => d.Listener)
            //        .WithMany(p => p.Songs)
            //        .HasForeignKey(d => d.ListenerId);
            //});

            //modelBuilder.Entity<Listener>(entity =>
            //{
            //    entity.ToTable("Listener");

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Surname)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //});
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
