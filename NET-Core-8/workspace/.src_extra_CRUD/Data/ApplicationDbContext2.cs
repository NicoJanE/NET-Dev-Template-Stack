using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app_mvc.Models;

namespace app_mvc.Data
{
    public class ApplicationDbContext2 : DbContext
    {
        public ApplicationDbContext2 (DbContextOptions<ApplicationDbContext2> options)
            : base(options)
        {
        }

        public DbSet<app_mvc.Models.MyModel> MyModel { get; set; } = default!;
    }
}
