namespace Luxus.Migrations
{
    using Luxus.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Luxus.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Luxus.Models.ApplicationDbContext context)
        {
            //var dbInit = new DbInitializer();
            //dbInit.SeedBook(context);
            
            
        }
    }
}
