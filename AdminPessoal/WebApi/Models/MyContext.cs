using System.Data.Entity;

namespace WebApi.Models
{
    public class MyContext : DbContext
    {
        public MyContext() : base ("name=TIExpress")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Aplicacao> Aplicacoes { get; set; }
    }
}