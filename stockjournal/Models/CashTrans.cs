using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Models
{
    public class CashTrans
    {
        public int Id { get; set; }

        public string TransType { get; set; }  //type--Deposit or Withdrawal
        public double Amount { get; set; }  //total amount of stock
        

        public DateTime TransDate { get; set; }  //date of transaction


        //navigation properties

        public int StockUserId { get; set; }
        public StockUser StockUser { get; set; }
    }
}
