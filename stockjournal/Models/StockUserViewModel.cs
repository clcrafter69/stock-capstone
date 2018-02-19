using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Models
{
    public class StockUserViewModel
    {
        public string loginUserId { get; set; } // user email 
        public int userTransId { get; set; }
        public StockUser user { get; set; }

        public StockUserViewModel()
        {
             user = new StockUser();
        }
    }
}
