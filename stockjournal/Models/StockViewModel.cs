using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StockJournal.Models
{
    public class StockViewModel
    {
        public List<SelectListItem> transTypes { get; set; }
        public int userJournalId { get; set; }
        public StockTrans Stock { get; set; }

        public StockViewModel()
        {
            transTypes = new List<SelectListItem>
         {
            new SelectListItem
            {
                Value = "BUY",
                Text = "   BUY   "
            },
              new SelectListItem
              {
                  Value = "SELL",
                  Text = "  SELL  "
              }
          };

        }
    }
}
 