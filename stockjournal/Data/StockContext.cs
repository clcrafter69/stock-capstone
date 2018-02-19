using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockJournal.Models;

namespace StockJournal.Data
{

    public class StockContext : DbContext
    {
        public DbSet<StockTrans> Stock { get; set; }
        public DbSet<CashTrans> Cash { get; set; }
        public DbSet<StockUser> User { get; set; }
   

        public StockContext(DbContextOptions<StockContext> options)
            : base(options)
        {

        }
    }
}
