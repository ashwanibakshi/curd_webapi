namespace Employees
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookContext : DbContext
    {
        public BookContext()
            : base("name=BookModel")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; } 
        public virtual DbSet<Gener> Geners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Author)
                .HasForeignKey(e => e.authors_Id);

            modelBuilder.Entity<Gener>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Gener)
                .HasForeignKey(e => e.geners_Id);
        }
    }
}
