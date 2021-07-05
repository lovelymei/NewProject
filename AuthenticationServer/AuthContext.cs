using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer
{
    public partial class AuthContext : DbContext
    {
        public AuthContext()
        {
            
        }
        public AuthContext(DbContextOptions<AuthContext> options)
            :base(options)
        {
            //при изменении бд 
            //    Database.EnsureDeleted();
            //    Database.EnsureCreated();
        }

        public virtual DbSet<Account> Accounts { get; set; }
        internal object GetCollection<T>()
        {
            throw new NotImplementedException();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-C8HRCMG8G6A\\SQLEXPRESS;Database=Auth;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");
            modelBuilder.Entity<Account>(entity =>
            {
                modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

                entity.ToTable("Account");
                entity.Property(e => e.Login)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();

            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
