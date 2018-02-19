using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Models
{
    public class StockViewModel
    {
        public int userJournalId { get; set; }
        public StockTrans Stock { get; set; }
    }
}
 