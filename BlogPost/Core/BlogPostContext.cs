using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BlogPost.Core
{
    public class BlogPostContext : DbContext
    {

        public BlogPostContext(): base("name=BlogPostConnectionstring")
        {     
        }

        public DbSet<Register> User { get; set; }
        public DbSet<BlogPost> Blog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Register>().HasKey(a => a.Id).ToTable("User");
            modelBuilder.Entity<Register>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Register>().Property(a => a.Name);
            modelBuilder.Entity<Register>().Property(a => a.Password);
            modelBuilder.Entity<Register>().Property(a => a.Email);

            modelBuilder.Entity<BlogPost>().HasKey(a => a.Id).ToTable("BlogPost");
            modelBuilder.Entity<BlogPost>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<BlogPost>().Property(a => a.Name);
            modelBuilder.Entity<BlogPost>().Property(a => a.Description);
            modelBuilder.Entity<BlogPost>().Property(a => a.IsPrivate);
            modelBuilder.Entity<BlogPost>().Property(a => a.UserId);

            base.OnModelCreating(modelBuilder);
        }




    }
}