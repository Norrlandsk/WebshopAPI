using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopAPI.Database
{
    public static class DatabaseCreator
    {
        public static void Create()
        {
            var contextFactory = new ContextFactory();
            using (var dbContext = contextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
